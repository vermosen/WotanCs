using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;
using System.IO;
using Wotan;

namespace wotanUnitTest
{
    /// <summary>
    /// Summary description for serialization
    /// </summary>
    [TestClass]
    public class serialization
    {
        [DataContract(Name = "parent", Namespace = "http://schemas.test.com")]
        [KnownType(typeof(child))]
        public class parent
        {
            [DataMember(IsRequired = true, Name = "myValue", Order = 0)]
            public int value { get; set; }
        }

        [DataContract(Name = "child", Namespace = "http://schemas.test.com")]
        public class child : parent
        {
            [DataMember(IsRequired = true, Name = "myOtherValue", Order = 1)]
            public string myOtherValue { get; set; }
        }

        [DataContract(Name = "container", Namespace = "http://schemas.test.com")]
        public class container
        {
            [DataMember(IsRequired = true, Name = "member", Order = 0)]
            public parent[] members { get; set; }
        }

        public serialization()
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
        public void serializeParent()
        {
            string result;
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamReader reader = new StreamReader(ms))
                {
                    parent ts = new parent() { value = 1 };
                    DataContractSerializer serializer = new DataContractSerializer(ts.GetType());
                    serializer.WriteObject(ms, ts);
                    ms.Position = 0;
                    result = reader.ReadToEnd();

                    Assert.IsTrue(result == "<parent xmlns=\"http://schemas.test.com\" " + 
                                            "xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">" +
                                            "<myValue>1</myValue></parent>");
                }
            }
        }

        [TestMethod]
        public void serializeContainer()
        {
            string result;
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamReader reader = new StreamReader(ms))
                {
                    parent ts = new child() { value = 1, myOtherValue = "hello" };
                    container c = new container()
                    {
                        members = new parent[]
                        {
                            new parent() { value = 1},
                            new child() { value = 1, myOtherValue = "hello" }
                        }
                    };
                    DataContractSerializer serializer = new DataContractSerializer(c.GetType());
                    serializer.WriteObject(ms, c);
                    ms.Position = 0;
                    result = reader.ReadToEnd();

                    Assert.IsTrue(result == "<container xmlns=\"http://schemas.test.com\" " + 
                                            "xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">" + 
                                            "<member><parent><myValue>1</myValue></parent>" + 
                                            "<parent i:type=\"child\"><myValue>1</myValue>" + 
                                            "<myOtherValue>hello</myOtherValue></parent></member></container>");
                }
            }
        }
        [TestMethod]
        public void serializeConfigurationContract()
        {
            configuration c = new configuration()
            {
                logger = new winLoggerContract
                {
                    log = "myLog",
                    source = "mySource",
                    threshold = verbosity.high
                },
                ibEnvironment = new interactiveBroker
                {
                    application = @"my\Application\path.exe",
                    credentials = new credentials
                    {
                        accountType = accountType.live,
                        login = new encryptedString("myLogin"),
                        password = new encryptedString("myPassword"),
                        port = 4001
                    }
                }
            };

            using (var s = new contractSerializer<configuration>())
            {
                using (MemoryStream ms = (MemoryStream)s.fromObject(c))
                {
                    using (StreamReader reader = new StreamReader(ms))
                    {
                        string res = reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
