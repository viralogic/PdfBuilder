using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfBuilder.Core;
using System.IO;
using iTextSharp.text;
using System.Reflection;

namespace PdfBuilderTest.Core.Builders
{
    [TestClass]
    public class ImageBuilderTest
    {
        private const string IMG_FILE_PATH = @"Images\20180330_110345-COLLAGE_Crop.jpg";

        [TestMethod]
        public void LogoTest()
        {
            var imgFile = Path.Combine(Directory.GetCurrentDirectory(), IMG_FILE_PATH);
            var imageBuilder = new ImageBuilder(imgFile);
            Assert.IsTrue((bool)imageBuilder.ReadProperty(i => i.IsJpeg()));
        }

        [TestMethod]
        public void ScaleTest()
        {
            var imgFile = Path.Combine(Directory.GetCurrentDirectory(), IMG_FILE_PATH);
            var imageBuilder = new ImageBuilder(imgFile);
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
