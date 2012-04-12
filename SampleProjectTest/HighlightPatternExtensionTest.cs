using SampleProject.HtmlHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace SampleProjectTest
{
    
    /// <summary>
    ///This is a test class for HighlightPatternExtensionTest and is intended
    ///to contain all HighlightPatternExtensionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HighlightPatternExtensionTest
    {


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


        /// <summary>
        ///A test for HighlightPattern
        ///</summary>
        [TestMethod()]
        public void HighlightPattern_Test()
        {
            HtmlHelper helper = null; 
            string target = "Spam string spam with spAm pattern sPAm";
            string pattern = "spam";
            string highlightTag = "i";
            MvcHtmlString expected = new MvcHtmlString("<i>Spam</i> string <i>spam</i> with <i>spAm</i> pattern <i>sPAm</i>");
            MvcHtmlString actual;
            actual = HighlightPatternExtension.HighlightPattern(helper, target, pattern, highlightTag);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
    }
}
