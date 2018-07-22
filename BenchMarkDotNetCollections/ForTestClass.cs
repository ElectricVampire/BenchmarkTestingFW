using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeedTest;

namespace BenchMarkDotNetCollections
{
    [SpeedTestClassAttribute]
    class ForSpeedTest
    {
        public List<int> list = null;

        //[SpeedTestMethod(1000, "I am faster than ForEach", "InitializeList", "EmptyList")]
        public void ForTest()
        {
            for (int i = 0; i < list.Count; i++)
            {
            }
        }

       [SpeedTestMethod(1000, "ForEach is always slower than For", "InitializeList", "EmptyList")]
        public void ForEachTest()
        {
            foreach (int i in list)
            {
            }
        }

        
        public void EmptyList()
        {
            list = null;
        }

        public void InitializeList()
        {
            list = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(i);
            }
        }
    }
}
