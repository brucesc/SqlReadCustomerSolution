using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlReadCustomer
{
   public class Customer
    {

        public Customer(int id, string name, string city, string state, bool isCorpAccount, int creditLimit, bool active)
        {
            Id = id;
            Name = name;
            City = city;
            State = state;
            IsCorpAccount = isCorpAccount;
            CreditLimit = creditLimit;
            Active = active;

        }
        public Customer(string name, string city, string state, bool isCorpAccount, int creditLimit, bool active)
        {
            Name = name;
            City = city;
            State = state;
            IsCorpAccount = isCorpAccount;
            CreditLimit = creditLimit;
            Active = active;

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool IsCorpAccount { get; set; }
        public int CreditLimit { get; set; }
        public bool Active { get; set; }

    }
}
