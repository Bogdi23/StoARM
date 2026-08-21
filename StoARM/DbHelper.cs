using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace StoARM
{
	public static class DbHelper
	{
		public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False";
		public static DataTable ExecuteQuery(string query)
		{
			DataTable dataTable = new DataTable();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					using (SqlDataAdapter adapter = new SqlDataAdapter(command))
					{
						adapter.Fill(dataTable);
					}
				}
			}
			return dataTable;
		}

		// Метод для записи/изменения/удаления (INSERT, UPDATE, DELETE)
		public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					if (parameters != null)
					{
						command.Parameters.AddRange(parameters);
					}
					return command.ExecuteNonQuery();
				}
			}
		}

		// Методы получения таблиц для отображения
		public static DataTable GetServices()
		{
			return ExecuteQuery("SELECT service_id AS [ID], name AS [Название], price AS [Цена (руб)] FROM Services");
		}

		public static DataTable GetClients()
		{
			return ExecuteQuery("SELECT client_id AS [ID], last_name AS [Фамилия], first_name AS [Имя], middle_name AS [Отчество], phone_number AS [Телефон] FROM Clients");
		}

		public static DataTable GetInventory()
		{
			return ExecuteQuery("SELECT part_id AS [ID], part_name AS [Запчасть], price AS [Цена], quantity AS [Остаток] FROM Inventory");
		}

		public static DataTable GetCars()
		{
			string query = @"SELECT c.car_id AS [ID], c.brand AS [Марка], c.model AS [Модель], c.license_plate AS [Гос. номер], 
                            cl.last_name + ' ' + cl.first_name AS [Владелец] 
                            FROM Cars c 
                            JOIN Clients cl ON c.client_id = cl.client_id";
			return ExecuteQuery(query);
		}

		public static DataTable GetOrders()
		{
			string query = @"SELECT o.order_id AS [№ Заказа], o.order_date AS [Дата], o.status AS [Статус],
                            c.brand + ' ' + c.model + ' (' + c.license_plate + ')' AS [Автомобиль],
                            s.name AS [Услуга],
                            ISNULL(i.part_name, 'Без запчастей') AS [Запчасть]
                            FROM Orders o
                            JOIN Cars c ON o.car_id = c.car_id
                            JOIN Services s ON o.service_id = s.service_id
                            LEFT JOIN Inventory i ON o.part_id = i.part_id";
			return ExecuteQuery(query);
		}
	}
}
