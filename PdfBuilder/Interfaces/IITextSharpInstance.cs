using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfBuilder.Interfaces
{
    public interface IITextSharpInstance
    {
        dynamic Instance { get; }
        bool PageBreakBefore { get; set; }
        bool PageBreakAfter { get; set; }
    }
}
