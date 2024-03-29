﻿using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services;

public interface IBookCategoryService
{
    Task Add(BookCategory bookCategory);
    Task Update(BookCategory book);
    Task Delete(Guid id);
    Task<BookCategory?> GetByCode(string code);
}