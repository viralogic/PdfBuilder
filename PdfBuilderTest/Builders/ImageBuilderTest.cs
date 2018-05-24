using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfBuilder;
using iTextSharp.text;

namespace PdfBuilderTest.Builders
{
    [TestClass]
    public class ImageBuilderTest
    {
        private const string IMG_FILE_PATH = @"..\Images\20180330_110345-COLLAGE_Crop.jpg";

        [TestMethod, TestCategory("Images")]
        public void LogoTest()
        {
            var imageBuilder = new ImageBuilder(IMG_FILE_PATH);
            Assert.IsTrue((bool)imageBuilder.ReadProperty(i => i.IsJpeg()));
        }

        [TestMethod, TestCategory("Images")]
        public void ScaleTest()
        {
            var imageBuilder = new ImageBuilder(IMG_FILE_PATH);
            var startHeight = (float)imageBuilder.ReadProperty(i => i.ScaledHeight);
            var startWidth = (float)imageBuilder.ReadProperty(i => i.ScaledWidth);
            imageBuilder.Set(i => { i.ScalePercent(60); });
            var endHeight = (float)imageBuilder.ReadProperty(i => i.ScaledHeight);
            Assert.IsTrue(startHeight > endHeight);
            var endWidth = (float)imageBuilder.ReadProperty(i => i.ScaledWidth);
            Assert.IsTrue(startWidth > endWidth);

            Assert.AreEqual<double>(Math.Round(0.6, 1), Math.Round(endHeight / startHeight, 1));
            Assert.AreEqual<double>(Math.Round(0.6, 1), Math.Round(endWidth / startWidth, 1));
        }
    }
}
