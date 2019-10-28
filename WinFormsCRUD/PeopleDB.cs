using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;

namespace WinFormsCRUD
{
    public class PeopleDB
    {
        //private static string connectionString = @"Data Source=DESKTOP-1U9MT7U\OVEDFS; Initial Catalog=People;Trusted_Connection=True;";
        //private static string connectionString = @"Data Source=.; Initial Catalog=People;Trusted_Connection=True;";
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ToString();

        public static bool Test()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public static List<Person> GetPeople()
        {
            List<Person> people = new List<Person>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id, name, age FROM people";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Person person = new Person() {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Age = (int)reader["age"]
                            };

                            people.Add(person);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Hubo un error: {e.Message}");
                    }
                }
            }

            return people;
        }

        public static Person GetPeople(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id, name, age FROM people WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        reader.Read();

                        Person person = new Person()
                        {
                            Id = (int)reader["id"],
                            Name = (string)reader["name"],
                            Age = (int)reader["age"]
                        };

                        return person;
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Hubo un error: {e.Message}");
                    }
                }
            }
        }

        public static int Insert(Person person)
        {
            int inserted = -1;

            using (SqlConnection connection =  new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO people(name, age) VALUES(@name, @age)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", person.Name);
                    command.Parameters.AddWithValue("@age", person.Age);

                    try
                    {
                        inserted = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                }
            }

            return inserted;
        }

        public static int Update(Person person, int id)
        {
            int updated = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE people SET name = @name, age = @age WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", person.Name);
                    command.Parameters.AddWithValue("@age", person.Age);
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        updated = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                }
            }

            return updated;
        }

        public static int Delete(int id)
        {
            int deleted = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM people WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    try
                    {
                        deleted = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                }
            }

            return deleted;
        }
    }
}
