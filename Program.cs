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

            if (clsContact.isContactExist(id))
            {
                if (clsContact.DeleteContact(id))
                {
                    Console.WriteLine($"Contact with ID [{id}] deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to delete contact with ID [{id}].");
                }
            }
            else
            {
                Console.WriteLine($"Contact with ID [{id}] does NOT exist.");
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
        static bool testIsContactExist(int ID)
        {
            return (clsContact.isContactExist(ID));
        }

        //------------------------------------------------------------

        static void testFindCountryByID(int ID)
        {
            clsCountry country = clsCountry.Find(ID);

            if (country != null)
            {
                Console.WriteLine($"ID: {country.ID}, Name: {country.CountryName}, Code: {country.Code}, PhoneCode: {country.PhoneCode}");
            }
            else
            {
                Console.WriteLine("Country [" + ID + "] Not found!");
            }
        }
        static void testFindCountryByName(string CountryName)
        {
            clsCountry Country1 = clsCountry.Find(CountryName);

            if (Country1 != null)
            {
                Console.WriteLine($"Country [{CountryName}] is Found with ID = {Country1.ID}, Code = {Country1.Code}, PhoneCode = {Country1.PhoneCode}");
            }
            else
            {
                Console.WriteLine("Country [" + CountryName + "] Is Not found!");
            }
        }
        static void testAddNewCountry()
        {
            clsCountry Country1 = new clsCountry();

            Country1.CountryName = "Lebanon";
            Country1.Code = "LB";
            Country1.PhoneCode = "961";

            if (Country1.Save())
            {
                Console.WriteLine("Country Added Successfully with id=" + Country1.ID);
            }
        }
        static void ListCountries()
        {
            DataTable dataTable = clsCountry.GetAllCountries();

            Console.WriteLine("Countries Data:");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["CountryID"]}, {row["CountryName"]}, Code: {row["Code"]}, PhoneCode: {row["PhoneCode"]}");
            }
        }
        static void testDeleteCountry(int ID)

        {

            if (clsCountry.isCountryExist(ID))

                if (clsCountry.DeleteCountry(ID))

                    Console.WriteLine("Country Deleted Successfully.");
                else
                    Console.WriteLine("Faild to delete Country.");

            else
                Console.WriteLine("Faild to delete: The Country with id = " + ID + " is not found");

        }
        static void testIsCountryExistByName(string CountryName)

        {

            if (clsCountry.isCountryExist(CountryName))

                Console.WriteLine("Yes, Country is there.");

            else
                Console.WriteLine("No, Country Is not there.");

        }
        static void testUpdateCountry(int ID)
        {
            clsCountry country = clsCountry.Find(ID);

            if (country != null)
            {
                country.CountryName = "Lebanon Updated";
                country.Code = "LBN";
                country.PhoneCode = "961";

                if (country.Save())
                {
                    Console.WriteLine("Country Updated Successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to Update Country.");
                }
            }
            else
            {
                Console.WriteLine("Country Not found!");
            }
        }
        static void Main(string[] args)
        {
            // TEST CONTACT FUNCTIONS

            //testFindContact(7);
            //testAddNewContact();
            //testUpdateContact(12);
            //testDeleteContact(11);
            //testListContacts();

            /*if (testIsContactExist(5))
            {
                Console.WriteLine("Contact with ID [5] exists.");
            }
            else
            {
                Console.WriteLine("Contact with ID [5] does NOT exist.");
            }*/


            // TEST COUNTRY FUNCTIONS

            //testFindCountryByID(8);
            //testFindCountryByName("Canada");
            //testAddNewCountry();
            //ListCountries();
            //testDeleteCountry(7);
            //testIsCountryExistByName("canada");
            //testUpdateCountry(9);
            ListCountries();
        }
    }
}
