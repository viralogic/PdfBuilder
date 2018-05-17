using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfBuilder;
using PdfBuilder.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfBuilderTest.Builders
{

    [TestClass]
    public class PdfBuilderTest
    {
        [TestMethod]
        public void AddElementTest()
        {
            var pdf = new Pdf(PageSize.A4).Add(new Builder<Paragraph>("Hello world!"));
            Assert.IsTrue(pdf.Count > 0);
        }

        [TestMethod]
        public void RenderTest()
        {
            var result = new Pdf(PageSize.A4).Add(new Builder<Paragraph>("Hello world!")).ToArray();
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void SaveTest()
        {
            var fileName = @"..\..\Files\HelloWorld.pdf";
            new Pdf(PageSize.A4).Add(new Builder<Paragraph>("Hello world!")).Save(fileName);
            var exists = File.Exists(fileName);
            Assert.IsTrue(File.Exists(fileName));

            using (var reader = new PdfReader(fileName))
            {
                var text = System.Text.Encoding.UTF8.GetString(reader.GetPageContent(1));
                Assert.IsTrue(text.Contains("Hello world!"));
            }

            if (exists)
            {
                File.Delete(fileName);
            }
        }
    }
}
