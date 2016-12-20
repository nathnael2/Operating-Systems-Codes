using System;
using System.Collections.Generic;
using System.Threading;

namespace Homework3 {
    internal class IsNumberPrimeCalculator {
        private SharedObject so;
        

        public IsNumberPrimeCalculator(SharedObject so1) {
              so = so1;   
        }

        public void CheckIfNumbersArePrime() {
			ControlKey controller = new ControlKey();
            while (true) {
				controller.waittill(so,false);  /* grab the lock when it is empty*/
                
                if(so._numbersToCheck.Count == 0)
                {
                    so.flag = false;
                    controller.release(so);
                    return;
                }
                var numberToCheck = so._numbersToCheck.Dequeue();
                if (IsNumberPrime(numberToCheck)) {
                    so._results.Add(numberToCheck);
                    

                }
				so.flag = false;    /*mark as open*/
				controller.release(so);  /* release the lock*/
                
                
            }
        }

        private bool IsNumberPrime(long numberWeAreChecking) {
            const long firstNumberToCheck = 3;

            if (numberWeAreChecking % 2 == 0) {
                return false;
            }
            var lastNumberToCheck = Math.Sqrt(numberWeAreChecking);
            for (var currentDivisor = firstNumberToCheck; currentDivisor < lastNumberToCheck; currentDivisor += 2) {
                if (numberWeAreChecking % currentDivisor == 0) {
                    return false;
                }
            }
            return true;
            
        }
        

    }
}