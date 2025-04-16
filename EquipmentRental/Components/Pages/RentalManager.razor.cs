using System.Diagnostics;
using EquipmentRental.Data;

namespace EquipmentRental.Components.Pages
{
    partial class RentalManager
    {
        public string equipmentId = "";
        public string customerId = "";
        public string rentalDate = "";
        public string returnDate = "";
        public string searchInputType = "";
        public string searchInputField = "";
        public List<Rental> rentals = new List<Rental>();
        public EquipmentManager equipmentManager = new EquipmentManager();
        public CustomerManager customerManager = new CustomerManager();

        public RentalManager()
        {
            DatabaseManager.InitializeDatabase();
            LoadRentals();
        }
        public void RegisterRental(string equipmentId, string customerId, string rentalDate, string returnDate)
        {
            try
            {
                Rental rental = new Rental(DatabaseManager.GetNextId("Equipment_Rental_Id", "equipment_rentals"), int.Parse(equipmentId), int.Parse(customerId), DateOnly.Parse(rentalDate), DateOnly.Parse(returnDate));
                DatabaseManager.AddRental(rental);
                LoadRentals();
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
                if (searchInputType == "Equipment Id" || searchInputType == "Customer Id")
                {
                    rentals = DatabaseManager.GetRentals(searchInputType, searchInputField);
                }
                else
                {
                    rentals = DatabaseManager.GetRentals();
                }
            }
            catch (Exception ex)
            {
                if (searchInputField != "")
                {
                    ModalService.ShowMessage("Error!", "Input field was an invalid type.", "red");
                }
                rentals = DatabaseManager.GetRentals();
            }
        }

        public void LoadRentals()
        {
            rentals = DatabaseManager.GetRentals();
        }
        public float GetTotalCost(Rental rental)
        {
            Equipment equipment = equipmentManager.GetEquipment(rental.equipmentId);
            return (rental.returnDate.DayNumber - rental.rentalDate.DayNumber) * equipment.dailyRentalCost;
        }
    }
}
