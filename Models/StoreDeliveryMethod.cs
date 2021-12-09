using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class StoreDeliveryMethod
    {
        public int ContactUsPageID {get;set;}
        public int DeliveryMethodID {get;set;}
        public DeliveryMethod DeliveryMethod {get;set;}
    }    
}