using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Eshop.Helpers
{
    public class CodeCreator
    {
        private DatabaseContext db = new DatabaseContext();
       
        public int ReturnOrderCode()
        {

            Order order = db.Orders.OrderByDescending(current => current.Code).FirstOrDefault();
            if (order != null)
            {
                return order.Code + 1;
            }
            else
            {
                return 30000;
            }
        }
  public int ReturnProductCode()
        {

            Product product = db.Products.OrderByDescending(current => current.Code).FirstOrDefault();
            if (product != null)
            {
                return product.Code + 1;
            }
            else
            {
                return 21;
            }
        }

    }
}