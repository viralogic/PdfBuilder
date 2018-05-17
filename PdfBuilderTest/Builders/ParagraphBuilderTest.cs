using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfBuilder;

namespace PdfBuilderTest.Builders
{
    [TestClass]
    public class ParagraphBuilderTest
    {

        [TestMethod]
        public void TestRead()
        {
            var paragraph = new Builder<Paragraph>();
            var result = paragraph.ReadProperty(p => p.Content);
            Assert.AreEqual<string>(result, string.Empty);


            paragraph = new Builder<Paragraph>("Hello world!");
            result = paragraph.ReadProperty(p => p.Content);
            Assert.AreEqual<string>("Hello world!", result);

        }

        [TestMethod]
        public void TestSet()
        {
            var alignment = new Builder<Paragraph>("Hello world!")
                .Set(p =>
                {
                    p.Alignment = Element.ALIGN_JUSTIFIED;
                })
                .ReadProperty(p => p.Alignment);

            Assert.AreEqual<int>(Element.ALIGN_JUSTIFIED, (int)alignment);
        }
    }
}
