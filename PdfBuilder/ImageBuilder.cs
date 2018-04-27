using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;

namespace PdfBuilder
{
    public class ImageBuilder : Builder<Image>
    {
        public ImageBuilder(string filePath)
        {
            this.Instance = Image.GetInstance(filePath);
        }
    }
}
