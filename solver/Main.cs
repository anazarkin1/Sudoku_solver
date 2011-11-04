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
		int[] range = new int[9]{1,2,3,4,5,6,7,8,9};
		
		private struct element
		{
		public 	List<int> candidates;
			public int status;	//0=set
								//1 = unchecked(never touched it before)
								//2 = unknown
		}
		
		private 	int[,]field = new int[9,9];
		private element[,] mask = new element[9,9];
		public void readField (string filename)
		{
			StreamReader streamReader = new StreamReader (filename);
			string text = streamReader.ReadToEnd ();
			string [] words = text.Split ();
			int i = 0, j=0, k = 0;
			while (k<words.Length)
			{
				if (j == 10)
				{
					j = 0;
					i++;
				}
				Int32.TryParse (words [i], out field [i, j]);
				if (field [i, j] == 0)
				{	
					mask [i, j].candidates.Add(1);
					mask [i, j].status = 1;
				} else
					mask [i, j].status = 0;
				
				
				k++;
				
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
