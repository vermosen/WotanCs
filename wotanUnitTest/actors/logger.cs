using Microsoft.VisualStudio.TestTools.UnitTesting;
using Akka.Actor;

namespace Wotan
{
    /// <summary>
    /// Summary description for logger
    /// </summary>
    [TestClass]
    public class logger
    {
        public logger()
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
        public void simpleLog()
        {
            var actorSystem_ = ActorSystem.Create("actorSystem");
            var d = actorSystem_.ActorOf(Props.Create<actors.logger>(new consoleLogger(verbosity.low)), "logger");
            d.Tell(new actors.log("hello World", logType.error, verbosity.high));
        }

        [TestMethod]
        public void windowsLog()
        {
            var actorSystem_ = ActorSystem.Create("actorSystem");
            var d = actorSystem_.ActorOf(Props.Create<actors.logger>(new winLogger("Application", "Wotan Test", verbosity.low)), "logger");
            d.Tell(new actors.log("hello World", logType.error, verbosity.high));
        }

        [TestMethod]
        public void multipleLog()
        {
            //var actorSystem_ = ActorSystem.Create("actorSystem");
            //var d = actorSystem_.ActorOf(Props.Create<actors.logger<winLogger>>(new winLogger("Application", "Wotan Test", verbosity.low)), "logger");
            //d.Tell(new actors.log("hello World", logType.error, verbosity.high));
        }
    }
}
