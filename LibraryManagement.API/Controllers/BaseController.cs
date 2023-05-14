using System.Net;
using LibraryManagement.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

public class BaseController : Controller
{
    protected ObjectResult StatusResult<T>(ResponseBody<T> body, HttpStatusCode httpStatus) where T : class
    {
        return StatusCode((int)httpStatus, body);
    }
}