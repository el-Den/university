using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Random rnd = new Random();
            //Console.Write("Введите длинну массива: ");
            //int len = int.Parse(Console.ReadLine());
            //byte[] nums = new byte[len];
            //rnd.NextBytes(nums);
            //int sum = 0;
            //for (int i = 0; i < len; i++)
            //{
            //    Console.Write($"{nums[i]} ");
            //    sum += nums[i];
            //}
            //Console.WriteLine($"\n{sum}");
            //Console.ReadKey();

            Random rnd = new Random();
            Console.Write("Введите длинну массива: ");
            int len = int.Parse(Console.ReadLine());
            int[,,] nums = new int[len, len, len];
            int maxNum = 0;

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    for (int k = 0; k < len; k++)
                    {
                        nums[i, j, k] = rnd.Next();
                    }
                }
            }
            int x = 0, y = 0, z = 0;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    for (int k = 0; k < len; k++)
                    {
                        if (nums[i, j, k] > maxNum)
                        { 
                            maxNum = nums[i, j, k];
                            x = i;
                            y = j;
                            z = k;
                        }
                    }
                }
            }
            Console.WriteLine($"Максимальный элемент:  {maxNum} : {x} {y} {z}");
            Console.ReadKey();
        }
    }
}