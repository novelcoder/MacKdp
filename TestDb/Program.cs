using System;
using System.IO;

namespace TestDb
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length >= 1)
                {
                    Console.Write("Processing File:\t");
                    Console.WriteLine(args[0]);


                    if (File.Exists(args[0]))
                    {
                        using (var fs = File.OpenRead(args[0]))
                        {
                            FickleDragon.MacKdp.ExcelUtility.WorkbookUtility.Parse(fs, args[0], "berandor@gmail.com");
                        }
                    }
                    else
                    {
                        Console.WriteLine("File Doesn't Exist");
                    }
                }
            }

            catch (Exception sqlex)
            {
                string sqlExcMsg = string.Format("{0} Inner Exception: {1} Stack Trace: {2}", sqlex.Message, sqlex.InnerException, sqlex.StackTrace);
                Console.WriteLine(sqlex.Message);
                Console.WriteLine(sqlex.InnerException);
            }
            finally
            {
                Console.WriteLine("Program Complete");
            }
        }
    }
}