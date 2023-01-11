using CrudModel;
using SIMS_Projekat_Bolnica_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Services
{
    public class MeetingService
    {
        MeetingFileStorage meetingFileStorage = new MeetingFileStorage();

        public ObservableCollection<SchedulingMeeting> getAllMeetings()
        {
            return meetingFileStorage.GetAllMeeting();
        }
        public bool createMeeting(SchedulingMeeting newMeeting)
        {
            return meetingFileStorage.CreateMeeting(newMeeting);
        }
    }
}
