using System.Security.Cryptography;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            printProcess("remcom");
        }

        //list processess by name
        //$param name - Process name even partial name with lower case
        public static void printProcess(string name)
        {
            Process[] processes = Process.GetProcesses();
            foreach(Process p in processes)
            {
                if(p.ProcessName.ToLower().Contains(name))
                    Console.WriteLine("Remcom path is {0}", p.MainModule.FileName);
            }
        }
        
    }
}