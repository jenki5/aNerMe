using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class DeliveryMethod
    {
        [Key]
        public int DeliveryMethodID {get;set;}
        public string DeliveryMethodName {get;set;}
    }    
}