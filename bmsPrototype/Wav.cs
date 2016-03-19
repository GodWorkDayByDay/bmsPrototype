using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bmsPrototype
{
    public class Wav
    {
        public string FileName;
        public int Number;

        public Wav(string str, int num)
        {
            FileName = str;
            Number = num;
        }
    }
}
