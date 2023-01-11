
using ConsoleApp.serialization;
using System;
using System.Collections.ObjectModel;

namespace CrudModel
{
    public class Manager : User, Serializable
    {
        public Manager()
        {
            address = new Address();
        }
        public String position
        {
            get;
            set;
        }
        public Manager(int id,String name, String surname, String email, String password, Address address,String phone,String position)
        {
            this.name = name;
            this.surname = surname;
            this.mail = email;
            this.password = password;
            this.address = address;
            this.mobilePhone = phone;
            this.position = position;

        }
        public Manager(string name, string surname, Address address, string password, string mobilePhone, string mail) : base(name, surname, address, password, mobilePhone, mail)
        {
            this.userID = User.generateID();
            this.position = "Manager";
        }

        
        public string[] toCSV()
        {
            string[] csvValues =
            {
                name,
                surname,
                address.country,
                address.city,
                address.street,
                address.number,
                password,
                mobilePhone,
                mail,
                userID.ToString()
            };
            return csvValues;
        }

        public void fromCSV(string[] values)
        {
            name = values[0];
            surname = values[1];
            address.country = values[2];
            address.city = values[3];
            address.street = values[4];
            address.number = values[5];
            password = values[6];
            mobilePhone = values[7];
            mail = values[8];
            userID = int.Parse(values[9]);
        }
    }
}