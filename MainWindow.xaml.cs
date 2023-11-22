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
            public List<StudentGradeEntity> ExecuteReaderUsers()
            {
                string query = "select * from [StudentGrade]";
                List<StudentGradeEntity> result = new List<StudentGradeEntity>();
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
                            result.Add(new StudentGradeEntity(reader[1].ToString(), reader[2].ToString(), int.Parse(reader[3].ToString()), reader[4].ToString(), int.Parse(reader[5].ToString()), reader[6].ToString(), int.Parse(reader[7].ToString())));
                        }
                    }
                }
                CloseConnection();
                return result;
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
            database = new DatabaseManager(@"Data Source=DESKTOP-OF66R01\SQLEXPRESS;Initial Catalog=StudentGrades;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            if (database.OpenConnection())
            {            
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("Error");
            }
            // 1
            //List<StudentGradeEntity> result = database.ExecuteReaderUsers();

            //List<NPCforString> result = database.ExecuteReaderString("Select Name from StudentGrade");

            //List<NPCforString> result = database.ExecuteReaderString("Select Name from StudentGrade where MinGrade > 8");

            //List<NPCforString> result = database.ExecuteReaderString("Select [MinGradeLesson] from StudentGrade group by MinGradeLesson");

            // Показати кількість студентів у кожній групі.
            //List<NPCforInt> result = database.ExecuteReaderIntList("select count(Name) from StudentGrade group by [Group]");

            // Показати середню оцінку групи
            List<NPCforInt> result = database.ExecuteReaderIntList("select avg([AverageGrade]) from StudentGrade group by [Group]");

            DGMain.ItemsSource = result;

            // 2
            //int result;

            // Показати мінімальну середню оцінку.
            //result = database.ExecuteReaderInt("select min([AverageGrade]) from StudentGrade");

            // Показати максимальну середню оцінку.
            //result = database.ExecuteReaderInt("select max([AverageGrade]) from StudentGrade");

            // Показати кількість студентів з мінімальною середньою оцінкою з математики.
            //result = database.ExecuteReaderInt("select count(Id) from StudentGrade where [MinGradeLesson] = \'Algebra\'");

            // Показати кількість студентів, в яких максимальна середня оцінка з математики.
            //result = database.ExecuteReaderInt("select count(Id) from StudentGrade where [MaxGradeLesson] = \'Algebra\'");

            //LInfo.Content = result.ToString();

        }
    }
}
