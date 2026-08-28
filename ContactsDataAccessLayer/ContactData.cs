using System;
using System.Data;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        public struct stContactInfo
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public DateTime DateOfBirth { get; set; }
            public int CountryID { get; set; }
            public string ImagePath { get; set; }
        }
        public static bool GetContactInfoByID(ref stContactInfo contactInfo)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Contacts WHERE ContactID = @Contact_ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Contact_ID", contactInfo.ID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    contactInfo.ID = (int)reader["ContactID"];
                    contactInfo.FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : "";
                    contactInfo.LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : "";
                    contactInfo.Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                    contactInfo.Phone = reader["Phone"] != DBNull.Value ? (string)reader["Phone"] : "";
                    contactInfo.Address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : "";
                    contactInfo.DateOfBirth = (DateTime)reader["DateOfBirth"];
                    contactInfo.CountryID = (int)reader["CountryID"];
                    contactInfo.ImagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";
                }
                else
                {
                    isFound = false;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
    }
}
