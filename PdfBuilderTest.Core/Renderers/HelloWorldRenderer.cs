using iTextSharp.text;
using PdfBuilder.Core;
using PdfBuilder.Core.Interfaces;

namespace PdfBuilderTest.Core.Renderers
{
    class HelloWorldRenderer : IPdfRenderer
    {
        public void RenderPdf(Pdf page)
        {
            page.Add(new Builder<Paragraph>("Hello World!"));
        }
    }
}
