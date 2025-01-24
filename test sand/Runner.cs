using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace test_sand
{
    internal class Runner
    {
        // Нам нужно считать количество созданных бегунов (экземпляров класса)
        private static int count = 0;

        // Мы знаем что бегун имеет имя
        private string name;

        // Есть скорость
        private double speed;
        // меняем скорость с помощью свойств
        public double Speed
        {
            get {
                return this.speed;
            }
            set {
            if (value >= 0)
                {
                    this.speed = value;
                    return;
                }
                //throw new ValidationException("Скорость не может быть отрицательной");
            }
        }


        // с помощью метода
        public void SetSpeed(double speed)
        {

        }

        // Рекорд

        public Runner() 
        {
            Runner.count++;
        }

        public Runner(string name)
        {
            this.name = name;
            Runner.count++;
        }

        public Runner(string name, double speed)
        {
            this.name = name;
            this.speed = speed;
            Runner.count++;
        }

    }
}
