using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class InformationPage
    {
        [Key]
        public int InformationPageID {get;set;}
        public string Title {get;set;}
        public string Body {get;set;}
        public int? CompanyID {get;set;}
        public void MatchInformationPage(InformationPage IP)
        {
            Title = IP.Title;
            Body = IP.Body;
        }
    }
}