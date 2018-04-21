using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfBuilder.Builders;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfBuilderTest.Builders
{
    [TestClass]
    public class ParagraphBuilderTest
    {
        private ParagraphBuilder Paragraph { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            this.Paragraph = new ParagraphBuilder("Hello world");
        }

        [TestMethod]
        public void TestRead()
        {
            var result = (string)this.Paragraph.Read(p => p.Content);
            Assert.AreEqual<string>("Hello world", result);
        }

        [TestMethod]
        public void TestSet()
        {
            this.Paragraph.Set(p =>
            {
                p.Alignment = Element.ALIGN_JUSTIFIED;
            });

            Assert.AreEqual<int>(Element.ALIGN_JUSTIFIED, (int)this.Paragraph.Read(p => p.Alignment));
        }
    }
}
