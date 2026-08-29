using ContactsDataAccessLayer;
using System;
using static ContactsDataAccessLayer.clsContactDataAccess;

namespace ContactsBusinessLayer
{
    public class clsContact
    {

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;


        public int ID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string ImagePath { set; get; }
        public int CountryID { set; get; }

        public clsContact()
        {
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";

            this.Mode = enMode.AddNew;
        }
        private clsContact(stContactInfo contactInfo)
        {
            this.ID = contactInfo.ID;
            this.FirstName = contactInfo.FirstName;
            this.LastName = contactInfo.LastName;
            this.Email = contactInfo.Email;
            this.Phone = contactInfo.Phone;
            this.Address = contactInfo.Address;
            this.DateOfBirth = contactInfo.DateOfBirth;
            this.CountryID = contactInfo.CountryID;
            this.ImagePath = contactInfo.ImagePath;

            this.Mode = enMode.Update;
        }
        public static clsContact Find(int ID)
        {
            stContactInfo contactInfo = new stContactInfo();
            contactInfo.ID = ID;

            if (clsContactDataAccess.GetContactInfoByID(ref contactInfo))
            {
                return new clsContact(contactInfo);
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewContact()
        {

            stContactInfo contactInfo = new stContactInfo();

            contactInfo.ID = -1;
            contactInfo.FirstName = this.FirstName;
            contactInfo.LastName = this.LastName;
            contactInfo.Email = this.Email;
            contactInfo.Phone = this.Phone;
            contactInfo.Address = this.Address;
            contactInfo.DateOfBirth = this.DateOfBirth;
            contactInfo.CountryID = this.CountryID;
            contactInfo.ImagePath = this.ImagePath;

            this.ID = clsContactDataAccess.AddNewContact(contactInfo);

            return (this.ID != -1);
        }
      
        private bool _UpdateContact()
        {
            stContactInfo contactInfo = new stContactInfo();

            contactInfo.ID = this.ID;
            contactInfo.FirstName = this.FirstName;
            contactInfo.LastName = this.LastName;
            contactInfo.Email = this.Email;
            contactInfo.Phone = this.Phone;
            contactInfo.Address = this.Address;
            contactInfo.DateOfBirth = this.DateOfBirth;
            contactInfo.CountryID = this.CountryID;
            contactInfo.ImagePath = this.ImagePath;

           bool result = clsContactDataAccess.UpdateContact(contactInfo);

            return result;
        }   
        public bool Save()
        {
            switch (this.Mode)
            {

                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateContact();
            }

            return false;
        }


    }
}
