using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rentos.Models.CarViewModels
{
    public class CarViewModel
    {

        public int CarId { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        [Display(Name = "Registration number")]
        public string RegistrationNumber { get; set; }

        public string Color { get; set; }

        public int Mileage { get; set; }

        public int Power { get; set; }

        public int Price { get; set; }

        [Display(Name = "Car is available.")]
        public Boolean IsAvailable { get; set; }
    }
}
