using CarListApp.Maui.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.Services
{
    public class CarService
    {
        SQLiteConnection conn;
        string _dbPath;
        public string StatusMessage;
        int result = 0;

        public CarService(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (conn != null) return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Car>();
        }
        public List<Car> GetCars()
        {
            try
            {
                Init();
                return conn.Table<Car>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retreive data.";
            }

            return new List<Car>();
        }

        public Car GetCars(int id)
        {
            try
            {
                Init();
                return conn.Table<Car>().FirstOrDefault(x => x.Id == id);

            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retreive data.";
            }

            return new Car();
        }

        public int AddCar(Car car)
        {
            try
            {
                Init();
                if(car == null)
                {
                    throw new Exception("Invalid car record.");
                }
                result = conn.Insert(car);
                StatusMessage = result == 0 ? "Failed adding data." : "Successfully added";
                return result;
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed adding data.";
            }
            return 0;
        }

        public int DeleteCar(int id)
        {
            try
            {
                Init();
                //result = conn.Delete(id);
                result = conn.Table<Car>().Delete(x=>x.Id == id);
                StatusMessage = result == 0 ? "Failed deleting data." : "Successfully deleted";
                return result;
            }
            catch(Exception ex)
            {
                StatusMessage = "Failed deleting data.";
            }
            return 0;
        }
    }
}
