using System;
using System.Text;
using System.Collections.Generic;
using Wotan.actors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IBApi;
using System.Threading;

namespace wotanUnitTest.tws
{
    /// <summary>
    /// Summary description for client
    /// </summary>
    [TestClass]
    public class client
    {
        private EReaderSignal signal_;
        private Wotan.client client_;

        private void dummy1(Wotan.message m)
        {

        }
        private void dummy2(log l)
        {

        }

        public client()
        {
            client_ = new Wotan.client(signal_, new dispatchDlg(dummy1), new logDlg(dummy2));
            signal_ = new EReaderMonitorSignal();
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
        public void connect()
        {
            if (!client_.socket.IsConnected())
            {
                client_.socket.eConnect("127.0.0.1", 4001, 1);
                EReader reader = new EReader(client_.socket, signal_);
                reader.Start();
                client_.connectAck();

                Thread.Sleep(1000);

                new Thread(() =>
                {
                    while (client_.socket.IsConnected())
                    {
                        signal_.waitForSignal();
                        reader.processMsgs();
                    }
                })
                { Name = "reading thread", IsBackground = true }.Start();
            }
        }
    }
}
