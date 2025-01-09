using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_sand
{
    internal class Money
    {
        public int rubles;
        public int kop;

        public void AddKop(int sum)
        {
            kop += sum;
        }

        public void Show()
        {
            Console.WriteLine($"{rubles} рублей, {kop} копеек");
        }
    }
}
