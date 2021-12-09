using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class PageCategory
    {
        public int CategoryID {get;set;}
        public Category Category {get;set;}
        public int MenuItemID {get;set;}
        public PageCategory(){}

        public PageCategory(int CatID, int MenID)
        {
            CategoryID = CatID;
            MenuItemID = MenID;
        }
        public PageCategory(int MenID, string Cat)
        {
            MenuItemID = MenID;
            Category = new Category(Cat);
        }
    }    
}