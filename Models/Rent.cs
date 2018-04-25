
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rentos.Models
{
    public class Rent
    {
        [Key]
        [Display(Name ="Reference")]
        public int RentId { get; set; }

        [Display(Name = "User id")]
        public int Car_CarId { get; set; }

        [Display(Name = "User name")]
        public string User_UserId { get; set; }

        [Display(Name = "Pick-up date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        [Column(TypeName = "PickupDate")]
        [Required(ErrorMessage = "A pick-up date is required.")]
        public DateTime PickupDate { get; set; }

        [Display(Name = "Drop-off date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Column(TypeName = "DropoffDate")]
        [Required(ErrorMessage = "A drop-off date is required.")]
        public DateTime DropoffDate { get; set; }

    }
}