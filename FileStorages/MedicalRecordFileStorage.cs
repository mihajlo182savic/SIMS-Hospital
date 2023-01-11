// File:    MedicalRecordFileStorage.cs
// Author:  Dusan
// Created: Sunday, April 3, 2022 8:06:22 PM
// Purpose: Definition of Class MedicalRecordFileStorage

using ConsoleApp.serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace CrudModel
{
   public class MedicalRecordFileStorage
   {
        static public ObservableCollection<MedicalRecord> medicalRecordList
        {
            set;
            get;
        }

        public MedicalRecord getMedialRecordByPatientID(int patientID)
        {
            Serializer<MedicalRecord> medicalRecordSerializer = new Serializer<MedicalRecord>();
            foreach (MedicalRecord mr in medicalRecordSerializer.fromCSV("../../TxtFajlovi/medicalRecords.txt"))
            {
                if (mr.patientID == patientID)
                {
            
                    return mr;
                }
            }
            return null;
        }
        public MedicalRecordFileStorage()
        {
            if (medicalRecordList == null)
            {
                medicalRecordList = new ObservableCollection<MedicalRecord>();
                Serializer<MedicalRecord> medicalRecordSerializer = new Serializer<MedicalRecord>();
                medicalRecordList = medicalRecordSerializer.fromCSV("../../TxtFajlovi/medicalRecords.txt");
            }
        }
        public ObservableCollection<string> getAlergensByPatientId(int patientID)
        {
            MedicalRecord mr = getMedialRecordByPatientID(patientID);
            return mr.alergenList;
        }
        public bool insertAlergen(ObservableCollection<string> alergens,int patientID)
        {
            ObservableCollection<MedicalRecord> mrList;
            MedicalRecord medRec = null;
            medRec = (MedicalRecord)getMedialRecordByPatientID(patientID);
            Serializer<MedicalRecord> medicalRecordSerializer = new Serializer<MedicalRecord>();
            mrList = medicalRecordSerializer.fromCSV("../../TxtFajlovi/medicalRecords.txt");
            foreach(MedicalRecord mr in mrList)
            {
                
                if(mr.patientID == patientID)
                {
                    
                    mr.patientID = patientID;
                    mr.medicalRecordID = medRec.medicalRecordID;
                    mr.alergenList = alergens;
                    break;
                }

            }
          
            medicalRecordSerializer.toCSV("../../TxtFajlovi/medicalRecords.txt", mrList);
            return true;

        }
        public bool CreateMedicalRecord(MedicalRecord newMedicalRecord)
      {
            ObservableCollection<MedicalRecord> mrList;
            Serializer<MedicalRecord> medicalRecordSerializer = new Serializer<MedicalRecord>();
            mrList = medicalRecordSerializer.fromCSV("../../TxtFajlovi/medicalRecords.txt");
            mrList.Add(newMedicalRecord);
            medicalRecordSerializer.toCSV("../../TxtFajlovi/medicalRecords.txt", mrList);
            return true;

        }
      
      public bool DeleteMedicalRecord(int medicalRecordID)
      {
         throw new NotImplementedException();
      }
      
      public bool UpdateMedicalRecord(MedicalRecord medicalRecord)
      {
         throw new NotImplementedException();
      }
      
      public static MedicalRecord GetMedicalRecordByID(int medicalRecordID)
      {
         foreach (MedicalRecord mr in medicalRecordList)
            {
                if (mr.medicalRecordID == medicalRecordID) return mr;
            }
            return null;
      }
      
      public ObservableCollection<MedicalRecord> GetAllMedicalRecord()
      {
            ObservableCollection<MedicalRecord> mrList;
            Serializer<MedicalRecord> medicalRecordSerializer = new Serializer<MedicalRecord>();
            mrList = medicalRecordSerializer.fromCSV("../../TxtFajlovi/medicalRecords.txt");
            return mrList;
        }
      
      public List<MedicalRecord> GetMedicalRecordByPatient(int patientID)
      {
         throw new NotImplementedException();
      }
   
   }
}