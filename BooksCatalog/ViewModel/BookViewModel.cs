﻿using System.Windows;
using AutoMapper;
using BooksCatalog.Model;
using BooksCatalog.Model.Entities;
using BooksCatalog.Model.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace BooksCatalog.ViewModel
{
    public class BookViewModel : ViewModelBase
    {
        private RelayCommand<object> _closeCommand;
        private RelayCommand _save;

        public BookViewModel(Book book)
        {
            Mapper.Map(book, this);
        }

        public RelayCommand Save => _save ?? (_save = new RelayCommand(SaveCommand));

        public RelayCommand<object> CloseWindow
            => _closeCommand ?? (_closeCommand = new RelayCommand<object>(CloseWindowCommand));

        private void SaveCommand()
        {
            Book book = Mapper.Map<Book>(this);
            ServiceLocator.Current.GetInstance<IRepository<Book>>().Update(book);
            Messenger.Default.Send(book, BooksMessageType.Saved);
        }

        //TODO добавить обработку закрытия по крестику
        private void CloseWindowCommand(object obj)
        {
            Window win = obj as Window;
            Messenger.Default.Send(Mapper.Map<Book>(this), BooksMessageType.Closed);
            win.Close();
        }

        #region Id

        private long _id;

        public long Id
        {
            get { return _id; }
            set { Set(() => Id, ref _id, value); }
        }

        #endregion

        #region Title

        private string _title;

        public string Title
        {
            get { return _title; }
            set { Set(() => Title, ref _title, value); }
        }

        #endregion

        #region Annotation

        private string _annotation;

        public string Annotation
        {
            get { return _annotation; }
            set { Set(() => Annotation, ref _annotation, value); }
        }

        #endregion

        #region CatalogId

        private long _catalogId;

        public long CatalogId
        {
            get { return _catalogId; }
            set { Set(() => CatalogId, ref _catalogId, value); }
        }

        #endregion
    }
}