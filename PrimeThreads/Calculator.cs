using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Homework3 {
    struct SharedObject
	{
		public List<long> _results;
	    public Queue<long> _numbersToCheck;
		public bool flag;
		public Mutex m;
	}
    

    internal class Calculator {
		
		

        public void Run(NumberReader reader) {
			
			SharedObject so;
			so._results = new List<long>();
			so._numbersToCheck = new Queue<long>();
			so.m = new Mutex();
			so.flag = false;
            ControlKey controller = new ControlKey();

            var results = new List<long>();
            var numbersToCheck = new Queue<long>();
			
			foreach (var value in reader.ReadIntegers()) {
                so._numbersToCheck.Enqueue(value);
            }
            

            StartComputationThreads(so);

            var progressMonitor = new ProgressMonitor(so);
			
            
            new Thread(progressMonitor.Run) {IsBackground = true}.Start();
            
            
           
            
            while (true) {
                
                controller.waittill(so, false);
                
                if (so._numbersToCheck.Count == 0)
                {
                    
                    Thread.Sleep(1000);
                    break;
                }
                else
                {
                    so.flag = false;
                    controller.release(so);
                    
                }


            }
            Console.WriteLine("{0} of the numbers were prime", progressMonitor.TotalCount);
        }

        private static void StartComputationThreads(SharedObject so) {
            var threads = CreateThreads(so);
            threads.ForEach(thread => thread.Start());
        }
        
        private static List<Thread> CreateThreads(SharedObject so) {
            var threadCount = Environment.ProcessorCount*2;

            Console.WriteLine("Using {0} compute threads and 1 I/O thread", threadCount);

            var threads =
                (from threadNumber in Sequence.Create(0, threadCount)
                    let calculator = new IsNumberPrimeCalculator(so)
                    let newThread =
                        new Thread(calculator.CheckIfNumbersArePrime) {
                            IsBackground = true,
                            Priority = ThreadPriority.BelowNormal
                        }
                    select newThread).ToList();
            return threads;
        }
    }
}
