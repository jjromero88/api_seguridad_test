using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SEG.Transversal.Common.Generics
{
    public class SegResponse
    {
        public int code { get; set; }
        public string? message { get; set; }
        public string? errordetails { get; set; } = string.Empty;
        public bool? error { get; set; } = false;
    }

    public class SegResponse<T> : SegResponse
    {
        public T? payload { get; set; }
    }

    public class ErrorDetail
    {
        public string? field { get; set; }
        public string? message { get; set; }
        public string? code { get; set; }
    }
}
