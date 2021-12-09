using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Anerme.Models
{
    public class User
    {
        [Key]
        public int UserID {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get;set;}
        public string Password {get;set;}
        public string DisplayName {get;set;}
        public string PhoneNumber {get;set;}
        public int? ProfileImageID {get;set;}
        public Image ProfileImage {get;set;}
        public DateTime? CreatedAt {get;set;}
        public List<Administrator> Administrators {get;set;}
        [NotMapped]
        public string ConfirmPassword {get;set;}
        [NotMapped]
        public string OldPassword {get;set;}
        public void UpdateUser(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            DisplayName = user.DisplayName;
            PhoneNumber = user.PhoneNumber;
            ProfileImageID = user.ProfileImageID;

            if(user.Password != null)
            {
                if(user.Password.TrimStart().TrimEnd() != "" && user.Password == user.ConfirmPassword)
                {                
                    PasswordHasher<string> Hasher = new PasswordHasher<string>();
                    if(Hasher.VerifyHashedPassword("", Password, user.OldPassword) != 0)
                    {
                        Password = Hasher.HashPassword(user.Password, user.Password);
                    }
                }
            }
        }

        public void SendNewPasswordEmail(string Password)
        {
            Console.WriteLine(Password);
        }

        public List<string> PasswordGenerator()
        {
            Random newRandom = new Random();
            int num = newRandom.Next() % 12;
            string newPassword = "";
            for(int i = 0; i < 8; i++)
            {
                newPassword += (char)(65 + newRandom.Next() % 26);
            }

            List<string> Passwords = new List<string>();
            Passwords.Add(newPassword);
            PasswordHasher<string> Hasher = new PasswordHasher<string>();           
            string EncryptedPassword = Hasher.HashPassword(newPassword, newPassword);
            Passwords.Add(EncryptedPassword);
            return Passwords;
        }
    }    
}