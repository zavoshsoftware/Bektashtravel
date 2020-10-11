using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class AboutViewModel:BaseViewModel
    {
        public TextTypeItem About { get; set; }
        public List<TextTypeItem> DownPageTexts { get; set; }
    }
}