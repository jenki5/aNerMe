using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class Banner
    {
        [Key]
        public int BannerID {get;set;}
        public int CompanyID {get;set;}
        public int? ImageID {get;set;}
        public Image Image {get;set;}
        
        public int? BannerPartialID {get;set;}
        public BannerPartial BannerPartial {get;set;}
        public string BannerColor {get;set;}
        public List<MenuItem> MenuItems {get;set;}

        public void MatchBanner(Banner newBanner)
        {
            ImageID = newBanner.ImageID;
            BannerColor = newBanner.BannerColor;
            BannerPartialID = newBanner.BannerPartialID;
            if(MenuItems == null)
            {
                MenuItems = new List<MenuItem>();
            }
            for(int i = 0; i < newBanner.MenuItems.Count(); i++)
            {
                if(MenuItems.Count() > i)
                {
                    MenuItems[i].MatchMenuItem(newBanner.MenuItems[i]);
                }
                else
                {
                    MenuItem _MenuItem = new MenuItem();
                    _MenuItem.MatchMenuItem(newBanner.MenuItems[i]);
                    MenuItems.Add(_MenuItem);
                }                
            }
            while(MenuItems.Count() > newBanner.MenuItems.Count())
            {
                MenuItems.Remove(newBanner.MenuItems[newBanner.MenuItems.Count() - 1]);
            }
        }

        public void RemoveParentMenuItems()
        {
            if(MenuItems != null)
            {
                for(int i = 0; i < MenuItems.Count(); i++)
                {
                    if(MenuItems[i].DropDownItems != null)
                    {
                        for(int x = 0; x < MenuItems[i].DropDownItems.Count(); x++)
                        {
                            MenuItems[i].DropDownItems[x].ParentMenuItem = null;
                        }
                    }
                }
            }
        }
    }
}