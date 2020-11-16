using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infinitint
{

    class Program
    {
        static void Main(string[] args)
        {
            int j = 0, i = 0, k = 0; 

            // read all the lines and put in string array
            string[] ReadFile = System.IO.File.ReadAllLines(@"D:\Desktop\Projects\infinitint/TextFile1.txt");
            //  every third line from textfile is either first operand, second or operation so we create array of size of all lines divided by 3
            INFINT[] Operand1 = new INFINT[ReadFile.Count() / 3];
            INFINT[] Operand2 = new INFINT[ReadFile.Count() / 3];
            string[] operation = new string[ReadFile.Count() / 3];


            while(i < ReadFile.Count())
            {
                Operand1[j] = new INFINT(ReadFile[i]); i++; //each list holds list of integers
                Operand2[j] = new INFINT(ReadFile[i]); i++;
                operation[j] = ReadFile[i]; i++;
                j++;
            }

            //if there is n lines, program does n/3 operations
            while(k < ReadFile.Count()/3)
            {
                //calling functions..
                if (operation[k] == "+")
                {
                    Operand1[k].addition(Operand2[k]);
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine();

                }
                else if (operation[k] == "-")
                {
                    Operand1[k].substraction(Operand2[k]);
                    Console.WriteLine();

                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine();

                }

                else if (operation[k] == "*")
                {
                    Operand1[k].multiply(Operand2[k]);
                    Console.WriteLine();

                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------");
                }

                k++;
            }

        }

    }
}

