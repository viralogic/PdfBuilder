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
        private string _testHtml = @"
                <table>
                    <tbody>
                        <tr>
                            <td>Name</td>
                            <td>Bruce Fenske</td>
                        </tr>
                    </tbody>
                </table>";

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
        public void RenderHtmlTest()
        {
            var result = new Pdf(PageSize.A4).AddHtml(this._testHtml);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void SaveTest()
        {
            var fileName = @"..\..\Files\HelloWorld.pdf";
            if (!Directory.Exists(@"..\..\Files"))
            {
                Directory.CreateDirectory(@"..\..\Files");
            }
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
            if (!Directory.Exists(@"..\..\Files"))
            {
                Directory.CreateDirectory(@"..\..\Files");
            }

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
            if (!Directory.Exists(@"..\..\Files"))
            {
                Directory.CreateDirectory(@"..\..\Files");
            }
            new Pdf(PageSize.A4).Save(new HelloWorldRenderer(), fileName);
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
        public void PageBreakAfterTest()
        {
            var fileName = @"..\..\Files\HelloWorldRenderer.pdf";
            if (!Directory.Exists(@"..\..\Files"))
            {
                Directory.CreateDirectory(@"..\..\Files");
            }

            var firstPage = new Builder<Paragraph>("This is page 1").SetBuilder(b => { b.PageBreakAfter = true; });

            new Pdf(PageSize.A4).Add(firstPage).Save(new HelloWorldRenderer(), fileName);
            var exists = File.Exists(fileName);
            Assert.IsTrue(File.Exists(fileName));

            using (var reader = new PdfReader(fileName))
            {
                Assert.AreEqual<int>(reader.NumberOfPages, 2);
            }

            if (exists)
            {
                File.Delete(fileName);
            }
        }

        public void PageBreakBeforeTest()
        {
            var fileName = @"..\..\Files\HelloWorldRenderer.pdf";
            if (!Directory.Exists(@"..\..\Files"))
            {
                Directory.CreateDirectory(@"..\..\Files");
            }

            var firstPage = new Builder<Paragraph>("This is page 1");

            new Pdf(PageSize.A4).Add(firstPage).Add(new Builder<Paragraph>("This is page 2").SetBuilder(b => { b.PageBreakBefore = true; }))
                .Save(new HelloWorldRenderer(), fileName);
            var exists = File.Exists(fileName);
            Assert.IsTrue(File.Exists(fileName));

            using (var reader = new PdfReader(fileName))
            {
                Assert.AreEqual<int>(reader.NumberOfPages, 2);
            }

            if (exists)
            {
                File.Delete(fileName);
            }
        }
    }
}
