using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMS_Utilities
{
    public static class Logger
    {
        static ReaderWriterLockSlim objReaderWriterLockSlim = new ReaderWriterLockSlim();
        public static void WriteErrorLog(string PageName, string MethodName, Exception exception)
        {
            objReaderWriterLockSlim.EnterWriteLock();
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                DateTime dateTime = DateTime.Now;
                string Year = dateTime.ToString("yyyy");
                string Month = dateTime.ToString("MMM");
                string LogFileName = dateTime.ToString("dd-MMM-yyyy") + ".txt";
                string LogFileDirName = GenericConstants.ErrorLog;
                string ErrorLogFilePath = Path.Combine(baseDir, LogFileDirName, Year, Month, LogFileName);

                if (!Directory.Exists(baseDir))
                    Directory.CreateDirectory(baseDir);
                if (!Directory.Exists(Path.Combine(baseDir, LogFileDirName)))
                    Directory.CreateDirectory(Path.Combine(baseDir, LogFileDirName));
                if (!Directory.Exists(Path.Combine(baseDir, LogFileDirName, Year)))
                    Directory.CreateDirectory(Path.Combine(baseDir, LogFileDirName, Year));
                if (!Directory.Exists(Path.Combine(baseDir, LogFileDirName, Year, Month)))
                    Directory.CreateDirectory(Path.Combine(baseDir, LogFileDirName, Year, Month));
                if (File.Exists(ErrorLogFilePath))
                {
                    RemoveFileReadOnly(ErrorLogFilePath);
                    using (StreamWriter writer = new StreamWriter(ErrorLogFilePath, true))
                    {
                        writer.WriteLine("-------------------START-------------" + DateTime.Now);
                        writer.WriteLine("Source : " + PageName + " - " + MethodName);
                        writer.WriteLine("Error : " + exception.Message);
                        writer.WriteLine("-------------------END-------------" + DateTime.Now);
                    }
                }
                else
                {
                    StreamWriter writer = File.CreateText(ErrorLogFilePath);
                    writer.WriteLine("-------------------START-------------" + DateTime.Now);
                    writer.WriteLine("Source : " + PageName + " - " + MethodName);
                    writer.WriteLine("Error : " + exception.Message);
                    writer.WriteLine("-------------------END-------------" + DateTime.Now);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                objReaderWriterLockSlim.ExitWriteLock();
            }
        }
        public static void WriteErrorLog(string PageName, string MethodName, string ErrorMessage)
        {
            objReaderWriterLockSlim.EnterWriteLock();
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                DateTime dateTime = DateTime.Now;
                string Year = dateTime.ToString("yyyy");
                string Month = dateTime.ToString("MMM");
                string LogFileName = dateTime.ToString("dd-MMM-yyyy") + ".txt";
                string LogFileDirName = GenericConstants.ErrorLog;
                string ErrorLogFilePath = Path.Combine(baseDir, LogFileDirName, Year, Month, LogFileName);

                if (!Directory.Exists(baseDir))
                    Directory.CreateDirectory(baseDir);
                if (!Directory.Exists(Path.Combine(baseDir, LogFileDirName)))
                    Directory.CreateDirectory(Path.Combine(baseDir, LogFileDirName));
                if (!Directory.Exists(Path.Combine(baseDir, LogFileDirName, Year)))
                    Directory.CreateDirectory(Path.Combine(baseDir, LogFileDirName, Year));
                if (!Directory.Exists(Path.Combine(baseDir, LogFileDirName, Year, Month)))
                    Directory.CreateDirectory(Path.Combine(baseDir, LogFileDirName, Year, Month));
                if (File.Exists(ErrorLogFilePath))
                {
                    RemoveFileReadOnly(ErrorLogFilePath);
                    using (StreamWriter writer = new StreamWriter(ErrorLogFilePath, true))
                    {
                        writer.WriteLine("-------------------START-------------" + DateTime.Now);
                        writer.WriteLine("Source : " + PageName + " - " + MethodName);
                        writer.WriteLine("Error : " + ErrorMessage);
                        writer.WriteLine("-------------------END-------------" + DateTime.Now);
                    }
                }
                else
                {
                    StreamWriter writer = File.CreateText(ErrorLogFilePath);
                    writer.WriteLine("-------------------START-------------" + DateTime.Now);
                    writer.WriteLine("Source : " + PageName + " - " + MethodName);
                    writer.WriteLine("Error : " + ErrorMessage);
                    writer.WriteLine("-------------------END-------------" + DateTime.Now);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                objReaderWriterLockSlim.ExitWriteLock();
            }
        }
        public static void RemoveFileReadOnly(string FilePath)
        {
            if (IsFileReadOnly(FilePath))
            {
                FileAttributes Attributes = File.GetAttributes(FilePath);
                Attributes = RemoveFileAttribute(Attributes, FileAttributes.ReadOnly);
                File.SetAttributes(FilePath, Attributes);
            }
        }
        public static bool IsFileReadOnly(string FilePath)
        {
            return (File.GetAttributes(FilePath) & FileAttributes.ReadOnly) ==
                    FileAttributes.ReadOnly;
        }
        private static FileAttributes RemoveFileAttribute(FileAttributes Attributes, FileAttributes AttributeToRemove)
        {
            return Attributes & ~AttributeToRemove;
        }
    }
}
