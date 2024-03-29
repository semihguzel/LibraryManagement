﻿using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Interfaces.Services;

public interface ILoanService
{
    Task<Loan?> GetById(Guid id);
    Task Add(Loan loan);
    Task Update(Loan loan);
    Task Delete(Guid id);
    Task<List<Loan>?> GetAllByBookId(Guid bookId);
    Task<List<Loan>> GetAllByUserId(Guid userId);
}