using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItemID {get;set;}
        public int? BannerID {get;set;}
        public string Title {get;set;}
        public int LTPActionID {get;set;}
        public LTPAction LTPAction {get;set;}
        public int? MenuActionID {get;set;}
        public MenuAction MenuAction {get;set;}
        public int OrderNumber {get;set;}
        [ForeignKey("ParentMenuItem")]
        public int? ParentMenuItemID {get;set;}
        public MenuItem ParentMenuItem {get;set;}
        public int? ContactUsPageID {get;set;}
        public ContactUsPage ContactUsPage {get;set;}
        public int? InformationPageID {get;set;}
        public InformationPage InformationPage {get;set;}
        public List<MenuItem> DropDownItems {get;set;}
        public List<PageCategory> PageCategories {get;set;}
        [NotMapped]
        public string CategoriesString {get;set;}

        public void RemoveParent()
        {
            for(int i = 0; i < DropDownItems.Count(); i++)
            {
                DropDownItems[i].ParentMenuItem = null;
            }
        }

        public void MatchMenuItem(MenuItem menuItem)
        {
            Title = menuItem.Title;
            LTPActionID = menuItem.LTPActionID;
            MenuActionID = menuItem.MenuActionID;
            OrderNumber = menuItem.OrderNumber;
            ParentMenuItem = menuItem.ParentMenuItem;
            ContactUsPageID = menuItem.ContactUsPageID;
            InformationPageID = menuItem.InformationPageID;

            if(menuItem.DropDownItems != null)
            {
                if(DropDownItems == null)
                {
                    DropDownItems = new List<MenuItem>();
                }
                for(int i = 0; i < menuItem.DropDownItems.Count(); i++)
                {
                    MenuItem _MenuItem;
                    if(DropDownItems.Count() <= i)
                    {
                        _MenuItem = new MenuItem();
                        _MenuItem.MatchMenuItem(menuItem.DropDownItems[i]);
                        DropDownItems.Add(_MenuItem);
                        _MenuItem.ParentMenuItem = this;
                    }
                    else
                    {
                        DropDownItems[i].MatchMenuItem(menuItem.DropDownItems[i]);
                        DropDownItems[i].ParentMenuItem = this;
                    }
                }
                while(DropDownItems.Count() > menuItem.DropDownItems.Count())
                {
                    DropDownItems.Remove(DropDownItems[DropDownItems.Count() - 1]);
                }
            }
        }

        public void CreatePageCategoriesList(List<Category> Categories, int _MenuItemID)
        {
            PageCategories = new List<PageCategory>();
            for(int i = 0; i < Categories.Count(); i++)
            {
                PageCategory PC = new PageCategory()
                {
                    MenuItemID = _MenuItemID,
                    CategoryID = Categories[i].CategoryID
                };
                PageCategories.Add(PC);
            }
        }

        public void CreateCategoriesPageCategoriesList(List<string> CategoriesArr, List<Category> Categories, int _MenuItemID)
        {
            PageCategories = new List<PageCategory>();
            for(int i = 0; i < CategoriesArr.Count(); i++)
            {
                if(CategoriesArr[i].Trim() != "")
                {
                    bool found = false;
                    string CatName = CategoriesArr[i].TrimStart().TrimEnd();
                    for(int x = 0; x < Categories.Count(); x++)
                    {
                        if(Categories[x].CategoryName == CatName)
                        {
                            PageCategory PC = new PageCategory(Categories[x].CategoryID, _MenuItemID);
                            PageCategories.Add(PC);
                            found = true;
                            break;
                        }
                    }
                    if(!found)
                    {
                        PageCategory PC = new PageCategory(_MenuItemID, CatName);
                        PageCategories.Add(PC);
                    }
                }
            }
        }
    }    
}