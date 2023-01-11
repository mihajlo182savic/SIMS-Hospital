using ConsoleApp.serialization;
using CrudModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Projekat_Bolnica_Zdravo.Model
{
    public class SchedulingMeeting : Serializable
    {
        public int duration
        {
            set;
            get;
        }
        public string meetingRoom
        {
            set;
            get;
        }
        public string timeStart
        {
            get;
            set;
        }
        public int meetingID
        {
            set;
            get;
        }
        public string invitedGroups
        {
            get;
            set;
        }
        public SchedulingMeeting(int id,string room,int duration,string timeStart,string invitedGroups)
        {
            this.meetingID = id;
            this.meetingRoom = room;
            this.duration = duration;
            this.timeStart = timeStart;
            this.invitedGroups = invitedGroups;
        }
        public SchedulingMeeting()
        {
       
        }
        public string[] toCSV()
        {
            string[] csvValues =
            {
                meetingID.ToString(),
                meetingRoom,
                duration.ToString(),
                timeStart.ToString(),
                invitedGroups
            };
            return csvValues;
        }
        public void fromCSV(string[] values)
        {
            this.meetingID = int.Parse(values[0]);
            this.meetingRoom = values[1];
            this.duration = int.Parse(values[2]);
            this.timeStart = values[3];
            this.invitedGroups = values[4];
        }
    }

}
