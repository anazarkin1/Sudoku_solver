using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Logging;
namespace solver
{
	
	class Sudoku
	{
        //public TraceSource ts = new TraceSource("mySource");
        //public TextWriterTraceListener tr = new TextWriterTraceListener("log.txt");
        //EventLogTraceListener et = new EventLogTraceListener();

        static int Main()
        {
            Log.Info("Program started", "");
            State st = new State();
            try
            {
                            
                //
                st.readFromFile("test.txt");
                st.showField();
                //for (int i = 0; i < 9; i++)
                //    for (int j = 0; j < 9; j++)
                //    {
                //        foreach (var el in st.getSquareNeighbours(i, j))
                //            Console.Write(el + " ");
                //        Console.WriteLine();
                //    }

                st.checkSquareCandidates();
                //for (int i = 0; i < 9; i++)

                //    for (int j = 0; j < 9; j++)
                //    {
                //        foreach (var el in st.candidates[i, j])
                //            Console.Write(el.ToString() + " ");
                //        Console.WriteLine();
                //    }
                Log.Info("Performed checkSquareCandidates()", "Main.cs, Main()");

                Log.Info(String.Format("isCorrect() returned : {0}", st.isCorrect()), "Main.cs,Main()");
                Log.Info(String.Format("isCompleted() returned : {0}", st.isCompleted()), "Main.cs,Main()");
                st.showField();
                Console.ReadKey();
            }


            catch (Exception ex)
            {
                Log.Error(ex.Message+ex.TargetSite+ex.StackTrace, "State.cs, Main()");
            }

            Log.Info("Successfully exiting", "Main.cs, Main()");
            return 0;
        }
	}
}
