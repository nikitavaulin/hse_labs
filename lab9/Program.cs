using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DialClock c1 = new DialClock(-3, 1);
                
                c1.Show();

                Console.WriteLine(c1.Hours);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Вычисления окончены");
            }

            DialClock c2 = new DialClock(5, 2);

            Console.WriteLine(c2);
            Console.ReadLine();
        }
    }
}
