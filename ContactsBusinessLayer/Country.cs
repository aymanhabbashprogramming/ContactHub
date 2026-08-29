using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
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

        clsCountry()
        {
            ID = -1;
            CountryName = "";
            this.Mode = enMode.AddNew;
        }
        clsCountry(int  ID,  string CountryName)
        {
            this.ID= ID;
            this.CountryName= CountryName;
            this.Mode= enMode.Update;
        }
        public static clsCountry Find(int ID)
        {
            string CountryName = "";
            bool isFound = clsCountryData.GetCountryInfoByID(ID,ref CountryName);
            if (isFound)
            {
                return new clsCountry(ID, CountryName);
            }
            else
            {
                return null;
            }
        }
        public static clsCountry Find(string CountryName)
        {

            int ID = -1;


            if (clsCountryData.GetCountryInfoByName(CountryName, ref ID))

                return new clsCountry(ID, CountryName);
            else
                return null;

        }



    }
}
