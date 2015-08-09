﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using BooksCatalog.Model;
using BooksCatalog.Model.Entities;
using BooksCatalog.Model.Interface;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using MvvmDialogs;

namespace BooksCatalog.ViewModel
{
    public class SearchViewModel : TableViewModel, IModalDialogViewModel
    {
        public SearchViewModel(IDialogService dialogService)
            : base(dialogService)
        {
            Messenger.Default.Register<Book>(this, BooksMessageType.FromSearch,
                book =>
                {
                    var originalBook = Books.FirstOrDefault(x => x.Id == book.Id);
                    if (originalBook != null)
                    {
                        Books[Books.IndexOf(originalBook)] = book;
                    }
                });
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

        #endregion

        #region Commands

        #region Search

        private RelayCommand _search;

        public RelayCommand Search => _search ?? (_search = new RelayCommand(SearchCommand));

        private void SearchCommand()
        {
            Books =
                new ObservableCollection<Book>(
                    ServiceLocator.Current.GetInstance<IRepository<Book>>().Search(SearchString));
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