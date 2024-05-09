using System.IO;
using NUnit.Framework;
using File = Sra.P2rmis.Bll.FileService;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace BLLTest.FileService
{
    /// <summary>
    /// Unit tests for FileService
    /// </summary>
    [TestClass()]
    public class FileServiceTests: BLLBaseTest
    {
        #region Constants
        private string _goodLowerCasePdfFileExtension = "abcd.pdf";
        private string _goodUpperCasePdfFileExtension = "efg.PDF";
        private string _goodMixedCasePdfFileExtension = "asdfasdfasdfasf.pDf";
        private string _NoFileExtensionSeparator = "pDf";
        private string _BadFileExtension = "asdfasdf   fasdfererer.Xya";
        private string _BadLongFileExtension = "a.LetsMakeALongNameHere";
        #endregion
        #region Overhead
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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        #endregion
        #region IsPdfFile - stream
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileGoodSignatureTest()
        {
            Stream stream = SetUpStream(File.FileService.PdfSignature);

            var result = File.FileService.IsPdfFile(stream);

            Assert.IsTrue(result, "did not return result as expected");
        }
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileBadSignatureTest()
        {
            byte[] signature = new byte[] { 0x20, 0x50, 0x44, 0x46 };
            Stream stream = SetUpStream(signature);

            var result = File.FileService.IsPdfFile(stream);

            Assert.IsFalse(result, "did not return result as expected");
        }

        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileBadSignature2Test()
        {
            byte[] signature = new byte[] { 0x20, 0x51, 0x44, 0x46 };
            Stream stream = SetUpStream(signature);

            var result = File.FileService.IsPdfFile(stream);

            Assert.IsFalse(result, "did not return result as expected");
        }
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileBadSignature3Test()
        {
            byte[] signature = new byte[] { 0x20, 0x51, 0x4, 0x46 };
            Stream stream = SetUpStream(signature);

            var result = File.FileService.IsPdfFile(stream);

            Assert.IsFalse(result, "did not return result as expected");
        }
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileEmptySignatureTest()
        {
            byte[] signature = new byte[] {};
            Stream stream = SetUpStream(signature);

            var result = File.FileService.IsPdfFile(stream);

            Assert.IsFalse(result, "did not return result as expected");
        }
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileNullSignatureTest()
        {
            Stream stream = null;

            var result = File.FileService.IsPdfFile((Stream)stream);

            Assert.IsFalse(result, "did not return result as expected");
        }
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileBadSignature4Test()
        {
            byte[] signature = new byte[] { 0x25, 0x50, 0x44, 0x16 };
            Stream stream = SetUpStream(signature);

            var result = File.FileService.IsPdfFile(stream);

            Assert.IsFalse(result, "did not return result as expected");
        }
        #region Helpers
        private Stream SetUpStream(byte[] signature)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(signature);
            writer.Flush();
            writer.Write("My cat's name is Darkmatter, or Fang boy for short.");
            writer.Flush();
            stream.Position = 0;

            return new MemoryStream(stream.GetBuffer(), 0, (int)stream.Length, false);
        }    
        #endregion
        #endregion
        #region IsPdfFile - extension
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileByGoodLowerCaseExtensionTest()
        {
            var result = File.FileService.IsPdfFile(_goodLowerCasePdfFileExtension);

            Assert.IsTrue(result, "did not return result as expected");
        }
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileByGoodUpperCaseExtensionTest()
        {
            var result = File.FileService.IsPdfFile(_goodUpperCasePdfFileExtension);

            Assert.IsTrue(result, "did not return result as expected");
        }
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileByGoodMixedCaseExtensionTest()
        {
            var result = File.FileService.IsPdfFile(_goodMixedCasePdfFileExtension);

            Assert.IsTrue(result, "did not return result as expected");
        }
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileNoExtensionSeparatorTest()
        {
            var result = File.FileService.IsPdfFile(_NoFileExtensionSeparator);

            Assert.IsFalse(result, "did not return result as expected");
        }
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileNBadExtensionTest()
        {
            var result = File.FileService.IsPdfFile(_BadFileExtension);

            Assert.IsFalse(result, "did not return result as expected");
        }
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileLongExtensionTest()
        {
            var result = File.FileService.IsPdfFile(_BadLongFileExtension);

            Assert.IsFalse(result, "did not return result as expected");
        }
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileNullStringTest()
        {
            var result = File.FileService.IsPdfFile(null as string);

            Assert.IsFalse(result, "did not return result as expected");
        }
        /// <summary>
        /// Test IsPdfFile
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsPdfFileEmptyStringTest()
        {
            var result = File.FileService.IsPdfFile(string.Empty);

            Assert.IsFalse(result, "did not return result as expected");
        }
        #endregion
        #region IsFileSizeCorrect tests
        /// <summary>
        /// Test file size
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsFileSizeCorrectTooLargeTest()
        {
            Stream stream = SetUpStream(File.FileService.PdfSignature);

            var result = File.FileService.IsFileSizeCorrect(stream, 5);

            Assert.IsFalse(result, "did not return result as expected");
        }
        /// <summary>
        /// Test file size
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsFileSizeCorrectSmallerTest()
        {
            Stream stream = SetUpStream(File.FileService.PdfSignature);

            var result = File.FileService.IsFileSizeCorrect(stream, 100);

            Assert.IsTrue(result, "did not return result as expected");
        }
        /// <summary>
        /// Test file size
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsFileSizeCorrectMaxTest()
        {
            Stream stream = SetUpStream(File.FileService.PdfSignature);
            var result = File.FileService.IsFileSizeCorrect(stream, 56);

            Assert.IsTrue(result, "did not return result as expected");
        }
        /// <summary>
        /// Test file size
        /// </summary>
        [TestMethod()]
        [Category("FileService")]
        public void IsFileSizeCorrectOverTest()
        {
            Stream stream = SetUpStream(File.FileService.PdfSignature);

            var result = File.FileService.IsFileSizeCorrect(stream, 55);

            Assert.IsFalse(result, "did not return result as expected");
        } 
      #endregion
    }
		
}
