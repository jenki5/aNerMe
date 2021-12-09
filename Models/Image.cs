using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace Anerme.Models
{
    public class Image
    {
        [Key]
        public int ImageID { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public int CompanyID { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
