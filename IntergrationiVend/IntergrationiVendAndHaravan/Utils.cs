using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntergrationHaravan
{
    public class Utils
    {
        static ILog log = LogManager.GetLogger(typeof(Utils));
        public static string GetContentFile(out string contentUpdate, out bool isFirst)
        {
            isFirst = true;
            string result = "";
            contentUpdate = "";
            try
            {
                string rootPath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = rootPath + "Asset\\keyRequest.txt";
                result = File.ReadAllText(filePath);
                if(string.IsNullOrWhiteSpace(result))
                    result = contentUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                else
                {
                    isFirst = false;
                    contentUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }    
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return result;
        }

        public static void UpdateContentFile(string content)
        {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = rootPath + "Asset\\keyRequest.txt";

            File.WriteAllText(filePath, content);
        }
    }
}
