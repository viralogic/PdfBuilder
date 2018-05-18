using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfBuilder;
using PdfBuilder.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfBuilderTest.Renderers;

namespace PdfBuilderTest.Builders
{

    [TestClass]
    public class PdfTest
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

        [TestMethod]
        public void RenderPdfTest()
        {
            byte[] l;
            using (var ms = new MemoryStream())
            {
                new Pdf(PageSize.A4).Render(new HelloWorldRenderer(), ms);
                l = ms.ToArray();
                Assert.IsTrue(l.Length > 0);
            }

            var fileName = @"..\..\Files\HelloWorldRenderer.pdf";

            using (var f = File.Create(fileName))
            {
                f.Write(l, 0, l.Length);
            }

            var exists = File.Exists(fileName);
            Assert.IsTrue(File.Exists(fileName));

            using (var reader = new PdfReader(fileName))
            {
                var text = System.Text.Encoding.UTF8.GetString(reader.GetPageContent(1));
                Assert.IsTrue(text.Contains("Hello World!"));
            }

            if (exists)
            {
                File.Delete(fileName);
            }
        }

        [TestMethod]
        public void RenderSaveTest()
        {
            var fileName = @"..\..\Files\HelloWorldRenderer.pdf";
            new Pdf(PageSize.A4).Add(new Builder<Paragraph>("Hello World!")).Save(new HelloWorldRenderer(), fileName);
            var exists = File.Exists(fileName);
            Assert.IsTrue(File.Exists(fileName));

            using (var reader = new PdfReader(fileName))
            {
                var text = System.Text.Encoding.UTF8.GetString(reader.GetPageContent(1));
                Assert.IsTrue(text.Contains("Hello World!"));
            }

            if (exists)
            {
                File.Delete(fileName);
            }
        }
    }
}
