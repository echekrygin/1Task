using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Task1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Sorting all;
        TimeSpan time = new TimeSpan();
        Stopwatch appTimer = new Stopwatch();
        Bubble bubble;
        Shaker shaker;
        QuickSort QS;

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();         
            progressBar1.Minimum = 1;
            progressBar1.Maximum = Convert.ToInt32(numericUpDown1.Value) * 10;
            progressBar1.Value = 1;
            all = new Sorting(Convert.ToInt32(numericUpDown1.Value));

            bubble = new Bubble(all.mas.Length);
            bubble.inAction = bubbleTime;

            shaker = new Shaker(all.mas.Length);
            shaker.inAction = shakerTime;

            QS = new QuickSort(all.mas.Length);
            QS.inAction = QSTime;

            int[] array = new int[Convert.ToInt32(numericUpDown1.Value)];
            all.mas.CopyTo(bubble.mas, 0);
            all.mas.CopyTo(shaker.mas, 0);
            all.mas.CopyTo(QS.mas, 0);

            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Не выбран способ вычисления");
            }
            else if (radioButton1.Checked)
            {
                appTimer.Restart();
                appTimer.Start();

                bubble.doSort();
                shaker.doSort();
                QS.doSort();

                appTimer.Stop();
                time = appTimer.Elapsed;              
            }
            else if (radioButton2.Checked) 
            {
                Thread thbubble = new Thread(bubble.doSort);
                Thread thshaker = new Thread(shaker.doSort);
                Thread thQS = new Thread(QS.doSort);
                
                appTimer.Restart();
                appTimer.Start();

                thbubble.Start();
                thshaker.Start();
                thQS.Start();

                appTimer.Stop();
                time = appTimer.Elapsed;
            }

        }
        private void bubbleTime()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(bubbleTime));
            }
            else
            {
                label6.Text = "Сравнений: " + bubble.Compares.ToString() + "\nПерестановок: " + bubble.Swaps.ToString() + "\nВремя: " + bubble.Time;
           
                for (int i = 0; i < all.mas.Length; i++)
                {
                    listBox1.Items.Add(bubble.mas[i]);
                    progressBar1.PerformStep();
                }
                label9.Text = "Общее время: " + (bubble.Time + shaker.Time + QS.Time);
            }
        }
        private void shakerTime()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(shakerTime));
            }
            else
            {
                label7.Text = "Сравнений: " + shaker.Compares.ToString() + "\nПерестановок: " + shaker.Swaps.ToString() + "\nВремя: " + shaker.Time;
                for (int i = 0; i < all.mas.Length; i++)
                {
                    listBox2.Items.Add(shaker.mas[i]);
                    progressBar1.PerformStep();
                }
               label9.Text = "Общее время: " + (bubble.Time + shaker.Time + QS.Time);
            }
        }
        private void QSTime()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(QSTime));
            }
            else
            {
                label8.Text = "Сравнений: " + QS.Compares.ToString() + "\nПерестановок: " + QS.Swaps.ToString() + "\nВремя: " + QS.Time;
                for (int i = 0; i < all.mas.Length; i++)
                {
                    listBox3.Items.Add(QS.mas[i]);
                    progressBar1.PerformStep();
                }
                label9.Text = "Общее время: " + (bubble.Time + shaker.Time + QS.Time);
            }
        }

    }
}
