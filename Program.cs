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

        static void Main(string[] args)
        {

            //testFindContact(7);
            testAddNewContact();
        }
    }
}
