using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MillionSimple.Common
{

    public static class ErrorMessageLog
    {
        private static string logFile = System.AppDomain.CurrentDomain.BaseDirectory + @"errlog.txt";//日志文件目录

        /// <summary>
        /// 写日志信息
        /// </summary>
        /// <param name="logInfo">日志内容</param>
        public static void writeLog(string logInfo)
        {
            if (File.Exists(logFile))
            {
                writeLogToFile(logInfo);
            }
            else
            {
                createAndWriteLogFile(logInfo);
            }
        }

        /// <summary>
        /// 往日志文件中追加信息
        /// </summary>
        /// <param name="logInfo"></param>
        static private void writeLogToFile(string logInfo)
        {
            try
            {
                using (StreamWriter m_streamWriter = File.AppendText(logFile))
                {
                    m_streamWriter.Flush();
                    m_streamWriter.WriteLine(logInfo);
                    m_streamWriter.Flush();
                    m_streamWriter.Dispose();
                    m_streamWriter.Close();
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }

        /// <summary>
        /// 创建文件并写日志信息
        /// </summary>
        /// <param name="logInfo"></param>
        static private void createAndWriteLogFile(string logInfo)
        {
            FileStream fs = new FileStream(logFile, FileMode.Create, FileAccess.Write);
            try
            {
                using (StreamWriter m_streamWriter = new StreamWriter(fs))
                {
                    m_streamWriter.Flush();
                    m_streamWriter.WriteLine(logInfo);
                    m_streamWriter.Flush();
                    m_streamWriter.Dispose();
                    m_streamWriter.Close();
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            finally
            {
                fs.Dispose();
                fs.Close();
            }
        }
    }
}
