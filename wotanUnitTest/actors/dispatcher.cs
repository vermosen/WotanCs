using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Akka.Actor;

namespace wotanUnitTest
{
    /// <summary>
    /// Summary description for dispatcher
    /// </summary>
    [TestClass]
    public class dispatcher
    {
        public dispatcher()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void sendToDispatcher()
        {
            var actorSystem_ = ActorSystem.Create("actorSystem");
            var d = actorSystem_.ActorOf(Props.Create<Wotan.actors.dispatcher>(new Wotan.nullLogger()), "dispatcher");
            d.Tell(new Wotan.historicalData(0, "20160312", 0.0, 0.0, 0.0, 0.0, 10, 10, 1.1, true), null);
        }
    }
}
