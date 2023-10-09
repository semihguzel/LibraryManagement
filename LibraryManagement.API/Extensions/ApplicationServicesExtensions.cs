using LibraryManagement.Core.Interfaces.Repositories;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Service.Book;
using LibraryManagement.Service.Loan;

namespace LibraryManagement.API.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //TODO:Repositories will be added here
        services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
        services.AddScoped<IBookCategoryService, BookCategoryService>();

        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<ILoanService, LoanService>();

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookService, BookService>();
        return services;
    }
}