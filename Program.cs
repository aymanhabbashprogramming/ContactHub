using ContactsBusinessLayer;
using System;
using System.Data;

namespace ContactsManagementSystemPresentationLayer
{
    internal class Program
    {
        //=============== Helper / Input Functions ===============

        static string ReadNonEmptyString(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    Console.WriteLine("This field cannot be empty. Try again.");
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        static int ReadInt(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result))
                    return result;

                Console.WriteLine("Invalid number. Try again.");
            }
        }

        static DateTime ReadDate(string prompt)
        {
            DateTime result;
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParse(Console.ReadLine(), out result))
                    return result;

                Console.WriteLine("Invalid date format. Try again (example: 1995-05-20).");
            }
        }

        static int ReadValidCountryID(string prompt)
        {
            int id;
            while (true)
            {
                id = ReadInt(prompt);
                if (clsCountry.isCountryExist(id))
                    return id;

                Console.WriteLine($"\nCountry with ID [{id}] does not exist. Try again.");
            }
        }

        //=============== Contact Data Collection ===============

        static clsContact ReadContactDataFromUser()
        {
            clsContact contact = new clsContact();

            contact.FirstName = ReadNonEmptyString("First Name: ");
            contact.LastName = ReadNonEmptyString("Last Name: ");
            contact.Email = ReadNonEmptyString("Email: ");
            contact.Phone = ReadNonEmptyString("Phone: ");
            contact.Address = ReadNonEmptyString("Address: ");
            contact.DateOfBirth = ReadDate("Date of Birth (yyyy-MM-dd): ");
            contact.CountryID = ReadValidCountryID("Country ID: ");
            contact.ImagePath = "";

            return contact;
        }

        //=============== Country Data Collection ===============

        static clsCountry ReadCountryDataFromUser()
        {
            clsCountry country = new clsCountry();

            country.CountryName = ReadNonEmptyString("Country Name: ");
            country.Code = ReadNonEmptyString("Country Code: ");
            country.PhoneCode = ReadNonEmptyString("Phone Code: ");

            return country;
        }

        //=============== Contact Functions ===============

        public static void PrintInfo(clsContact contactInf)
        {
            Console.WriteLine($"{"First Name",-15}: {contactInf.FirstName}");
            Console.WriteLine($"{"Last Name",-15}: {contactInf.LastName}");
            Console.WriteLine($"{"Email",-15}: {contactInf.Email}");
            Console.WriteLine($"{"Phone",-15}: {contactInf.Phone}");
            Console.WriteLine($"{"Address",-15}: {contactInf.Address}");
            Console.WriteLine($"{"Date of Birth",-15}: {contactInf.DateOfBirth.ToShortDateString()}");
            Console.WriteLine($"{"Country ID",-15}: {contactInf.CountryID}");
            Console.WriteLine($"{"Image Path",-15}: {contactInf.ImagePath}");
        }

        static void testFindContact()
        {
            int id = ReadInt("Enter Contact ID: ");
            clsContact contact = clsContact.Find(id);

            if (contact != null)
                PrintInfo(contact);
            else
                Console.WriteLine("Contact [" + id + "] Not found!");
        }

        static void testAddNewContact()
        {
            clsContact contact = ReadContactDataFromUser();

            if (contact.Save())
                Console.WriteLine("Contact Added Successfully with id = " + contact.ID);
            else
                Console.WriteLine("Failed to add contact.");
        }

        static void testUpdateContact()
        {
            int id = ReadInt("Enter Contact ID to update: ");
            clsContact contact = clsContact.Find(id);

            if (contact == null)
            {
                Console.WriteLine($"Contact with ID {id} not found.");
                return;
            }

            Console.WriteLine("\n-----------------------------");
            Console.WriteLine("      Enter new data");
            Console.WriteLine("-----------------------------\n");
            clsContact newData = ReadContactDataFromUser();

            contact.FirstName = newData.FirstName;
            contact.LastName = newData.LastName;
            contact.Email = newData.Email;
            contact.Phone = newData.Phone;
            contact.Address = newData.Address;
            contact.DateOfBirth = newData.DateOfBirth;
            contact.CountryID = newData.CountryID;

            if (contact.Save())
                Console.WriteLine("Contact Updated Successfully with id = " + contact.ID);
            else
                Console.WriteLine("Failed to update contact.");
        }

        static void testDeleteContact()
        {
            int id = ReadInt("Enter Contact ID to delete: ");

            if (clsContact.isContactExist(id))
            {
                if (clsContact.DeleteContact(id))
                    Console.WriteLine($"Contact with ID [{id}] deleted successfully.");
                else
                    Console.WriteLine($"Failed to delete contact with ID [{id}].");
            }
            else
            {
                Console.WriteLine($"Contact with ID [{id}] does NOT exist.");
            }
        }

        static void PrintRow(DataRow row)
        {
            Console.WriteLine($"{row["ContactID"]}] {row["FirstName"]} {row["LastName"]}");
        }

        static void testListContacts()
        {
            DataTable dataTable = clsContact.GetAllContacts();

            Console.WriteLine("\n=========== Contacts List ===========\n");

            foreach (DataRow row in dataTable.Rows)
                PrintRow(row);

            Console.WriteLine("\n=====================================\n");
        }

        static void testIsContactExist()
        {
            int id = ReadInt("Enter Contact ID: ");

            if (clsContact.isContactExist(id))
                Console.WriteLine($"Contact with ID [{id}] exists.");
            else
                Console.WriteLine($"Contact with ID [{id}] does NOT exist.");
        }

        //=============== Country Functions ===============

        static void testFindCountryByID()
        {
            int id = ReadInt("Enter Country ID: ");
            clsCountry country = clsCountry.Find(id);

            if (country != null)
                Console.WriteLine($"ID: {country.ID}, Name: {country.CountryName}, Code: {country.Code}, PhoneCode: {country.PhoneCode}");
            else
                Console.WriteLine("Country [" + id + "] Not found!");
        }

        static void testFindCountryByName()
        {
            string name = ReadNonEmptyString("Enter Country Name: ");
            clsCountry country = clsCountry.Find(name);

            if (country != null)
                Console.WriteLine($"Country [{name}] is Found with ID = {country.ID}, Code = {country.Code}, PhoneCode = {country.PhoneCode}");
            else
                Console.WriteLine("Country [" + name + "] Is Not found!");
        }

        static void testAddNewCountry()
        {
            clsCountry country = ReadCountryDataFromUser();

            if (country.Save())
                Console.WriteLine("Country Added Successfully with id = " + country.ID);
            else
                Console.WriteLine("Failed to add country.");
        }

        static void ListCountries()
        {
            DataTable dataTable = clsCountry.GetAllCountries();

            Console.WriteLine("Countries Data:");

            foreach (DataRow row in dataTable.Rows)
                Console.WriteLine($"{row["CountryID"]}, {row["CountryName"]}, Code: {row["Code"]}, PhoneCode: {row["PhoneCode"]}");
        }

        static void testDeleteCountry()
        {
            int id = ReadInt("Enter Country ID to delete: ");

            if (clsCountry.isCountryExist(id))
            {
                if (clsCountry.DeleteCountry(id))
                    Console.WriteLine("Country Deleted Successfully.");
                else
                    Console.WriteLine("Failed to delete Country.");
            }
            else
            {
                Console.WriteLine("Failed to delete: The Country with id = " + id + " is not found");
            }
        }

        static void testIsCountryExistByName()
        {
            string name = ReadNonEmptyString("Enter Country Name: ");

            if (clsCountry.isCountryExist(name))
                Console.WriteLine("Yes, Country is there.");
            else
                Console.WriteLine("No, Country Is not there.");
        }

        static void testUpdateCountry()
        {
            int id = ReadInt("Enter Country ID to update: ");
            clsCountry country = clsCountry.Find(id);

            if (country == null)
            {
                Console.WriteLine("Country Not found!");
                return;
            }

            Console.WriteLine("\n-----------------------------");
            Console.WriteLine("      Enter new data");
            Console.WriteLine("-----------------------------\n");
            clsCountry newData = ReadCountryDataFromUser();

            country.CountryName = newData.CountryName;
            country.Code = newData.Code;
            country.PhoneCode = newData.PhoneCode;

            if (country.Save())
                Console.WriteLine("Country Updated Successfully.");
            else
                Console.WriteLine("Failed to Update Country.");
        }

        //=============== Menus (Print) ===============

        static void PrintMainMenu()
        {
            Console.WriteLine("_____ Main Menu _____\n");
            Console.WriteLine("1. Country Management");
            Console.WriteLine("2. Contact Management");
            Console.WriteLine("0. Exit");
        }

        static void PrintCountryMenu()
        {
            Console.WriteLine("________ Country Management ________\n");
            Console.WriteLine("1. Find Country by ID");
            Console.WriteLine("2. Find Country by Name");
            Console.WriteLine("3. Add New Country");
            Console.WriteLine("4. List All Countries");
            Console.WriteLine("5. Delete Country");
            Console.WriteLine("6. Check if Country Exists (by Name)");
            Console.WriteLine("7. Update Country");
            Console.WriteLine("0. Back to Main Menu");
        }

        static void PrintContactMenu()
        {
            Console.WriteLine("____ Contact Management ____\n");
            Console.WriteLine("1. Find Contact by ID");
            Console.WriteLine("2. Add New Contact");
            Console.WriteLine("3. Update Contact");
            Console.WriteLine("4. Delete Contact");
            Console.WriteLine("5. List All Contacts");
            Console.WriteLine("6. Check if Contact Exists");
            Console.WriteLine("0. Back to Main Menu");
        }

        //=============== Menus (Logic) ===============

        static void CountryMenu()
        {
            while (true)
            {
                Console.Clear();

                PrintCountryMenu();

                Console.Write("\nChoose an option: ");
                string choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1": testFindCountryByID(); break;
                    case "2": testFindCountryByName(); break;
                    case "3": testAddNewCountry(); break;
                    case "4": ListCountries(); break;
                    case "5": testDeleteCountry(); break;
                    case "6": testIsCountryExistByName(); break;
                    case "7": testUpdateCountry(); break;
                    case "0": return;
                    default: Console.WriteLine("\nInvalid option."); break;
                }

                Console.WriteLine("\n----------------------------------------------------");
                Console.WriteLine("Press any key to return to the country menu...");
                Console.ReadKey();
            }
        }

        static void ContactMenu()
        {
            while (true)
            {
                Console.Clear();

                PrintContactMenu();

                Console.Write("\nChoose an option: ");
                string choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1": testFindContact(); break;
                    case "2": testAddNewContact(); break;
                    case "3": testUpdateContact(); break;
                    case "4": testDeleteContact(); break;
                    case "5": testListContacts(); break;
                    case "6": testIsContactExist(); break;
                    case "0": return;
                    default: Console.WriteLine("\nInvalid option."); break;
                }

                Console.WriteLine("\n----------------------------------------------------");
                Console.WriteLine("Press any key to return to the contact menu...");
                Console.ReadKey();
            }
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();

                PrintMainMenu();

                Console.Write("\nChoose an option: ");
                string choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1": CountryMenu(); continue;
                    case "2": ContactMenu(); continue;
                    case "0": return;
                    default: Console.WriteLine("\nInvalid option."); break;
                }

                Console.WriteLine("\n----------------------------------------------------");
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
            }
        }

        static void Main(string[] args)
        {
            MainMenu();
        }
    }
}