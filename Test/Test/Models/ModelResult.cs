using System;
using System.Collections.Generic;

namespace Test.Models
{
    public class ResultBase
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }
        public Exception Error { get; set; }
        public int TotalCount { get; set; }
    }
    public class ResultObject<T> : ResultBase
    {
        public T Data { get; set; }
    }
    public class ResultList<T> : ResultBase
    {
        public List<T> Data { get; set; }
    }
    public class ResultBaseToken : ResultBase
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
    }
}
