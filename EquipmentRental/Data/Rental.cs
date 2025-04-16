using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentRental.Components.Pages;

namespace EquipmentRental.Data
{
    public class Rental
    {
        public int rentalId {  get; set; }
        public int equipmentId { get; set; }
        public int customerId { get; set; }
        public DateOnly rentalDate { get; set; }
        public DateOnly returnDate { get; set; }

        public Rental() { }
        public Rental(int rentalId, int equipmentId, int customerId, DateOnly rentalDate, DateOnly returnDate)
        {
            this.rentalId = rentalId;
            this.equipmentId = equipmentId;
            this.customerId = customerId;
            this.rentalDate = rentalDate;
            this.returnDate = returnDate;
        }
    }
}
