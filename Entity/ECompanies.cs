using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class ECompanies
    {
        [Key]
        public int CompanyID { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string StreetNo { get; set; }
        public string? Telephone { get; set; }
        public string? Fax { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Altitude { get; set; }
        public DateTime?  EndDate { get; set; }
        public string? CompanyLogo { get; set; }
        public int? CompanyTypeId { get; set; }
        [NotMapped]
        public string? CountryName { get; set; }
        public int OpId { get; set; }
    
   
    }
}
