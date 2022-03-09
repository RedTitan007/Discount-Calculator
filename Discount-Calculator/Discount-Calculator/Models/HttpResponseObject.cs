using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IMDB.Models
{
    public class HttpResponseObject {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    
    }
}
