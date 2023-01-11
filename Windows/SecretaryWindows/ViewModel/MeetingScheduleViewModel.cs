using CrudModel;
using MVVM;
using SIMS_Projekat_Bolnica_Zdravo.Controllers;
using SIMS_Projekat_Bolnica_Zdravo.CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Model;
using SIMS_Projekat_Bolnica_Zdravo.Services;
using SIMS_Projekat_Bolnica_Zdravo.Windows.SecretaryWindows.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Windows.SecretaryWindows.ViewModel
{
    public class MeetingScheduleViewModel: BindableBase
    { 
        MeetingService meetingService;
        MeetingController meetingController;
        public MyICommand ConfirmCommand { get; set; }
        public MyICommand ReverseCommand { get; set; }

        public RoomCrAppDTO selectedRoom
        {
            get;
            set;
        }
        public BindingList<string> timeList
        {
            get;
            set;
        }
        public ObservableCollection<SchedulingMeeting> meetingList
        {
            get;
            set;
        }
        public ObservableCollection<string> roomList
        {
            get;
            set;
        }
        public SchedulingMeeting newMeeting
        {
            get;
            set;
        }
        
        public bool secretars
        {
            get;
            set;
        }
        public bool doctors
        {
            get;
            set;
        }
        public bool managers
        {
            get;
            set;
        }
        public string invited
        {
            get;
            set;
        }
        public string Room
        {
            get { return newMeeting.meetingRoom; }
            set
            {
                if (value != newMeeting.meetingRoom)
                {
                    newMeeting.meetingRoom = value;
                    OnPropertyChanged("Room");
                }
            }
        }
        public string TimeStart
        {
            get { return newMeeting.timeStart; }
            set
            {
                if (value != newMeeting.timeStart)
                {
                    newMeeting.timeStart = value;
                    OnPropertyChanged("TimeStart");
                }
            }
        }

        public bool Secretars
        {
            get { return secretars; }
            set
            {
                if (value != secretars)
                {
                    secretars = value;
                    OnPropertyChanged("Secretars");
                }
            }
        }
        public bool Doctors
        {
            get { return doctors; }
            set
            {
                if (value != doctors)
                {
                    doctors = value;
                    OnPropertyChanged("Doctors");
                }
            }
        }
        public bool Managers
        {
            get { return managers; }
            set
            {
                if (value != managers)
                {
                    managers = value;
                    OnPropertyChanged("Managers");
                }
            }
        }

        public RoomCrAppDTO SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                if (value != selectedRoom)
                {
                    selectedRoom = value;
                    OnPropertyChanged("RoomList");
                }
            }
        }


        public MeetingScheduleViewModel()
        {
            newMeeting = new SchedulingMeeting();
            fillDataGrid();
            ReverseCommand = new MyICommand(OnReverse);
            ConfirmCommand = new MyICommand(OnConfirm);
            fillRoomCB();
            fillTimeCB();
            meetingService = new MeetingService();
            
        }
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
        private void OnReverse()
        {

            CloseAllWindows();
            SecretaryWindow secretaryWindow = new SecretaryWindow();
            secretaryWindow.Show();
                    
            

        }
        private void OnConfirm()
        {
            meetingController = new MeetingController();
            if (Secretars == true) invited += " Secretary ";
            if (Doctors == true) invited += " Doctors ";
            if (Managers == true) invited += " Managers "; 
            SchedulingMeeting newMeeting = new SchedulingMeeting(1, Room, 30, TimeStart, invited);
            invited = "";
            meetingController.createMeeting(newMeeting);
            meetingList.Add(newMeeting);
        }
        private void fillRoomCB()
        {
            RoomController roomController = new RoomController();
            ObservableCollection<RoomCrAppDTO> tempList = new ObservableCollection<RoomCrAppDTO>();
            roomList = new ObservableCollection<string>();

            tempList = roomController.getAllRoomsDTO();
            foreach (RoomCrAppDTO room in tempList)
            {
                roomList.Add(room.name);
            }
            
          
        }
        private void fillDataGrid()
        {

            MeetingController meetingController = new MeetingController();
            meetingList = meetingController.getAllMeetings();

        }
        private void fillTimeCB()
        {
            AppointmentController appointmentController = new AppointmentController();
            BindingList<TimePatient> tempTime = new BindingList<TimePatient>();
            timeList = new BindingList<string>();
            tempTime = appointmentController.getAllTimes();
            foreach(TimePatient time in tempTime)
            {
                timeList.Add(time.time);
            }
            
          
        }
    }
}

