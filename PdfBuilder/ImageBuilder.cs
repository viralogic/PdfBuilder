using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;

namespace PdfBuilder
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
