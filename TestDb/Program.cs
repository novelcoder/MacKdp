using System;
using System.Linq;
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
                    Console.Write("Processing Directory:\t");
                    Console.WriteLine(args[0]);
                    if (Directory.Exists(args[0]))
                    {
                        var files = Directory.EnumerateFiles(args[0], "*.xlsx");
                        foreach (var file in files.OrderBy( x => x))
                        {
                            Console.WriteLine("File: {0}", file);
                            using (var fs = File.OpenRead(file))
                            {
                               FickleDragon.MacKdp.ExcelUtility.WorkbookUtility.Parse(fs, file);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Directory does not exist {0)", args[0]);
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