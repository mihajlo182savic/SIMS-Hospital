using ConsoleApp.serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Projekat_Bolnica_Zdravo.CrudModel
{
    public class Medicine : Serializable
    {
        public Medicine() { }
        public Medicine(string name,List<StringValue> content)
        {
            this.name = name;
            this.content = content;
            this.allComponents = "";
            this.approved = 0;
            this.id = ids++;
            int i = 0;
            foreach(StringValue s in content)
            {
                if(content.Count > i + 1)
                this.allComponents += s.Value + ",";
                else
                {
                    this.allComponents += s.Value;
                }
                i++;
            }
        }

        public string[] toCSV()
        {
            string[] csvValues =
                {
                name,
                allComponents,
                approved.ToString(),
                id.ToString()
                };
            return csvValues;
        }

        public void fromCSV(string[] values)
        {
            name = values[0];
            approved = int.Parse(values[2]);
            id = int.Parse(values[3]);
            allComponents = values[1];
            content = new List<StringValue>();
            string[] sts = values[1].Split(',');
            for (int i = 0; i < sts.Length; i++)
            {
                content.Add(new StringValue(sts[i]));
            }
        }

        private static int ids;

        public static int getids()
        {
            return ids;
        }

        public static void setids(int set)
        {
            ids = set;
        }

        public int id
        {
            set;
            get;
        }
        public int approved
        {
            set;
            get;
        }
        public string name
        {
            set;
            get;
        }

        public List<StringValue> content
        {
            set;
            get;
        }

        public string allComponents
        {
            set;
            get;
        }

    }
}
