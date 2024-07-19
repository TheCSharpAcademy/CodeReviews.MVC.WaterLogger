using System.Data;

namespace WaterLogger.samggannon.Data
{
    public class DataAccess
    {
        private readonly DatabaseAccess _dbConnection;
        private readonly string _connectionString;

        public DataAccess()
        {
        }

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
            _dbConnection = new DatabaseAccess(connectionString);
        }

        public void InsertDrinkingWaterRecord(string date, int quantity)
        {
            string sql = $"INSERT INTO drinking_water(date, quantity) VALUES('{date}', {quantity})";
            _dbConnection.Insert(sql);
        }

        internal void DeleteDrinkingWaterRecordById(int id)
        {
            string sql = $"DELETE from drinking_water WHERE Id ={id}";
            _dbConnection.Delete(sql);
        }

        internal void EditDrinkingWaterRecordById(DrinkingWaterDto drinkingWater)
        {
            string sql = $"UPDATE drinking_water SET Date = '{drinkingWater.Date}', Quantity = {drinkingWater.Quantity} WHERE Id = {drinkingWater.Id} ";
            _dbConnection.Update(sql);
        }

        internal DataTable GetAllDrinkingWaterRecords()
        {
            string sql = "SELECT * FROM drinking_water";
            return _dbConnection.GetDataTable(sql);
        }

        internal DrinkingWaterDto GetDrinkingWaterRecordById(int id)
        {
            string sql = $"SELECT * FROM drinking_water WHERE Id = {id}";
            return _dbConnection.GetDrinkingWaterRecordById(sql);
        }
    }
}
