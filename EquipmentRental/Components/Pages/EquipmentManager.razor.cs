using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentRental.Data;

namespace EquipmentRental.Components.Pages
{
    public partial class EquipmentManager
    {

        public string categoryId = "";
        public string name = "";
        public string description = "";
        public string dailyRentalCost = "";
        public string searchInputType = "";
        public string searchInputField = "";
        public List<Equipment> equipments { get; set; }
        public List<Category> categories { get; set; }

        public EquipmentManager()
        {
            DatabaseManager.InitializeDatabase();
            LoadEquipment();
            LoadCategories();
        }
        public Equipment GetEquipment(int id)
        {
            Equipment equip = new Equipment();
            foreach (Equipment equipment in equipments)

                if (equipment.equipmentId == id)
                {
                    return equipment;
                }
            equip.name = "Not Found.";
            return equip; // Equipment instance will be empty if id not found
        }
        public void RegisterEquipment(string categoryId, string name, string descripton, string dailyRentalCost)
        {
            try 
            {
                Equipment newEquipment = new Equipment(DatabaseManager.GetNextId("Equipment_Id", "equipment"), Int32.Parse(categoryId), name, descripton, float.Parse(dailyRentalCost));
                DatabaseManager.AddEquipment(newEquipment);
                LoadEquipment();
                ModalService.ShowMessage("Success!", "You have inputed new data.", "limegreen");
            }
            catch (Exception ex)
            {
                ModalService.ShowMessage("Error!", "One or more input fields is invalid.", "red");
            }
        }

        public void RemoveEquipment(Equipment equipment)
        {
            equipments.Remove(equipment);
            DatabaseManager.RemoveEquipment(equipment);
        }

        public void SearchByFilter()
        {
            try
            {
                if (searchInputType == "Equipment Id" || searchInputType == "Category Id")
                {
                    equipments = DatabaseManager.GetEquipment(searchInputType, searchInputField);
                }
                else
                {
                    equipments = DatabaseManager.GetEquipment();
                }
            }
            catch (Exception ex)
            {
                if (searchInputField != "")
                {
                    ModalService.ShowMessage("Error!", "Input field was an invalid type.", "red");
                }
                equipments = DatabaseManager.GetEquipment();
            }
        }
        public void LoadEquipment()
        {
            equipments = DatabaseManager.GetEquipment();
        }
        public void LoadCategories()
        {
            categories = DatabaseManager.GetCategories();
        }
    }
}
