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
        public static bool Contains(Abiturient[] a, Abiturient b)
        {
            bool res = false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].Id == b.Id)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Abiturient> ab = new List<Abiturient>
            {

                new Abiturient { Id = 3, Result = 370, Specialities = new int[]{ 1, 2, 0 } }, // 1
                new Abiturient { Id = 2, Result = 270, Specialities = new int[]{ 2, 1, 0 } }, // 2
                new Abiturient { Id = 4, Result = 270, Specialities = new int[]{ 1, 2, 0 } }, // 1
                new Abiturient { Id = 10, Result = 250, Specialities = new int[]{ 2, 1, 3 } }, // -
                new Abiturient { Id = 5, Result = 240, Specialities = new int[]{ 1, 2, 0 } }, // -
                new Abiturient { Id = 6, Result = 245, Specialities = new int[]{ 1, 2, 0 } }, // -
                new Abiturient { Id = 8, Result = 245, Specialities = new int[]{ 3, 2, 0 } }, // -
                new Abiturient { Id = 9, Result = 245, Specialities = new int[]{ 1, 3, 2 } }, // -
                new Abiturient { Id = 1, Result = 230, Specialities = new int[]{ 2, 1, 0 } }, // 2

                //SPec1 = 3 - 370, 4 - 270
                //SPec2 = 1, 2


                //new Abiturient { Id = 3, Result = 300, Specialities = new int[]{ 1, 3, 0 } }, // 1
                //new Abiturient { Id = 6, Result = 270, Specialities = new int[]{ 1, 0, 0 } }, // -
                //new Abiturient { Id = 2, Result = 250, Specialities = new int[]{ 2, 0, 0 } }, // 2
                //new Abiturient { Id = 9, Result = 225, Specialities = new int[]{ 1, 2, 3 } }, // -
                //new Abiturient { Id = 4, Result = 200, Specialities = new int[]{ 1, 2, 0 } }, // 1
                //new Abiturient { Id = 8, Result = 180, Specialities = new int[]{ 2, 3, 0 } }, // -
                //new Abiturient { Id = 10, Result = 150, Specialities = new int[]{ 1, 2, 3 } }, // -
                
                
                
                //new Abiturient { Id = 5, Result = 240, Specialities = new int[]{ 1, 2, 0 } }, // -
                //new Abiturient { Id = 1, Result = 260, Specialities = new int[]{ 2, 3, 0 } }, // 2
            };

            //SPec1 = 3 - 300, 6 - 270
            //SPec2 = 2 - 250, 9 - 225
            //SPec3 = 1, 2

            List<Abiturient> intermediate = new List<Abiturient>();
            List<Abiturient> failed = new List<Abiturient>();
            List<int> success = new List<int>();

            List<Abiturient> sp1 = new List<Abiturient>(2);
            List<Abiturient> sp2 = new List<Abiturient>(2);
            List<Abiturient> sp3 = new List<Abiturient>(2);
            Dictionary<string, List<Abiturient>> dict = new Dictionary<string, List<Abiturient>>() { ["sp1"] = sp1, ["sp2"] = sp2, ["sp3"] = sp3 };


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < ab.Count; j++)
                {
                    int sp = ab[j].Specialities[i];
                    Dictionary<string, List<Abiturient>> abiturients = dict;
                    if (sp != 0)
                    {
                        if (abiturients["sp" + sp].Count < abiturients["sp" + sp].Capacity)
                        {
                            if (!success.Contains(ab[j].Id))
                            {
                                abiturients["sp" + sp].Add(ab[j]);
                                success.Add(ab[j].Id);
                            }
                        }
                        else
                        {
                            int minResSp = abiturients["sp" + sp].Min(a => a.Result);
                            if (ab[j].Result > minResSp && !success.Contains(ab[j].Id))
                            {
                                Abiturient a = abiturients["sp" + sp].Where(a => a.Result == minResSp).LastOrDefault();
                                //Console.WriteLine("Ab: " + a.Id);
                                abiturients["sp" + sp].Remove(a);
                                abiturients["sp" + sp].Add(ab[j]);
                                success.Remove(a.Id);
                                success.Add(ab[j].Id);

                                //Console.WriteLine(ab[j].Id);
                            }

                        }
                        if (i + 1 >= ab[j].Specialities.Length && !success.Contains(ab[j].Id))
                        {
                            failed.Add(ab[j]);
                        }
                    }
                    else
                    {
                        if (!success.Contains(ab[j].Id))
                        {
                            failed.Add(ab[j]);
                        }
                    }
                }
            }
            Console.WriteLine("Spec1");
            for (int i = 0; i < dict["sp1"].Count; i++)
            {
                Console.WriteLine(dict["sp1"][i].Id + " - " + dict["sp1"][i].Result);
            }
            Console.WriteLine(" ----------------------------- ");
            Console.WriteLine("Spec2");
            for (int i = 0; i < dict["sp2"].Count; i++)
            {
                Console.WriteLine(dict["sp2"][i].Id + " - " + dict["sp2"][i].Result);
            }
            Console.WriteLine(" ----------------------------- ");
            Console.WriteLine("Intermediate");

            for (int i = 0; i < intermediate.Count; i++)
            {
                Console.WriteLine(intermediate[i].Id);
            }

            Console.WriteLine(" ----------------------------- ");
            Console.WriteLine("Spec3");
            for (int i = 0; i < dict["sp3"].Count; i++)
            {
                Console.WriteLine(dict["sp3"][i].Id + " - " + dict["sp3"][i].Result);
            }


            Console.WriteLine(" ----------------------------- ");
            Console.WriteLine("Success");
            for (int i = 0; i < success.Count; i++)
            {
                Console.WriteLine(success[i]);
            }

            Console.WriteLine(" ----------------------------- ");
            Console.WriteLine("Failed");
            for (int i = 0; i < failed.Count; i++)
            {
                Console.WriteLine(failed[i].Id);
            }


            Console.ReadKey();
        }
    }
}
