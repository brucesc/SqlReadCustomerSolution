using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Data.SqlClient; //added this

namespace SqlReadCustomer
{
    public class CustomerController
    {
        public bool Insert(Customer customer)
        {
            string connStr = @"server=DESKTOP-7CC7HCF\SQLEXPRESS;database=SqlTutorial;Trusted_Connection=true";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if (conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("The connection didn't open.");
                return false;
            }

            string sql = "insert into customer (Name, City, State, IsCorpAcct, CreditLimit, Active) values (@Name, @City, @State, @IsCorpAcct, @CreditLimit, @Active)";
            SqlCommand cmd = new SqlCommand(sql, conn);
           
            cmd.Parameters.Add(new SqlParameter("@Name", customer.Name));
            cmd.Parameters.Add(new SqlParameter("@City", customer.City));
            cmd.Parameters.Add(new SqlParameter("@State", customer.State));
            cmd.Parameters.Add(new SqlParameter("@IsCorpAcct", customer.IsCorpAccount));
            cmd.Parameters.Add(new SqlParameter("@CreditLimit", customer.CreditLimit));
            cmd.Parameters.Add(new SqlParameter("@Active", customer.Active));

            int recsAffected = cmd.ExecuteNonQuery();
            conn.Close();

            if (recsAffected != 1)
            {
                Console.WriteLine("Insert Failed. Blame it on Gred");
                return false;
            }


            return true;



        }

        public bool Update(Customer customer)
        {
            string connStr = @"server=DESKTOP-7CC7HCF\SQLEXPRESS;database=SqlTutorial;Trusted_Connection=true";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if (conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("The connection didn't open.");
                return false;
            }

            string sql = "update customer set Name = @Name, City = @City, State = @State, IsCorpAcct = @IsCorpAcct, CreditLimit = @CreditLimit, Active = @Active where id = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add(new SqlParameter("@Id", customer.Id));
            cmd.Parameters.Add(new SqlParameter("@Name", customer.Name));
            cmd.Parameters.Add(new SqlParameter("@City", customer.City));
            cmd.Parameters.Add(new SqlParameter("@State", customer.State));
            cmd.Parameters.Add(new SqlParameter("@IsCorpAcct", customer.IsCorpAccount));
            cmd.Parameters.Add(new SqlParameter("@CreditLimit", customer.CreditLimit));
            cmd.Parameters.Add(new SqlParameter("@Active", customer.Active));

            int recsAffected = cmd.ExecuteNonQuery();
            conn.Close();

            if (recsAffected != 1)
            {
                Console.WriteLine("Update Failed. Blame it on Gred.");
                return false;
            }


            return true;



        }

        public bool Delete(int Id)
        {
            string connStr = @"server=DESKTOP-7CC7HCF\SQLEXPRESS;database=SqlTutorial;Trusted_Connection=true";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if (conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("The connection didn't open.");
                return false;
            }

            string sql = "delete from Customer where Id = @Id";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add(new SqlParameter("@Id", Id));
            
            int recsAffected = cmd.ExecuteNonQuery();
            conn.Close();

            if (recsAffected != 1)
            {
                Console.WriteLine("Delete Failed. Please, double check your Customer Id.");
                return false;
            }


            return true;



        }

        public List<Customer> SearchByCreditLimitRange(int lower, int upper)
        {
            string connStr = @"server=DESKTOP-7CC7HCF\SQLEXPRESS;database=SqlTutorial;Trusted_Connection=true";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            if (conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("The connection didn't open.");
                return null;
            }

            string sql = "select * from customer where creditlimit between @lowercl and @uppercl order by CreditLimit desc";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlParameter pLower = new SqlParameter("@lowercl", lower);
            SqlParameter pUpper = new SqlParameter("@uppercl", upper);

            cmd.Parameters.Add(pLower);
            cmd.Parameters.Add(pUpper);




            SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("Result has no row.");
                return null;
            }

