using System;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PdfBuilderTest.Builders
{
    [TestClass]
    public class TableBuilderTest
    {
        [TestMethod]
        public void AddRowTest()
        {
            var table = new TableBuilder(3)
                .Add(new TableRowBuilder(
                        new List<Builder<PdfPCell>>()
                        {
                            new Builder<PdfPCell>(new Phrase("Bruce Fenske")),
                            new Builder<PdfPCell>(new Phrase("780-269-4321")),
                            new Builder<PdfPCell>(new Phrase("Canada")),
                        }
                    )
                )
                .Add(new TableRowBuilder(
                        new List<Builder<PdfPCell>>()
                        {
                            new Builder<PdfPCell>(new Phrase("Wayne Gretzky")),
                            new Builder<PdfPCell>(new Phrase("780-123-4567")),
                            new Builder<PdfPCell>(new Phrase("Canada"))
                        }
                    )
                );

            Assert.AreEqual<int>(table.ReadProperty(t => t.Rows.Count), 2);
        }
    }
}
