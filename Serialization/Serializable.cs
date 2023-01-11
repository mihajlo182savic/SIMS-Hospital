using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.serialization
{
    public interface Serializable
    {

        string[] toCSV();

        void fromCSV(string[] values);

    }
}
