using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IBApi;
using System.Threading;
using Wotan;

namespace wotanUnitTest.tws
{
    /// <summary>
    /// Summary description for client
    /// </summary>
    [TestClass]
    public class client
    {
        public client()
        {

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
        public void connectSync()
        {
            EReaderMonitorSignal signal_ = new EReaderMonitorSignal();
            Wotan.client client_ = new Wotan.client(signal_, new dispatchDlg((twsMessage m) => {}), new logDlg((log l) => {}), false);

            if (!client_.socket.IsConnected())
            {
                client_.socket.eConnect("127.0.0.1", 4001, 0);
                EReader reader = new EReader(client_.socket, signal_);
                reader.Start();

                new Thread(() =>
                {
                    while (client_.socket.IsConnected())
                    {
                        signal_.waitForSignal();
                        reader.processMsgs();
                    }
                })
                { Name = "reading thread", IsBackground = true }.Start();

                Thread.Sleep(1000);
                client_.socket.eDisconnect();
            }
        }

        [TestMethod]
        public void connectAsync()
        {
            bool result = false;
            EReaderMonitorSignal signal_ = new EReaderMonitorSignal();
            Wotan.client client_ = null;
            EReader reader_ = null;

            client_ = new Wotan.client(
                    signal_,
                    new dispatchDlg((twsMessage m) =>
                    {
                        result = true;
                    }),
                    new logDlg((log l) => 
                    {

                    }), true);

            reader_ = new EReader(client_.socket, signal_);
            reader_.Start();
            client_.connectAck();

            Thread.Sleep(1000);
            new Thread(() =>
            {
                while (client_.socket.IsConnected())
                {
                    signal_.waitForSignal();
                    reader_.processMsgs(); 
                }
            }) { Name = "reading thread", IsBackground = true }.Start();

            Thread.Sleep(10000);

        }
    }
}