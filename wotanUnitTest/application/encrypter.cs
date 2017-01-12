﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wotan;
using System.IO;

namespace wotanUnitTest.application
{
    /// <summary>
    /// Summary description for encrypter
    /// </summary>
    [TestClass]
    public class encrypter
    {
        public encrypter()
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
        public void encrypterFactory()
        {
            var fact = new encrypterFactory();
            Wotan.encrypter e = fact.create(encrypterType.AES);

            Assert.IsInstanceOfType(e, typeof(AESEncrypter));
        }

        [TestMethod]
        public void encryptAES()
        {
            Wotan.encrypter e = (new encrypterFactory()).create(encrypterType.AES);

            string s = "Hello World";
            string encrypt = e.encrypt(s);
            string decrypt = e.decrypt(encrypt);
            Assert.AreEqual(decrypt, s);
        }

        [TestMethod]
        public void deserializePassword()
        {
            Wotan.encrypter e = (new encrypterFactory()).create(encrypterType.AES);

            string s = "Hello World";
            encryptedString encrypt = new encryptedString(s);
            var test = encrypt.decrypted;

            using (var serializer = new contractSerializer<encryptedString>())
            {
                using (MemoryStream ms = (MemoryStream)serializer.fromObject(encrypt))
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
