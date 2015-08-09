using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using BooksCatalog.Model.Entities;
using BooksCatalog.Model.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace BooksCatalog.ViewModel
{
    public class TreeViewModel : ViewModelBase
    {
        public TreeViewModel()
        {
            Children = new ObservableCollection<TreeViewModel>();
            Id = null;
            LoadChildren();
        }

        private TreeViewModel(Catalog catalog)
        {
            Mapper.Map(catalog, this);
            Children = new ObservableCollection<TreeViewModel>();
        }

        private bool HasDummyChild => Children.Count == 1 && Children[0] == DummyChild;

        private void LoadChildren()
        {
            var repository = ServiceLocator.Current.GetInstance<IRepository<Catalog>>();
            foreach (var catalogVm in repository.Where(x => x.ParentId == Id))
            {
                TreeViewModel catalogViewModel = new TreeViewModel(catalogVm);
                if (repository.GetAll().Any(y => y.ParentId == catalogVm.Id))
                    catalogViewModel.Children.Add(DummyChild);
                Children.Add(catalogViewModel);
            }
        }

        #region Properties

        #region DummyChildren

        private static readonly TreeViewModel DummyChild = new TreeViewModel();

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
            set
            {
                Set(() => IsSelected, ref _isSelected, value);
                var id = Id;
                if (id != null) Messenger.Default.Send(new Catalog {Id = id.Value});
            }
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

        #region Children

        private ObservableCollection<TreeViewModel> _children;

        public ObservableCollection<TreeViewModel> Children
        {
            get { return _children; }
            set { Set(() => Children, ref _children, value); }
        }

        #endregion

        #endregion
    }
}