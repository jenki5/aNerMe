using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class LTPAction
    {
        [Key]
        public int LTPActionID {get;set;}
        public string LTPActionName {get;set;}
    }    
}