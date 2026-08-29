using ContactsBusinessLayer;
using System;
using System.Data;
using System.Diagnostics.Contracts;

namespace ContactsManagementSystemPresentationLayer
{
    internal class Program
    {
        public static void PrintInfo(clsContact contactInf)
        {
            Console.WriteLine(contactInf.FirstName + " " + contactInf.LastName);
            Console.WriteLine(contactInf.Email);
            Console.WriteLine(contactInf.Phone);
            Console.WriteLine(contactInf.Address);
            Console.WriteLine(contactInf.DateOfBirth);
            Console.WriteLine(contactInf.CountryID);
            Console.WriteLine(contactInf.ImagePath);
        }
        static void testFindContact(int ID)
        {
            clsContact contact = clsContact.Find(ID);

            if (contact!= null)
            {
                PrintInfo(contact);
            }
            else
            {
                Console.WriteLine("Contact [" + ID + "] Not found!");
            }

        }
        static void testAddNewContact()
        {
            clsContact Contact1 = new clsContact();

            Contact1.FirstName = "Fadi";
            Contact1.LastName = "Maher";
            Contact1.Email = "A@a.com";
            Contact1.Phone = "010010";
            Contact1.Address = "address1";
            Contact1.DateOfBirth = new DateTime(1977, 11, 6, 10, 30, 0);
            Contact1.CountryID = 1;
            Contact1.ImagePath = "";

            if (Contact1.Save())
            {
                Console.WriteLine("Contact Added Successfully with id = " + Contact1.ID);
            }


        }
        static void testUpdateContact(int id)
        {
            clsContact Contact1 = clsContact.Find(id);

            if (Contact1 != null)
            {
                Contact1.FirstName = "Rami";
                Contact1.LastName = "Kassab";
                Contact1.Email = "sami.kassab@gmail.com";
                Contact1.Phone = "05312345678";
                Contact1.Address = "Istanbul, Turkey";
                Contact1.DateOfBirth = new DateTime(1995, 5, 20);
                Contact1.CountryID = 1;
                Contact1.ImagePath = "";

                if (Contact1.Save())
                {
                    Console.WriteLine("Contact Updated Successfully with id = " + Contact1.ID);
                }
                else
                {
                    Console.WriteLine("Failed to update contact.");
                }
            }
            else
            {
                Console.WriteLine($"Contact with ID {id} not found.");
            }
        }
        static void testDeleteContact(int id)
        {
            bool result = clsContact.DeleteContact(id);

            if (result)
            {
                Console.WriteLine("Contact Deleted Successfully with id = " + id);
            }
            else
            {
                Console.WriteLine("Failed to Delete Contact with id = " + id);
            }

        }
        static void PrintRow(DataRow row)
        {

            Console.WriteLine($"{row["ContactID"]}] {row["FirstName"]}" +
                              $"{row["LastName"]}");
        }
        static void testListContacts()
        {
            DataTable dataTable = clsContact.GetAllContacts();

            Console.WriteLine("\n=========== Contacts List ===========\n");
            
            foreach (DataRow row in dataTable.Rows)
            {
                PrintRow(row);
            }

            Console.WriteLine("\n=====================================\n");
        }
        static void Main(string[] args)
        {

            //testFindContact(7);
            //testAddNewContact();
            //testUpdateContact(12);
            //testDeleteContact(11);
            testListContacts();
        }
    }
}
