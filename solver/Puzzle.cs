using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;
namespace solver
{
    static public class Puzzle
    {
        static State st = new State();
       
        public static int depth = 0;

        public static bool useBacktrackingAlgorithm(State state, int i, int j)
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
            foreach (var val in state.candidates[i, j])
                if (checkValid(i, j, val, state) == true)
                {
                    state.field[i, j] = val;
                    if (goodGuessSol(state, i, j + 1) == true)
                        return true;
                }

            state.field[i, j] = 0;
            return false;
        }

        public static bool checkValid(int i, int j, int val, State tmp)
        {
            for (int k = 0; k < 9; ++k)
                if ((val == tmp.field[k, j]))
                    return false;
            for (int k = 0; k < 9; ++k)
                if ((val == tmp.field[i, k]))
                    return false;
            int boxRowOffset = (i / 3) * 3;
            int boxColOffset = (j / 3) * 3;
            for (int k = 0; k < 3; ++k)
                for (int m = 0; m < 3; ++m)
                    if ((val == tmp.field[boxRowOffset + k, boxColOffset + m]))
                        return false;
            return true;

        }


        public static void readFromFile(string fileName)
        {
            st.readFromFile(fileName);
        }
        public static void useAlgorithms()
        {
            st.checkSquareCandidates();
        }
        public static void showField()
        {
            st.showField();
        }
        static int Main()
        {

           

            Log.Info("Program started", "");
          
            try
            {
                Puzzle.readFromFile("test.txt");
                Puzzle.useAlgorithms();
                Log.Info("Performed checkSquareCandidates()", "Main.cs, Main()");

                
                Console.WriteLine("RESULTS:");
            

                Puzzle.useBacktrackingAlgorithm(st, 0, 0);
                Console.WriteLine("DEPTH = {0}", Puzzle.depth);
                Puzzle.showField();
                


                Log.Info(String.Format("isCorrect() returned : {0}", st.isCorrect()), "Main.cs,Main()");
                Log.Info(String.Format("isCompleted() returned : {0}", st.isCompleted()), "Main.cs,Main()");

                Console.ReadKey();
            }


            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.TargetSite + ex.StackTrace, "State.cs, Main()");
            }

            Log.Info("Successfully exiting", "Main.cs, Main()");
            return 0;
        }
    }
}
