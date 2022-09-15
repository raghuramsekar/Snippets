using System.Security.Cryptography;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            string ans = checksum(@"D:\agent.msi");
            Console.WriteLine(ans);
        }

        public static void checksum(string path)
        {
            byte[] hashBytes = null;
            using (var md5 = MD5.Create())
            {
	            using (var stream = File.OpenRead(path))
	            {
		            hashBytes = md5.ComputeHash(stream);
	            }
            }
            string base64 = Convert.ToBase64String(hashBytes);
            Console.WriteLine(base64);
        }
    }
}