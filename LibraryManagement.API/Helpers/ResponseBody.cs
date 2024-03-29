﻿using System.ComponentModel;

namespace LibraryManagement.API.Helpers;

public class ResponseBody<T> where T : class
{
    [DisplayName("message")] public string Message { get; set; }

    [DisplayName("count")] public int Count { get; set; }

    [DisplayName("items")] public List<object> Items { get; set; }
    [DisplayName("token")] public string Token { get; set; }

    [DisplayName("item")] public T Item { get; set; }
} 