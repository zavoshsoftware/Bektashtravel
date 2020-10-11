using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class CallBackViewMode:BaseViewModel
    {
        public double Result { get; set; }
        public string Message { get; set; }
        public string RefId { get; set; }
        public bool IsSuccess { get; set; }
    }
}