            List<Customer> customers = new List<Customer>();


            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                string name = reader.GetString(reader.GetOrdinal("Name"));
                string city = reader.GetString(reader.GetOrdinal("City"));
                string state = reader.GetString(reader.GetOrdinal("State"));
                bool isCorpAcct = reader.GetBoolean(reader.GetOrdinal("IsCorpAcct"));
                int creditLimit = reader.GetInt32(reader.GetOrdinal("CreditLimit"));
                bool active = reader.GetBoolean(reader.GetOrdinal("Active"));

                Customer customer = new Customer(id, name, city, state, isCorpAcct, creditLimit, active);
                //customer.Id = id;
                //customer.Name = name;
                //customer.City = city;
                //customer.State = state;
                //customer.IsCorpAccount = isCorpAcct;
                //customer.CreditLimit = creditLimit;
                //customer.Active = active;

                customers.Add(customer);
            }
            conn.Close();
            return customers;
        }
        
        public List<Customer> Get(string stateAbrev)
        {
            string connStr = @"server=DESKTOP-7CC7HCF\SQLEXPRESS;database=SqlTutorial;Trusted_Connection=true";

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if (conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("The connection didn't open.");
                return null;
            }

            string sql = "Select * from Customer where [State] = @State";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlParameter pState = new SqlParameter("@State", stateAbrev); // same thing: pState.ParameterName = "@State"; pState.Value = stateAbrev;
            cmd.Parameters.Add(pState);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                Console.WriteLine($"Customer {stateAbrev} not found.");
                return null;
            }

            List<Customer> customers = new List<Customer>();
            
            while (reader.Read())
            {

                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                string name = reader.GetString(reader.GetOrdinal("Name"));
                string city = reader.GetString(reader.GetOrdinal("City"));
                string state = reader.GetString(reader.GetOrdinal("State"));
                bool isCorpAcct = reader.GetBoolean(reader.GetOrdinal("IsCorpAcct"));
                int creditLimit = reader.GetInt32(reader.GetOrdinal("CreditLimit"));
                bool active = reader.GetBoolean(reader.GetOrdinal("Active"));

                Customer customer = new Customer(id, name, city, state, isCorpAcct, creditLimit, active);
                //customer.Id = id;
                //customer.Name = name;
                //customer.City = city;
                //customer.State = state;
                //customer.IsCorpAccount = isCorpAcct;
                //customer.CreditLimit = creditLimit;
                //customer.Active = active;

                customers.Add(customer);

            }
            conn.Close();
            return customers;
        }
        
        public Customer Get(int CustomerId)
        {
            string connStr = @"server=DESKTOP-7CC7HCF\SQLEXPRESS;database=SqlTutorial;Trusted_Connection=true"; 
            
            SqlConnection conn = new SqlConnection(connStr); 
            conn.Open(); 
            if (conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("The connection didn't open.");
                return null; 
            }
           
            string sql = "select * from customer where Id = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.Value = CustomerId;
            cmd.Parameters.Add(pId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                Console.WriteLine($"Customer {CustomerId} not found.");
                return null;
            }
            reader.Read();
            
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                string name = reader.GetString(reader.GetOrdinal("Name"));
                string city = reader.GetString(reader.GetOrdinal("City"));
                string state = reader.GetString(reader.GetOrdinal("State"));
                bool isCorpAcct = reader.GetBoolean(reader.GetOrdinal("IsCorpAcct"));
                int creditLimit = reader.GetInt32(reader.GetOrdinal("CreditLimit"));
                bool active = reader.GetBoolean(reader.GetOrdinal("Active"));

                Customer customer = new Customer(id, name, city, state, isCorpAcct, creditLimit, active); // Constructor in Customer class would be helpful here
                //customer.Id = id;
                //customer.Name = name;
                //customer.City = city;
                //customer.State = state;
                //customer.IsCorpAccount = isCorpAcct;
                //customer.CreditLimit = creditLimit;
                //customer.Active = active;

            conn.Close();

                return customer;


            
        }        

        public List<Customer> SearchByName(string custName)
        {
            string connStr = @"server=DESKTOP-7CC7HCF\SQLEXPRESS;database=SqlTutorial;Trusted_Connection=true";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            if (conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("The connection didn't open.");
                return null;
            }

            string sql = "select * from Customer where Name like '%'+@search+'%' order by name";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlParameter pName = new SqlParameter("@search", custName);

            cmd.Parameters.Add(pName);




            SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows) //if there aren't any rows to iterate through, then stop the search
            {
                Console.WriteLine("Result has no row.");
                return null;
            }

            List<Customer> customers = new List<Customer>();


            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                string name = reader.GetString(reader.GetOrdinal("Name"));
                string city = reader.GetString(reader.GetOrdinal("City"));
                string state = reader.GetString(reader.GetOrdinal("State"));
                bool isCorpAcct = reader.GetBoolean(reader.GetOrdinal("IsCorpAcct"));
                int creditLimit = reader.GetInt32(reader.GetOrdinal("CreditLimit"));
                bool active = reader.GetBoolean(reader.GetOrdinal("Active"));

                Customer customer = new Customer(id, name, city, state, isCorpAcct, creditLimit, active);
                //customer.Id = id;
                //customer.Name = name;
                //customer.City = city;
                //customer.State = state;
                //customer.IsCorpAccount = isCorpAcct;
                //customer.CreditLimit = creditLimit;
                //customer.Active = active;

                customers.Add(customer);
            }
            conn.Close();
            return customers;
        }

        // create a method to list all the customers from SSMS
        public List<Customer> List() // List<Customer> is a type and can only take customer things. List is like an array but it is dynamic
        {
            // Must first establish a connection to SQL. Need what server is hosting the database and what database is holding the instance you want
            string connStr = @"server=DESKTOP-7CC7HCF\SQLEXPRESS;database=SqlTutorial;Trusted_Connection=true"; // server= This can be found in SSMS at the master database level; database= Name of the database; Trusted_Connection= can only use this when accessing a database on your local machine
            // conn is a convention
            SqlConnection conn = new SqlConnection(connStr); // New instance of SqlConnection (don't forget using statement System.Data.SqlClient;) then pass in the connection string (previous line)
            conn.Open(); // Method to open an sql connection
            if (conn.State != System.Data.ConnectionState.Open) // if statement to test for a connection made. the .State
            {
                Console.WriteLine("The connection didn't open.");
                return null; // because we cant return List<Customer> if it returns null then we know something went wrong
            }
            //Console.WriteLine("Connection open successful!");
            string sql = "select * from customer"; // run your sql statements in SSMS to verify they work first. 
            SqlCommand cmd = new SqlCommand(sql, conn); // SqlCommand allows us to make sql commands. instance of SqlCommand (cmd) with parameters filled in. sql is our string select statement. conn is the SqlConnection
            SqlDataReader reader = cmd.ExecuteReader(); // DataReader step. Called the execute reader method. returns a SqlDataReader object
            if (!reader.HasRows) // If this has rows move past, if not display this message
            {
                Console.WriteLine("Result has no row.");
                return null;
            }

            List<Customer> customers = new List<Customer>(); // Generic list of customer things we are going to fill


            while(reader.Read()) // reader points to the first row
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));                
                string name = reader.GetString(reader.GetOrdinal("Name"));
                string city = reader.GetString(reader.GetOrdinal("City"));
                string state = reader.GetString(reader.GetOrdinal("State"));
                bool isCorpAcct = reader.GetBoolean(reader.GetOrdinal("IsCorpAcct"));
                int creditLimit = reader.GetInt32(reader.GetOrdinal("CreditLimit"));
                bool active = reader.GetBoolean(reader.GetOrdinal("Active")); // pull all these out and stuff them into local variables (below)

                Customer customer = new Customer(id, name, city, state, isCorpAcct, creditLimit, active); // take those pulled data and put them into and instance of customer
                //customer.Id = id;
                //customer.Name = name;
                //customer.City = city;
                //customer.State = state;
                //customer.IsCorpAccount = isCorpAcct;
                //customer.CreditLimit = creditLimit;
                //customer.Active = active;

                customers.Add(customer); // take the newly filled instance of Customer customer and stuff it into the List<Customer> customers


                //Console.WriteLine($"Name is {name}");
            }


            conn.Close(); //ALWAYS CLOSE WHEN YOU ARE DONE WITH THE CONNECTION!!!
            return customers;
        }
    }
}
