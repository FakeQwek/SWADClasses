using System;
using System.Collections.Generic;

namespace SWAD_Assignment_Classes
{
    public class Renter
    {
        private List<string> backgroundInfo = new List<string>();
        private bool isApproved = false;
        private bool isPrime;
        private DateTime licenseExpiry;
        private List<DateTime> driversLicenseInfo = new List<DateTime>();
        private DateTime dateOfBirth;

        // Add this property to hold the Registration instance
        public Registration Registration { get; set; }

        public List<string> BackgroundInfo
        {
            get { return backgroundInfo; }
            set { backgroundInfo = value; }
        }

        public bool IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }

        public DateTime LicenseExpiry
        {
            get { return licenseExpiry; }
            set { licenseExpiry = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public bool IsPrime
        {
            get { return isPrime; }
            set { isPrime = value; }
        }

        public List<DateTime> DriversLicenseInfo
        {
            get { return driversLicenseInfo; }
            set { driversLicenseInfo = value; }
        }

        public void ApproveRenter()
        {
            isApproved = true;
        }

        public bool GetValidityOfDriversLicense()
        {
            LicenseExpiry = DriversLicenseInfo[1];
            DateTime currentTime = DateTime.Today;
            int compare = DateTime.Compare(currentTime, LicenseExpiry);
            int compare2 = DateTime.Compare(DateOfBirth, DriversLicenseInfo[0]);

            if (compare2 == 0)
            {
                if (compare < 0 || compare == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<string> ScrapeForBackgroundInfo()
        {
            Random random = new Random();
            string[] drivingRecords = { "Drunk driving - 19/05/20", "None" };
            string[] criminalRecords = { "Assault and Battery - 09/09/16", "None" };

            int randNo = random.Next(drivingRecords.Length);
            int randNo2 = random.Next(criminalRecords.Length);

            string drivingRecord = drivingRecords[randNo];
            string criminalRecord = criminalRecords[randNo2];

            if (drivingRecord != "None" || criminalRecord != "None")
            {
                BackgroundInfo.Add("List of driving offenses:\n" + drivingRecord);
                BackgroundInfo.Add("List of criminal offenses:\n" + criminalRecord);
            }

            return BackgroundInfo;
        }

        public List<string> GetBackgroundInfo()
        {
            return ScrapeForBackgroundInfo();
        }

        public Renter(DateTime dateOfBirth, List<DateTime> driversLicenseInfo)
        {
            DateOfBirth = dateOfBirth;
            DriversLicenseInfo = driversLicenseInfo;
        }
    }
}
