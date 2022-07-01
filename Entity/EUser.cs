using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class EUser
    {
        [Key]
        public int UserId { get; set; }
        
        public string ?Title { get; set; }
     
        public string ?Username { get; set; }
       
        public string FirstName { get; set; }
       
        public string ?Lastname { get; set; }
       
        public string Email { get; set; }
       
        public string ?Password { get; set; }
       
        public string ?Mobile { get; set; }
        public string ?Telephone { get; set; }

        [Required]
        public Int32 AuthLevelRefId { get; set; }
        public bool IsDeleted { get; set; }

        public int Nationality { get; set; }
        public int CountryId { get; set; }
        public int PositionId { get; set; }
        public string ?City { get; set; }
       
        public DateTime Birthday { get; set; }
        public DateTime ?EndDate { get; set; }

        public int OpId { get; set; }

        public string ?PictureFileName { get; set; }
        [NotMapped]
        public int BranchId { get; set; }
        [NotMapped]
        public int CompanyId { get; set; }
        [NotMapped]
        public string ?BranchName { get; set; }
        [NotMapped]
        public string ?CompanyName { get; set; }
        [NotMapped]
        public string ?ResidentContry { get; set; }
        [NotMapped]
        public string ?NationalityName { get; set; }
        [NotMapped]
        public int CompanyTypeId { get; set; }
        [NotMapped]
        public int UserAndBranchId { get; set; }
        [NotMapped]
        public string ?UserRole { get; set; }
    }
}

