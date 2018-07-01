using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int[,] PlayersRating = new int[11, 16];
        Random ran = new Random();
        int[,] TopRating = new int[11,3];
        int[] TeamRating = new int[16];
        int[,] SortedRating = new int[16,2];
        public int TopScore = 0;

        public void ArrayInsert(int cell,int player,int team,int rating)
        {
            for (int n = cell; n < 10; n++)
            {
                TopRating[n + 1, 0] = TopRating[n, 0];
                TopRating[n + 1, 1] = TopRating[n, 1];
                TopRating[n + 1, 2] = TopRating[n, 2];
            }
            TopRating[cell, 0] = player;
            TopRating[cell, 1] = team;
            TopRating[cell, 2] = rating;
        }

        public void SortBinInsert(int[] a)
        {
            int N = a.Length;
            for (int i = 0; i <= N - 1; i++)
            {
                int tmp = a[i], left = 0, right = i - 1;
                while (left <= right)
                {
                    int m = (left + right) / 2;
                    //определение индекса среднего элемента
                    if (tmp < a[m])
                        right = m - 1;  // сдвиг правой 
                    else left = m + 1; //или левой границы
                }
                for (int j = i - 1; j >= left; j--)
                    a[j + 1] = a[j]; // сдвиг элементов
                a[left] = tmp; // вставка элемента на нужное место
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool swap = false;
            dataGridView2.RowCount = 11;
            dataGridView2.ColumnCount = 16;
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    PlayersRating[j, i] = ran.Next(1, 40);
                    dataGridView2[i, j].Value = PlayersRating[j, i];
                    for (int n = 0; n < 11; n++)
                    {
                        if ((swap == false) && (PlayersRating[j, i] > TopRating[n,2]))
                        {
                            swap = true;
                            ArrayInsert(n, i, j, PlayersRating[j, i]);
                        }
                    }
                    TeamRating[i] = TeamRating[i] + PlayersRating[j, i];
                    SortedRating[i,j] = TeamRating[i];
                    swap = false;
                    
                }

                Console.WriteLine();
            }
            dataGridView3.RowCount = 12;
            dataGridView3.ColumnCount = 3;
            for (int i = 0; i < 11; i++)
            {
                dataGridView3[0, i].Value = TopRating[i, 0];
                dataGridView3[1, i].Value = TopRating[i, 1];
                dataGridView3[2, i].Value = TopRating[i, 2];
            }
            dataGridView4.RowCount = 16;
            dataGridView4.ColumnCount = 2;
            for (int i = 0; i < 16; i++)
            {
                dataGridView4[0, i].Value = i;
                dataGridView4[1, i].Value = TeamRating[i];
            }
        }
        
        private void TeamOutput(int team)
        {
            dataGridView1.RowCount = 12;
            dataGridView1.ColumnCount = 2;
            dataGridView1[0, 0].Value = "Номер";
            dataGridView1[1, 0].Value = "Рейтинг";
            //dataGridView1.CellTemplate = new DataGridViewTextBoxCell();
            for (int i = 0; i < 11; i++)
            {
                dataGridView1[0, i + 1].Value = i+1;
                dataGridView1[1, i + 1].Value = PlayersRating[i,team - 1];
                dataGridView1[1, i + 1].Value = PlayersRating[i,team - 1];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dataGridView1.AutoSizeRowsMode =DataGridViewAutoSizeRowsMode.AllCells;
            int TeamNumber = Int32.Parse(textBox1.Text);
            TeamOutput(TeamNumber);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SortBinInsert(TeamRating);
            /*for (int i = 0; i < 16; i++)
            {
                dataGridView4[0, i].Value = TeamRating[i];
                dataGridView4[1, i].Value = TeamRating[i];
            }*/
        }
    }
}
