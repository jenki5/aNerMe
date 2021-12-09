using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class StreetAddress
    {
        [Key]
        public int StreetAddressID {get;set;}
        public string StreetAddressName {get;set;}
    }    
}