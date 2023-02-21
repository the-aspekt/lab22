using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность массива случайных чисел");
            int scale = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> step1 = new Func<object, int[]>(Array);
            Task<int[]> task1 = new Task<int[]>(step1, scale);

            Action <Task<int[]>> step2 = new Action<Task<int[]>>(SumArray);
            Task task2 = task1.ContinueWith(step2);

            Action<Task<int[]>> step3 = new Action<Task<int[]>>(MaxArray);
            Task task3 = task1.ContinueWith(step3);


            task1.Start();

           Console.ReadLine();
        }

        static int[] Array (object n)
        {
            int scale = (int)n;
            int[] result = new int[scale];
            Random random = new Random();
            for (int i = 0; i < scale; i++)
            {
                result[i] = random.Next(0, 100);
            }
            return result;
        }

        static void SumArray(Task<int[]> task)
        {
            int[] array = task.Result;
            Console.WriteLine("Сумма чисел в массиве равна " + array.Sum());
        }

        static void MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            Console.WriteLine("Максимальное значение в массиве равно " + array.Max());
        }
    }
}
