using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using PdfBuilder.Interfaces;
using PdfBuilder.Handlers;

namespace PdfBuilder
{

    /// <summary>
    /// The business part for creating a PDF. This class will handle all the underlying iTextSharp functions
    /// </summary>
    public class Pdf : Builder<Document>, IDisposable
    {
        private bool _disposed = false;

        private IList<IITextSharpInstance> _elements;

        /// <summary>
        /// The number of elements contained in the PDF instance
        /// </summary>
        public int Count
        {
            get { return this._elements.Count(); }
        }

       
        /// <summary>
        /// Instantiates a PdfBuilder instance from a page size
        /// </summary>
        /// <param name="pageSize">Rectangle that gives the size of page<see cref="iTextSharp.text.Rectangle"/></param>
        public Pdf(Rectangle pageSize)
        {
            this.Instance = new Document(pageSize);
            this._elements = new List<IITextSharpInstance>();
        }

        /// <summary>
        /// Execute an action on the underlying Document instance
        /// </summary>
        /// <param name="cb">Callback to execute to set properties of the underlying document <see cref="System.Action{iTextSharp.text.Document}"/></param>
        /// <returns><see cref="PdfBuilder.Pdf"/> instance</returns>
        /// <example>
        /// <code>
        /// To set properties or perform actions:
        /// 
        /// var pdf = new Pdf(PageSize.A4);
        /// pdf.Set(p =>
        ///     {
        ///         p.Alignment = Element.ALIGN_LEFT; // The paragraph is now aligned on left side of page.
        ///     });
        /// </code>
        /// </example>
        public new Pdf Set(Action<Document> cb)
        {
            base.Set(cb);
            return this;
        }

        /// <summary>
        /// Adds an builder element to the document
        /// </summary>
        /// <param name="builder"><see cref="IITextSharpInstance"/>An instance of a Builder object</param>
        public Pdf Add(IITextSharpInstance builder)
        {
            this._elements.Add(builder);
            return this;
        }

        /// <summary>
        /// Parse html to elements and adds elements to the Pdf
        /// </summary>
        /// <param name="html"><see cref="System.String"/>The HTML as string</param>
        public Pdf AddHtml(string html)
        {
            var htmlHandler = new HtmlHandler();
            using (var sr = new StringReader(html))
            {
                XMLWorkerHelper.GetInstance().ParseXHtml(htmlHandler, sr);
            }
            foreach(var e in htmlHandler.Elements)
            {
                this._elements.Add(new Builder<IElement>(e));
            }
            return this;
        }

        /// <summary>
        /// Renders content on the page using a renderer instance to a given stream
        /// </summary>
        /// <param name="renderer">Instance of object that implements a IPdfRenderer interface<see cref="PdfBuilder.Interfaces.IPdfRenderer"/></param>
        /// <param name="stream">Stream instance used to write pdf text to <see cref="System.IO.Stream"/></param>
        public void Render(IPdfRenderer renderer, Stream stream)
        {
            renderer.RenderPdf(this);
            this.Render(stream);
        }

        /// <summary>
        /// Saves content on the page rendered by a renderer instance to a given file
        /// </summary>
        /// <param name="renderer">Instance of object that implements a IPdfRenderer interface<see cref="PdfBuilder.Interfaces.IPdfRenderer"/> </param>
        /// <param name = "fileName" > The name of the file to write PDF content to<see cref="System.String"/></param>
        public void Save(IPdfRenderer renderer, string fileName)
        {
            using (var f = File.Create(fileName))
            {
                this.Render(renderer, f);
            }
        }

        /// <summary>
        /// Renders HTML content on the page
        /// </summary>
        /// <param name="html">HTML content as <see cref="System.String"/></param>
        /// <param name="stream">Stream instance used to write HTML text to <see cref="System.IO.Stream"/></param>
        public void RenderHtml(string html, Stream stream)
        {
            this.AddHtml(html);
            this.Render(stream);
        }

        /// <summary>
        /// Renders the added elements to a stream
        /// </summary>
        /// <param name="stream">Stream instance used to write elements to <see cref="System.IO.Stream"/></param>
        public void Render(Stream stream)
        {
            using (var writer = PdfWriter.GetInstance(this.Instance, stream))
            {
                this.Instance.Open();
                foreach(var e in this._elements)
                {
                    if (e.PageBreakBefore)
                    {
                        this.Instance.NewPage();
                    }
                    this.Instance.Add(e.Instance as IElement);
                    if (e.PageBreakAfter)
                    {
                        this.Instance.NewPage();
                    }
                }
                this.Instance.Close();
            }
        }

        /// <summary>
        /// Renders the PDF document to a byte array
        /// </summary>
        /// <returns><see cref="System.Byte[]"/></returns>
        public byte[] ToArray()
        {
            byte[] result;
            using (var ms = new MemoryStream())
            {
                this.Render(ms);
                result = ms.ToArray();
            }
            return result;
        }

        /// <summary>
        /// Saves the PDF document to a given file
        /// </summary>
        /// <param name="fileName">The name of the file to write PDF content to <see cref="System.String"/></param>
        /// <example>
        /// <code>
        /// To write to current directory:
        /// pdf.Save("myPdf.pdf");
        /// 
        /// To write to another directory, give its absolute path:
        /// pdf.Save("C:\\MyDocs\\myPdf.pdf") // Note the escapes!
        /// 
        /// You can also write using string literal:
        /// pdf.Save(@"C:\MyDocs\myPdf.pdf") //Note the @ symbol
        /// </code>
        /// </example>
        public void Save(string fileName)
        {
            using (var file = File.Create(fileName))
            {
                var b = this.ToArray();
                file.Write(b, 0, b.Length);
            }
        }



        /// <summary>
        /// Disposes the underlying document object
        /// </summary>
        /// <param name="disposing">boolean to represent if the document object has been disposed <see cref="System.Boolean"/></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this.Instance.Dispose();
                }

                this._disposed = true;
            }
        }

        /// <summary>
        /// The dispose method to call
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
