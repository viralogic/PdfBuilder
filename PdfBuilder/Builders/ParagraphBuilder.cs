using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;

namespace PdfBuilder.Builders
{
    /// <summary>
    /// Class that wraps the Paragraph class in iTextSharp
    /// </summary>
    public class ParagraphBuilder
    {
        private Paragraph Paragraph { get; set; }

        public string Text { get; private set; }

        /// <summary>
        /// Instantiate a ParagraphBuilder instance
        /// </summary>
        /// <param name="text">Paragraph text <see cref="Sytem.String"/></param>
        public ParagraphBuilder(string text)
        {
            this.Text = text;
            this.Paragraph = new Paragraph(this.Text);
        }

        /// <summary>
        /// Default constructor. The paragraph text has not been set
        /// </summary>
        public ParagraphBuilder()
        {
            this.Paragraph = new Paragraph();
        }

        /// <summary>
        /// Set the paragraph text
        /// </summary>
        /// <param name="text">Paragraph text <see cref="System.String"/></param>
        /// <returns>ParagraphBuilder <see cref="PdfBuilder.Builders.ParagraphBuilder"/> instance</returns>
        public ParagraphBuilder SetText(string text)
        {
            this.Text = text;
            return this;
        }

        /// <summary>
        /// Set properties for the paragraph
        /// </summary>
        /// <param name="cb">Callback to execute when rendering this paragraph instance <see cref="Action{T}"/></param>
        /// <typeparam name="T"><see cref="iTextSharp.text.Paragraph"/></typeparam>
        /// <code>
        /// To format the paragraph:
        /// 
        /// var builder = new ParagraphBuilder("Some text");
        /// builder.Set(p =>
        ///     {
        ///         p.FontSize = 12;
        ///     });
        /// </code>
        /// <returns>ParagraphBuilder <see cref="PdfBuilder.Builders.ParagraphBuilder"/> instance</returns>
        public ParagraphBuilder Set(Action<Paragraph> cb)
        {
            cb(this.Paragraph);
            return this;
        }

        /// <summary>
        /// Read properties of the paragraph
        /// </summary>
        /// <param name="cb">Callback to execute to read the paragraph property <see cref="Func{T, TResult}"/></param>
        /// <typeparam name="T"><see cref="iTextSharp.text.Paragraph"/></typeparam>
        /// <typeparam name="TResult"><see cref="System.Object"/></typeparam>
        /// <code>
        /// To return a value from the underlying iTextSharp Paragraph:
        /// 
        /// var builder = new ParagraphBuilder("Some text");
        /// return (string)builder.Read(p => p.Content); // Returns "Some text"
        /// 
        /// Please note how the value obtained by the Read method will need to be explicitly cast into appropriate type
        /// 
        /// </code>
        /// <returns><see cref="System.Object"/></returns>
        public object Read(Func<Paragraph, object> cb)
        {
            return cb(this.Paragraph);
        }
    }
}
