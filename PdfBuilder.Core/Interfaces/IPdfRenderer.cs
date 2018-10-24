using System;
using System.Collections.Generic;
using System.Text;

namespace PdfBuilder.Core.Interfaces
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
