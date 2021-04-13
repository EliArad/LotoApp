using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotoApp
{
    public struct LotoNumbers
    {
        public int lotoNumber;
        public byte[] numbers;
        public DateTime date;
        public byte strongNum;
    }
    public partial class Form1 : Form
    {

        Dictionary<string , Tuple<int, DateTime>> m_dicLotoString = new Dictionary<string, Tuple<int, DateTime>>();
        int m_numOfRepeat = 0;

        LotoUtils m_util;
        List<LotoNumbers> m_list = new List<LotoNumbers>();
        

        LotoScriptEngine m_script;
        
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;    
            try
            {
                textBox2.Text = File.ReadAllText("script.txt");
            }
            catch (Exception err)
            {

            }

            try
            {
                LotoUtils u = new LotoUtils();
                u.readFile("Lotto.csv", out int numOfRepeat);
                textBox1.AppendText("Total numbers of letery are: " + m_list.Count + Environment.NewLine);
                textBox1.AppendText("There are " + m_numOfRepeat + " lottery that repeated" + Environment.NewLine);
                m_util = new LotoUtils(m_list, m_dicLotoString);

                LotoScriptEngine.MsgCallback p = new LotoScriptEngine.MsgCallback(ScriptCallback);
                m_script = new LotoScriptEngine(m_list, m_dicLotoString,p);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        void ScriptCallback(int code, string msg)
        {
            switch (code)
            {
                case 0:
                    listBox1.Items.Add(msg);
                    label5.Text = listBox1.Items.Count.ToString();
                break;
                case 1:
                    listBox1.Items.Clear();
                    label5.Text = listBox1.Items.Count.ToString();
                break;
                case 100:
                    button2.Enabled = true;
                break;
                case 101:
                    MessageBox.Show(msg);
                    button2.Enabled = true;
                break;
                case 10:
                    MessageBox.Show(msg);
                break;
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            string result;
            byte maxNum = 0;
            byte minNum = 0;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    result = m_util.GetSixNumbersThanNeverApeard();
                    listBox1.Items.Add(result);
                break;
                case 1:
                  
                    try
                    {
                        maxNum = byte.Parse(txtMaxNumber.Text);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Please enter maximum number");
                        return;
                    }
                    result = m_util.GetSixNumbersThanNeverApeardUpToMax(maxNum);
                    listBox1.Items.Add(result);
                break;
                case 2:
                {
                    try
                    {
                        maxNum = byte.Parse(txtMaxNumber.Text);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Please enter maximum number");
                        return;
                    }

                    try
                    {
                        minNum = byte.Parse(txtMinNumber.Text);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Please enter maximum number");
                        return;
                    }
                    result = m_util.GetSixNumbersThanNeverApeardFromMinToMax(minNum, maxNum);
                    listBox1.Items.Add(result);
                }
                break;
                case 3:
                    try
                    {
                        maxNum = byte.Parse(txtMaxNumber.Text);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Please enter maximum number");
                        return;
                    }

                    int r = m_util.GetNumberOflotteryThatDidNotPassedTheMaximum(maxNum);
                    MessageBox.Show(r.ToString());
                break;
                case 4:
                {
                    try
                    {
                        minNum = byte.Parse(txtMinNumber.Text);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Please enter maximum number");
                        return;
                    }
                    int r2 = m_util.GetNumberOflotteryGreaterThanMinimum(minNum);
                    MessageBox.Show(r2.ToString());
                }
                break;
                case 5:
                    try
                    {
                        maxNum = byte.Parse(txtMaxNumber.Text);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Please enter maximum number");
                        return;
                    }
                    int r1 = m_util.GetNumberOflotteryBetweenMinimumToMaximum(byte.Parse(txtMinNumber.Text), byte.Parse(txtMaxNumber.Text));
                    MessageBox.Show(r1.ToString());
                break;
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            label5.Text = listBox1.Items.Count.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                File.WriteAllText("script.txt", textBox2.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText("script.txt", textBox2.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            button2.Enabled = false;
            m_script.Start("script.txt");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_script.Stop();
            button2.Enabled = true;
        }
    }
}
