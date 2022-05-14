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
        [Required]
        [StringLength(15, ErrorMessage = "title cannot be longer than 15 characters.")]
        public string Title { get; set; }

        [StringLength(50, ErrorMessage = "User Name cannot be longer than 30 characters.")]
        public string Username { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "First Name cannot be longer than 30 characters.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Last Name cannot be longer than 30 characters.")]
        public string Lastname { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Email cannot be longer than 150 characters.")]
        public string Email { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Password cannot be longer than 150 characters.")]
        public string Password { get; set; }

        [StringLength(15, ErrorMessage = "Mobile cannot be longer than 30 characters.")]
        public string Mobile { get; set; }

        [StringLength(15, ErrorMessage = "Telephone cannot be longer than 30 characters.")]
        public string Telephone { get; set; }
        public string Role { get; set; }

        [Required]
        public Int32 AuthLevelRefId { get; set; }
        public bool IsDeleted { get; set; }



        public int Nationality { get; set; }
        public int CountryId { get; set; }
        public int PositionId { get; set; }

        [StringLength(50, ErrorMessage = "City Name Can't br longer than 150 characters")]
        public string City { get; set; }
       
        public DateTime Birthday { get; set; }
        public DateTime EndDate { get; set; }
       
        [StringLength(150, ErrorMessage = "File Name Can't br longer than 150 characters")]
        public string PictureFileName { get; set; }
        public int OpId { get; set; }
    }
}

