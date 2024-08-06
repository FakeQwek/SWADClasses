using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<DateTime> getAvailableBookingDateTimes()
        {
            return ListOfAvailableBookingDateTimes;
        }

        public bool checkIfDateTimesAvailable(DateTime startDateTime, DateTime endDateTime)
        {
            if (ListOfAvailableBookingDateTimes.Contains(startDateTime) && ListOfAvailableBookingDateTimes.Contains(endDateTime))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public void addBooking(Booking booking)
        {
            BookingList.Add(booking);
        }

        public void updateBookingDateTimes(DateTime startDateTime, DateTime endDateTime)
        {
            ListOfAvailableBookingDateTimes.Remove(startDateTime);
            ListOfAvailableBookingDateTimes.Remove(endDateTime);
        }

        public void deleteBooking(Booking booking)
        {
            BookingList.Remove(booking);
        }

        public void releaseBookingDateTimes(DateTime startDateTime, DateTime endDateTime)
        {
            ListOfAvailableBookingDateTimes.Add(startDateTime);
            ListOfAvailableBookingDateTimes.Add(endDateTime);
        }
    }
}
