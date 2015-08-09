using AutoMapper;
using BooksCatalog.Model.Entities;
using BooksCatalog.ViewModel;

namespace BooksCatalog.Model
{
    internal static class AutoMapperConfiguration
    {
        public static void Install()
        {
            Mapper.CreateMap<Book, BookViewModel>().ReverseMap();
            Mapper.CreateMap<Catalog, TreeViewModel>().ReverseMap();
        }
    }
}