using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Abiturient : IComparable<Abiturient>
    {

        public int Id { get; set; }
        public int Result { get; set; }
        public int[] Specialities { get; set; }
        public Abiturient() { }
        public Abiturient(Abiturient a)
        {
            Id = a.Id; Result = a.Result; Specialities = a.Specialities;
        }
        public int CompareTo(Abiturient a)
        {
            return Result.CompareTo(a.Result);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Abiturient[] ab = new Abiturient[]
            {
                new Abiturient { Id = 1, Result = 230, Specialities = new int[]{1, 2} },
                new Abiturient { Id = 2, Result = 270, Specialities = new int[]{2, 1} },
                new Abiturient { Id = 3, Result = 370, Specialities = new int[]{2, 1} },
                new Abiturient { Id = 4, Result = 170, Specialities = new int[]{1, 2} },
                new Abiturient { Id = 5, Result = 240, Specialities = new int[]{1, 2} }
            };
            Array.Sort(ab);

            List<List<Abiturient>> abiturients = new List<List<Abiturient>>() { new List<Abiturient>(2), new List<Abiturient>(2) };
            List<Abiturient> abInQueue = new List<Abiturient>();

            //abiturients[0].Add(ab[1]);
            //for(int j = 0; j < 2; j++)
            //{
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < ab.Length; j++)
                {
                    if (abiturients[ab[j].Specialities[0] - 1].Count < abiturients[ab[j].Specialities[0] - 1].Capacity)
                    {
                        abiturients[ab[j].Specialities[0] - 1].Add(ab[j]);
                    }
                    else
                    {
                        abInQueue.Add(ab[j]);
                    }
                }
            }
            Console.WriteLine("Абитуриенты 1 специальности: ");
            for (int i = 0; i < abiturients[0].Count; i++)
            {
                Console.WriteLine(abiturients[0][i].Id.ToString());
            }
            Console.WriteLine("Абитуриенты 2 специальности: ");
            for (int i = 0; i < abiturients[1].Count; i++)
            {
                Console.WriteLine(abiturients[1][i].Id.ToString());
            }
            Console.WriteLine("Абитуриенты в очереди: ");
            for (int i = 0; i < abInQueue.Count; i++)
            {
                Console.WriteLine(abInQueue[i].Id.ToString());
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
