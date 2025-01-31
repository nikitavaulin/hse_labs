﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace lab9
{
    internal class DialClock
    {
        private int hours;
        private int minutes;

        

        public int Hours
        {
            get => hours;
            set 
            {
                if (0 <= value && value <= 12)
                {
                    hours = value;
                }
                else
                {
                    throw new Exception("Часы должны быть в пределах от 0 до 12./n");
                }
            }
            
        }
        public int Minutes
        {
            get => minutes;
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("ERROR");
                    minutes = 0;
                }
                else
                {
                    minutes = value;
                }
            }
        }

        #region Конструкторы
        public DialClock()
        {
            Hours = 0;
            Minutes = 0;
        }
        public DialClock(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }   
        public DialClock(DialClock dialClock)
        {
            Hours = dialClock.hours;
            Minutes = dialClock.minutes;
        }
        #endregion


        //public double CalcAngle()
        //{

        //}

        //public override bool Equals(object obj) { }

        public override string ToString()
        {
            return $"Время на часах {Hours} часов {Minutes} минут";
        }
        
  
    }
}
