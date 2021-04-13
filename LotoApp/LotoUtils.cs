using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp
{
    public class LotoUtils
    {
        List<LotoNumbers> m_list;
        Dictionary<string, Tuple<int, DateTime>> m_dicLotoString;
        Random m_rand = new Random();
        const int start = 1;
        const int stop = 37 + 1;
        public LotoUtils(List<LotoNumbers> list, Dictionary<string , Tuple<int, DateTime>> dic)
        {
            m_list = list;
            m_dicLotoString = dic;
        }
        public LotoUtils()
        {
         
        }
        public Dictionary<string, Tuple<int, DateTime>> GetLoto()
        {
            return m_dicLotoString;
        }

        public int GetNumberOflotteryBetweenMinimumToMaximum(int min, int max)
        {

            int count = 0;
            for (int i = 0; i < m_list.Count; i++)
            {
                LotoNumbers l = m_list[i];
                if ((l.numbers[5] <= max)  && (l.numbers[0] >= min))
                {
                    count++;
                }
            }
            return count;
        }
        public int GetNumberOflotteryGreaterThanMinimum(int min)
        {

            int count = 0;
            for (int i = 0; i < m_list.Count; i++)
            {
                LotoNumbers l = m_list[i];
                if (l.numbers[0] > min)
                {
                    count++;
                }
            }
            return count;
        }
        public int GetNumberOflotteryThatDidNotPassedTheMaximum(int num)
        {
            int count = 0;
            for (int i = 0 ; i < m_list.Count;i++)
            {
                LotoNumbers l = m_list[i];
                if (l.numbers[5] < num)
                {
                    count++;
                }
            }
            return count;
        }
        public string GetSixNumbersThanNeverApeardFromMinToMax(byte minNum , byte maxNum)
        {
            bool found = false;
            do
            {
                string str = string.Empty;
                List<byte> b = new List<byte>();

                int i = 0;
                while (i < 6)
                {
                    byte n = (byte)m_rand.Next(start, stop);
                    if (n >= maxNum)
                        continue;
                    if (n < minNum)
                        continue;
                    var exists = b.Contains(n);
                    if (exists == true)
                        continue;
                    b.Add(n);
                    i++;
                }
                b.Sort();

                for (i = 0; i < 6; i++)
                {
                    str += b[i].ToString();
                    if (i < 5)
                        str += ",";
                }


                if (m_dicLotoString.ContainsKey(str) == false)
                {
                    found = true;
                    return str;
                }

            } while (found == false);


            return "1";
        }
        public string GetSixNumbersThanNeverApeardUpToMax(byte maxNum)
        {
            bool found = false;
            do
            {
                string str = string.Empty;
                List<byte> b = new List<byte>();

                int i = 0;
                while (i < 6)
                {
                    byte n = (byte)m_rand.Next(start, stop);
                    if (n >= maxNum)
                        continue;
                    var exists = b.Contains(n);
                    if (exists == true)
                        continue;
                    b.Add(n);
                    i++;
                }
                b.Sort();

                for (i = 0; i < 6; i++)
                {
                    str += b[i].ToString();
                    if (i < 5)
                        str += ",";
                }


                if (m_dicLotoString.ContainsKey(str) == false)
                {
                    found = true;
                    return str;
                }

            } while (found == false);


            return "1";
        }
        public string GetSixNumbersThanNeverApeardUpToMax(byte minNum , byte maxNum)
        {
            return "e";
        }

        public string GetSixNumbersThanNeverApeard()
        {
            bool found = false;
            do
            {
                string str = string.Empty;
                List<byte> b = new List<byte>();

                int i = 0;
                while (i < 6)
                {
                    byte n = (byte)m_rand.Next(start, stop);
                    var exists = b.Contains(n);
                    if (exists == true)
                        continue;
                    b.Add(n);
                    i++;
                }
                b.Sort();

                for ( i = 0; i < 6; i++)
                {
                    str += b[i].ToString();
                    if (i < 5)
                        str += ",";
                }


                if (m_dicLotoString.ContainsKey(str) == false)
                {
                    found = true;
                    return str;
                }

            } while (found == false);


            return "1";
        }

        public void readFile(string fileName, out int numOfRepeat)
        {

            m_list = new List<LotoNumbers>();
            m_dicLotoString = new Dictionary<string, Tuple<int, DateTime>>();
            numOfRepeat = 0;
            StreamReader sr;
            LotoNumbers l = new LotoNumbers();
            try
            {
                sr = new StreamReader(fileName);
                sr.ReadLine();
                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null)
                    {
                        sr.Close();
                        numOfRepeat = 0;
                        return;
                    }
                    string[] lData = line.Split(new Char[] { ',' });
                    l = new LotoNumbers();
                    l.numbers = new byte[6];

                    l.lotoNumber = int.Parse(lData[0]);


                    string[] dTemp = lData[1].Split(new Char[] { '-' });
                    string goodDate = int.Parse(dTemp[1]).ToString("00") + "/" + int.Parse(dTemp[0]).ToString("00") + "/" + getYear(dTemp[2]);

                    l.date = DateTime.ParseExact(goodDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    l.numbers[0] = byte.Parse(lData[2]);
                    l.numbers[1] = byte.Parse(lData[3]);
                    l.numbers[2] = byte.Parse(lData[4]);
                    l.numbers[3] = byte.Parse(lData[5]);
                    l.numbers[4] = byte.Parse(lData[6]);
                    l.numbers[5] = byte.Parse(lData[7]);

                    if (l.numbers[3] > 37)
                    {
                        Console.WriteLine(line);
                        continue;
                    }
                    if (l.numbers[4] > 37)
                    {
                        Console.WriteLine(line);
                        continue;
                    }
                    if (l.numbers[5] > 37)
                    {
                        Console.WriteLine(line);
                        continue;
                    }


                    string str = string.Empty;
                    for (int i = 0; i < 6; i++)
                    {
                        str += l.numbers[i].ToString();
                        if (i < 5)
                            str += ",";
                    }
                    try
                    {
                        m_dicLotoString.Add(str, new Tuple<int, DateTime>(l.lotoNumber, l.date));
                    }
                    catch (Exception err)
                    {
                        numOfRepeat++;
                    }

                    l.strongNum = byte.Parse(lData[8]);
                    m_list.Add(l);
                }
            }
            catch (Exception err)
            {

                throw (new SystemException("Loto number error:" + l.lotoNumber + Environment.NewLine + err.Message));
            }
        }
        string getYear(string year)
        {
            switch (year)
            {
                case "24":
                    return "2024";
                case "23":
                    return "2023";
                case "22":
                    return "2022";
                case "21":
                    return "2021";
                case "20":
                    return "2020";
                case "19":
                    return "2019";
                case "18":
                    return "2018";
                case "17":
                    return "2017";
                case "16":
                    return "2016";
                case "15":
                    return "2015";
                case "14":
                    return "2014";
                case "13":
                    return "2013";
                case "12":
                    return "2012";
                case "11":
                    return "2011";
                case "10":
                    return "2010";
                case "09":
                    return "2009";
                case "08":
                    return "2008";
                case "07":
                    return "2007";
                case "06":
                    return "2006";
                case "05":
                    return "2005";
                case "04":
                    return "2004";
                case "03":
                    return "2003";
                case "02":
                    return "2002";
                case "01":
                    return "2001";
                case "00":
                    return "2000";
                case "99":
                    return "1999";
                case "98":
                    return "1998";
                case "97":
                    return "1997";
                case "96":
                    return "1996";
                case "95":
                    return "1995";
                case "94":
                    return "1994";
                case "93":
                    return "1993";
                case "92":
                    return "1992";
                case "91":
                    return "1991";
                case "90":
                    return "1990";
            }
            return year;
        }

    }
}
