using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class ContactViewModel:BaseViewModel
    {
        public TextTypeItem Description { get; set; }
    }
}