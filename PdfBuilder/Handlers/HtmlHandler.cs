using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline;

namespace PdfBuilder.Handlers
{
    class HtmlHandler : IElementHandler
    {
        public List<IElement> Elements { get; private set; }

        public HtmlHandler()
        {
            this.Elements = new List<IElement>();
        }

        public void Add(IWritable w)
        {
            if (w is WritableElement)
            {
                this.Elements.AddRange(((WritableElement)w).Elements());
            }
        }
    }
}
