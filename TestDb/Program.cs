using System;
using FickleDragon.MacKdp.DataContracts;

namespace TestDb
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                using (var db = new KdpDbContext())
                {
                    foreach (var rr in db.BookEntries)
                    {
                        Console.WriteLine("{0} ASIN: {1}", rr.KENP, rr.ASIN);
                    }
                    Console.WriteLine("Hello World!");
                }
            }

            catch (Exception sqlex)
            {
                string sqlExcMsg = string.Format("{0} Inner Exception: {1} Stack Trace: {2}", sqlex.Message, sqlex.InnerException, sqlex.StackTrace);
                Console.WriteLine(sqlex.Message);
                Console.WriteLine(sqlex.InnerException);
            }
        }
    }
}