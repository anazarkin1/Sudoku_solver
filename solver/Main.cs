using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
namespace solver
{
	
	class Sudoku
	{
		Stack gameStates = new Stack();
		
		
		
		
	
	}
	class State
	{
		private struct element
		{
		public 	List<int> candidates;
			public int status;
		}
		
		private 	int[,]field = new int[9,9];
		private element[,] mask = new element[9,9];
		public void readField (string filename)
		{
			using (StreamReader inputReader = new StreamReader(filename))
			{
				int number = 0;
				int i = 0, j = 0;
				while ((number = inputReader.Read()) != -1)
				{
					if (j == 10)
					{
						j -= 10;
						i++;
					}
					
					field [i, j] = number;
					int tmp = 1;
					if (number != 0)
					{
						mask [i, j].status = 2;
						
						while (tmp != 10)
						{
							if (tmp == number)
								continue;
							
							mask [i, j].candidates.Add (i);
							tmp++;
						}
					
					} else
					{
						mask [i, j].status = 0;
						
						while (tmp != 10)
						{
							mask [i, j].candidates.Add (i);
							tmp++;
						}

					}
					
					
				}
						
			}
		}
			
		public void showField ()
		{
			foreach (int i in field)
			{
				Console.WriteLine (i + "");
			}		
		}
	}
	
	class MainClass
	{
		public static void Main (string[] args)
		{
			State st = new State();
			st.readField("test.txt");
		
			
		}
	}
}
