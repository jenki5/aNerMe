using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class Administrator
    {
        [Key]
        public int AdministratorID {get;set;}
        public string AdministratorName {get;set;}
        public string Password {get;set;}
        [NotMapped]
        public string ConfirmPassword {get;set;}
        public int UserID {get;set;}
        public User User {get;set;}
        public int CompanyID {get;set;}
        public Company company {get;set;}
        public int AdminLevelID {get;set;}
        public AdminLevel adminLevel {get;set;}
        public DateTime CreatedAt {get;set;}
        public Administrator(){}
        public Administrator(string adminName, string encryptedPassword, int companyID, int adminLevelID)
        {
            AdministratorName = adminName;
            Password = encryptedPassword;
            CompanyID = companyID;
            AdminLevelID = adminLevelID;
            CreatedAt = DateTime.Now;
        }
    }    
}