using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace ShopContent.Classes
{
    public class Connection
    {
        private static readonly string config = "server=student.permaviat.ru;" +
            "Trusted_Connection=No;" +
            "DataBase=ShopContent;" +
            "User=***;" +
            "PWD=***";
        public static SqlConnection OpenConnection()
        {
            // Создаём поключение к базе данных
            SqlConnection connection = new SqlConnection(config);
            // Открываем соединение
            connection.Open();
            // Возвращаем соединение
            return connection;
        }

        public static SqlDataReader Query(string SQL, out SqlConnection connection)
        {
            // Создаём подключение к базе данных
            connection = OpenConnection();
            // Возвращаем данные
            return new SqlCommand(SQL, connection).ExecuteReader();
        }

        public static void CloseConnection(SqlConnection connection)
        {
            // Закрываем подключение
            connection.Close();
            SqlConnection.ClearPool(connection);
        }
    }
}
