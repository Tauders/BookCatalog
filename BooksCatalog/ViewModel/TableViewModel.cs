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
    public class TableViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly List<Book> _openedBooks = new List<Book>();

        public TableViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Messenger.Default.Register<Catalog>(this, SetBooksByCatalog);
            Messenger.Default.Register<Book>(this, BooksMessageType.Saved,
                book => { SetBooksByCatalogId(book.CatalogId); });
            Messenger.Default.Register<Book>(this, BooksMessageType.Closed,
                book => { _openedBooks.RemoveAll(x => x.Id == book.Id); });
            Messenger.Default.Register<List<Book>>(this, books => { Books = new ObservableCollection<Book>(books); });
        }

        #region Properties

        #region Books

        private ObservableCollection<Book> _books;

        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { Set(() => Books, ref _books, value); }
        }

        #endregion

        #region SelectedBook

        private Book _selectedBook;

        public Book SelectedBook
        {
            get { return _selectedBook; }
            set { Set(() => SelectedBook, ref _selectedBook, value); }
        }

        #endregion

        #endregion

        #region Commands

        #region ShowDetails

        private RelayCommand _showDetails;

        public RelayCommand ShowDetails => _showDetails ?? (_showDetails = new RelayCommand(ShowDetailsCommand));

        private void ShowDetailsCommand()
        {
            if (_openedBooks.All(x => x.Id != SelectedBook.Id))
            {
                _openedBooks.Add(SelectedBook);
                _dialogService.Show<BookView>(this, new BookViewModel(SelectedBook));
            }
        }

        #endregion

        #endregion

        #region Message Handler

        private void SetBooksByCatalog(Catalog catalog)
        {
            SetBooksByCatalogId(catalog.Id);
        }

        private void SetBooksByCatalogId(long id)
        {
            Books =
                new ObservableCollection<Book>(
                    ServiceLocator.Current.GetInstance<IRepository<Book>>()
                        .Where(x => x.CatalogId == id));
        }

        #endregion
    }
}