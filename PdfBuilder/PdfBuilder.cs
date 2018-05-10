using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using PdfBuilder.Interfaces;

namespace PdfBuilder
{

    public class PdfBuilder : Builder<Document>, IDisposable
    {
        private bool _disposed = false;

        /// <summary>
        /// Instantiates a PdfBuilder instance from a page size
        /// </summary>
        /// <param name="pageSize">Rectangle that gives the size of page<see cref="iTextSharp.text.Rectangle"/></param>
        public PdfBuilder(Rectangle pageSize)
        {
            this.Instance = new Document(pageSize);
        }

        /// <summary>
        /// Adds an builder element to the document
        /// </summary>
        /// <param name="builder"><see cref="Builder{T}"/>An instance of a Builder object</param>
        public void Add(Builder<IElement> builder)
        {
            this.Instance.Add(builder.Instance);
        }

        /// <summary>
        /// Add HTML to a document
        /// </summary>
        /// <param name="html"><see cref="System.String"/> HTML text</param>
        public void AddHtml(string html)
        {
            using (var sr = new StringReader(html))
            {
                var providers = new Dictionary<string, object>();
                var elements = HTMLWorker.ParseToList(sr, null, providers);
                foreach (var e in elements)
                {
                    this.Instance.Add((IElement)e);
                }
            }
        }

        /// <summary>
        /// Renders content on the page using a renderer instance
        /// </summary>
        /// <param name="renderer">Instance of object that implements of IPdfRenderer interface</param>
        /// <param name="stream">Stream instance used to write pdf text to <see cref="System.IO.Stream"/></param>
        public void GeneratePdf(IPdfRenderer renderer, Stream stream)
        {
            using (var writer = PdfWriter.GetInstance(this.Instance, stream))
            {
                this.Instance.Open();
                renderer.RenderPdf(this);
                this.Instance.Close();
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

//    public class PdfBuilder
//    {
//        private Stream _stream;

//        private Rectangle Page { get; set; }

//        private ParagraphBuilder Title { get; set; }
//        private List<ImageBuilder> Images { get; set; }

//        private List<ParagraphBuilder> Paragraphs { get; set; }

//        private string Html { get; set; }

//        public PdfBuilder(Stream stream)
//        {
//            this._stream = stream;
//            this.Page = PageSize.A4;
//            this.Images = new List<ImageBuilder>();
//            this.Paragraphs = new List<ParagraphBuilder>();
//        }

//        public PdfBuilder SetPage(Rectangle rect)
//        {
//            this.Page = rect;
//            return this;
//        }

//        public PdfBuilder SetLandscape()
//        {
//            this.Page = this.Page.Rotate();
//            return this;
//        }

//        public PdfBuilder SetTitle(ParagraphBuilder paragraph)
//        {
//            this.Title = paragraph;
//            return this;
//        }

//        public PdfBuilder AddImage(ImageBuilder image)
//        {
//            this.Images.Add(image);
//            return this;
//        }

//        private Image CreateImage(ImageBuilder image)
//        {
//            var img = Image.GetInstance(image.LogoFilePath);
//            img.Alignment = image.Alignment;
//            img.ScalePercent(image.ScalePercent);
//            return img;
//        }

//        public PdfBuilder AddParagraph(ParagraphBuilder paragraph)
//        {
//            this.Paragraphs.Add(paragraph);
//            return this;
//        }

//        private Paragraph CreateParagraph(ParagraphBuilder paragraph)
//        {
//            var font = FontFactory.GetFont(paragraph.Font, paragraph.FontSize);
//            var p = new Paragraph(paragraph.Text, font);
//            p.SpacingBefore = paragraph.Spacing;
//            p.SpacingAfter = paragraph.Spacing;
//            p.Alignment = paragraph.TextAlignment;
//            return p;
//        }

//        public PdfBuilder AddHtml(string html)
//        {
//            this.Html = html;
//            return this;
//        }

//        public void GeneratePdf(string baseUrl = null)
//        {
//            using (var document = new Document(this.Page))
//            {
//                using (var writer = PdfWriter.GetInstance(document, this._stream))
//                {
//                    document.Open();

//                    //Add title
//                    if (this.Title != null)
//                    {
//                        document.Add(this.CreateParagraph(this.Title));
//                    }

//                    //Add images
//                    foreach (var img in this.Images)
//                    {
//                        document.Add(this.CreateImage(img));
//                    }

//                    //Add paragraphs
//                    foreach (var p in this.Paragraphs)
//                    {
//                        document.Add(this.CreateParagraph(p));
//                    }

//                    //Add html
//                    if (!string.IsNullOrEmpty(this.Html))
//                    {
//                        using (var sr = new StringReader(this.Html))
//                        {
//                            // Just to add ability to load up images in img tag -- not styled
//                            var providers = new Dictionary<string, object>();
//                            if (!string.IsNullOrEmpty(baseUrl))
//                            {
//                                providers.Add(HTMLWorker.IMG_BASEURL, baseUrl);
//                            }
//                            var elements = HTMLWorker.ParseToList(sr, null, providers);
//                            foreach (var e in elements)
//                            {
//                                document.Add((IElement)e);
//                            }
//                        }
//                    }
//                    document.Close();
//                }
//            }
//        }
//    }
}
