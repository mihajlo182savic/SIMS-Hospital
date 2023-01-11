using SIMS_Projekat_Bolnica_Zdravo.Model;
using SIMS_Projekat_Bolnica_Zdravo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Controllers
{
    public class MeetingController
    {
        MeetingService meetingService = new MeetingService();

        public ObservableCollection<SchedulingMeeting> getAllMeetings()
        {
            return meetingService.getAllMeetings();
        }
        public bool createMeeting(SchedulingMeeting newMeeting)
        {
            return meetingService.createMeeting(newMeeting);
        }
    }
}
