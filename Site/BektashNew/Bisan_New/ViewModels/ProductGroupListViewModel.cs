using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class ProductGroupListViewModel : BaseViewModel
    {
        public ProductGroup ProductGroup { get; set; }
        public List<Product> Products { get; set; }
    }


    public class ProductGroupWithProduct
    {
        public ProductGroup ProductGroup { get; set; }
        public List<Product> Products { get; set; }
    }
}