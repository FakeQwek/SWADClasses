using SWAD_Assignment_Classes;
using System;
using System.Collections.Generic;

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
        get { return renter; }
        set
        {
            if (renter != value)
            {
                renter = value;
                value.Registration = this;
            }
        }
    }

    public List<string> GetBackgroundInfo()
    {
        return Renter.GetBackgroundInfo();
    }

    public bool GetValidityOfDriversLicense()
    {
        return Renter.GetValidityOfDriversLicense();
    }

    public int GetRegistrationId()
    {
        if (!isChecked)
        {
            return registrationId;
        }
        return -1;
    }

    public void DeleteRegistration()
    {
        Renter.Registration = null;
    }

    public void UpdateRegistration()
    {
        IsChecked = true;
        Renter.ApproveRenter();
    }

    public Registration(int registrationId, Renter renter)
    {
        RegistrationId = registrationId;
        Renter = renter;
    }
}
