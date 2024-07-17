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
    }
}
