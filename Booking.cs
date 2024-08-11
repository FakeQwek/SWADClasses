using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SWAD_Assignment_Classes
{
    public class Booking
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

            if (pickup.PickupOption == "delivery")
            {
                Cost += 10;
            }

            if (dropOff.DropOffOption == "delivery")
            {
                Cost += 10;
            }
        }

        public void updateBookingPaidStatus()
        {
            PaidFor = true;
        }

        public void CalculateCost()
        {
            Cost = ((EndDateTime - StartDateTime).TotalDays + 1) * Vehicle.RentalRate;

            if (Pickup.PickupOption.ToLower() == "delivery")
            {
                Cost += 10;
            }

            if (DropOff.DropOffOption.ToLower() == "delivery")
            {
                Cost += 10;
            }
        }

        public void addRenterBooking()
        {
            Renter.BookingList.Add(this);
        }

        public static List<Booking> GetExistingBookings(string filePath, List<Vehicle> vehicles)
        {
            List<Booking> bookings = new List<Booking>();
            var lines = File.ReadAllLines(filePath);
            var dateFormat = "dd/MM/yyyy";
            var timeFormat = "HH:mm:ss";

            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');

                if (values.Length < 14)
                {
                    Console.WriteLine($"Skipping line {i + 1} due to insufficient data (length: {values.Length}): {lines[i]}");
                    continue;
                }

                try
                {
                    int bookingId = int.Parse(values[0]);
                    var startDateTime = DateTime.ParseExact($"{values[1]} {values[2]}", $"{dateFormat} {timeFormat}", CultureInfo.InvariantCulture);
                    var endDateTime = DateTime.ParseExact($"{values[3]} {values[4]}", $"{dateFormat} {timeFormat}", CultureInfo.InvariantCulture);

                    string pickupOption = values[5];
                    string dropOffOption = values[6];
                    string pickupLocation = values[7];
                    string dropOffLocation = values[8];

                    bool paidFor = bool.Parse(values[9]);

                    int vehicleId = int.Parse(values[10]);
                    var vehicle = vehicles.Find(v => v.Id == vehicleId);

                    if (vehicle == null)
                    {
                        throw new Exception($"Vehicle with ID {vehicleId} not found");
                    }

                    string carModel = values[11];
                    double vehicleRate = double.Parse(values[12]);
                    double cost = double.Parse(values[13]);

                    var defaultDateOfBirth = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var defaultLicenseExpiry = DateTime.ParseExact("01/01/2030", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var driversLicenseInfo = new List<DateTime> { defaultDateOfBirth, defaultLicenseExpiry };

                    var renter = new Renter(
                        id: 0,
                        fullName: "Default Name",
                        email: "default@example.com",
                        dateOfBirth: defaultDateOfBirth,
                        driversLicenseInfo: driversLicenseInfo,
                        bookingList: new List<Booking>()
                    )
                    {
                        IsPrime = false,
                        BackgroundInfo = new List<string> { "No background information available" }
                    };

                    var booking = new Booking(
                        id: bookingId,
                        startDateTime: startDateTime,
                        endDateTime: endDateTime,
                        paidFor: paidFor,
                        pickup: new Pickup(pickupOption, pickupLocation),
                        dropOff: new DropOff(dropOffOption, dropOffLocation),
                        vehicle: vehicle,
                        renter: renter
                    );

                    renter.BookingList.Add(booking);

                    bookings.Add(booking);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Skipping line {i + 1} due to format exception: {ex.Message}");
                    Console.WriteLine($"Line content: {lines[i]}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Skipping line {i + 1} due to unexpected exception: {ex.Message}");
                    Console.WriteLine($"Line content: {lines[i]}");
                }
            }

            return bookings;
        }

        public static void updateBookingDetails(string filePath, List<Booking> bookings)
        {
            var dateFormat = "dd/MM/yyyy";
            var timeFormat = "HH:mm:ss";
            var lines = new List<string>
            {
                "Id,StartDateTime,EndDateTime,PickupOption,DropOffOption,PickupLocation,DropOffLocation,PaidFor,VehicleId,CarModel,VehicleRate,Cost"
            };

            foreach (var booking in bookings)
            {
                var line = string.Join(",",
                    booking.Id,
                    booking.StartDateTime.ToString(dateFormat),
                    booking.StartDateTime.ToString(timeFormat),
                    booking.EndDateTime.ToString(dateFormat),
                    booking.EndDateTime.ToString(timeFormat),
                    booking.Pickup.PickupOption,
                    booking.DropOff.DropOffOption,
                    booking.Pickup.PickupLocation,
                    booking.DropOff.DropOffLocation,
                    booking.PaidFor,
                    booking.Vehicle.Id,
                    booking.Vehicle.Model,
                    booking.Vehicle.RentalRate,
                    booking.Cost);
                lines.Add(line);
            }
            File.WriteAllLines(filePath, lines);
        }
    }
}
