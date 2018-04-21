//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html.simpleparser;

//namespace PHI_Umbraco_Components.Templating
//{

//    public class ImageBuilder
//    {
//        public string LogoFilePath { get; private set; }
//        public int Alignment { get; private set; }

//        public float ScalePercent { get; private set; }

//        public ImageBuilder(string filePath)
//            : this(filePath, Image.ALIGN_TOP | Image.ALIGN_LEFT, 100)
//        {

//        }

//        public ImageBuilder(string filePath, int alignment)
//            : this(filePath, alignment, 100)
//        {

//        }

//        public ImageBuilder(string filePath, int alignment, float scale)
//        {
//            this.LogoFilePath = filePath;
//            this.Alignment = alignment;
//            this.ScalePercent = scale;
//        }

//        /// <summary>
//        /// Set alignment for image.
//        /// </summary>
//        /// <param name="alignment">iTextSharp Alignment constant</param>
//        /// <returns></returns>
//        public ImageBuilder Align(int alignment)
//        {
//            this.Alignment = alignment;
//            return this;
//        }

//        /// <summary>
//        /// Scale image
//        /// </summary>
//        /// <param name="scale">float representing percent scale</param>
//        /// <returns></returns>
//        public ImageBuilder Scale(float scale)
//        {
//            this.ScalePercent = scale;
//            return this;
//        }
//    }

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
//}
