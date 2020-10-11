using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        public Product Product { get; set; }
        public List<ProductGroup> SidebarProductGroups { get; set; }
        public ProductDetailPostViewModel Form { get; set; }
        public List<Product> RelatedProducts { get; set; }
    

    }

    public class ProductDetailPostViewModel
    {
        public string Code { get; set; }

        [Display(Name = "نام لاتین")] 
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی لاتین")]
        public string LastName { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "شماره موبایل")]
        public string CellNumber { get; set; }


        [Display(Name = "نوع ویزا")]
        public Guid? VisaTypeId { get; set; }

        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "شماره پاسپورت")]
        public string PasportNumber { get; set; }

        [Display(Name = "تاریخ اعتبار پاسپورت")]
        public DateTime? PasportExpireDate { get; set; }

        [Display(Name = "ملیت")]
        public string Nationality { get; set; }

        [Display(Name = " تاریخ درخواست وقت سفارت 1 ")]
        public DateTime? VisitDate1 { get; set; }

        [Display(Name = " تاریخ درخواست وقت سفارت 2")]
        public DateTime? VisitDate2 { get; set; }

        [Display(Name = "تاریخ ")]
        public DateTime? TravelDate { get; set; }

        [Display(Name = "آپلود پاسپورت")]
        public string PasportFileUrl { get; set; }
    }
}