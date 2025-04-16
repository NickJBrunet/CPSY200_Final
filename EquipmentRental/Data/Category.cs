using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentRental.Data
{
    public class Category
    {
        public int categoryId { get; set; }
        public string name { get; set; }

        public Category() { }
        public Category(int categoryId, string name)
        {
            this.categoryId = categoryId;
            this.name = name;
        }
    }
}
