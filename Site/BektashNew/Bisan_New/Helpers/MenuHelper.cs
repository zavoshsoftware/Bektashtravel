using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModels;

namespace Helpers
{

    public class MenuHelper
    {
        DatabaseContext db = new DatabaseContext();
        public BaseViewModel baseViewModel()
        {
            BaseViewModel baseView = new BaseViewModel();
            baseView.Menu = ReturnMenuTours();
            baseView.Footer = ReturnFooter();
            baseView.MenuBlogGroups = ReturnBlogGroups();

            return baseView;
        }
        public List<MenuTour> ReturnMenuTourCategory(Guid typeId)
        {
            List<MenuTour> menuTour = new List<MenuTour>();

            List<TourCategory> tourCategories = db.TourCategories.Where(current =>current.TypeId==typeId&& current.IsDelete == false && current.ParentId == null).OrderByDescending(c=>c.Priority).ToList();

            foreach (TourCategory tourCategory in tourCategories)
            {
                menuTour.Add(new MenuTour
                {
                    TourCategoryParent = tourCategory,
                    TourCategory = db.TourCategories.Where(current => current.IsDelete == false && current.ParentId == tourCategory.Id).OrderByDescending(c => c.Priority).ToList()
                });
            }
            return menuTour;
        }

        public List<TourTypeViewModel> ReturnMenuTours()
        {
            List<TourTypeViewModel> tourTypes=new List<TourTypeViewModel>();

            List<Models.Type> types = db.Types.Where(current => current.IsDelete == false)
                .OrderBy(current => current.Order).ToList();

            foreach (Models.Type type in types)
            {
                tourTypes.Add(new TourTypeViewModel()
                {
                    Type = type,
                    TourCategories = ReturnMenuTourCategory(type.Id)
                });
            }

            return tourTypes;
        }
        public List<MenuTour> ReturnEorupMenuTours()
        {
            List<MenuTour> menuTour = new List<MenuTour>();

            List<TourCategory> tourCategories = db.TourCategories.Where(current => current.IsDelete == false && current.ParentId == null).ToList();

            foreach (TourCategory tourCategory in tourCategories)
            {
                menuTour.Add(new MenuTour
                {
                    TourCategoryParent = tourCategory,
                    TourCategory = db.TourCategories.Where(current => current.IsDelete == false && current.ParentId == tourCategory.Id).ToList()
                });
            }
            return menuTour;
        }
       
    public TextTypeItem ReturnFooter()
    {
        Guid footerAboutId = new Guid("e3e502a5-9348-4d95-931e-18eee54c055f");
        TextTypeItem textTypeItem = db.TextTypeItems.Find(footerAboutId);
        return textTypeItem;
    }
    public List<BlogGroup> ReturnBlogGroups()
    {
        List<BlogGroup> blogGroups = db.BlogGroups.Where(current => current.IsDelete == false).ToList();

        return blogGroups;
    }
}
}