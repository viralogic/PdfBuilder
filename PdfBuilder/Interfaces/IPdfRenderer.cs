using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfBuilder.Interfaces
{
    /// <summary>
    /// Interface for rendering a PDF document
    /// </summary>
    public interface IPdfRenderer
    {
        /// <summary>
        /// Renders a PDF document to the given PdfBuilder instance
        /// </summary>
        /// <param name="page"><see cref="Pdf"/>A PdfBuilder instance to render PDF to</param>
        void RenderPdf(Pdf page);
    }
}
