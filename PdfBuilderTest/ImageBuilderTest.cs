using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfBuilder;
using iTextSharp.text;

namespace PdfBuilderTest
{
    [TestClass]
    public class ImageBuilderTest
    {
        [TestMethod]
        public void LogoTest()
        {
            var imageBuilder = new ImageBuilder(@"..\Images\20180330_110345-COLLAGE_Crop.jpg");
            Assert.IsTrue((bool)imageBuilder.ReadProperty(i => i.IsJpeg()));
        }
    }
}
