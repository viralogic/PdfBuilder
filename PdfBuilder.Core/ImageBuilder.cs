using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;

namespace PdfBuilder.Core
{
    /// <summary>
    /// Handles PDF images
    /// </summary>
    public class ImageBuilder : Builder<Image>
    {
        /// <summary>
        /// Constructs an ImageBuilder instance from a file name
        /// </summary>
        /// <param name="filePath">The full path to the file<see cref="System.String"/></param>
        public ImageBuilder(string filePath)
        {
            this.Instance = Image.GetInstance(filePath);
        }
    }
}
