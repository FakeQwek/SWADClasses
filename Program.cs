using SWAD_Assignment_Classes;

// mock data (renters book slots that are 1 day/24 hours in length, assumed that minimum slot time is 1 day)
List<DateTime> dateTimes = new List<DateTime>();
for (int i = 1; i < 31; i++)
{
    dateTimes.Add(new DateTime(2024, 1, i));
}

List<Booking> bookings = new List<Booking>();
List<Vehicle> vehicles = new List<Vehicle>();
Vehicle vehicle1 = new Vehicle(1, "make", "model", "2022", 200, "SHA1025A", 40, false, dateTimes, bookings);
vehicles.Add(vehicle1);
List<DateTime> driversLicenseInfo = new List<DateTime>();
DateTime licenseDob = new DateTime(2000, 1, 15);
DateTime dob = new DateTime(2000, 1, 15);
DateTime licenseExpiry = new DateTime(2005, 5, 16);
driversLicenseInfo.Add(licenseDob);
driversLicenseInfo.Add(licenseExpiry);
List<Booking> bookingList = new List<Booking>();
Renter renter = new Renter(1, "Apple Tan", "appletan@gmail.com", dob, driversLicenseInfo, bookingList);

int vehicleId = 1;

// start sequence diagram

Vehicle vehicle = startCreateBooking(vehicleId, renter);

List<DateTime> listOfAvailableBookingDateTimes = vehicle.getAvailableBookingDateTimes();

if (listOfAvailableBookingDateTimes.Count == 0)
{
    displayNoAvailableBookingDateTimes();
}

while (listOfAvailableBookingDateTimes.Count != 0)
{
    DateTime startDateTime = DateTime.MinValue;

    while (startDateTime == DateTime.MinValue)
    {
        displayAvailableStartDateTimes(dateTimes);

        DateTime dateTime = selectStartBookingDateTime(Console.ReadLine());

        startDateTime = dateTime;

        if (startDateTime == DateTime.MinValue)
        {
            displayInvalidBookingStartDateTime();
            Console.ReadLine();
        }
    }

    DateTime endDateTime = DateTime.MinValue;

    while (endDateTime == DateTime.MinValue)
    {
        displayAvailableEndDateTimes(startDateTime, dateTimes);
        DateTime dateTime = selectEndBookingDateTime(Console.ReadLine());

        endDateTime = dateTime;

        if (endDateTime == DateTime.MinValue)
        {
            displayInvalidBookingEndDateTime();
            Console.ReadLine();
        }
    }

    Pickup pickup = selectPickupOption();

    DropOff dropOff = selectDropOffOption();

    bool available = vehicle.checkIfDateTimesAvailable(startDateTime, endDateTime);

    if (available == true)
    {
        Booking booking = createNewBooking(1, startDateTime, endDateTime, false, pickup, dropOff, vehicle, renter);

        addBooking(booking);

        vehicle.updateBookingDateTimes(startDateTime, endDateTime);

        bool paymentSuccess = makePayment(booking);

        if (paymentSuccess == true)
        {
            booking.updateBookingPaidStatus();

            displaySuccessfulBookingCreation(booking);
            Console.ReadLine();
        }
        else
        {
            displayUnsuccessfulBookingCreation();

            vehicle.deleteBooking(booking);

            vehicle.releaseBookingDateTimes(startDateTime, endDateTime);

            Console.ReadLine();
        }
    }
    else
    {
        displaySelectedBookingDateTimesUnavailable();
        Console.ReadLine();
    }
}

Vehicle startCreateBooking(int vehicleId, Renter renter)
{
    Vehicle vehicle = getVehicle(vehicleId);

    return vehicle;
}


Vehicle getVehicle(int vehicleId)
{
    return vehicles[vehicleId - 1];
}

void displayAvailableStartDateTimes(List<DateTime> dateTimes)
{
    for (int i = 0; i < dateTimes.Count; i++)
    {
        Console.WriteLine(dateTimes[i].ToString("dd/MM/yyyy"));
    }
    Console.WriteLine();
    Console.Write("Input your desired booking start date time: ");
}

void displayAvailableEndDateTimes(DateTime startDateTime, List<DateTime> dateTimes)
{
    int day = startDateTime.Day;

    for (int i = 0; i < dateTimes.Count; i++)
    {
        if (dateTimes[i].Day == day)
        {
            Console.WriteLine(dateTimes[i].ToString("dd/MM/yyyy"));
            day += 1;
        }
    }
    Console.WriteLine();
}

void displaySelectedBookingDateTimesUnavailable()
{
    Console.WriteLine("Your selected booking date times are no longer available");
}

void displayNoAvailableBookingDateTimes()
{
    Console.WriteLine("This vehicle has no more available booking date times");
}

