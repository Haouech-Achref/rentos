using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rentos.Models.RentViewModels
{
    public class RentSearchViewModel
    {

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

        [Display(Name = "Manufacturer")]
        public string ManufacturerFilter { get; set; }

        [Display(Name = "Model")]
        public string ModelFilter { get; set; }

        [Display(Name = "Maximum price per day")]
        public string MaxPriceFilter { get; set; }

        [Display(Name = "Minimum brake horsepower")]
        public string MinPowerFilter { get; set; }

        [Display(Name = "Color")]
        public string ColorFilter { get; set; }

    }

}