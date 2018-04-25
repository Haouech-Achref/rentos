using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentos.Models.RentViewModels
{
    public class RentViewModel
    {
        public IEnumerable<Car> AvailableCars { get; set; }
        public Rent Rent { get; set; }
    }
}
