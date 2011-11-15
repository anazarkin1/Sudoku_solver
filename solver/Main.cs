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
        public int depth = 0;
        //public TraceSource ts = new TraceSource("mySource");
        //public TextWriterTraceListener tr = new TextWriterTraceListener("log.txt");
        //EventLogTraceListener et = new EventLogTraceListener();
        public bool naiveGuessSol(State state,int i, int j)
        {
           
            //state.showField();
            depth++;
            if (j == 9)//
            {
                j = 0;
                i++;
                if (i == 9)
                    return true;
            }
            if (state.field[i, j] != 0)
                return (naiveGuessSol(state, i, j+1));
            for (int val = 1; val <= 9; ++val)
            {
                if (checkValid(i, j, val, state) == true)
                {
                    state.field[i, j] = val;
                    if (naiveGuessSol(state, i,j+1) == true)
                        return true;
                }
            }
               state.field[i, j] = 0;
            return false;
        }
        public bool goodGuessSol(State state, int i, int j)
        {
           
           // state.showField();
            depth++;
            if (j == 9)//
            {
                j = 0;
                i++;
                if (i == 9)
                {
                   
                    return true;
                }
            }
            if (state.field[i, j] != 0)
                return (goodGuessSol(state, i, j + 1));
            foreach(var val in state.candidates[i,j])
                if (checkValid(i, j, val, state) == true)
                {
                    state.field[i, j] = val;
                    if (goodGuessSol(state, i, j + 1) == true)
                        return true;
                }
            
            state.field[i, j] = 0;
            return false;
        }

        public bool checkValid(int i, int j, int val, State tmp)
        {
            for (int k = 0; k < 9; ++k)
                if ((val == tmp.field[k,j]) )
                    return false;
            for (int k = 0; k < 9; ++k)
                if ((val == tmp.field[i,k]) )
                    return false;
            int boxRowOffset = (i / 3) * 3;
            int boxColOffset = (j / 3) * 3;
            for(int k = 0; k < 3; ++k)
                for(int m = 0; m < 3; ++m)
                    if((val==tmp.field[boxRowOffset+k,boxColOffset+m]))
                        return false;
            return true;
            
        }
        static int Main()
        {
            

              
            Log.Info("Program started", "");
            State st = new State();
            try
            {                          
              
                st.readFromFile("test.txt");




                
                st.checkSquareCandidates();
                Log.Info("Performed checkSquareCandidates()", "Main.cs, Main()");

                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        Console.Write(":{0}:",st.field[i,j]);
                        foreach (var k in st.candidates[i, j])
                            Console.Write("{0} ", k);
                        Console.WriteLine();
                    }
               // st.showField();
                Console.WriteLine("RESULTS:");
                Sudoku s = new Sudoku();
              
                s.goodGuessSol(st,0,0);
                Console.WriteLine("DEPTH = {0}", s.depth);
              
                st.showField();
              
               
                Log.Info(String.Format("isCorrect() returned : {0}", st.isCorrect()), "Main.cs,Main()");
                Log.Info(String.Format("isCompleted() returned : {0}", st.isCompleted()), "Main.cs,Main()");
               
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
