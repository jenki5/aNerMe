using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class ProductPageCategory
    {
        public int CategoryID {get;set;}
        public Category Category {get;set;}
        public int ProductPageID {get;set;}
        
    }    
}