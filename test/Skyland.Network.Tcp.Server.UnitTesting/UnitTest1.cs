#region using

using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace Skyland.Network.Tcp.Server.UnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var hub = Configuration.Configure
            //    .WithEndpoint(IPAddress.Parse("127.0.0.1"), 5770)
            //    .Compression(
            //        c =>
            //            c.Use<GZipCompressor>())
            //    .Ssl(
            //        s =>
            //            s
            //                .Certificate(new X509Certificate())
            //                .CheckCertificateRevocation()
            //                .ClientCertificateRequired()
            //                .Policy(EncryptionPolicy.RequireEncryption))
            //     .Events(
            //        e => 
            //            e.OnMessageReceived(m => { Trace.WriteLine(Convert.ToBase64String(m.Data));})
            //             .OnClientConnected(p => Trace.WriteLine(string.Format("connected from: {0}", p)))
            //             .OnClientDisconnected(p => Trace.WriteLine(string.Format("disconnected from: {0}", p)))
            //             .OnClientAccepted(p => Trace.WriteLine(string.Format("accepted connection from: {0}", p)))
            //             .OnClientRejected(p => Trace.WriteLine(string.Format("rejected connection from: {0}", p))))
                        
            //    .Create();

            //hub.Start();
            //Task.Delay(600000).Wait();
            //hub.Stop();
        }

        private void WriteFileToDisk(Message obj)
        {
            using (var stream = new FileStream("file", FileMode.Append))
            using (var writter = new BinaryWriter(stream))
            {
                writter.Seek(0, SeekOrigin.End);
                writter.Write(obj.Data);
                writter.Flush();
                writter.Close();
            }
        }


        //[TestMethod]
        //public void Worker()
        //{
        //    var worker = new Worker(Method);
        //    worker.Start();
        //    worker.Cancel();
        //}
    }
}
