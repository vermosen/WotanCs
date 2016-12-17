using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Wotan
{
    /// <summary>
    /// Summary description for correlationId
    /// </summary>
    [TestClass]
    public class correlationId
    {
        private actors.correlationManager manager_;
        public correlationId()
        {
            manager_ = new actors.correlationManager();
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
        public void getCorrelationsSingleThread()
        {
            Assert.IsTrue(manager_.next().id == 1);
            Assert.IsTrue(manager_.next().id == 2);
            Assert.IsTrue(manager_.next().id == 3);
            Assert.IsTrue(manager_.next().id == 4);
            Assert.IsTrue(manager_.next().id == 5);
            Assert.IsTrue(manager_.next().id == 6);
        }
        public void getCorrelationsMiltiThread()
        {
            // TODO
        }
    }
}
