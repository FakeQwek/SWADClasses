using System;

public class Registration
{
    private int registrationId;

    private bool isChecked = false;

    private Renter renter;

    public bool IsChecked
    {
        get { return isChecked; }
        set { isChecked = value; }
    }

    public int RegistrationId
    {
        get { return registrationId; }
        set { registrationId = value; }
    }

    public Renter Renter
    {
        get
        {
            return renter;
        }
        set
        {
            if (renter != value)
            {
                renter = value;
                value.Registration = this;
            }

        }
    }


    public List<string> getBackgroundInfo()
    {

        return Renter.getBackgroundInfo();
    }

    public bool getValidityOfDriversLicense()
    {
        return Renter.getValidityOfDriversLicense();
    }



    public int getRegistrationId()
    {
        if (isChecked == false)
        {
            return registrationId;
        }
        return -1;

    }

    public void deleteRegistration()
    {

        Renter.Registration = null;
    }

    public void updateRegistration()
    {
        IsChecked = true;
        Renter.approveRenter();
    }

    public Registration(int registrationId, Renter renter)
    {
        RegistrationId = registrationId;
        Renter = renter;
    }
}