﻿using System.Net;

namespace Puzzle_API.Model
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode;
        public bool IsSuccess { get; set; }

        public List<string>? ErrorMessages { get; set; }

        public object? Result { get; set; }
    }
}
