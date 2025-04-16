using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using static System.Reflection.Metadata.BlobBuilder;

namespace EquipmentRental.Data
{
    public class DatabaseManager
    {
        static MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            UserID = "root",
            Password = "password",
            Database = "equipment_rental",
        };
        protected static MySqlConnection connection = new MySqlConnection(builder.ConnectionString);

        // create the database tables if they do not exis
        public static void InitializeDatabase()
        {
            connection.Open();

            // create the category table
            var sql = @"CREATE TABLE IF NOT EXISTS categories (
                Category_Id INT AUTO_INCREMENT PRIMARY KEY,
                Name VARCHAR(255) NOT NULL
            )";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            // create the equipment table
            sql = @"CREATE TABLE IF NOT EXISTS equipment (
                Equipment_Id INT AUTO_INCREMENT PRIMARY KEY,
                Category_Id INT NOT NULL,
                Name VARCHAR(255) NOT NULL,
                Description VARCHAR(255) NOT NULL,
                Daily_Rental_Cost FLOAT NOT NULL
            ) AUTO_INCREMENT = 100";

            cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            // create the customer table
            sql = @"CREATE TABLE IF NOT EXISTS customers (
                Customer_Id INT AUTO_INCREMENT PRIMARY KEY,
                Last_Name VARCHAR(50) NOT NULL,
                First_Name VARCHAR(50) NOT NULL,
                Phone_Number VARCHAR(50) NOT NULL,
                Email VARCHAR(50) NOT NULL,
                Manager_Note VARCHAR(255) NOT NULL
            ) AUTO_INCREMENT = 1000";

            cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            // create the equipment rental table
            sql = @"CREATE TABLE IF NOT EXISTS equipment_rentals (
                Equipment_Rental_Id INT AUTO_INCREMENT PRIMARY KEY,
                Equipment_Id INT NOT NULL,
                Customer_Id INT NOT NULL,
                Rental_Date DATE NOT NULL,
                Return_Date DATE NOT NULL
            ) AUTO_INCREMENT = 1000";

            cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public static void AddCustomer(Customer customer)
        {
            connection.Open();

            // insert new customer into table
            var sql = $"INSERT INTO customers (Last_Name,First_Name,Phone_Number,Email,Manager_Note) VALUES ('{customer.lastName}','{customer.firstName}','{customer.phoneNumber}','{customer.email}','{customer.managerNote}');";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
        public static List<Customer> GetCustomers(string searchType = "", string searchInput = "")
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                connection.Open();

                var sql = $"SELECT * FROM customers;";

                MySqlCommand cmd = new MySqlCommand(sql, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.customerId = reader.GetInt32(0);
                        customer.lastName = reader.GetString(1);
                        customer.firstName = reader.GetString(2);
                        customer.phoneNumber = reader.GetString(3);
                        customer.email = reader.GetString(4);
                        customer.managerNote = reader.GetString(5);
                        if (searchType == "")
                        {
                            customers.Add(customer);
                        }
                        else if (searchType == "Customer Id" && customer.customerId == Int32.Parse(searchInput))
                        {
                            customers.Add(customer);
                        }
                        else if (searchType == "Last Name" && customer.lastName == searchInput)
                        {
                            customers.Add(customer);
                        }
                        else if (searchType == "First Name" && customer.firstName == searchInput)
                        {
                            customers.Add(customer);
                        }
                    }
                }

                connection.Close();

                return customers;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw new Exception();
            }
        }

        public static void UpdateCustomerNote(Customer customer)
        {
            connection.Open();

            // insert new customer into table
            var sql = $"UPDATE customers SET Manager_Note = '{customer.managerNote}' WHERE Customer_ID = {customer.customerId};";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public static List<Category> GetCategories(int categoryId = 0)
        {
            List<Category> categories = new List<Category>();
            connection.Open();

            var sql = $"SELECT * FROM categories;";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Category category = new Category();
                    category.categoryId = reader.GetInt32(0);
                    category.name = reader.GetString(1);

                    if (categoryId == 0 || (categoryId != 0 && categoryId == category.categoryId))
                    {
                        categories.Add(category);
                    }
                }
            }

            connection.Close();

            return categories;
        }

        // EQUIPMENT DATABASE SECTION
        public static void AddEquipment(Equipment equipment)
        {
            connection.Open();

            // insert new customer into table
            var sql = $"INSERT INTO equipment (Category_Id,Name,Description,Daily_Rental_Cost) VALUES ('{equipment.categoryId}','{equipment.name}','{equipment.description}',{equipment.dailyRentalCost});";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
        public static void RemoveEquipment(Equipment equipment)
        {
            connection.Open();

            // insert new customer into table
            var sql = $"DELETE FROM equipment WHERE Equipment_Id = {equipment.equipmentId};";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
        public static List<Equipment> GetEquipment(string searchType = "", string searchInput = "")
        {
            List<Equipment> equipment = new List<Equipment>();
            try
            {
                connection.Open();

                var sql = $"SELECT * FROM equipment;";

                MySqlCommand cmd = new MySqlCommand(sql, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Equipment newEquipment = new Equipment();
                        newEquipment.equipmentId = reader.GetInt32(0);
                        newEquipment.categoryId = reader.GetInt32(1);
                        newEquipment.name = reader.GetString(2);
                        newEquipment.description = reader.GetString(3);
                        newEquipment.dailyRentalCost = reader.GetFloat(4);
                        if (searchType == "")
                        {
                            equipment.Add(newEquipment);
                        }
                        else if (searchType == "Equipment Id" && newEquipment.equipmentId == Int32.Parse(searchInput))
                        {
                            equipment.Add(newEquipment);
                        }
                        else if (searchType == "Category Id" && newEquipment.categoryId == Int32.Parse(searchInput))
                        {
                            equipment.Add(newEquipment);
                        }
                    }
                }
                connection.Close();
                return equipment;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw new Exception();
            }
        }

        // RENTAL DATABASE SECTION
        public static void AddRental(Rental rental)
        {
            connection.Open();

            // insert new customer into table
            var sql = $"INSERT INTO equipment_rentals (Equipment_Id,Customer_Id,Rental_Date,Return_Date) VALUES ('{rental.equipmentId}','{rental.customerId}',CAST('{rental.rentalDate}' as date),CAST('{rental.returnDate}' as date));";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            connection.Close();
        }
        public static List<Rental> GetRentals(string searchType = "", string searchInput = "")
        {
            List<Rental> rentals = new List<Rental>();
            try
            {
                connection.Open();

                var sql = $"SELECT * FROM equipment_rentals;";

                MySqlCommand cmd = new MySqlCommand(sql, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rental newRental = new Rental();
                        newRental.rentalId = reader.GetInt32(0);
                        newRental.equipmentId = reader.GetInt32(1);
                        newRental.customerId = reader.GetInt32(2);
                        newRental.rentalDate = reader.GetDateOnly(3);
                        newRental.returnDate = reader.GetDateOnly(4);
                        if (searchType == "")
                        {
                            rentals.Add(newRental);
                        }
                        else if (searchType == "Equipment Id" && newRental.equipmentId == Int32.Parse(searchInput))
                        {
                            rentals.Add(newRental);
                        }
                        else if (searchType == "Customer Id" && newRental.customerId == Int32.Parse(searchInput))
                        {
                            rentals.Add(newRental);
                        }
                    }
                }

                connection.Close();

                return rentals;
            }
            catch (Exception ex) 
            {
                connection.Close();
                throw new Exception();
            }
        }
        public static int GetNextId(string column, string table)
        {
            int count = 0;
            connection.Open();

            var sql = $"SELECT MAX({column}) FROM {table};";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    try
                    {
                        count = reader.GetInt32(0);
                    }
                    catch
                    {
                        count = 0;
                    }
                }
            }

            connection.Close();
            return count + 1;
        }
    }
}
