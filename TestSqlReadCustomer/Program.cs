using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlReadCustomer;

namespace TestSqlReadCustomer
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Create a new customer and put it in the SQL Database
            //// Then update the customers name and state
            //// Then delete the customer

            CustomerController cust = new CustomerController();
            ////Customer customer = new Customer("Madtree", "Cincinnati", "OH", true, 5000000, true);
            ////cust.Insert(customer);
            //Customer c = new Customer("Rhinegeist", "Cincinnati", "OH", true, 600000, true);
            //cust.Insert(c);
            //Customer b = new Customer("50 West", "Cincinnati", "OH", true, 400000, true);
            //cust.Insert(b);


            //Customer upCustomer = cust.Get(20);
            //upCustomer.Name = "Changed";

            //cust.Update(upCustomer);


            ////cust.Delete(10);

            List<Customer> customers = cust.SearchByName("e");

            foreach (Customer customer in customers)
            {
                Console.WriteLine($"{customer.Id} | {customer.Name} | {customer.City} | {customer.State} | {customer.IsCorpAccount} | {customer.CreditLimit} | {customer.Active}");

            }
            Console.ReadKey();

        }
    }
}
