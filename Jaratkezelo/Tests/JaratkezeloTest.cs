using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaratkezelo.Tests
{
    [TestFixture]
    class JaratkezeloTest
    {
        Jaratkezelo j;

        [SetUp]
        public void Setup()
        {
            j = new Jaratkezelo();
        }


        // --------------- UjJarat Test Cases ---------------

        [TestCase]
        public void UjJaratLetrehoz()
        {
            j.UjJarat("101", "Budapest", "Reykjavík", new DateTime(2022,03,01,11,0,0));
            j.UjJarat("201", "Reykjavík", "Budapest", new DateTime(2022,04,01,20,0,0));
            Assert.IsNotEmpty(j.jaratok);
        }


        [TestCase]
        public void UjJaratLetezo()
        {
            j.UjJarat("101", "Budapest", "Reykjavík", new DateTime(2022, 03, 01, 11, 0, 0));
            Assert.Throws<ArgumentException>(
                () =>
                {
                    j.UjJarat("101", "Budapest", "Reykjavík", new DateTime(2022, 03, 01, 11, 0, 0));
                }
                );
        }


        [TestCase]
        public void HanyJaratVanFelveveJelenleg()
        {
            j.UjJarat("102", "Budapest", "Madrid", new DateTime(2022, 07, 01, 17, 0, 0));
            j.UjJarat("103", "Madrid", "Budapest", new DateTime(2022, 08, 01, 20, 0, 0));
            j.UjJarat("104", "Budapest", "Velence", new DateTime(2022, 08, 10, 18, 0, 0));
            Assert.AreEqual(3, j.jaratok.Count);
        }


        // --------------- Keses Test Cases ---------------

        [TestCase]
        public void Keses()
        {
            j.UjJarat("105", "Budapest", "London", new DateTime(2021, 05, 15, 21, 0, 0));
            j.Keses("105", 10);
            Assert.AreEqual(10, j.JaratInfo("105").Keses);

        }


        [TestCase]
        public void KesesTS()
        {
            j.UjJarat("105", "Budapest", "London", new DateTime(2021, 05, 15, 21, 0, 0));
            j.KesesTS("105", new TimeSpan(0, 10, 0));
            Assert.AreEqual(new TimeSpan(0, 10, 0), j.JaratInfo("105").KesesTS);
        }


        [TestCase]
        public void TobbKesesOsszeadodik()
        {
            j.UjJarat("105", "Budapest", "London", new DateTime(2021, 05, 15, 21, 0, 0));
            j.UjJarat("106", "Budapest", "Moszkva", new DateTime(2023, 07, 15, 11, 0, 0));
            j.UjJarat("107", "Budapest", "Szentpétervár", new DateTime(2023, 08, 09, 21, 0, 0));
            j.Keses("105", 10);
            j.Keses("106", 10);
            j.Keses("107", 10);
            Assert.AreEqual(30, j.JaratInfo("105").Keses + j.JaratInfo("106").Keses + j.JaratInfo("107").Keses);
        }


        [TestCase]
        public void TobbKesesOsszeadodikTS()
        {
            j.UjJarat("105", "Budapest", "London", new DateTime(2021, 05, 15, 21, 0, 0));
            j.UjJarat("106", "Budapest", "Moszkva", new DateTime(2023, 07, 15, 11, 0, 0));
            j.UjJarat("107", "Budapest", "Szentpétervár", new DateTime(2023, 08, 09, 21, 0, 0));
            j.KesesTS("105", new TimeSpan(0, 10, 0));
            j.KesesTS("106", new TimeSpan(0, 10, 0));
            j.KesesTS("107", new TimeSpan(0, 10, 0));
            Assert.AreEqual(new TimeSpan(0, 30, 0), j.JaratInfo("105").KesesTS + j.JaratInfo("106").KesesTS + j.JaratInfo("107").KesesTS);
        }


        [TestCase]
        public void KesesNegativ()
        {
            j.UjJarat("108", "Budapest", "London", new DateTime(2021, 05, 15, 21, 0, 0));
            j.Keses("108", 10);
            Assert.Throws<NegativKesesException>(
               () =>
               {
                   j.Keses("108", -15);
               }
               );

        }


        [TestCase]
        public void KesesNegativTS()
        {
            j.UjJarat("108", "Budapest", "London", new DateTime(2021, 05, 15, 21, 0, 0));
            j.KesesTS("108", new TimeSpan(0, 10, 0));
            Assert.Throws<NegativKesesException>(
               () =>
               {
                   j.KesesTS("108", new TimeSpan(0, -10, 0));
               }
               );
        }


        // --------------- MikorIndul Test Cases ---------------

        [TestCase]
        public void MikorIndul()
        {
            j.UjJarat("109", "Budapest", "Barcelona", new DateTime(2021, 11, 10, 23, 0, 0));
            Assert.AreEqual(j.MikorIndul("109"), j.JaratInfo("109").Indulas + j.JaratInfo("109").KesesTS);
            //Debug.WriteLine(j.JaratInfo("109").Indulas);
            TestContext.WriteLine(j.MikorIndul("109"));
            Console.WriteLine(j.JaratInfo("109").Indulas);
        }


        [TestCase]
        public void MikorIndulNincsIlyenJarat()
        {
            j.UjJarat("110", "Budapest", "Paris", new DateTime(2021, 12, 27, 09, 0, 0));
            Assert.Throws<ArgumentException>(
                () =>
                {
                    j.MikorIndul("nemletezojaratszam");
                }
                );
        }


        [TestCase]
        public void MikorIndulKeses()
        {
            j.UjJarat("111", "Budapest", "Rome", new DateTime(2021, 08, 21, 10, 30, 0));
            j.KesesTS("111", new TimeSpan(1, 0, 0)); // 1 óra késés
            Assert.AreEqual(j.MikorIndul("111"), j.JaratInfo("111").Indulas + j.JaratInfo("111").KesesTS);
            TestContext.WriteLine("Mikor indul: " + j.MikorIndul("111"));
            TestContext.WriteLine("JaratInfo indulás: " + j.JaratInfo("111").Indulas);
            TestContext.WriteLine("JaratInfo késés: " + j.JaratInfo("111").KesesTS);
            TestContext.WriteLine("JaratInfo indulás + késés: " + (j.JaratInfo("111").Indulas + j.JaratInfo("111").KesesTS));
        }


        [TestCase]
        public void MikorIndulTobbKeses()
        {
            j.UjJarat("112", "Budapest", "Oslo", new DateTime(2021, 11, 21, 10, 45, 0));
            j.KesesTS("112", new TimeSpan(1, 0, 0)); // 1 óra késés
            j.KesesTS("112", new TimeSpan(2, 0, 0));
            Assert.AreEqual(j.MikorIndul("112"), j.JaratInfo("112").Indulas + j.JaratInfo("112").KesesTS);
        }


        // --------------- MikorIndul Test Cases ---------------

        [TestCase]
        public void RepuloterrolInduloJarat()
        {
            j.UjJarat("113", "Budapest", "Soul", new DateTime(2022, 01, 18, 10, 00, 0));
            Assert.AreEqual(new List<string>() {"113"}, j.JaratokRepuloterrol("Budapest"));
        }


        [TestCase]
        public void RepuloterrolInduloTobbJarat()
        {
            j.UjJarat("114", "Budapest", "Soul", new DateTime(2022, 01, 18, 10, 00, 0));
            j.UjJarat("115", "Budapest", "Tokyo", new DateTime(2022, 01, 18, 10, 00, 0));
            Assert.AreEqual(new List<string>() { "114" }, j.JaratokRepuloterrol("Budapest"));
        }


        [TestCase]
        public void NemLetezeoRepuloterrolJaratIndul()
        {
            j.UjJarat("117", "Budapest", "Soul", new DateTime(2022, 01, 18, 10, 00, 0));
            Assert.Throws<ArgumentException>(
                () =>
                {
                    Assert.AreEqual(new List<string>() { "117" }, j.JaratokRepuloterrol("Tibet"));
                }
                );
        }


        // --------------- Csak a kiírások tesztelésésre ---------------
        // ----- A Trace és Debug a "Debug" ablakban olvasható -----
        // ----- A TestContext és WriteLine a "Tests" ablakban olvasható -----


        [TestCase]
        public void KiirasTeszteles()
        {
            j.UjJarat("118", "Budapest", "Shanghai", new DateTime(2020, 02, 21, 10, 00, 0));

            Trace.WriteLine("Trace - " + "JaratInfo indulás: " + j.JaratInfo("118").Indulas);
            Debug.WriteLine("Debug - " + "JaratInfo indulás: " + j.JaratInfo("118").Indulas);
            TestContext.WriteLine("TestContext - " + "JaratInfo indulás: " + j.JaratInfo("118").Indulas);
            Console.WriteLine("Console - " + "JaratInfo indulás: " + j.JaratInfo("118").Indulas);
        }
    }
}
