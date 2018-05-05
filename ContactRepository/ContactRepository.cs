using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ContactRepo
{
    public class ContactRepository
    {
        private readonly SQLiteConnection m_connection;

        public ContactRepository()
        {
            m_connection = new SQLiteConnection(@"Data Source=C:\SQLiteDB\Contact.db");
            AddTable();
        }

        private void AddTable()
        {
            m_connection.Open();
            using (var command = m_connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS Contact(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "FirstName TEXT,LastName TEXT, PhoneNumber NUMERIC, Email TEXT, Active BOOLEAN)";
                command.ExecuteNonQuery();
            }
            m_connection.Close();
        }

        public IEnumerable<ContactModel> GetContactList()
        {
            List<ContactModel> contactModelList = new List<ContactModel>();

            try
            {
                m_connection.Open();
                using (var command = m_connection.CreateCommand())
                {
                    command.CommandText = "select * from Contact";
                    using (var sqlReader = command.ExecuteReader())
                    {
                        while (sqlReader.Read())
                        {
                            contactModelList.Add(new ContactModel()
                            {
                                Id = Convert.ToInt32(sqlReader["Id"]),//TODO - refactor ID property
                                FirstName = Convert.ToString(sqlReader["FirstName"]),
                                LastName = Convert.ToString(sqlReader["LastName"]),
                                PhoneNumber = Convert.ToString(sqlReader["PhoneNumber"]),
                                Email = Convert.ToString(sqlReader["Email"]),
                                Active = Convert.ToBoolean(sqlReader["Active"])
                            });
                        }
                    }
                }
            }
            finally
            {
                m_connection.Close();
            }

            return contactModelList;
        }

        public bool UpdateContact(int id, ContactModel model)
        {
            int queryResult = 0;
            try
            {
                m_connection.Open();
                using (var command = m_connection.CreateCommand())
                {
                    command.CommandText =
                        "UPDATE Contact SET FirstName = @firstname, LastName = @lastname, PhoneNumber = @phonenumber," +
                        "Email = @email, Active = @active WHERE id=@id";
                    command.Parameters.AddWithValue("@firstname", model.FirstName);
                    command.Parameters.AddWithValue("@lastname", model.LastName);
                    command.Parameters.AddWithValue("@phonenumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@email", model.Email);
                    command.Parameters.AddWithValue("@active", model.Active);
                    command.Parameters.AddWithValue("@id", id);

                    queryResult = command.ExecuteNonQuery();
                }
            }
            finally
            {
                m_connection.Close();
            }
            return queryResult != 0;
        }

        public bool AddContact(ContactModel model)
        {
            int queryResult = 0;
            try
            {
                m_connection.Open();
                using (var command = m_connection.CreateCommand())
                {
                    command.CommandText =
                        "INSERT INTO Contact(FirstName, LastName, PhoneNumber, Email, Active)" +
                        "VALUES(@firstname,@lastname,@phonenumber,@email,@active)";
                    command.Parameters.AddWithValue("@firstname", model.FirstName);
                    command.Parameters.AddWithValue("@lastname", model.LastName);
                    command.Parameters.AddWithValue("@phonenumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@email", model.Email);
                    command.Parameters.AddWithValue("@active", model.Active);

                    queryResult = command.ExecuteNonQuery();
                }
            }
            finally
            {
                m_connection.Close();
            }
            return queryResult != 0;
        }

        public bool DeleteContact(int id)
        {
            int queryResult = 0;
            try
            {
                m_connection.Open();
                using (var command = m_connection.CreateCommand())
                {
                    command.CommandText = "DELETE from Contact WHERE id = " + id;
                    queryResult = command.ExecuteNonQuery();
                }
            }
            finally
            {
                m_connection.Close();
            }
            return queryResult != 0;
        }
    }
}