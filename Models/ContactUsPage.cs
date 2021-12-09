using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Anerme.Models
{
    public class ContactUsPage
    {
        [Key]
        public int ContactUsPageID {get;set;}
        public int CompanyID {get;set;}
        public int? ImageID {get;set;}
        public Image Image {get;set;}
        public int AddressID {get;set;}
        public Address Address {get;set;}
        public string StoreName {get;set;}
        public string PhoneNumber {get;set;}
        public DateTime? SundayOpen {get;set;}
        public DateTime? SundayClose {get;set;}
        public DateTime? MondayOpen {get;set;}
        public DateTime? MondayClose {get;set;}
        public DateTime? TuesdayOpen {get;set;}
        public DateTime? TuesdayClose {get;set;}
        public DateTime? WednesdayOpen {get;set;}
        public DateTime? WednesdayClose {get;set;}
        public DateTime? ThursdayOpen {get;set;}
        public DateTime? ThursdayClose {get;set;}
        public DateTime? FridayOpen {get;set;}
        public DateTime? FridayClose {get;set;}
        public DateTime? SaturdayOpen {get;set;}
        public DateTime? SaturdayClose {get;set;}
        [NotMapped]
        public string SundayOpenString {get;set;}
        [NotMapped]
        public string MondayOpenString {get;set;}
        [NotMapped]
        public string TuesdayOpenString {get;set;}
        [NotMapped]
        public string WednesdayOpenString {get;set;}
        [NotMapped]
        public string ThursdayOpenString {get;set;}
        [NotMapped]
        public string FridayOpenString {get;set;}
        [NotMapped]
        public string SaturdayOpenString {get;set;}
        [NotMapped]
        public string SundayCloseString {get;set;}
        [NotMapped]
        public string MondayCloseString {get;set;}
        [NotMapped]
        public string TuesdayCloseString {get;set;}
        [NotMapped]
        public string WednesdayCloseString {get;set;}
        [NotMapped]
        public string ThursdayCloseString {get;set;}
        [NotMapped]
        public string FridayCloseString {get;set;}
        [NotMapped]
        public string SaturdayCloseString {get;set;}
        public List<StoreDeliveryMethod> StoreDeliveryMethods {get;set;}

        public void SetStringOpenClose()
        {
            string format = "t";
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            if(SundayOpen != null)
            {
                SundayOpenString = ((DateTime)SundayOpen).ToString(format, culture);
            }
            if(MondayOpen != null)
            {
                MondayOpenString = ((DateTime)MondayOpen).ToString(format, culture);
            }
            if(TuesdayOpen != null)
            {
                TuesdayOpenString = ((DateTime)TuesdayOpen).ToString(format, culture);
            }
            if(WednesdayOpen != null)
            {
                WednesdayOpenString = ((DateTime)WednesdayOpen).ToString(format, culture);
            }
            if(ThursdayOpen != null)
            {
                ThursdayOpenString = ((DateTime)ThursdayOpen).ToString(format, culture);
            }
            if(FridayOpen != null)
            {
                FridayOpenString = ((DateTime)FridayOpen).ToString(format, culture);
            }
            if(SaturdayOpen != null)
            {
                SaturdayOpenString = ((DateTime)SaturdayOpen).ToString(format, culture);
            }
            if(SundayClose != null)
            {
                SundayCloseString = ((DateTime)SundayClose).ToString(format, culture);
            }
            if(MondayClose != null)
            {
                MondayCloseString = ((DateTime)MondayClose).ToString(format, culture);
            }
            if(TuesdayClose != null)
            {
                TuesdayCloseString = ((DateTime)TuesdayClose).ToString(format, culture);
            }
            if(WednesdayClose != null)
            {
                WednesdayCloseString = ((DateTime)WednesdayClose).ToString(format, culture);
            }
            if(ThursdayClose != null)
            {
                ThursdayCloseString = ((DateTime)ThursdayClose).ToString(format, culture);
            }
            if(FridayClose != null)
            {
                FridayCloseString = ((DateTime)FridayClose).ToString(format, culture);
            }
            if(SaturdayOpen != null)
            {
                SaturdayCloseString = ((DateTime)SaturdayClose).ToString(format, culture);
            }
        }
        public void MatchContactUsPage(ContactUsPage CUP)
        {
            ImageID = CUP.ImageID;
            CompanyID = CUP.CompanyID;
            StoreName = CUP.StoreName;
            PhoneNumber = CUP.PhoneNumber;
            SundayOpen = CUP.SundayOpen;
            SundayClose = CUP.SundayClose;
            MondayOpen = CUP.MondayOpen;
            MondayClose = CUP.MondayClose;
            TuesdayOpen = CUP.TuesdayOpen;
            TuesdayClose = CUP.TuesdayClose;
            WednesdayOpen = CUP.WednesdayOpen;
            WednesdayClose = CUP.WednesdayClose;
            ThursdayOpen = CUP.ThursdayOpen;
            ThursdayClose = CUP.ThursdayClose;
            FridayOpen = CUP.FridayOpen;
            FridayClose = CUP.FridayClose;
            SaturdayOpen = CUP.SaturdayOpen;
            SaturdayClose = CUP.SaturdayClose;

            for(int x = 0; x < StoreDeliveryMethods.Count(); x++)
            {
                bool InThat = false;
                for(int j = 0; j < CUP.StoreDeliveryMethods.Count(); j++)
                {
                    if(CUP.StoreDeliveryMethods[j].DeliveryMethodID == StoreDeliveryMethods[x].DeliveryMethodID)
                    {
                        InThat = true;
                    }
                }
                if(!InThat){
                    StoreDeliveryMethods.Remove(StoreDeliveryMethods[x]);
                    x--;
                }
            }

            for(int x = 0; x < CUP.StoreDeliveryMethods.Count(); x++)
            {
                bool InThis = false;
                for(int j = 0; j < StoreDeliveryMethods.Count(); j++)
                {
                    if(CUP.StoreDeliveryMethods[x].DeliveryMethodID == StoreDeliveryMethods[j].DeliveryMethodID)
                    {
                        InThis = true;
                    }
                }
                if(!InThis){
                    StoreDeliveryMethods.Add(CUP.StoreDeliveryMethods[x]);
                    x--;
                }
            }
        }
    }    
}