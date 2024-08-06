using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assignment_Classes
{
    internal class Booking
    {
        public int Id { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public double Cost { get; set; }

        public bool PaidFor { get; set; }

        public Pickup Pickup { get; set; }

        public DropOff DropOff { get; set; }

        public Vehicle Vehicle { get; set; }

        public Renter Renter { get; set; }

        public Booking(int id, DateTime startDateTime, DateTime endDateTime, bool paidFor, Pickup pickup, DropOff dropOff, Vehicle vehicle, Renter renter)
        {
            Id = id;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            PaidFor = paidFor;
            Pickup = pickup;
            DropOff = dropOff;
            Vehicle = vehicle;
            Renter = renter;

            Cost = ((endDateTime - startDateTime).TotalDays + 1) * vehicle.RentalRate;
        }

        public void updateBookingPaidStatus()
        {
            PaidFor = true;
        }
    }
}
