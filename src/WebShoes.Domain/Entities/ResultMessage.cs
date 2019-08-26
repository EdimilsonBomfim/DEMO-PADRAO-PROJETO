﻿namespace WebShoes.Domain.Entities
{
    public class ResultMessage
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Obj { get; set; }
    }
}
