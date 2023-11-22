using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;
using wirstADO.net.DBEntity;

namespace wirstADO.net
{

    namespace DBEntity
    {
        public class DatabaseManager
        {
            public string ConnectionString { get; set; }

            private SqlConnection sqlConnection { get; set; }

            public DatabaseManager(string connectionString)
            {
                ConnectionString = connectionString;
                sqlConnection = new SqlConnection(ConnectionString);
            }
            public bool OpenConnection()
            {
                try
                {
                    sqlConnection.Open();
                    return true;
                }
                catch (SqlException sql_ex)
                {
                    throw new Exception(sql_ex.Message);
                }
            }
            public bool CloseConnection()
            {
                try
                {
                    sqlConnection.Close();
                    return true;
                }
                catch (SqlException sql_ex)
                {
                    throw new Exception(sql_ex.Message);
                }
            }
            public void ExecuteNonQuery(string query)
            {
                if (sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    OpenConnection();
                }
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                CloseConnection();
            }
            public List<NPCforString> ExecuteReaderString(string query)
            {
                List<NPCforString> result = new List<NPCforString>();
                using (SqlConnection _sqlConnection = new SqlConnection(ConnectionString))
                {
                    if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        _sqlConnection.OpenAsync();
                    }
                    SqlCommand sqlCommand = new SqlCommand(query, _sqlConnection);
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new NPCforString(reader.GetString(0)));
                        }
                    }
                }
                CloseConnection();
                return result;
            }
            public int ExecuteReaderInt(string query)
            {
                int result = 0;
                using (SqlConnection _sqlConnection = new SqlConnection(ConnectionString))
                {
                    if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        _sqlConnection.OpenAsync();
                    }
                    SqlCommand sqlCommand = new SqlCommand(query, _sqlConnection);
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = reader.GetInt32(0);
                        }
                    }
                }
                CloseConnection();
                return result;
            }

            public List<NPCforInt> ExecuteReaderIntList(string query)
            {
                List<NPCforInt> result = new List<NPCforInt>();
                using (SqlConnection _sqlConnection = new SqlConnection(ConnectionString))
                {
                    if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        _sqlConnection.OpenAsync();
                    }
                    SqlCommand sqlCommand = new SqlCommand(query, _sqlConnection);
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new NPCforInt(reader.GetInt32(0)));
                        }
                    }
                }
                CloseConnection();
                return result;
            }
            public List<FAV> ExecuteReaderFAV(string query)
            {
                List<FAV> result = new List<FAV>();
                using (SqlConnection _sqlConnection = new SqlConnection(ConnectionString))
                {
                    if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        _sqlConnection.OpenAsync();
                    }
                    SqlCommand sqlCommand = new SqlCommand(query, _sqlConnection);
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new FAV(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4)));
                        }
                    }
                }
                CloseConnection();
                return result;
            }
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseManager database;
        public MainWindow()
        {
            InitializeComponent();
            database = new DatabaseManager(@"Data Source=DESKTOP-OF66R01\SQLEXPRESS;Initial Catalog=FruitsAndVegetables;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            if (database.OpenConnection())
            {
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("Error");
            }
            // 1.1

            // Відображення всієї інформації з таблиці овочів і фруктів.
            //List<FAV> result = database.ExecuteReaderFAV("select * from FAV");

            // Відображення усіх назв овочів і фруктів.
            //List<NPCforString> result = database.ExecuteReaderString("select Name from FAV");

            // Відображення усіх кольорів.
            List<NPCforString> result = database.ExecuteReaderString("select Color from FAV");

            // 2.2

            // Показати кількість овочів і фруктів кожного кольору.
            //List<NPCforInt> result = database.ExecuteReaderIntList("select count(*) from FAV group by color");

            // Показати овочі та фрукти з калорійністю нижче вказаної.
            //List<FAV> result = database.ExecuteReaderFAV("select * from FAV where Callories < 400");

            // Показати овочі та фрукти з калорійністю вище вказаної.
            //List<FAV> result = database.ExecuteReaderFAV("select * from FAV where Callories > 400");

            // Показати овочі та фрукти з калорійністю у вказаному діапазоні.
            //List<FAV> result = database.ExecuteReaderFAV("select * from FAV where Callories between 100 and 300");

            // Показати усі овочі та фрукти жовтого або червоного кольору
            //List<FAV> result = database.ExecuteReaderFAV("select * from FAV where Color = \'Yellow\' or Color = \'Red\'");


            DGMain.ItemsSource = result;





            //int result;

            // 1.2

            // Показати максимальну калорійність.
            //result = database.ExecuteReaderInt("select max(Callories) from FAV");

            // Показати мінімальну калорійність.
            //result = database.ExecuteReaderInt("select min(Callories) from FAV");

            // Показати середню калорійність
            //result = database.ExecuteReaderInt("select avg(Callories) from FAV");

            // 2.1

            // Показати кількість овочів.
            //result = database.ExecuteReaderInt("select count(*) from FAV where Type = \'Vegetable\'");

            // Показати кількість фруктів.
            //result = database.ExecuteReaderInt("select count(*) from FAV where Type = \'Fruit\'");

            // Показати кількість овочів і фруктів заданого кольору.
            //result = database.ExecuteReaderInt("select count(*) from FAV where Color = \'Yellow\'");


            //LInfo.Content = result.ToString();

        }
    }
}
