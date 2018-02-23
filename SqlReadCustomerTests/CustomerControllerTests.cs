using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlReadCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SqlReadCustomer.Tests
{
    [TestClass()]
    public class CustomerControllerTests
    {
        CustomerController cc;
        Customer customer;
        //Customer updatedCustomer;
        [TestInitialize]
        public void TestInit()
        {
            cc = new CustomerController(); // just initialized an instance of the CustomerController class that is not filled with any data.
            customer = new Customer("TestName", "TestCity", "TS", true, 500000, true); //this is an instance of Customer, my test case customer.
           // updatedCustomer = new Customer("UpdateName", "UpdateCity", "US", true, 500000, true); //this is my instance of Customer that I intend to use for testing the Update method.
        }

        //[Ignore]
        [TestMethod()]
        public void InsertTest() // this is in working condition
        {
          cc.Insert(customer);
          List<Customer> customers = cc.SearchByName("TestName");
          Customer testCustomer = customers[0];            
          Assert.AreEqual(testCustomer.Name, customer.Name);
        }

        //[Ignore]
        [TestCategory("Update")] //This is in working condition and feeds off of the Insert Method
        [TestMethod()]
        public void UpdateTest()
        {
            List<Customer> customers = cc.SearchByName("TestName");
            Customer testCustomer = customers[0];
            //int cId = testCustomer.Id;
            testCustomer.Name = "UpdatedName";
            cc.Update(testCustomer);
            Customer updatedCustomer = cc.Get(testCustomer.Id);
            Assert.AreEqual(updatedCustomer.Name, testCustomer.Name);
            

            bool actual = cc.Update(testCustomer);
            Assert.IsTrue(true == actual);
        }
        //TODO Finish testing these methods and use deletetest as the [Cleanup]
        [Ignore]
        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod()]
        public void SearchByCreditLimitRangeTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod()]
        public void GetTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod()]
        public void GetTest1()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod()]
        public void SearchByNameTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod()]
        public void ListTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestCleanup()]
        public void CleanUp()
        {

        }

    }
}