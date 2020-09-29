using System;
using System.IO;
using System.Text;

namespace Logger
{
    //Singleton == Log
    public sealed class Log : ILog
    {
        private Log()
        {
        }

        private static readonly Lazy<Log> instance = new Lazy<Log>(() => new Log());

        public static Log GetInstance
        {
            get
            {
                return instance.Value;
            }
        }

        public void LogException(string message)
        {
            string fileName = string.Format("{0}_{1}.log", "Logs", DateTime.Today.Day);
        
            string logFilePath = string.Format(Directory.GetCurrentDirectory() + @"\Logs\" + fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----------------------------------------");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(message);
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.Write(sb.ToString());
                writer.Flush();
            }
        }

    }
}
