using System.Security.Cryptography;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            printSvc("remcom");
        }

        //list services
        //$param name - service name even partial name with lower case
        public static void printSvc(string name)
        {
            System.ServiceProcess.ServiceController[] services = System.ServiceProcess.ServiceController.GetServices();
            foreach (System.ServiceProcess.ServiceController svc in services)
            {
                if (svc.ServiceName.ToLower().Contains(name))
                {
                    Console.WriteLine("Service Name is {0}", svc.ServiceName);
                }
            }
        }
        
    }
}