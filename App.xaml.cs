using ConsoleApp.serialization;
using CrudModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Projekat_Bolnica_Zdravo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {

            //Serializer<MedicalRecord> medicalRecorderializer = new Serializer<MedicalRecord>();
            //medicalRecorderializer.toCSV("medicalRecords.txt", MedicalRecordFileStorage.medicalRecordList);
            //Serializer<Patient> patientSerializer = new Serializer<Patient>();
            ////patientSerializer.toCSV("patients.txt", PatientFileStorage.patientList);
            //Serializer<Doctor> doctorSerializer = new Serializer<Doctor>();
            //doctorSerializer.toCSV("doctors.txt", DoctorFileStorage.doctorList);

            //Serializer<Secretary> secretarySerializer = new Serializer<Secretary>();
            //secretarySerializer.toCSV("secretary.txt", SecretaryFileStorage.secretaryList);
            //Serializer<Manager> managersSerializer = new Serializer<Manager>();
            //managersSerializer.toCSV("managers.txt", ManagerFileStorage.managerList);

            //Serializer<Appointment> appoitmentSerializer = new Serializer<Appointment>();
            //appoitmentSerializer.toCSV("appoitments.txt", AppointmentFileStorage.appointmentList);

            //Serializer<Room> roomSerializer = new Serializer<Room>();
            ////roomSerializer.toCSV("room.txt", RoomFileStorage.roomList);

            //Serializer<Note> noteSerializer = new Serializer<Note>();
            //noteSerializer.toCSV("notes.txt", NoteFileStorage.noteList);
            Serializer<IdsStorage> idsSerializer = new Serializer<IdsStorage>();
            new IdsStorage();
            idsSerializer.toCSV("../../TxtFajlovi/ids.txt", IdsStorage.IDS);
        }
    }
}
