using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class ProductImage
    {
        public int ProductID {get;set;}
        public int ImageID {get;set;}
        public Image Image {get;set;}
        public int OrderNumber {get;set;}
    }    
}