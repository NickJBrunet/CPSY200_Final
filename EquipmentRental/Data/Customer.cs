using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentRental.Data
{
    public class Customer
    {
        public int customerId { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string managerNote { get; set; }

        public Customer() { }
        public Customer(int customerId, string lastName, string firstName, string phoneNumber, string email, string managerNote)
        {
            this.customerId = customerId;
            this.lastName = lastName;
            this.firstName = firstName;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.managerNote = managerNote;
        }
    }
}
