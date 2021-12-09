using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class ProductCategory
    {
        public int CategoryID {get;set;}
        public Category Category {get;set;}
        public int ProductID {get;set;}
        public ProductCategory(){}

        public ProductCategory(int CatID, int ProdID)
        {
            CategoryID = CatID;
            ProductID = ProdID;
        }
        public ProductCategory(int ProdID, string Cat)
        {
            ProductID = ProdID;
            Category = new Category(Cat);
        }
    }    
}