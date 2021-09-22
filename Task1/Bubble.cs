using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Bubble : Sorting
    {
        public Bubble(int emCount) : base(emCount)
        {

        }
        public override void doSort()
        {
            DateTime curTime = DateTime.Now;
            Compares = Swaps = 0;
            int tmp = 0;
            for (int i = 0; i < mas.Length - 1; i++)
            {
                for (int j = 0; j < mas.Length - 1 - i; j++)
                {
                    Term_Bool = true;
                    if (mas[j] > mas[j + 1])
                    {
                        tmp = mas[j];
                        mas[j] = mas[j + 1];
                        mas[j + 1] = tmp;
                        Swaps++;
                        Term_Bool = false;
                    }
                    Compares++;
                }
            }
            Time = DateTime.Now - curTime;
            inAction();
        }
    }
}
