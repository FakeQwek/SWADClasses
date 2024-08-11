using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment_Classes
{
    public class Pickup
    {
        public string PickupOption { get; set; }

        public string PickupLocation { get; set; }

        public Pickup(string pickupOption, string pickupLocation)
        {
            PickupOption = pickupOption;
            PickupLocation = pickupLocation;
        }
    }
}
