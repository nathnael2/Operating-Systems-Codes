using System;
using System.Collections.Generic;
using System.Threading;

namespace Homework3 {
    internal class ProgressMonitor {
        private SharedObject so;
        public long TotalCount = 0;

        public ProgressMonitor(SharedObject so1) {
            so = so1;
        }

        public void Run() {
			ControlKey controller = new ControlKey();
			
			
            while (true) {

                
                controller.waittill(so, false);
                int currentcount = so._results.Count;
                
                while (so._results.Count == 0){
                    
                    so.flag = false;
                    controller.release(so);
                    
                    controller.waittill(so, false);

                }
				
                TotalCount += currentcount;

                so._results.Clear(); 
                if (so._numbersToCheck.Count == 0)
                {
                    so.flag = false;
                    controller.release(so);
                    return;
                }

                
                so.flag = false;
				controller.release(so);
                if (currentcount > 0)
                {
                    Console.WriteLine("{0} primes found so far", TotalCount);
                }
               



            }
        }
    }
}