using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsBusinessLayer
{
    public class clsCountry
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID {  get; set; }
        public string CountryName { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }

        public clsCountry()
        {
            ID = -1;
            CountryName = "";
            Code = "";
            PhoneCode = "";
            this.Mode = enMode.AddNew;
        }
        clsCountry(int  ID,  string CountryName, string Code, string PhoneCode)
        {
            this.ID= ID;
            this.CountryName= CountryName;
            this.Code= Code;
            this.PhoneCode= PhoneCode;
            this.Mode= enMode.Update;
        }
        public static clsCountry Find(int ID)
        {
            string CountryName = "";
            string Code = "";
            string PhoneCode = "";

            bool isFound = clsCountryData.GetCountryInfoByID(ID, ref CountryName, ref Code, ref PhoneCode);
            if (isFound)
            {
                return new clsCountry(ID, CountryName, Code, PhoneCode);
            }
            else
            {
                return null;
            }
        }
        public static clsCountry Find(string CountryName)
        {
            int ID = -1;
            string Code = "";
            string PhoneCode = "";

            if (clsCountryData.GetCountryInfoByName(CountryName, ref ID, ref Code, ref PhoneCode))
                return new clsCountry(ID, CountryName, Code, PhoneCode);
            else
                return null;
        }
        private bool _AddNewCountry()
        {
            //call DataAccess Layer 

            this.ID = clsCountryData.AddNewCountry(this.CountryName,this.Code,this.PhoneCode);

            return (this.ID != -1);
        }
        private bool _UpdateContact()
        {
            //call DataAccess Layer 

            return clsCountryData.UpdateCountry(this.ID, this.CountryName, this.Code, this.PhoneCode);

        }
        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {

                        Mode = enMode.Update;
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
        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();

        }
        public static bool DeleteCountry(int ID)
        {
            return clsCountryData.DeleteCountry(ID);
        }
        public static bool isCountryExist(int ID)
        {
            return clsCountryData.IsCountryExist(ID);
        }
        public static bool isCountryExist(string CountryName)
        {
            return clsCountryData.IsCountryExist(CountryName);
        }
    }
}
