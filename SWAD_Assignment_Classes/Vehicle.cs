using System;
using System.Collections.Generic;

namespace SWAD_Assignment_Classes
{
    internal class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public int Mileage { get; set; }
        public string LicensePlateNumber { get; set; }
        public int RentalRate { get; set; }
        public bool IsHidden { get; set; }
        public List<DateTime> ListOfAvailableBookingDateTimes { get; set; }
        public List<Booking> BookingList { get; set; }

        public Vehicle(int id, string make, string model, string year, int mileage, string licensePlateNumber, int rentalRate, bool isHidden, List<DateTime> listOfAvailableBookingDateTimes, List<Booking> bookingList)
        {
            Id = id;
            Make = make;
            Model = model;
            Year = year;
            Mileage = mileage;
            LicensePlateNumber = licensePlateNumber;
            RentalRate = rentalRate;
            IsHidden = isHidden;
            ListOfAvailableBookingDateTimes = listOfAvailableBookingDateTimes;
            BookingList = bookingList;
        }

        public List<DateTime> GetAvailableBookingDateTimes()
        {
            return ListOfAvailableBookingDateTimes;
        }

        public bool CheckIfDateTimesAvailable(DateTime startDateTime, DateTime endDateTime)
        {
            return ListOfAvailableBookingDateTimes.Contains(startDateTime) && ListOfAvailableBookingDateTimes.Contains(endDateTime);
        }

        public void AddBooking(Booking booking)
        {
            BookingList.Add(booking);
        }

        public void UpdateBookingDateTimes(DateTime startDateTime, DateTime endDateTime)
        {
            ListOfAvailableBookingDateTimes.Remove(startDateTime);
            ListOfAvailableBookingDateTimes.Remove(endDateTime);
        }

        public void DeleteBooking(Booking booking)
        {
            BookingList.Remove(booking);
        }

        public void ReleaseBookingDateTimes(DateTime startDateTime, DateTime endDateTime)
        {
            ListOfAvailableBookingDateTimes.Add(startDateTime);
            ListOfAvailableBookingDateTimes.Add(endDateTime);
        }
    }
}
