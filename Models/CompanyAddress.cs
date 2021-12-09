namespace Anerme.Models
{
    public class CompanyAddress
    {
        public int CompanyID {get;set;}
        public Company Company {get;set;}
        public int AddressID {get;set;}
        public Address Address {get;set;}
    }
}