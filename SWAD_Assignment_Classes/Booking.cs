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

            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle), "Vehicle cannot be null.");
            }

            Cost = ((endDateTime - startDateTime).TotalDays + 1) * vehicle.RentalRate;

            if (pickup.PickupOption == "delivery")
            {
                Cost += 10;
            }

            if (dropOff.DropOffOption == "delivery")
            {
                Cost += 10;
            }
        }

        public void UpdateBookingPaidStatus()
        {
            PaidFor = true;
        }

        public bool ValidateBookingDetails(DateTime endDateTime)
        {
            Console.WriteLine("Validating booking details...");
            return DateTime.Now > endDateTime;
        }

        public void CalculatePenalty(DateTime endDateTime)
        {
            double penaltyAmount = (DateTime.Now - endDateTime).TotalHours * 10;
            Console.WriteLine($"Calculated Penalty: {penaltyAmount:C}");
        }

        public static Booking GetBooking(int id)
        {
            return new Booking(id, DateTime.Now.AddDays(-7), DateTime.Now.AddDays(-3), false, new Pickup("pickup", "location"), new DropOff("dropoff", "location"), new Vehicle(1, "Toyota", "Corolla", "2020", 50000, "ABC123", 100, false, new List<DateTime>(), new List<Booking>()), new Renter(DateTime.Parse("1990-01-01"), new List<DateTime> { DateTime.Parse("2015-01-01"), DateTime.Parse("2025-01-01") }));
        }

        public void DisplayBooking()
        {
            Console.WriteLine($"Booking ID: {Id}, Start Date: {StartDateTime}, End Date: {EndDateTime}, Cost: {Cost}, Paid: {PaidFor}");
        }
    }
}
