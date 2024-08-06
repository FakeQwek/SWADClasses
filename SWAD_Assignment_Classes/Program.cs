using SWAD_Assignment_Classes;

List<DateTime> dateTimes = new List<DateTime>();
for (int i = 1; i < 31; i++)
{
    dateTimes.Add(new DateTime(2024, 1, i));
}

List<Booking> bookings = new List<Booking>();
List<Vehicle> vehicles = new List<Vehicle>();
Vehicle vehicle1 = new Vehicle(1, "make", "model", "2022", 200, "SHA1025A", 40, false, dateTimes, bookings);
vehicles.Add(vehicle1);
Renter renter = new Renter();

int vehicleId = 1;

Vehicle vehicle = getVehicle(vehicleId);

List<DateTime> listOfAvailableBookingDateTimes = vehicle.getAvailableBookingDateTimes();

while (listOfAvailableBookingDateTimes.Count != 0)
{
    DateTime startDateTime = DateTime.MinValue;

    while (!listOfAvailableBookingDateTimes.Contains(startDateTime))
    {
        displayAvailableStartDateTimes(dateTimes);

        Console.Write("Input your desired booking start date time: ");
        try
        {
            startDateTime = Convert.ToDateTime(Console.ReadLine());
        } catch
        {
            displayInvalidBookingStartDateTime();
            Console.ReadLine();
        }
    }

    DateTime endDateTime = DateTime.MinValue;

    while (!listOfAvailableBookingDateTimes.Contains(endDateTime))
    {
        displayAvailableEndDateTimes(startDateTime, dateTimes);

        Console.Write("Input your desired booking end date time: ");
        try
        {
            endDateTime = Convert.ToDateTime(Console.ReadLine());
        } catch
        {
            displayInvalidBookingEndDateTime();
            Console.ReadLine();
        }
    }

    Pickup pickup = selectPickupOption();

    DropOff dropOff = selectDropOffOption();

    bool available = vehicle.checkIfDateTimesAvailable(startDateTime, endDateTime);

    if (available)
    {
        Booking booking = createNewBooking(1, startDateTime, endDateTime, false, pickup, dropOff, vehicle);

        vehicle.addBooking(booking);

        vehicle.updateBookingDateTimes(startDateTime, endDateTime);

        double bookingCost = booking.Cost;

        bool paymentSuccess = makePayment(bookingCost);

        if (paymentSuccess)
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
    } else
    {
        displaySelectedBookingDateTimesUnavailable();
        Console.ReadLine();
    }
}

if (listOfAvailableBookingDateTimes.Count == 0)
{
    displayNoAvailableBookingDateTimes();
}



Vehicle getVehicle(int vehicleId)
{
    return vehicles[vehicleId];
}

void displayAvailableStartDateTimes(List<DateTime> dateTimes)
{
    for (int i = 0; i < dateTimes.Count; i++)
    {
        Console.WriteLine(dateTimes[i].ToString("dd/MM/yyyy"));
    }
    Console.WriteLine();
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

bool makePayment(double cost)
{
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
            return true;
        }
    }
    else if (paymentMethod == "credit card")
    {
        Console.Write("Input your credit card number: ");
        Console.ReadLine();
        Console.Write("Input your credit card's CVV: ");
        Console.ReadLine();
        Console.Write("Input your credit card's expiration date: ");
        DateTime expirationDate = Convert.ToDateTime(Console.ReadLine());
        if (expirationDate < DateTime.Now)
        {
            return false;
        } else
        {
            return true;
        }
    }
    else
    {
        Console.Write("Input the digital wallet you will be using: ");
        Console.ReadLine();
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

Booking createNewBooking(int id, DateTime startDateTime, DateTime endDateTime, bool paidFor, Pickup pickup, DropOff dropOff, Vehicle vehicle)
{
    return new Booking(id, startDateTime, endDateTime, paidFor, pickup, dropOff, vehicle, renter);
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
            Console.Write("We will inform you about the pickup location 1 hour before your booking starts");
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
            Console.Write("We will inform you about the drop off location 1 hour before your booking starts");
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
