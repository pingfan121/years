using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GameLib.Util
{
    public class Config
    {
        static public Dictionary<string, string> config;
        static public string getConfig(string name)
        {
            if (config == null)
            {
                config = new Dictionary<string, string>();
                if (File.Exists(Environment.CurrentDirectory + "/config.txt"))
                {
                    string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "/config.txt");
                    for (int i = 0; i < lines.Length; i++)
                    {

                        int equalpos = lines[i].IndexOf('=');
                        //
                        if (equalpos >= 0)
                        {
                            string key = lines[i].Substring(0, equalpos);
                            key = key.Trim();
                            string value = lines[i].Substring(equalpos + 1);
                            value = value.Trim();
                            config.Add(key, value);
                        }
                    }
                }
            }
            return config[name];
        }
    }
}