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
    public class TableViewModel : ViewModelBase
    {
        private List<Book> _openedBooks = new List<Book>();
        private readonly IDialogService _dialogService;
        private ObservableCollection<Book> _books;

        public TableViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Messenger.Default.Register<Catalog>(this, SetBooksByCatalog);
            Messenger.Default.Register<Book>(this, BooksMessageType.Saved, book => { SetBooksByCatalogId(book.CatalogId); });
            Messenger.Default.Register<Book>(this, BooksMessageType.Closed, book => { _openedBooks.RemoveAll(x=>x.Id==book.Id); });
        }

        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { Set(() => Books, ref _books, value); }
        }

        private Book _selectedBook;

        public Book SelectedBook
        {
            get { return _selectedBook; }
            set { Set(() => SelectedBook, ref _selectedBook, value); }
        }

        private RelayCommand _showDetails;

        public RelayCommand ShowDetails => _showDetails ?? (_showDetails = new RelayCommand(ShowDetailsCommand));

        private void ShowDetailsCommand()
        {
            if (_openedBooks.All(x => x.Id != SelectedBook.Id))
            {
                _openedBooks.Add(SelectedBook);
                var bookViewModel = new BookViewModel(SelectedBook);
                _dialogService.Show<BookView>(this, bookViewModel);
            }
        }

        private void SetBooksByCatalog(Catalog catalog)
        {
            SetBooksByCatalogId(catalog.Id);
        }

        private void SetBooksByCatalogId(long id)
        {
            Books =
                new ObservableCollection<Book>(
                    ServiceLocator.Current.GetInstance<IRepository<Book>>()
                        .GetAll()
                        .Where(x => x.CatalogId == id));
        }
    }
}