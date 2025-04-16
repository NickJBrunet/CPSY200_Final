using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentRental.Data
{
    public class Equipment
    {
        public int equipmentId { get; set; }
        public int categoryId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float dailyRentalCost { get; set; }
        public Equipment() { }
        public Equipment(int equipmentId, int categoryId, string name, string description, float dailyRentalCost)
        {
            this.equipmentId = equipmentId;
            this.categoryId = categoryId;
            this.name = name;
            this.description = description;
            this.dailyRentalCost = dailyRentalCost;
        }
    }
}