bool makePayment(Booking booking)
{
    double bookingCost = booking.Cost;

    String paymentMethod = "";

    while (paymentMethod != "debit card" && paymentMethod != "credit card" && paymentMethod != "digital wallet")
    {
        Console.Write("Would you like to pay via debit card, credit card or digital wallet: ");
        paymentMethod = Console.ReadLine().ToLower();
    }

    if (paymentMethod == "debit card")
    {
        Console.Write("Input your debit card number: ");
        Console.ReadLine();
        Console.Write("Input your debit card's CVV: ");
        Console.ReadLine();
        Console.Write("Input your debit card's expiration date: ");
        DateTime expirationDate = Convert.ToDateTime(Console.ReadLine());
        if (expirationDate < DateTime.Now)
        {
            return false;
        }
        else
        {
            Console.WriteLine($"Your payment of ${bookingCost} has been processed");
            return true;
        }
    }
    else if (paymentMethod == "credit card")
    {
        Console.Write("Input your credit card number: ");
        Console.ReadLine();
        Console.Write("Input your credit card's CVV: ");
        Console.ReadLine();

        DateTime expirationDate = DateTime.MinValue;

        while (expirationDate == DateTime.MinValue)
        {
            try
            {
                Console.Write("Input your credit card's expiration date: ");
                expirationDate = Convert.ToDateTime(Console.ReadLine());
            } catch
            {
                Console.WriteLine("This is an invalid input");
            }
        }

        if (expirationDate < DateTime.Now)
        {
            return false;
        } else
        {
            Console.WriteLine($"Your payment of ${bookingCost} has been processed");
            return true;
        }
    }
    else
    {
        string digitalWallet = "";

        while (digitalWallet == "")
        {
            try
            {
                Console.Write("Input the digital wallet you will be using: ");
                digitalWallet = Console.ReadLine();
                Console.WriteLine($"Your payment of ${bookingCost} has been processed");
            } catch
            {
                Console.WriteLine("This is an invalid input");
            }
        }

        return true;
    }
}

void displaySuccessfulBookingCreation(Booking booking)
{
    Console.WriteLine("Booking created successfully");
    Console.WriteLine("Booking ID: " + booking.Id);
    Console.WriteLine("Booking Start Date Time: " + booking.StartDateTime);
    Console.WriteLine("Booking End Date Time: " + booking.EndDateTime);
    Console.WriteLine("Booking Cost: " + booking.Cost);
    Console.WriteLine("Booking Paid For: " + booking.PaidFor);
    Console.WriteLine("Booking Pickup Option: " + booking.Pickup.PickupOption);
    Console.WriteLine("Booking Pickup Location: " + booking.Pickup.PickupLocation);
    Console.WriteLine("Booking Drop Off Option: " + booking.DropOff.DropOffOption);
    Console.WriteLine("Booking Drop Off Location: " + booking.DropOff.DropOffLocation);
}

void displayUnsuccessfulBookingCreation()
{
    Console.WriteLine("Booking created unsuccessfully");
}

Booking createNewBooking(int id, DateTime startDateTime, DateTime endDateTime, bool paidFor, Pickup pickup, DropOff dropOff, Vehicle vehicle, Renter renter)
{
    Booking booking = new Booking(id, startDateTime, endDateTime, paidFor, pickup, dropOff, vehicle, renter);

    booking.addRenterBooking();

    return booking;
}

Pickup selectPickupOption()
{
    String pickupOption = "";

    while (pickupOption != "self-service" && pickupOption != "delivery")
    {
        Console.Write("Input your desired pickup option of either self-service or delivery: ");
        pickupOption = Console.ReadLine().ToLower();
    }

    String pickupLocation = "";

    while (pickupLocation == "")
    {
        if (pickupOption == "self-service")
        {
            Console.WriteLine("We will inform you about the pickup location 1 hour before your booking starts");
            pickupLocation = "pending";
        }
        else
        {
            Console.Write("Input your desired delivery location: ");
            pickupLocation = Console.ReadLine();
        }
    }

    return new Pickup(pickupOption, pickupLocation);
}

DropOff selectDropOffOption()
{
    String dropOffOption = "";

    while (dropOffOption != "self-service" && dropOffOption != "delivery")
    {
        Console.Write("Input your desired drop off option of either self-service or delivery: ");
        dropOffOption = Console.ReadLine().ToLower();
    }

    String dropOffLocation = "";

    while (dropOffLocation == "")
    {
        if (dropOffOption == "self-service")
        {
            Console.WriteLine("We will inform you about the drop off location 1 hour before your booking starts");
            dropOffLocation = "pending";
        }
        else
        {
            Console.Write("Input your desired delivery location: ");
            dropOffLocation = Console.ReadLine();
        }
    }

    return new DropOff(dropOffOption, dropOffLocation);
}

void displayInvalidBookingStartDateTime()
{
    Console.WriteLine("This is not a valid booking start date time");
}

void displayInvalidBookingEndDateTime()
{
    Console.WriteLine("This is not a valid booking end date time");
}

DateTime selectStartBookingDateTime(string startDateTimeInput)
{
    Boolean valid = validateDateTime(startDateTimeInput);

    if (valid == true)
    {
        DateTime dateTime = Convert.ToDateTime(startDateTimeInput);
        return dateTime;
    } else
    {
        DateTime dateTime = DateTime.MinValue;
        return dateTime;
    }
}

DateTime selectEndBookingDateTime(string endDateTimeInput)
{
    Boolean valid = validateDateTime(endDateTimeInput);

    if (valid == true)
    {
        DateTime dateTime = Convert.ToDateTime(endDateTimeInput);
        return dateTime;
    }
    else
    {
        DateTime dateTime = DateTime.MinValue;
        return dateTime;
    }
}

Boolean validateDateTime(string dateTime)
{
    try
    {
        DateTime validatedDateTime = Convert.ToDateTime(dateTime);
        return true;
    } catch
    {
        return false;
    }
}

void addBooking(Booking booking)
{
    vehicle.addBooking(booking);
}
