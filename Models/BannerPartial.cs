using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class BannerPartial
    {
        [Key]
        public int BannerPartialID {get;set;}
        public string BannerPartialName {get;set;}
    }    
}