using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class CartViewModel :  BaseViewModel
    {
        public List<ProductInCart> Products { get; set; }
        public string Total { get; set; }


        public string ResNum { get; set; }
        public string Amount { get; set; }
        public string MID { get; set; }
        public string RedirectURL { get; set; }
    }

    public class ProductInCart
    {
        public Guid OrderDetailId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string FullName { get; set; }
        public string Amount
        {

            get
            {
                return Product.Amount.ToString("n0") + " تومان";

            }
        }
        public string RowAmount
        {
            get
            {
                return (Product.Amount * Quantity).ToString("n0") + " تومان";

            }
        }

    
    }
}