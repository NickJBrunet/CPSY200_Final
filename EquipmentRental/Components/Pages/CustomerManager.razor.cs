using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentRental.Data;

namespace EquipmentRental.Components.Pages
{
    partial class CustomerManager
    {
        public string customerId = "";
        public string lastName = "";
        public string firstName = "";
        public string phoneNumber = "";
        public string email = "";
        public string newManagerNote = "";
        public string searchInputType = "";
        public string searchInputField = "";
        public List<Customer> customers { get; set; }

        public CustomerManager()
        {
            DatabaseManager.InitializeDatabase();
            LoadCustomers();
        }

        public Customer GetCustomer(int id)
        {
            foreach (Customer customer in customers)

                if (customer.customerId == id)
                {
                    return customer;
                }
            return new Customer(); // will be null if id not found
        }

        public void RegisterCustomer(string lastName, string firstName, string phoneNumber, string email)
        {
            try
            {
                if (lastName == "" || firstName == "" || phoneNumber == "" || email == "") { throw new Exception(); }
                Customer newCustomer = new Customer(DatabaseManager.GetNextId("Customer_Id", "customers"), lastName, firstName, phoneNumber, email, "");
                DatabaseManager.AddCustomer(newCustomer);
                LoadCustomers();
                ModalService.ShowMessage("Success!", "You have inputed new data.", "limegreen");
            }
            catch (Exception ex) 
            {
                ModalService.ShowMessage("Error!", "One or more input fields is invalid.", "red");
            }
        }

        public void SearchByFilter()
        {
            try
            {
                if (searchInputType == "Customer Id" || searchInputType == "Last Name" || searchInputType == "First Name")
                {
                    if (searchInputField == "") { throw new Exception(); }
                    customers = DatabaseManager.GetCustomers(searchInputType, searchInputField);
                }
                else
                {
                    customers = DatabaseManager.GetCustomers();
                }
            }
            catch (Exception ex)
            {
                if (searchInputField != "")
                {
                    ModalService.ShowMessage("Error!", "Input field was an invalid type.", "red");
                }
                customers = DatabaseManager.GetCustomers();
            }
        }

        public void LoadCustomers()
        {
            customers = DatabaseManager.GetCustomers();
        }

        public void EditManagerNote(Customer customer)
        {
            if (!string.IsNullOrEmpty(customer.managerNote))
            {
                customer.managerNote = "";
            }
            else
            {
                customer.managerNote = newManagerNote;
                newManagerNote = "";
            }
            DatabaseManager.UpdateCustomerNote(customer);
        }
    }
}
