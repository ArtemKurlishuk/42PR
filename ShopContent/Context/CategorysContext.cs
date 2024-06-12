using ShopContent.Modell;
using System;
using System.Collections.Generic;
using System.Text;
using ShopContent.Classes;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace ShopContent.Context
{
    public class CategorysContext : Categorys
    {
        public static ObservableCollection<CategorysContext> AllCategoys()
        {
            ObservableCollection<CategorysContext> allCategoys = new ObservableCollection<CategorysContext>();
            SqlConnection connection;
            SqlDataReader dataCategotys = Connection.Query("SELECT * FROM [dbo].[Categorys]", out connection);
            while (dataCategotys.Read())
            {
                allCategoys.Add(new CategorysContext()
                {
                    Id = dataCategotys.GetInt32(0),
                    Name = dataCategotys.GetString(1)
                });
            }
            Connection.CloseConnection(connection); return allCategoys;
        }
    }
}
