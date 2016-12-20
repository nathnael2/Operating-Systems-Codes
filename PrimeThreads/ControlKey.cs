using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3
{
    class ControlKey
    {
        public int markAsTaken(SharedObject so)
        {
            so.flag = true;
            while (so.flag) { }
            return 1;
        }

        public int markAsFree(SharedObject so)
        {
            so.flag = false;
            while (!so.flag) { }
            return 1;
        }

        public bool waittill(SharedObject so, bool flag1)
        {
            while (true)
            {
                so.m.WaitOne();
                if (so.flag == flag1)
                {
                    return true; /* return with the object locked*/
                }
                so.m.ReleaseMutex();
            }
        }

        public bool release(SharedObject so)
        {
            so.m.ReleaseMutex();
            return true;
        }
    }
}
