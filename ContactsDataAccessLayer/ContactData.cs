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
        public static int AddNewContact(stContactInfo contactInfo)
        {
            int insertedID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO [Contacts] ([FirstName], [LastName], [Email], [Phone], [Address], [DateOfBirth], [CountryID], [ImagePath]) 
                             VALUES (@FirstName,@LastName, @Email, @Phone, @Address, @DateOfBirth, @CountryID, @ImagePath);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", contactInfo.FirstName);
            command.Parameters.AddWithValue("@LastName", contactInfo.LastName);
            command.Parameters.AddWithValue("@Email", contactInfo.Email);
            command.Parameters.AddWithValue("@Phone", contactInfo.Phone);
            command.Parameters.AddWithValue("@Address", contactInfo.Address);
            command.Parameters.AddWithValue("@DateOfBirth", contactInfo.DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", contactInfo.CountryID);

            if (string.IsNullOrEmpty(contactInfo.ImagePath))
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", contactInfo.ImagePath);
            }

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedIDResult))
                {
                    insertedID = insertedIDResult;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return insertedID;
        }
        public static bool UpdateContact(stContactInfo contactInfo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [Contacts]
                           SET [FirstName] = @FirstName
                              ,[LastName] = @LastName
                              ,[Email] = @Email
                              ,[Phone] = @Phone
                              ,[Address] = @Address
                              ,[DateOfBirth] = @DateOfBirth
                              ,[CountryID] = @CountryID
                              ,[ImagePath] = @ImagePath
                         WHERE ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ContactID", contactInfo.ID);
            command.Parameters.AddWithValue("@FirstName", contactInfo.FirstName);
            command.Parameters.AddWithValue("@LastName", contactInfo.LastName);
            command.Parameters.AddWithValue("@Email", contactInfo.Email);
            command.Parameters.AddWithValue("@Phone", contactInfo.Phone);
            command.Parameters.AddWithValue("@Address", contactInfo.Address);
            command.Parameters.AddWithValue("@DateOfBirth", contactInfo.DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", contactInfo.CountryID);

            if (string.IsNullOrEmpty(contactInfo.ImagePath))
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", contactInfo.ImagePath);
            }

            int rowsAffected = 0;
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Handling error
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);

        }

    }
}
