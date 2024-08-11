using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment_Classes
{
    public class DropOff
    {
        public string DropOffOption { get; set; }

        public string DropOffLocation { get; set; }

        public DropOff(string dropOffOption, string dropOffLocation)
        {
            DropOffOption = dropOffOption;
            DropOffLocation = dropOffLocation;
        }
    }
}
