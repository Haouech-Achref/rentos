using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentos.Models.RentViewModels
{
    public class RentListViewModel
    {
        public Car Car { get; set; }
        public Rent Rent { get; set; }
        public ApplicationUser User { get; set; }
    }
}
