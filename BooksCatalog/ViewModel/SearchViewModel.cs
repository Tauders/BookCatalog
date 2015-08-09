using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BooksCatalog.Model;
using BooksCatalog.Model.Entities;
using BooksCatalog.Model.Interface;
using BooksCatalog.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using MvvmDialogs;

namespace BooksCatalog.ViewModel
{
    public class SearchViewModel : ViewModelBase, IModalDialogViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly List<Book> _openedBooks = new List<Book>();

        public SearchViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Messenger.Default.Register<Book>(this, BooksMessageType.FromSearch,
                book =>
                {
                    var originalBook = SearchResult.FirstOrDefault(x => x.Id == book.Id);
                    if (originalBook != null)
                    {
                        SearchResult[SearchResult.IndexOf(originalBook)] = book;
                    }
                });

            Messenger.Default.Register<Book>(this, BooksMessageType.Closed,
                book => { _openedBooks.RemoveAll(x => x.Id == book.Id); });
        }

        public bool? DialogResult { get; }

        #region Properties

        #region SearchString

        private string _searchString;

        public string SearchString
        {
            get { return _searchString; }
            set { Set(() => SearchString, ref _searchString, value); }
        }

        #endregion

        #region SearchResult

        private ObservableCollection<Book> _searchResult = new ObservableCollection<Book>();

        public ObservableCollection<Book> SearchResult
        {
            get { return _searchResult; }
            set { Set(() => SearchResult, ref _searchResult, value); }
        }

        #endregion

        #region SelectedResult

        private Book _selectedResult;

        public Book SelectedResult
        {
            get { return _selectedResult; }
            set { Set(() => SelectedResult, ref _selectedResult, value); }
        }

        #endregion

        #endregion

        #region Commands

        #region Search

        private RelayCommand _search;

        public RelayCommand Search => _search ?? (_search = new RelayCommand(SearchCommand));

        private void SearchCommand()
        {
            SearchResult =
                new ObservableCollection<Book>(
                    ServiceLocator.Current.GetInstance<IRepository<Book>>().Search(SearchString));
        }

        #endregion

        #region ShowDetails

        private RelayCommand _showDetails;

        public RelayCommand ShowDetails => _showDetails ?? (_showDetails = new RelayCommand(ShowDetailsCommand));

        private void ShowDetailsCommand()
        {
            if (_openedBooks.All(x => x.Id != SelectedResult.Id))
            {
                _openedBooks.Add(SelectedResult);
                _dialogService.Show<BookView>(this, new BookViewModel(SelectedResult, true));
            }
        }

        #endregion

        #region ShowInCatalog

        private RelayCommand _showInCatalogs;

        public RelayCommand ShowInCatalog
            => _showInCatalogs ?? (_showInCatalogs = new RelayCommand(ShowInCatalogCommand));

        private void ShowInCatalogCommand()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}