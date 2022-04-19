using AutoMapper;
using WebApi.Application.AuthorOperations.Commands;
using WebApi.Application.AuthorOperations.Queries;
using WebApi.Application.BookOperations.Commands;
using WebApi.Application.BookOperations.Queries;
using WebApi.Application.GenreOperations.Queries;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();
        }

    }
}