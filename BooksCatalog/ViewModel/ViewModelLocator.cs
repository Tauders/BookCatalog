/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:BooksCatalog"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using BooksCatalog.Model;
using BooksCatalog.Model.Entities;
using BooksCatalog.Model.Implementation;
using BooksCatalog.Model.Interface;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MvvmDialogs;

namespace BooksCatalog.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        ///     Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            AutoMapperConfiguration.Install();
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IRepository<Book>>(
                () => new BooksRepository(new FileRepository<Book>().GetAll()));
            SimpleIoc.Default.Register<IRepository<Catalog>>(
                () => new CacheRepository<Catalog>(new FileRepository<Catalog>().GetAll()));
            SimpleIoc.Default.Register<IDialogService>(() => new DialogService());
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<BookViewModel>();
            SimpleIoc.Default.Register<TreeViewModel>();
            SimpleIoc.Default.Register<TableViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public TreeViewModel Tree => ServiceLocator.Current.GetInstance<TreeViewModel>();
        public TableViewModel Table => ServiceLocator.Current.GetInstance<TableViewModel>();
        public SearchViewModel Search => ServiceLocator.Current.GetInstance<SearchViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}