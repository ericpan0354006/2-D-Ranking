using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
 
namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
        float[,] Data = new float[2000, 3]; //Date[,0] => Coordinate of X  
                                            //Data[,1] =>Coordinate of Y
                                            //Data[,2] =>Rank
        int count = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void show1()
        {
            string str = "(" + textBox1.Text + "," + textBox2.Text + ")";
            richTextBox1.Text += str + "\n" + "";
        }
        public void quicksort(int a, int count)
        {
            if (a >= count) return;
            int i, j;
            float X_Coordinate;
            X_Coordinate = Data[a,0];
            i = a + 1;
            j = count;
            while (i < j)
            {
                while (Data[j,0] >= X_Coordinate && j >= a + 1)
                    j--;
                while (Data[i,0] <= X_Coordinate && i <= count)
                    i++;
                if (i < j)
                {
                    float temp1, temp2;
                    temp1 = Data[i,0];
                    temp2 = Data[i,1];
                    Data[i,0] = Data[j,0];
                    Data[i,1] = Data[j,1];
                    Data[j,0] = temp1;
                    Data[j,1] = temp2;
                }
            }
            float temp3, temp4;
            temp3 = Data[a,0];
            temp4 = Data[a,1];
            Data[a,0] = Data[j,0];
            Data[a,1] = Data[j,1];
            Data[j,0] = temp3;
            Data[j,1] = temp4;
            quicksort(a, j - 1);
            quicksort(j + 1, count);
        }
        public void DC(int left, int right)
        {
            if (left >= right) return;
            int mid = (left + right) / 2;
            DC(left, mid);
            DC(mid + 1, right);
            merge(left, mid, right);
        }
       public  void merge(int left, int mid, int right)
        {
            int idx1 = left, idx2 = mid + 1;
            while (idx1 <= mid && idx2 <= right)
            {
                if (Data[idx1,1] < Data[idx2,1])
                {
                    Data[idx2, 2] += 1;
                    idx1++;
                }
                else
                {
                    idx1++;
                }
                if (idx1 > mid && idx2 <= right)
                {
                    idx2++;
                    idx1 = left;
                }
            }
        }

       private void button3_Click(object sender, EventArgs e)
       {

           // Displays an OpenFileDialog so the user can select a Cursor.
           OpenFileDialog openFileDialog1 = new OpenFileDialog();
           openFileDialog1.Filter = "Text File|*.txt";
           openFileDialog1.Title = "Select a Text File";

           // Show the Dialog.
           // If the user clicked OK in the dialog and
           // a .CUR file was selected, open it.
           /*
           if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
           {
               // Assign the cursor in the Stream to the Form's Cursor property.
               //this.Text = new Text(openFileDialog1.OpenFile());
           }
           */
           if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
           {
               System.IO.StreamReader sr = new
                  System.IO.StreamReader(openFileDialog1.FileName);
              // MessageBox.Show(sr.ReadToEnd());

               int txtLength = 0, i = 0; //textLength  
               string s1;
               try
               {
                   // 取得行數 
                   s1 = sr.ReadLine();

                   while (s1 != null)
                   {
                       txtLength++;
                       s1 = sr.ReadLine();
                       //richTextBox2.Text = Convert.ToString(txtLength);
                   }
                   //fs.Position = 0;

                   // 取得資料並放入陣列 
                   string[] s2 = new string[2];
                   //float[,] txtValue = new float[2000, 2];

                   s1 = sr.ReadLine();
                   do
                   {
                       s2 = s1.Split(' ');
                       Data[i, 0] = float.Parse(s2[0]);
                       Data[i, 1] = float.Parse(s2[1]);
                       richTextBox1.Text += "(" + s2[0] + ", " + s2[1] + ")" + "\n";
                       s1 = sr.ReadLine();
                       i++;
                   } while (s1 != null);
                   count = txtLength;
                   sr.Close();
                   //fs.Close();
               }
               catch (Exception a)
               {
                   Console.WriteLine("Exception of type {0} occurred.",
                       a.GetType());
               }

               /* System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();*/
           }
           /*
           int txtLength = 0, i = 0; //textLength  
           string s1;
            
           FileStream fs = new FileStream("C:\\test1.txt", FileMode.Open);
           StreamReader sr = new StreamReader(fs, Encoding.Default);
           try
           {
               // 取得行數 
               s1 = sr.ReadLine();
                
               while (s1 != null)
               {
                   txtLength++;
                   s1 = sr.ReadLine();
                   //richTextBox2.Text = Convert.ToString(txtLength);
               }
               fs.Position = 0;
                
               // 取得資料並放入陣列 
               string[] s2 = new string[2];
               //float[,] txtValue = new float[2000, 2];

               s1 = sr.ReadLine();
               do
               {
                   s2 = s1.Split(' ');
                   Data[i, 0] = float.Parse(s2[0]);
                   Data[i, 1] = float.Parse(s2[1]);
                   richTextBox1.Text += "(" + s2[0] + ", " + s2[1] + ")" + "\n";
                   s1 = sr.ReadLine();
                   i++;
               } while (s1 != null) ;
               count = txtLength;
               sr.Close();
               fs.Close();
           }
           catch (Exception a)
           {
               Console.WriteLine("Exception of type {0} occurred.",
                   a.GetType());
           }
             
            */
       } 
        
        private void button2_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(@"D:\text.txt");
            /*foreach (string line in str)
            {
                // If the line doesn't contain the word 'Second', write the line to the file.
                if (line.Contains("\n"))
                {
                    sw.WriteLine(line);
                }
            }*/
            string str = richTextBox1.Text + "\r\n";
            sw.WriteLine(str);
            sw.Close();
        }

        public void ranking()
        {
            for (int i = 0; i < count; i++)
                Data[i, 2] = 0;
            quicksort(0, count - 1);
            DC(0, count - 1);
            for (int i = 0; i < count; i++)
            {
                richTextBox2.Text += "( " + Data[i, 0] + ", " + Data[i, 1] + " )" + "  => rank = " + Data[i, 2] + "\n";
            }
            float max = Data[0, 2];
            for (int i = 1; i < count; i++)
            {
                if (Data[i, 2] > max) max = Data[i, 2];
            }
            float min = Data[0, 2];
            for (int i = 1; i < count; i++)
            {
                if (Data[i, 2] < min) min = Data[i, 2];
            }
            float total = 0;
            for (int i = 0; i < count; i++)
            {
                total += Data[i, 2];
            }
            float avg = (float)total / count;
            richTextBox2.Text += "Max Rank = " + max + "\n" + "Min Rank = " + min + "\n" + "Average Rank = " + avg + "\n";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            show1();
            try
            {
                int i = 0;
                int s1 = Convert.ToInt32(richTextBox1.Lines);
                string str2 = richTextBox1.Text;
                string s2;
                string[] s3 = new string[2];
                for (i = 0; i < s1; i++)
                {
                    s2 = str2.Trim('(', ')', '\n');
                    s3 = s2.Split(',');
                    Data[i, 0] = float.Parse(s3[0]);
                    Data[i, 1] = float.Parse(s3[1]);
                    i++;
                }
            }
            catch(Exception a)
            {
                Console.WriteLine("Exception of type {0} occurred.",
                    a.GetType());
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = " ";
            ranking();
        }
        private void Clear()
        {
            try
            {
                richTextBox1.Text = null;
                richTextBox2.Text = null;
                textBox1.Text = null;
                textBox2.Text = null;
                Array.Clear(Data, 0, 2000);
            }
            catch (DataException e)
            {
                Console.WriteLine("Exception of type {0} occurred.",
                    e.GetType());
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
