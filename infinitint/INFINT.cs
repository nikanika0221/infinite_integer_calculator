using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infinitint
{
    public class INFINT
    {
        List<int> InfNumber = new List<int>();      //infinite integer list for input
        List<int> result = new List<int>();          //infinite integer list for result
        int negative { get; set; }      //to see which inputs are negative
        bool transferred = false;       //to transfer from one function to another

        public INFINT(string list1)     //constructor
        {
            //if character on index 0 is minus sign, then remember it in variable negative and start creating list from index 1
            if (list1[0] == '-') negative = 1;
            else negative = 0;

            for (int i = negative; i < list1.Length; i++)
            {
                InfNumber.Add(int.Parse(list1[i].ToString())); //construct
            }
        }


        /******************* ADDITION *****************/

        public void addition(INFINT additive)
        {

            if ((this.negative == 1 || additive.negative == 1) && !transferred) // if any of the operands are negative, use subtraction instead
            {
                transferred = true;
                substraction(additive);
            }
            else
            {
                if (this.negative == 1)
                    Console.Write("-");
                this.InfNumber.ForEach(Console.Write);
                if (transferred)
                    Console.Write(" - ");       //if function is transferred from subtraction then write "-" sign otherwise "+"
                else
                    Console.Write(" + ");
                if (additive.negative == 1)
                    Console.Write("(-)");
                additive.InfNumber.ForEach(Console.Write);
                Console.Write(" = ");

                transferred = false;
                this.InfNumber.Reverse();       //sort from last to very first to make calculations
                additive.InfNumber.Reverse();
                int carry = 0;                  //carry for calculations
                if (this.InfNumber.Count >= additive.InfNumber.Count)       //how many times should program operate addition
                {
                    for (int j = 0; j < this.InfNumber.Count; j++)
                    {
                        if ((this.InfNumber.ElementAtOrDefault(j) + additive.InfNumber.ElementAtOrDefault(j) + carry) > 9)
                        {
                            //if sum is greater than 9 subtract 10 remember carry and add to result list
                            result.Add((this.InfNumber.ElementAtOrDefault(j) + additive.InfNumber.ElementAtOrDefault(j)) - 10 + carry);
                            carry = 1;
                        }
                        else
                        {
                            //if sum is not greater than 9 then just add to result list
                            result.Add((this.InfNumber.ElementAtOrDefault(j) + additive.InfNumber.ElementAtOrDefault(j)) + carry);
                            carry = 0;
                        }
                    }
                    if (carry == 1)     //when all the elements are summed if program still has carry = 1, write it at the end of list
                        result.Add(1);
                }
                else if (this.InfNumber.Count < additive.InfNumber.Count) //same operations but if second list has more elements
                {
                    for (int j = 0; j < additive.InfNumber.Count; j++)
                    {
                        if ((this.InfNumber.ElementAtOrDefault(j) + additive.InfNumber.ElementAtOrDefault(j) + carry) > 9)
                        {
                            result.Add((this.InfNumber.ElementAtOrDefault(j) + additive.InfNumber.ElementAtOrDefault(j)) - 10 + carry);
                            carry = 1;
                        }
                        else
                        {
                            result.Add((this.InfNumber.ElementAtOrDefault(j) + additive.InfNumber.ElementAtOrDefault(j)) + carry);
                            carry = 0;
                        }
                    }
                    if (carry == 1)
                        result.Add(1);
                }
                result.Reverse();       //then reversed result again and display answer
                if ((this.negative == 1 && additive.negative == 1) || (this.negative == 1 && additive.negative == 0))
                    Console.Write("-");     // if programm added negative numbers than writes - front of it
                foreach (int element in result)     //display all the elements of answer
                {
                    Console.Write(element);
                }
                Console.WriteLine();
            }
        }


        /********************************* Substraction *******************************/

        public void substraction(INFINT sub)
        {
            //everything same, transfer to addition if needed
            if (((this.negative == 1 && sub.negative == 0) || (this.negative == 0 && sub.negative == 1)) && !transferred)
            {
                transferred = true;
                addition(sub);

            }
            else
            {
                if (this.negative == 1)
                    Console.Write("-");
                this.InfNumber.ForEach(Console.Write);
                if (transferred)
                    Console.Write(" + ");
                else
                    Console.Write(" - ");
                if (sub.negative == 1)
                    Console.Write("(-)");
                sub.InfNumber.ForEach(Console.Write);
                Console.Write(" = ");

                transferred = false;

                int borrow = 0;
                bool isGreater1 = false;
                int numZeros = 0;

                if (this.InfNumber.Count == sub.InfNumber.Count) //if both lists have same number of elements find which one is bigger and subtract from it
                {
                    for (int q = 0; q < this.InfNumber.Count; q++)
                    {
                        if (this.InfNumber.ElementAtOrDefault(q) > sub.InfNumber.ElementAtOrDefault(q))
                        {
                            isGreater1 = true;
                            numZeros = this.InfNumber.Count - sub.InfNumber.Count;
                            break;
                        }
                        else if (this.InfNumber.ElementAtOrDefault(q) < sub.InfNumber.ElementAtOrDefault(q))
                        {
                            isGreater1 = false;
                            numZeros = sub.InfNumber.Count - this.InfNumber.Count;
                            break;
                        }
                        else
                        {
                            numZeros++;
                        }
                    }
                }
                this.InfNumber.Reverse(); //reverse to make operations
                sub.InfNumber.Reverse();

                //if first list has more elements or is just greater than second, substract from first
                if ((this.InfNumber.Count > sub.InfNumber.Count) || isGreater1)
                {
                    for (int j = 0; (j < this.InfNumber.Count - numZeros); j++)
                    {
                        // calculations are almost the same as in addition
                        if ((this.InfNumber.ElementAtOrDefault(j) - sub.InfNumber.ElementAtOrDefault(j) - borrow) < 0)
                        {

                            result.Add((this.InfNumber.ElementAtOrDefault(j) - sub.InfNumber.ElementAtOrDefault(j)) + 10 - borrow);
                            borrow = 1;
                        }
                        else
                        {
                            result.Add((this.InfNumber.ElementAtOrDefault(j) - sub.InfNumber.ElementAtOrDefault(j)) - borrow);
                            borrow = 0;
                        }
                    }
                    if (numZeros == this.InfNumber.Count)
                        Console.WriteLine("0"); //in case we have same operands (3-3)
                    else if (this.negative == 1)
                        Console.Write("-");
                }
                //if second number is greater. subtract first from it
                else if (this.InfNumber.Count < sub.InfNumber.Count || !isGreater1)
                {

                    for (int j = 0; j < (sub.InfNumber.Count - numZeros); j++)
                    {
                        if ((sub.InfNumber.ElementAtOrDefault(j) - this.InfNumber.ElementAtOrDefault(j) - borrow) < 0)
                        {

                            result.Add((sub.InfNumber.ElementAtOrDefault(j) - this.InfNumber.ElementAtOrDefault(j)) + 10 - borrow);
                            borrow = 1;
                        }
                        else
                        {
                            result.Add((sub.InfNumber.ElementAtOrDefault(j) - this.InfNumber.ElementAtOrDefault(j)) - borrow);
                            borrow = 0;
                        }
                    }
                    if (numZeros == this.InfNumber.Count)
                        Console.WriteLine("0"); //in case we have same operands (3-3)
                    if(transferred)
                        Console.Write("-");
                    if(sub.negative == 0)
                        Console.Write("-");





                }
                result.Reverse();
                foreach (int element in result)
                {
                    Console.Write(element);
                }
                if (numZeros != this.InfNumber.Count)
                    Console.WriteLine();
            }
        }

        /******************* MULTIPLICATION ********************/

        public void multiply(INFINT mul)
        {
            int j = 0;
            int k = 0;
            int carry;
            // array of integers to sum up calculations
            int[] product = new int[this.InfNumber.Count + mul.InfNumber.Count];


            if (this.negative == 1)
                Console.Write("-");
            foreach (int element in this.InfNumber)
            {
                Console.Write(element);
            }

            Console.Write(" * ");

            if (mul.negative == 1)
                Console.Write("(-)");
            foreach (int element in mul.InfNumber)
            {
                Console.Write(element);
            }
            Console.Write(" = ");


            //list one
            for (int i = this.InfNumber.Count - 1; i >= 0; i--)
            {
                carry = 0;
                int number1 = this.InfNumber.ElementAt(i);
                k = 0;
                //list two inside
                for (int a = mul.InfNumber.Count - 1; a >= 0; a--)
                {
                    int sum = number1 * mul.InfNumber.ElementAt(a) + product[j + k] + carry;
                    carry = sum / 10;
                    product[j + k] = sum % 10;
                    k++;
                }
                if (carry > 0)// check if it has carry
                    product[j + k] += carry;
                j++;
            }
            int l = product.Length - 1;
            while (l >= 0 && product[l] == 0)
                l--;
            while (l >= 0)
                result.Add(product[l--]);
            //different cases od inputs
            if ((this.negative == 0 && mul.negative == 0) || (this.negative == 1 && mul.negative == 1))
            {
                foreach (int element in result)
                {
                    Console.Write(element);
                }
            }
            else
            {
                Console.Write("-");
                foreach (int element in result) //display result
                {
                    Console.Write(element);
                }

            }
            if ((this.InfNumber.Count() == 1 && this.InfNumber[0] == 0) || (mul.InfNumber.Count() == 1 && mul.InfNumber[0] == 0))
                Console.WriteLine("0");
            Console.WriteLine();
        }

    }
   

}
