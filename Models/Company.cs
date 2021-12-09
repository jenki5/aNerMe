using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class Company
    {
        [Key]
        public int CompanyID {get;set;}
        public string CompanyName {get;set;}
        public DateTime CreatedAt {get;set;}
        public List<Administrator> Administrators {get;set;}
        public List<CompanyAddress> CompanyAddresses {get;set;}
        public List<Product> Products {get;set;}
        public List<Image> Images {get;set;}
        public Banner Banner {get;set;}
        [NotMapped]
        public List<AdminLevel> AdminLevels {get;set;}
        [NotMapped]
        public List<LTPAction> LTPActions {get;set;}
        [NotMapped]
        public List<MenuAction> MenuActions {get;set;}
        [NotMapped]
        public List<BannerPartial> BannerPartials {get;set;}
        [NotMapped]
        public List<DeliveryMethod> DeliveryMethods {get;set;}
        [NotMapped]
        public MenuItem MenuItem {get;set;}
    }
}