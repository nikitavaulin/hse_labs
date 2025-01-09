using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_sand
{
    internal class Person
    {
        //public string name;
        //public int age;
        //public int id;


        //public Person(string myName, int myAge)
        //{
        //    name = myName;
        //    age = myAge;
        //}
        //public Person()
        //{
        //    name = "noName";
        //    age = 1;
        //}
        //public Person(Person person)
        //{
        //    name = person.name;
        //    age = person.age;
        //}

        public string name;
        public int age;
        public Person() : this("Неизвестно")    // первый конструктор
        { }
        public Person(string name) : this(name, 18) // второй конструктор
        { }
        public Person(string name, int age)     // третий конструктор
        {
            this.name = name;
            this.age = age;
        }
        public void Print() => Console.WriteLine($"Имя: {name}  Возраст: {age}");

        //public void Show()
        //{
        //    Console.WriteLine($"{name}, {age}");
        //}
    }
}
