using System.Net;
using AutoMapper;
using LibraryManagement.API.Helpers;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Service.Book.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IBookService _bookService;

    public BookController(IMapper mapper, IBookService bookService)
    {
        _mapper = mapper;
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> Test()
    {
        return Ok("Success");
    }

    [HttpPost]
    public async Task<IActionResult> AddBooks(BookCreateUpdateDto dto)
    {
        try
        {
            var book = _mapper.Map<Book>(dto);

            await _bookService.Add(book);
            return StatusResult(new ResponseBody<Book>() { Item = book }, HttpStatusCode.OK);
        }
        catch (ArgumentNullException e)
        {
            return StatusResult(new ResponseBody<Book> { Message = e.Message }, HttpStatusCode.BadRequest);
        }
        catch (ArgumentException e)
        {
            return StatusResult(new ResponseBody<Book> { Message = e.Message }, HttpStatusCode.BadRequest);
        }
        catch (Exception e)
        {
            return StatusResult(new ResponseBody<Book> { Message = e.Message }, HttpStatusCode.InternalServerError);
        }
    }
}