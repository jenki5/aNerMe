using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Anerme.Models
{
    public class Address
    {
        [Key]
        public int AddressID {get;set;}
        public int StreetAddressID {get;set;}
        public StreetAddress StreetAddress {get;set;} 
        public int CityID {get;set;}
        public City City {get;set;}
        public int StateID {get;set;}
        public State State {get;set;}
        public int Zip {get;set;}
    }    
}