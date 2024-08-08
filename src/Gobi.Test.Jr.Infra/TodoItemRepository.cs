using Gobi.Test.Jr.Domain;
using Gobi.Test.Jr.Domain.Interfaces;
using System.Data.SQLite;

namespace Gobi.Test.Jr.Infra
{
	public class TodoItemRepository : ITodoItemRepository
	{
		public TodoItemRepository()
		{
			CreateDatabase();
			CreateTable();
		}

		private static SQLiteCommand CreateCommand()
		{
			var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gobi.sqlite");
			var connectionString = $"Data Source={filePath}; Version=3;";
			var connection = new SQLiteConnection(connectionString);

			return new SQLiteCommand(connection);
		}

		private void CreateDatabase()
		{
			var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gobi.sqlite");

			if (File.Exists(filePath) is false)
			{
				SQLiteConnection.CreateFile(filePath);
			}			
		}

		private void CreateTable()
		{
			var command = CreateCommand();

			command.CommandText = """
                CREATE TABLE IF NOT EXISTS "TodoItem" 
                (
                    "Id" integer NOT NULL PRIMARY KEY AUTOINCREMENT,
                    "Description" TEXT NOT NULL,
                    "Completed" integer NOT NULL
                );
                """;

			command.Connection.Open();
			command.ExecuteNonQuery();
			command.Connection.Close();
		}


		public IEnumerable<TodoItem> GetAll()
		{
			var items = new List<TodoItem>();
			var command = CreateCommand();

			command.CommandText = "SELECT * FROM TodoItem";
			command.Connection.Open();

			using (var reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					items.Add(new TodoItem
					{
						Id = Convert.ToInt32(reader["Id"]),
						Description = reader["Description"].ToString(),
						Completed = Convert.ToBoolean(reader["Completed"])
					});
				}
			}

			command.Connection.Close();
			return items;
		}
		public void Save(TodoItem item)
		{
			var command = CreateCommand();
			command.CommandText = "INSERT INTO TodoItem (Description, Completed) VALUES (@Description, @Completed)";
			command.Parameters.AddWithValue("@Description", item.Description);
			command.Parameters.AddWithValue("@Completed", item.Completed ? 1 : 0);

			command.Connection.Open();
			command.ExecuteNonQuery();

			command.CommandText = "SELECT last_insert_rowid()";
			item.Id = (int)(long)command.ExecuteScalar();

			command.Connection.Close();
			
		}
		public void Edit(TodoItem item)
		{
			var command = CreateCommand();
			command.CommandText = "UPDATE TodoItem SET Description = @Description, Completed = @Completed WHERE Id = @Id";
			command.Parameters.AddWithValue("@Description", item.Description);
			command.Parameters.AddWithValue("@Completed", item.Completed ? 1 : 0);
			command.Parameters.AddWithValue("@Id", item.Id);

			command.Connection.Open();
			command.ExecuteNonQuery();
			command.Connection.Close();

		}

		public void Delete(TodoItem item)
		{
			var command = CreateCommand();
			command.CommandText = "DELETE FROM TodoItem WHERE Id = @Id";
			command.Parameters.AddWithValue("@Id", item.Id);

			command.Connection.Open();
			command.ExecuteNonQuery();
			command.Connection.Close();
		}


	}
}
