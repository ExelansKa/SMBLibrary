using System.Diagnostics;
using System.Management;

namespace FileRWDApp
{
    internal class Program
    {
        private static string textFile = String.Empty;

        static void Main(string[] args)
        {
            for (int i = 0; i < 3; i++)
            {
                textFile = @$"\\192.168.1.23\Shared_0{i}\test.txt";

                Stopwatch sw = Stopwatch.StartNew();
                Console.WriteLine("Dosya Okunuyor!");

                string text = File.ReadAllText(textFile);
                Console.WriteLine(text);

                sw.Stop();
                Console.WriteLine($"Süre: {sw.ElapsedMilliseconds} ms");

                Console.ReadLine();
            }

            Console.WriteLine("Bitti...");

            Console.ReadLine();

            string rootPath = @$"\\192.168.1.23\Shared_01\";
            string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly);

            foreach (string dir in dirs)
            {
                Console.WriteLine($"Dizin:{dir}");
            }

            Console.ReadLine();

            using (ManagementClass shares = new(@"\\192.168.1.23\root\cimv2", "Win32_Share", new ObjectGetOptions()))
            {
                foreach (ManagementObject share in shares.GetInstances())
                {
                    Console.WriteLine(share["Name"]);
                }
            }

            Console.ReadLine();

            SharesTest.MainX();


            Console.ReadLine();



        }
    }
}