// File:    VacationRequesFileStorage.cs
// Author:  duros
// Created: Tuesday, May 10, 2022 8:59:57 PM
// Purpose: Definition of Class VacationRequesFileStorage
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using ConsoleApp.serialization;
using System.Windows;

namespace CrudModel
{
   public class VacationRequesFileStorage
   {
      public bool CreateVacationRequest(VacationRequest crVR)
      {
            ObservableCollection<VacationRequest> patients = new ObservableCollection<VacationRequest>();
            Serializer<VacationRequest> patientSerializer = new Serializer<VacationRequest>();
            foreach (VacationRequest p in patientSerializer.fromCSV("../../TxtFajlovi/vacationrequests.txt"))
            {
                patients.Add(p);
            }
            patients.Add(crVR);
            patientSerializer.toCSV("../../TxtFajlovi/vacationrequests.txt", patients);
            return true;
      }

      public VacationRequest GetVacationRequestById(int userID)
      {
         throw new NotImplementedException();
      }
      
      public ObservableCollection<VacationRequest> GetAllVacationRequestsByDoctorId(int doctorID)
      {
            ObservableCollection<VacationRequest> vacreq = new ObservableCollection<VacationRequest>();
            Serializer<VacationRequest> patientSerializer = new Serializer<VacationRequest>();
            foreach (VacationRequest p in patientSerializer.fromCSV("../../TxtFajlovi/vacationrequests.txt"))
            {
                if(p.doctorID == doctorID)
                    vacreq.Add(p);
            }
            return vacreq;
       }
        public bool UpdateVacationState(int vacationID,StateEnum enumVr)
        {
            ObservableCollection<VacationRequest> vacations = new ObservableCollection<VacationRequest>();
            Serializer<VacationRequest> patientSerializer = new Serializer<VacationRequest>();
            vacations = patientSerializer.fromCSV("../../TxtFajlovi/vacationrequests.txt");
            foreach (VacationRequest p in vacations)
            {
                if (p.id == vacationID)
                {
                   
                    p.state = enumVr;
                    break;
                }
            }
            patientSerializer.toCSV("../../TxtFajlovi/vacationrequests.txt", vacations);
            return true;
           
        }
        public ObservableCollection<VacationRequest> GetAllVacations()
        {
            ObservableCollection<VacationRequest> vacreq = new ObservableCollection<VacationRequest>();
            Serializer<VacationRequest> patientSerializer = new Serializer<VacationRequest>();
            foreach (VacationRequest p in patientSerializer.fromCSV("../../TxtFajlovi/vacationrequests.txt"))
            {
                    vacreq.Add(p);
            }
            return vacreq;
        }

        public ObservableCollection<VacationRequest> GetAllVacationRequests()
        {
            ObservableCollection<VacationRequest> vacreq = new ObservableCollection<VacationRequest>();
            Serializer<VacationRequest> patientSerializer = new Serializer<VacationRequest>();
            foreach (VacationRequest p in patientSerializer.fromCSV("../../TxtFajlovi/vacationrequests.txt"))
            {
                vacreq.Add(p);
            }
            return vacreq;
        }

    }
}