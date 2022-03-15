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
            List<Abiturient> ab = new List<Abiturient>
            {
                new Abiturient { Id = 3, Result = 370, Specialities = new int[]{1, 2} }, // 1
                new Abiturient { Id = 2, Result = 270, Specialities = new int[]{2, 1} }, // 2
                new Abiturient { Id = 5, Result = 240, Specialities = new int[]{1, 2} }, // -
                new Abiturient { Id = 1, Result = 230, Specialities = new int[]{2, 1} }, // 2
                new Abiturient { Id = 4, Result = 270, Specialities = new int[]{1, 2} } // 1
            };

            List<List<Abiturient>> abiturients = new List<List<Abiturient>>() { new List<Abiturient>(2), new List<Abiturient>(2) };
            List<Abiturient> abInQueue = new List<Abiturient>();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < ab.Count; j++)
                {

                    if (abiturients[ab[j].Specialities[i] - 1].Count < abiturients[ab[j].Specialities[i] - 1].Capacity)
                    {
                        abiturients[ab[j].Specialities[i] - 1].Add(ab[j]);
                        ab.Remove(ab[j]);
                        j--;
                    }
                    else
                    {
                        var minValueInList = abiturients[ab[j].Specialities[i] - 1].Min(a => a.Result); // 240
                        if (minValueInList < ab[j].Result)
                        {
                            Abiturient removeAbit = abiturients[ab[j].Specialities[i] - 1]
                                .Where(a => a.Result < minValueInList).FirstOrDefault();
                            ab.Add(removeAbit);
                            abiturients[ab[j].Specialities[i] - 1].Remove(removeAbit);
                            abiturients[ab[j].Specialities[i] - 1].Add(ab[j]);

                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            //Console.WriteLine(ab[0].Id);
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
            Console.WriteLine(abiturients[ab[0].Specialities[0] - 1]
                                .Min(a => a.Result));
            Console.ReadKey();
        }
    }
}
