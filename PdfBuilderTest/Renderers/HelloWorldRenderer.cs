using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfBuilder;
using PdfBuilder.Interfaces;
using iTextSharp.text;

namespace PdfBuilderTest.Renderers
{
    class HelloWorldRenderer : IPdfRenderer
    {
        public void RenderPdf(Pdf page)
        {
            page.Add(new Builder<Paragraph>("Hello World!"));
        }
    }
}
