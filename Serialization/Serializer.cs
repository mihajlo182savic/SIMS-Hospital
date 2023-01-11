using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace ConsoleApp.serialization
{
    public class Serializer<T> where T: Serializable, new()
    {
        private static String DELIMITER1 = "|";
        private static char DELIMITER2 = '|';
        public void toCSV(string fileName, ObservableCollection<T> objects)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {

                foreach (Serializable obj in objects)
                {
                    string line = string.Join(DELIMITER1, obj.toCSV());
                    sw.WriteLine(line);
                }
            }
        }

        public ObservableCollection<T> fromCSV(string fileName)
        {
            string fullPath = Path.GetFullPath(fileName);
            ObservableCollection<T> objects = new ObservableCollection<T>();
            foreach(string line in File.ReadLines(fileName))
            {
                string[] csvValues = line.Split(DELIMITER2);
                T obj = new T();
                obj.fromCSV(csvValues);
                objects.Add(obj);
            }

            return objects;
        }
    }
}
