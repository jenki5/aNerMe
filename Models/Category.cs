using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class Category
    {
        [Key]
        public int CategoryID {get;set;}
        public string CategoryName {get;set;}

        public Category(){}
        public Category(string CatName)
        {
            CategoryName = CatName;
        }
    }    
}