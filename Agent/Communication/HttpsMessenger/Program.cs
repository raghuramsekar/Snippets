using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace v4._7._2
{
    class Program
    {
        /*
            Example : command line parameters 
            v4.7.2.exe https adapdev-ms6 8555 servlet/adsmserverstatus true
        */
        public static string urlFull = "https://adapdev-ms6:8555";
        public static Boolean ValidateSSL = false;

        //protocol url port api validatessl
        static void Main(string[] args)
        {
            if (args != null && args.Length > 1)
            {
                urlFull = "";
                urlFull = args[0] + "://" + args[1] + ":" + args[2] + "/" + args[3];
            }
            if (args.Length > 4)
            {
                ValidateSSL = true;
                Console.WriteLine("Validation of ssl certificate is enabled");
            }
            Thread.Sleep(15000);
            SendMessage();
            Console.WriteLine(Directory.GetCurrentDirectory());
        }

        public static string SendMessage()
        {
            string responseFromServer = null;
            try
            {
                string url = urlFull;
                Console.WriteLine("Url Communicating \"" + urlFull+"\"");
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                byte[] byteArray = Encoding.UTF8.GetBytes(url);


                //Ignores certificate validation
                ServicePointManager.ServerCertificateValidationCallback = OnValidateCertificate;
                ServicePointManager.Expect100Continue = true;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Display the status.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
                // Display the content.
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
            return responseFromServer;
        }

        private static bool OnValidateCertificate(object sender, X509Certificate certificate, X509Chain chain,
                                                  SslPolicyErrors sslPolicyErrors)
        {

            try
            {
                if (ValidateSSL)
                {
                    // If the certificate is a valid, signed certificate, return true.
                    if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                    {
                        Console.WriteLine("SSL Policy errors none");
                        return true;
                    }

                    // If there are errors in the certificate chain, look at each error to determine the cause.
                    if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
                    {
                        if (chain != null && chain.ChainStatus != null)
                        {
                            foreach (X509ChainStatus status in chain.ChainStatus)
                            {
                                Console.WriteLine("status is {0}",status.Status);
                                Console.WriteLine("Certificate issuer is {0}",certificate.Issuer);
                                Console.WriteLine("Certificate subject is {0}", certificate.Subject);
                                if (
                                  (certificate.Subject == certificate.Issuer) &&
                                  (status.Status == X509ChainStatusFlags.UntrustedRoot))
                                {
                                    // Self-signed certificates with an untrusted root are valid.
                                    Console.WriteLine("Self-signed certificates with an untrusted root are valid");
                                    continue;
                                }
                                else
                                {
                                    if (status.Status != X509ChainStatusFlags.NoError)
                                    {
                                        // If there are any other errors in the certificate chain, the certificate is invalid,
                                        // so the method returns false.
                                        Console.WriteLine("Certificate error status :" + status.Status.ToString());
                                        return false;
                                    }
                                }
                            }
                        }
                        // When processing reaches this line, the only errors in the certificate chain are
                        // untrusted root errors for self-signed certificates. These certificates are valid
                        // for default Exchange server installations, so return true.
                        //AgentMain.isValidCertificate = true;
                    }
                    if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) != 0)
                    {
                        var host = (string)sender;
                        // This means that the remote certificate is unavailable. Notify the user and return false.
                        Console.WriteLine("The SSL certificate was not available for {0}", host);
                        return false;
                    }
                    if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) != 0)
                    {
                        var host = sender;
                        // This means that the server's SSL certificate did not match the host name that we are trying to connect to.
                        var certificate2 = certificate as X509Certificate2;
                        var cn = certificate2 != null ? certificate2.GetNameInfo(X509NameType.SimpleName, false) : certificate.Subject;

                        Console.WriteLine("The Common Name for the SSL certificate did not match. Instead, it was {0}.", cn);
                        return false;
                    }
                    Console.WriteLine("Reached Check end");
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
