using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksCatalog.Model.Entities;
using BooksCatalog.Model.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace BooksCatalog.ViewModel
{
    public class TreeViewModel:ViewModelBase
    {
        public TreeViewModel()
        {
            Children = new ObservableCollection<TreeViewModel>();
            Id = null;
            LoadChildren();
        }

        private TreeViewModel(Catalog catalog)
        {
            Id = catalog.Id;
            Title = catalog.Title;
            ParentId = catalog.ParentId;
            Children = new ObservableCollection<TreeViewModel>();
        }

        #region DummyChildren

        public static readonly TreeViewModel DummyChild = new TreeViewModel();

        #endregion


        #region IsExpanded

        private bool _isExpanded;

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                Set(() => IsExpanded, ref _isExpanded, value);
                if (HasDummyChild)
                {
                    Children.Remove(DummyChild);
                    LoadChildren();
                }
            }
        }

        #endregion

        #region IsSelected

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(() => IsSelected, ref _isSelected, value); }
        }

        #endregion

        #region Id

        private long? _id;

        public long? Id
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

        #region ParentId

        private long? _parentId;

        public long? ParentId
        {
            get { return _parentId; }
            set { Set(() => ParentId, ref _parentId, value); }
        }

        #endregion

        private bool HasDummyChild => Children.Count == 1 && Children[0] == DummyChild;

        private void LoadChildren()
        {
            var repository = ServiceLocator.Current.GetInstance<IRepository<Catalog>>();
            foreach (var catalogVm in repository.GetAll().Where(x => x.ParentId == Id))
            {
                TreeViewModel catalogViewModel = new TreeViewModel(catalogVm);
                if (repository.GetAll().Any(y => y.ParentId == catalogVm.Id))
                    catalogViewModel.Children.Add(DummyChild);
                Children.Add(catalogViewModel);
            }
        }

        #region Children

        private ObservableCollection<TreeViewModel> _children;

        public ObservableCollection<TreeViewModel> Children
        {
            get { return _children; }
            set { Set(() => Children, ref _children, value); }
        }

        #endregion

    }
}
