using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfBuilder.Interfaces
{
    public interface IPdfRenderer
    {
        void RenderPdf(PdfBuilder page);
    }
}
