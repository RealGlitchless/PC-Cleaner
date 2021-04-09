using System;
using System.IO;
using System.Threading;

namespace PC_Cleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                // Starting the recycle bin cleaner process
                Console.WriteLine("Starting Recycle bin cleaner");
                Thread.Sleep(500);
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = "ebclean.bat";
                proc.Start();
                Thread.Sleep(500);
                Console.WriteLine("Recycle bin has been deleted");

                // Find path to temp folder
                Thread.Sleep(1500);
                Console.WriteLine("Finding Temp folder");
                Thread.Sleep(500);
                string result = Path.GetTempPath();
                System.IO.DirectoryInfo di = new DirectoryInfo(result);
                Console.WriteLine("Temp folder found");

                // Measuring size of temp folder
                long size = 0;
                foreach (FileInfo fi in di.GetFiles("*", SearchOption.AllDirectories))
                {
                    size += fi.Length;
                }
                long mbsize = size / 1000000;
                Console.WriteLine($"The directory size: {mbsize} MB");
                Thread.Sleep(1500);

                // Delete files
                Console.WriteLine("Deleting temp files");
                foreach (FileInfo file in di.EnumerateFiles())
                {
                    try
                    {
                        file.Delete();
                    }

                    catch (Exception)
                    {
                        // Ignore the failure and continue
                    }

                    // Delete folders
                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        try
                        {
                            dir.Delete(true);
                        }

                        catch (Exception)
                        {
                            // Ignore the failure and continue
                        }
                    }
                }
                Thread.Sleep(1000);

                // Comparing the two values
                long delsize = 0;
                foreach (FileInfo fi in di.GetFiles("*", SearchOption.AllDirectories))
                {
                    delsize += fi.Length;
                }
                long mbdelsize = delsize / 1000000;
                long ttldelsize = mbsize - mbdelsize;
                Console.WriteLine($"There has been deleted {ttldelsize}MB from the Temp folder");
                Thread.Sleep(2000);

                // Done
                Console.WriteLine("Everything is done and your PC has been cleaned!");
                Thread.Sleep(1500);
            }
        }
    }
}