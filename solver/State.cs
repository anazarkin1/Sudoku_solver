using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Logging;
namespace solver
{
	public class State
	{
        //
        public List<int> defaults = Enumerable.Range(1, 9).ToList<int>();
		public int[,] field = new int[9,9];
		public int[,] status = new int[9,9];
		public List<int>[,] candidates = new List<int>[9,9];
		public State ()
		{
            resetAllCandidates();
            resetAllValues();

		}
        /// <summary>
        /// Resets all candidates to default range(1-9)
        /// </summary>
        public void resetAllCandidates()
        {
            for(int i =0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    candidates[i, j] = new List<int>(defaults);
                }
        }


        /// <summary>
        /// Returns array with the current column values
        /// </summary>
        /// <param name="x">Receives column number</param>
        /// <returns></returns>
        private int[] getColumnValues(int x)
        {
            int[] colVals = new int[9];
            for (int i = 0; i < 9; i++)
            {
                colVals[i] = field[x, i];
            }

            return colVals;
        }

        /// <summary>
        /// Returns array with the current row values
        /// </summary>
        /// <param name="x">Receives row number</param>
        /// <returns></returns>
        private int[] getRowValues(int x)
        {

            int[] rowVals = new int[9];
            for (int i = 0; i < 9; i++)
            {
                rowVals[i] = field[i,x];
            }
            return rowVals;

        }

        /// <summary>
        /// Returns array with the current cell neighbours
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <returns></returns>
        public int[] getSquareNeighbours(int x, int y)
        {
            int[] squareNeighs = new int[9];
            
            int minx = (x / 3) * 3;
            int miny = (y / 3) * 3;
            int maxy = miny + 3;
            int maxx = minx + 3;
            int counter = 0;

            for (int i = minx; i < maxx; i++)
            {

                for (int j = miny; j < maxy; j++)
                {
                    squareNeighs[counter] = field[i, j];
                    counter++;
                }
            }

          

            return squareNeighs;
        }

        /// <summary>
        /// Sets possible candidates for each square
        /// </summary>
        public void checkSquareCandidates()
        {
            bool flag = false;
            int i = 0, j = 0;
            while (i<9)
            {
                if (isCorrect() == false)
                {
                    Log.Error(String.Format("isCorrect() returned false on {0},{1}",i,j), "State.cs,checkSquareCandidates()");
                    return;
                }
                if (field[i, j] == 0 && candidates[i, j].Count == 1)
                {
                    field[i, j] = candidates[i, j].First();
                    candidates[i, j].Clear();

                    flag = true;
                }
                else if (field[i, j] == 0)
                {
                    int[] rowVals = getRowValues(j);
                    int[] colVals = getColumnValues(i);
                    int[] squareNeighbours = getSquareNeighbours(i, j);
                    
                    for (int k = 0; k < rowVals.Length; k++)
                        candidates[i, j].Remove(rowVals[k]);
                    for(int k  =0; k < colVals.Length;k++)
                        candidates[i, j].Remove(colVals[k]);
                    for(int k =0; k < squareNeighbours.Length;k++)
                        candidates[i, j].Remove(squareNeighbours[k]);
                    flag  = true;
                }
                else if (field[i, j] != 0)
                {
                    candidates[i, j].Clear();
                }
                    j++;

                if (j > 8)
                { i++; j = 0; }
                if(i>8 && flag)
                { i = 0; j = 0; flag = false; }
                             
            }
        }

        /// <summary>
        /// Resets all values in field[,] to 0
        /// </summary>
        public void resetAllValues()
        {

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    field[i, j] = 0;
        }

       
        /// <summary>
        /// Reads Puzzle from File
        /// </summary>
        /// <param name="filename">File name with Puzzle</param>
        public void readFromFile(string filename)     
		{
			using (TextReader reader = File.OpenText("test.txt"))
			{
                int counter = 0;
                
                for(int i = 0; i < 9; i++)
                {

                    string text = reader.ReadLine();
                    for (int j = 0; j < 9; j++)
                    {
                        int num = 0;
                        if (Char.IsDigit(text[j]))
                        {
                            Int32.TryParse(text[j].ToString(), out num);
                           
                                candidates[i, j] = new List<int>(defaults);
                                status[i, j] = 1;
                           
                        }
                        field[i, j] = num;
                        counter++;
                    }
                }
                
			}
		}

        /// <summary>
        /// Prints current field arrays
        /// </summary>
        public void showField()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write("{0} ", field[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("");
        }

        /// <summary>
        /// Check if puzzle is correct at current state
        /// </summary>
        /// <returns>False if is not correct, True if is correct</returns>
        public bool isCorrect()
        {
            for(int i =0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    if (field[i, j] == 0 && candidates[i, j].Count == 0)
                    {
                        Log.Error(String.Format("Cell {0}x{1} has no candidates and currently is not set", i, j),"State.cs,isCorrect()");
                        return false;
                    }
                }


            return true;

        }

        /// <summary>
        /// Checks if puzzle is completed and correct
        /// </summary>
        /// <returns>False if is not completed, True if is completed</returns>
        public bool isCompleted()
        {
            if (isCorrect() != true)
            {
                return false;
            }
            else
            {
                for(int i =0; i< 9; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        if (field[i, j] == 0)
                            return false;
                        }
            }

            return true;

        }


        //static int Main()
        //{
        //    Log.Info("Program started", "");
        //    try
        //    {
               
        //        State st = new State ();
                
        //        st.readFromFile("test.txt");
        //        st.showField();
        //    }
           
           
        //     catch (Exception ex)
        //    {
        //        Log.Error(ex, "State.cs, Main()");
        //    }
            
        //    Log.Info("Successfully exiting","State.cs, Main()");
        //    return 0;
        //}
	}
}

