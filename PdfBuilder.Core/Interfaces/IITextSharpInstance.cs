using System;
using System.Collections.Generic;
using System.Text;

namespace PdfBuilder.Core.Interfaces
{
    public interface IITextSharpInstance
    {
        dynamic Instance { get; }
        bool PageBreakBefore { get; set; }
        bool PageBreakAfter { get; set; }
    }
}
