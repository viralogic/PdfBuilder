﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using PdfBuilder.Interfaces;

namespace PdfBuilder
{

    /// <summary>
    /// The business part for creating a PDF. This class will handle all the underlying iTextSharp functions
    /// </summary>
    public class Pdf : Builder<Document>, IDisposable
    {
        private bool _disposed = false;

        private IList<object> _elements;

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
            this._elements = new List<object>();
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
        ///         p.Add; // The font size for the paragraph has been set to 12
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
        /// <param name="builder"><see cref="Builder{T}"/>An instance of a Builder object</param>
        public Pdf Add<T>(Builder<T> builder)
        {
            this._elements.Add(builder.Instance);
            return this;
        }

        /// <summary>
        /// Renders content on the page using a renderer instance
        /// </summary>
        /// <param name="renderer">Instance of object that implements of IPdfRenderer interface<see cref="PdfBuilder.Interfaces.IPdfRenderer"/></param>
        /// <param name="stream">Stream instance used to write pdf text to <see cref="System.IO.Stream"/></param>
        public void Render(IPdfRenderer renderer, Stream stream)
        {
            using (var writer = PdfWriter.GetInstance(this.Instance, stream))
            {
                this.Instance.Open();
                renderer.RenderPdf(this);
                this.Instance.Close();
            }
        }

        /// <summary>
        /// Renders HTML content on the page
        /// </summary>
        /// <param name="html">HTML content as <see cref="System.String"/></param>
        /// <param name="stream">Stream instance used to write HTML text to <see cref="System.IO.Stream"/></param>
        public void RenderHtml(string html, Stream stream)
        {
            using (var sr = new StringReader(html))
            {
                using (var writer = PdfWriter.GetInstance(this.Instance, stream))
                {
                    this.Instance.Open();
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, this.Instance, sr);
                    this.Instance.Close();
                }
            }
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
                    this.Instance.Add(e as IElement);
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