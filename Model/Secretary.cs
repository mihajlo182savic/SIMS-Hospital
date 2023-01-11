// File:    Secretary.cs
// Author:  Dusan
// Created: Wednesday, March 30, 2022 4:13:07 PM
// Purpose: Definition of Class Secretary

using ConsoleApp.serialization;
using System;

namespace CrudModel
{
   public class Secretary : User , Serializable
    {
        public Secretary()
        {
            address = new Address();
        }
        public String position
        {
            get;
            set;
        }
      public Secretary(int id,String name,String surname,String email,String password,Address address,String phone,String position)
        {
            this.userID = id;
            this.name = name;
            this.surname = surname;
            this.mail = email;
            this.password = password;
            this.address = address;
            this.mobilePhone = phone;
            this.position = position;
        }
      public System.Collections.Generic.List<Meeting> meeting
        {
            set;
            get;
        }

        /// <summary>
        /// Property for collection of Meeting
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public System.Collections.Generic.List<Meeting> Meeting
      {
         get
         {
            if (meeting == null)
               meeting = new System.Collections.Generic.List<Meeting>();
            return meeting;
         }
         set
         {
            RemoveAllMeeting();
            if (value != null)
            {
               foreach (Meeting oMeeting in value)
                  AddMeeting(oMeeting);
            }
         }
      }
      
      /// <summary>
      /// Add a new Meeting in the collection
      /// </summary>
      /// <pdGenerated>Default Add</pdGenerated>
      public void AddMeeting(Meeting newMeeting)
      {
         if (newMeeting == null)
            return;
         if (this.meeting == null)
            this.meeting = new System.Collections.Generic.List<Meeting>();
         if (!this.meeting.Contains(newMeeting))
         {
            this.meeting.Add(newMeeting);
            newMeeting.Secretary = this;
         }
      }
      
      /// <summary>
      /// Remove an existing Meeting from the collection
      /// </summary>
      /// <pdGenerated>Default Remove</pdGenerated>
      public void RemoveMeeting(Meeting oldMeeting)
      {
         if (oldMeeting == null)
            return;
         if (this.meeting != null)
            if (this.meeting.Contains(oldMeeting))
            {
               this.meeting.Remove(oldMeeting);
               oldMeeting.Secretary = null;
            }
      }
      
      /// <summary>
      /// Remove all instances of Meeting from the collection
      /// </summary>
      /// <pdGenerated>Default removeAll</pdGenerated>
      public void RemoveAllMeeting()
      {
         if (meeting != null)
         {
            System.Collections.ArrayList tmpMeeting = new System.Collections.ArrayList();
            foreach (Meeting oldMeeting in meeting)
               tmpMeeting.Add(oldMeeting);
            meeting.Clear();
            foreach (Meeting oldMeeting in tmpMeeting)
               oldMeeting.Secretary = null;
            tmpMeeting.Clear();
         }
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