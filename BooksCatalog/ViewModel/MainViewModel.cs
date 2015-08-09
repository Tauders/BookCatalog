using BooksCatalog.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs;

namespace BooksCatalog.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         You can also use Blend to data bind with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;

        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        #region Search

        private RelayCommand _search;

        public RelayCommand Search => _search ?? (_search = new RelayCommand(SearchCommand));

        private void SearchCommand()
        {
            _dialogService.ShowDialog<SearchWindow>(this, new SearchViewModel(_dialogService));
        }

        #endregion
    }
}