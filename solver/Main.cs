using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
namespace solver
{
	
	class Sudoku
	{


        public Sudoku(string fileName)
        {
            readField(fileName);
        }

        /// <summary>
        /// Reads sudoku puzzle from file
        /// </summary>
        /// <param name="filename">Receives filename with .txt type</param>
        /// <returns>Returns 0 if successfully, 1 if unsuccessfuly</returns>
        int  readField(string filename)
        {

            StreamReader streamReader = new StreamReader(filename);
            string text = streamReader.ReadToEnd();
            string[] words = text.Split();
            int i = 0, j = 0, k = 0;

            //while (k < words.Length)
            //{
            //    if (j == 10)
            //    {
            //        j = 0;
            //        i++;
            //    }
            //    Int32.TryParse(words[i], out field[i, j]);
            //    if (field[i, j] == 0)
            //    {

            //        // mask[i, j].candidates.AddRange(range) ;
            //        //mask [i, j].status = 1;
            //    }
            //    else
            //        //	mask [i, j].status = 0;


            //        k++;

            //}
            return 0;
        }


		Stack gameStates = new Stack();
        /// <summary>
        /// Function checks sudoku, whether it's completed or not
        /// </summary>
        /// <param name="curState">Receive State param</param>
        /// <returns>Return 0 if completed, 1 if not completed</returns>
        public int isCompleted(State curState)
        {
            if (isCorrect(curState) == 1)
                return 1;
            else
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        if (curState.field[i, j].candidates.Count != 0)
                            return 1;
                    }
            }
             return 0;
        }
        /// <summary>
        /// Checks sudoku, whether it is correct or not
        /// </summary>
        /// <param name="curState">Receives State param</param>
        /// <returns>Return 0 if correct, 1 if not correct</returns>
        public int isCorrect(State curState)
        {

            return 0;
        }
        
		
		
	
	}
	class State
	{


        private static readonly List<int> defaultPossibs = Enumerable.Range(1, 9).ToList();

        /// <summary>
        /// Resets candidates of the whole field to default(1-9)
        /// </summary>
        /// <returns>Returns 0 if successfully reseted, 1 if unsuccessfully</returns>
        private int resetCandidates()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    field[i, j].candidates.Clear();
                    field[i, j].candidates.AddRange(defaultPossibs);
                }
            return 0;
        }


        public State()
        {
            resetCandidates();
        }


		
		public struct element
		{
            public int value;
		    public 	List<int> candidates;
			public int status;	//0=set
								//1 = unchecked(never touched it before)
								//2 = unknown
		}
		
		public 	element[,]field = new element[9,9];
			
		
			
		public void showField ()
		{
			foreach (var i in field)
			{
				Console.WriteLine (i + "");
			}		
		}
	}
	
	class MainClass
	{
		public static void Main (string[] args)
		{
            Sudoku sd = new Sudoku("test.txt");


            Console.WriteLine("Successfully Finished");
            Console.ReadLine();
			
		}
	}
}
