using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoApp
{

    /*
    clear
    no_repeat
    add_rand,1,10
    add_rand,1,10
    add_rand,1,10
    add_rand,1,10
    add_rand,1,10
    add_rand,1,10
    */
    public class LotoScriptEngine
    {
        List<LotoNumbers> m_list;
        Dictionary<string, Tuple<int, DateTime>> m_dicLotoString;
        Random m_rand = new Random();
        Task m_task = null;
        List<string> m_scriptCommands = new List<string>();
        bool m_running = false;
        int m_ip = 0;
        public delegate void MsgCallback(int code, string msg);
        MsgCallback pCallback;
        public LotoScriptEngine(List<LotoNumbers> list, 
                                Dictionary<string , Tuple<int, DateTime>> dic,
                                MsgCallback p)
        {
            m_list = list;
            m_dicLotoString = dic;
            pCallback = p;
             
        }

        void Load(string scriptFileName)
        {
            StreamReader sr = new StreamReader(scriptFileName);
            m_scriptCommands.Clear();
            while (true)
            {
                string line = sr.ReadLine();
                if (line == null)
                {
                    sr.Close();
                    m_scriptCommands.Add("end_script");
                    return;
                }
                if (line[0] == ';')
                    continue;
                m_scriptCommands.Add(line);              
            }             
        }
        string ScriptEngine()
        {
            m_ip = 0;
            byte minDiff = 0;
            bool no_repeat = false;
            List<byte> b = new List<byte>();
            while (m_running)
            {
                string[] args = m_scriptCommands[m_ip].Split(new Char[] { ',' });
                switch (args[0])
                {
                    case "add_rand":
                        if (b.Count >= 6)
                        {
                            return "error in add_run , too many numbers";
                        }                        
                        bool exists = true;
                        byte n = 0;
                        while (exists == true)
                        {
                            n = (byte)m_rand.Next(byte.Parse(args[1]), byte.Parse(args[2]) + 1);
                            exists = b.Contains(n);
                        }
                        b.Sort();
                        bool skip = false;
                        if (minDiff > 0)
                        {
                            for (int i = 0 ; i < b.Count; i++)
                            {
                                if (Math.Abs(b[i] - n) <= minDiff)
                                {
                                    skip = true;
                                    break;
                                }
                            }
                        }
                        if (skip == false)
                        {
                            b.Add(n);
                            m_ip++;
                        }
                    break;
                    case "min_diff":
                        minDiff = byte.Parse(args[1]);
                        m_ip++;
                    break;
                    case "sort":
                        b.Sort();
                        m_ip++;
                    break;
                    case "clear":
                        b.Clear();
                        m_ip++;
                    break;
                    case "clear_list":
                        pCallback(1, "");
                        m_ip++;
                    break;
                    case "no_repeat":
                        no_repeat = true;
                        m_ip++;
                    break;
                    case "loto":
                    {
                        b.Sort();
                        string str = string.Empty;
                        for (int i = 0; i < 6; i++)
                        {
                            str += b[i].ToString();
                            if (i < 5)
                                str += ",";
                        }
                        if (no_repeat == true)
                        {
                            if (m_dicLotoString.ContainsKey(str) == false)
                            {
                                pCallback(0, str);
                                m_ip++;
                            }
                            else
                            {
                                pCallback(10, "Loto numbers already was in the past: " + str);
                                m_ip = int.Parse(args[1]);
                            }
                        }
                        else
                        {
                            pCallback(0, str);
                            m_ip++;
                        }
                    }
                    break;
                    case "end_script":
                    {
                        pCallback(100, "stop");
                        return "ok";
                    }
                    default:
                    {
                        pCallback(101, "Command not found " + m_scriptCommands[m_ip]);
                        return "Command not found " + m_scriptCommands[m_ip];
                    }
                }
            }
            pCallback(100, "ok");
            return "ok";
        }
        public void Start(string scriptFileName)
        {
            if (m_task != null)
                m_task.Wait();
            m_running = true;
            Load(scriptFileName);
            m_task = new Task( () => ScriptEngine() );
            m_task.Start();

        }
        public void Stop()
        {
            m_running = false;
            if (m_task != null)
                m_task.Wait();

        }
    }
}
