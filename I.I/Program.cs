using System;
using System.Collections.Generic;

bool isDone = false;
List<Customer> customers = new();
List<Admin> admins = new();
while (!isDone)
{
    Console.WriteLine("Add a new Person? (Press Enter. Close Program to Exit.)");
     var key = Console.ReadKey();
     if (key.Key == ConsoleKey.Enter)
     {
         Console.WriteLine("Admin?");
         var yn  = Console.ReadLine();
         if (yn == "y")
         {
             var tempAdm = new Admin();
             Console.Write("Input Admin profile type: ");
             tempAdm.profile = Console.ReadLine() ?? "default";
             Console.WriteLine("Is this the correct phone number? ");
             Console.WriteLine(tempAdm.telephone_number.ToString());
             yn = Console.ReadLine();
             if (yn != "y")
             {
                 Console.Write("Please add the phone number:");
                 tempAdm.telephone_number = Convert.ToInt32(Console.ReadLine());
             }
             admins.Add(tempAdm);
         }
         else
         {
             var tempCustomer = new Customer();
             Console.Write("Add Customer name: ");
             tempCustomer.name = Console.ReadLine() ?? "new_customer";
             Console.Write("Add Surname: ");
             tempCustomer.surname = Console.ReadLine() ?? "new_customer";
             Console.Write("Level? ");
             tempCustomer.level = Convert.ToInt32(Console.ReadLine());
             Console.WriteLine("Is this the correct phone number? ");
             Console.WriteLine(tempCustomer.telephone_number.ToString());
             yn = Console.ReadLine();
             if (yn != "y")
             {
                 Console.Write("Please add the phone number:");
                 tempCustomer.telephone_number = Convert.ToInt32(Console.ReadLine());
             }
             Console.WriteLine("Add orders to Customer? ");
             yn = Console.ReadLine();
             if (yn != "n")
             {
                 Console.WriteLine("How many? ");
                 int i = Convert.ToInt32(Console.ReadLine());
                 for (int j = 1; j < i+1; j++)
                 {
                     Console.Write("Add date (Empty for current): ");
                     var dateString = Console.ReadLine() ?? String.Empty;
                     bool is_c = false;
                     Console.Write("Is order completed? (y/n)");
                     if (Console.ReadLine() == "y")
                         is_c = true;
                     Console.WriteLine("Add address: ");
                     var addString = Console.ReadLine() ?? String.Empty;
                     
                     Console.Write("Add amount: ");
                     var payment = Convert.ToDouble(Console.ReadLine());
                     Console.WriteLine("Choose payment type");
                     Console.WriteLine("1. Cash");
                     Console.WriteLine("2. Credit Card");
                     Console.WriteLine("3. Paypal");
                     Console.Write("Payment type: ");
                     var paymentType = (PaymentType) Enum.ToObject(typeof(PaymentType),Convert.ToInt32(Console.ReadLine()));
                     
                     var tempOrderDel = new Delivery(addString);
                     var tempOrderPayment = new Payment(payment,paymentType);
                     tempCustomer.orders.Add(new Order(dateString,is_c,tempOrderPayment,tempOrderDel));
                 }
                 
             }
             customers.Add(tempCustomer);
             Console.WriteLine("Done? Press y and Enter");
             if (Console.ReadLine() == "y")
             {
                 isDone = true;
             }
         }
     }
}

Console.WriteLine("Output Collections? (y/n)");
if (Console.ReadLine() == "y")
{
    if (admins.Count >0)
    {
        Console.WriteLine("---Admins---");
        foreach (var admin in admins)
        {
            Console.WriteLine("Username: {0}, \n\tPhone Number: {1}, \n\tProfile type: {2}",admin.username,admin.telephone_number,admin.profile);
        }
    }

    if (customers.Count>0)
    {
        Console.WriteLine("--- Customers ---");
        foreach (var customer in customers)
        {
            Console.WriteLine("Name: {0}, \n\tSurname: {1}, \n\tUsername: {2}, \n\tAccount Level: {3}, \n\tTelephone Number : {4}",customer.name,customer.surname,customer.username,customer.level,customer.telephone_number);
            if (customer.orders.Count >0)
            {
                foreach (var order in customer.orders)
                {
                    Console.WriteLine("\tOrders: ");
                    Console.WriteLine(
                        "\t\tOrder Date: {0}, \n\t\tDelivery: {1}, \n\t\tPayment Amount: {2}, \n\t\tPayment Type: {3}, \n\t\tCompleted: {4}",
                        order.date, order.order_delivery.adress, order.order_payment.Amount, order.order_payment.PType,
                        order.is_completed);
                };
            }
        }
    }
    
}

internal class Person
{
    public int telephone_number  = GenerateTelephoneNumber();
    
    static int GenerateTelephoneNumber()                //Niks Slinks. Sorri.
    {
        Random rand = new();
        return rand.Next(00000000, 99999999);
    }
}

internal class User : Person
{
    public User()
    {
        Console.Write("Username: ");
        username = Console.ReadLine() ?? "new_user";
        Console.Write("Password (type slowly) grrr: ");
        this.password = Console.ReadLine() ?? "default_password";
    }
    public string username { get; }
    protected string password { get;}
}

internal class Customer : User
{
    public string name;
    public string surname;
    public int level { get; set; }
    public List<Order> orders = new();
}

internal class Admin : User
{
    public string profile;
}

class Order
{
    public Order(string date,bool isCompleted, Payment payment, Delivery delivery)
    {
        if (date is "")
            date = System.DateTime.Today.ToLongDateString();
        this.date = date;
        this.is_completed = isCompleted;
        this.order_payment = payment;
        this.order_delivery = delivery;
    }
    public readonly string date;
    public readonly bool is_completed;
    public readonly Payment order_payment;
    public readonly Delivery order_delivery;
}

class Payment
{
    public Payment(double amount, PaymentType paymentType)
    {
        this.Amount = amount;
        this.PType = paymentType;
    }
    public double Amount;
    public PaymentType PType;
}

class Delivery
{
    public string adress;

    public Delivery(string adress)
    {
        this.adress = adress;
    }
}

enum PaymentType
{
    Cash,
    Credit,
    PayPal
}