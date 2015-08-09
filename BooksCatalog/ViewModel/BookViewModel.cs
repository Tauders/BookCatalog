using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BooksCatalog.Model;
using BooksCatalog.Model.Entities;
using BooksCatalog.Model.Interface;
using BooksCatalog.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace BooksCatalog.ViewModel
{
    public class BookViewModel:ViewModelBase
    {
        public BookViewModel(Book book)
        {
            Title = book.Title;
            Id = book.Id;
            Annotation = book.Annotation;
            CatalogId = book.CatalogId;
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

        private RelayCommand _save;

        public RelayCommand Save => _save ?? (_save = new RelayCommand(SaveCommand));

        private void SaveCommand()
        {
            Book book = new Book() {Id = Id, Annotation = Annotation, CatalogId = CatalogId, Title = Title};
            ServiceLocator.Current.GetInstance<IRepository<Book>>().Update(new Book() { Id=Id,Annotation = Annotation,CatalogId = CatalogId,Title = Title});
            Messenger.Default.Send(book, BooksMessageType.Saved);
        }

        private RelayCommand<object> _closeCommand;

        public RelayCommand<object> CloseWindow => _closeCommand ?? (_closeCommand = new RelayCommand<object>(CloseWindowCommand));

        //TODO добавить обработку закрытия по крестику
        private void CloseWindowCommand(object obj)
        {
            Book book = new Book() { Id = Id, Annotation = Annotation, CatalogId = CatalogId, Title = Title };
            Window win = obj as Window;
            Messenger.Default.Send(book, BooksMessageType.Closed);
            win.Close();
        }

    }
}
