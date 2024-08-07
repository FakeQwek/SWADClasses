using System;
using System.Collections.Generic;

public class Renter
{
	private List<string> backgroundInfo = new List<string>();

	private bool isApproved = false;

    private bool isPrime;

	private DateTime licenseExpiry;

    private Registration registration;

    private List<DateTime> driversLicenseInfo = new List<DateTime>();

    private DateTime dateOfBirth;

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
        get { return  dateOfBirth; }
        set {  dateOfBirth = value; }
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

    public void approveRenter()
    {
        isApproved = true;
    }

    public bool getValidityOfDriversLicense()
    {

        
        LicenseExpiry = DriversLicenseInfo[1];
        DateTime currentTime = DateTime.Today;
        //Checks if license has expired
        int compare = DateTime.Compare(currentTime, LicenseExpiry);
        //Checks if given date of birth and license date of birth is the same
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
        
      
        //Console.WriteLine($"Debug: {compare}");
      
    }

    //Cannot actually scrape for non-existent renters, this method simulates the background information retrieved from the web.
    public List<string> scrapeForBackgroundInfo()
    {
        Random random = new Random();
       
        Random random2 = new Random();

        
        
        string[] drivingRecords = {"Drunk driving - 19/05/20\nParking violation - 30/07/24\nHit and Run - 2/8/19", "None",  "Careless driving - 12/01/20" , "None" ,"Careless driving - 24/05/21\nDisobeying traffic officer - 05/05/22" , "Drunk driving - 06/05/15\nParking violation - 30/07/26\nHit and Run - 2/8/19", "None"  };
        string[] criminalRecords = {"Assault and Battery - 09/09/16", "None" , "Shoplifting - 09/09/20\nLittering - 10/12/21", "None", "Assault - 02/09/19",  "None", };
        int randNo = random.Next(drivingRecords.Length);
        int randNo2 = random.Next(criminalRecords.Length);
        string drivingRecord = drivingRecords[randNo];
        string criminalRecord = criminalRecords[randNo2];
        //If both records cannot be found return empty background info
        if(drivingRecord != "None" ||  criminalRecord != "None" )
        {
            BackgroundInfo.Add("List of driving offenses:\n" + drivingRecords[randNo]);
            BackgroundInfo.Add("List of criminal offenses:\n" + criminalRecords[randNo2]);
        }
    
        return BackgroundInfo;
    }

    public Registration Registration
    {
        get
        {
            return registration;
        }
        set { 
            if(registration != value)
			{
				registration = value;
                if(registration != null)
                {
                    value.Renter = this;
                }
				
			}
        }
    }


    public List<string> getBackgroundInfo()
    {
        return scrapeForBackgroundInfo();
    }

    public Renter(DateTime dateOfBirth, List<DateTime> driversLicenseInfo)
	{
        
        DateOfBirth = dateOfBirth;
        DriversLicenseInfo = driversLicenseInfo;
	}
}
