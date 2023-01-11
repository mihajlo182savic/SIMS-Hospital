// File:    MeetingFileStorage.cs
// Author:  Dusan
// Created: Sunday, April 3, 2022 7:55:27 PM
// Purpose: Definition of Class MeetingFileStorage

using ConsoleApp.serialization;
using SIMS_Projekat_Bolnica_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrudModel
{
   public class MeetingFileStorage
   {
      public bool CreateMeeting(SchedulingMeeting newMeeting)
      {
            ObservableCollection<SchedulingMeeting> mrList = new ObservableCollection<SchedulingMeeting>();
            Serializer<SchedulingMeeting> medicalRecordSerializer = new Serializer<SchedulingMeeting>();
            mrList = medicalRecordSerializer.fromCSV("../../TxtFajlovi/meetings.txt");
            mrList.Add(newMeeting);
            medicalRecordSerializer.toCSV("../../TxtFajlovi/meetings.txt", mrList);
            return true;

        }

      public bool DeleteMeeting(int meetingID)
      {
         throw new NotImplementedException();
      }
      
      public bool UpdateMeeting(Meeting meeting)
      {
         throw new NotImplementedException();
      }
      
      public Meeting GetMeetingByID(int meetingID)
      {
         throw new NotImplementedException();
      }
      
      public ObservableCollection<SchedulingMeeting> GetAllMeeting()
      {
            ObservableCollection<SchedulingMeeting> mrList;
            Serializer<SchedulingMeeting> medicalRecordSerializer = new Serializer<SchedulingMeeting>();
            mrList = medicalRecordSerializer.fromCSV("../../TxtFajlovi/meetings.txt");
            return mrList;
        }
   
   }
}