using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9
{
    internal class DialClock
    {
        public int hours;
        public int minutes;

        #region Конструкторы
        public DialClock()
        {
            this.hours = 0;
            this.minutes = 0;
        }
        public DialClock(int hours, int minutes)
        {
            this.hours = hours;
            this.minutes = minutes;
        }   
        public DialClock(DialClock dialClock)
        {
            this.hours = dialClock.hours;
            this.minutes = dialClock.minutes;
        }
        #endregion



    }
}
