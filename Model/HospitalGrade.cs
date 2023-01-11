

using ConsoleApp.serialization;
using System;

namespace CrudModel
{
   public class HospitalGrade : Serializable
    {
        public int staffKindnessGrade { get; set; }
        public int hospitalHygieneGrade { get; set; }
        public int waitingGrade { get; set; }
        public int ambienceGrade { get; set; }
        public int hospitalOrganizationGrade { get; set; }
        public int informationAccessGrade { get; set; }
        public int hospitalGradeID { get; set; }
        public String comment { get; set; }

        public Patient patient;

        public HospitalGrade(int staffKindnessGrade, int hospitalHygieneGrade, int waitingGrade, int ambienceGrade, int hospitalOrganizationGrade, int informationAccessGrade, int hospitalGradeID, String comment)
        {
            this.staffKindnessGrade = staffKindnessGrade;
            this.hospitalHygieneGrade = hospitalHygieneGrade;
            this.waitingGrade = waitingGrade;
            this.ambienceGrade = ambienceGrade;
            this.hospitalOrganizationGrade = hospitalOrganizationGrade;
            this.informationAccessGrade = informationAccessGrade;
            this.hospitalGradeID = hospitalGradeID;
            this.comment = comment;
        }
        public HospitalGrade() 
        {
        }

        public void fromCSV(string[] values)
        {
            staffKindnessGrade = int.Parse(values[0]);
            hospitalHygieneGrade = int.Parse(values[1]);
            waitingGrade = int.Parse(values[2]);
            ambienceGrade = int.Parse(values[3]);
            hospitalOrganizationGrade = int.Parse(values[4]);
            informationAccessGrade = int.Parse(values[5]);
            comment = values[6];
            hospitalGradeID = int.Parse(values[7]);
        }

        public string[] toCSV()
        {
            string[] csvValues =
{               staffKindnessGrade.ToString(),
                hospitalHygieneGrade.ToString(),
                waitingGrade.ToString(),
                ambienceGrade.ToString(),
                hospitalOrganizationGrade.ToString(),
                informationAccessGrade.ToString(),
                comment,
                hospitalGradeID.ToString(),
                };
            return csvValues;
        }
    }
}