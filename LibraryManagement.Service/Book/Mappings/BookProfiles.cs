using AutoMapper;
using LibraryManagement.Service.Book.Dto;

namespace LibraryManagement.Service.Book.Mappings;

public class BookProfiles : Profile
{
    public BookProfiles()
    {
        CreateMap<Core.Entities.Book, BookCreateUpdateDto>();
        CreateMap<BookCreateUpdateDto, Core.Entities.Book>();
    }
}