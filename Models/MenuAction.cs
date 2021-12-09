using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class MenuAction
    {
        [Key]
        public int MenuActionID {get;set;}
        public string MenuActionName {get;set;}
    }    
}