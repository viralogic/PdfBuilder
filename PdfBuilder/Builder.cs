using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;

namespace PdfBuilder
{
    /// <summary>
    /// Wrapper around and underlying instance of T where T is the type of iTextSharp instance
    /// </summary>
    public class Builder<T>
    {
        public T Instance { get; set; }

        /// <summary>
        /// Default constructor. The underlying iTextSharp instance is attempted to be created using
        /// its default constructor.
        /// </summary>
        public Builder()
        {
            if (typeof(T).GetConstructor(System.Reflection.BindingFlags.Default, null, Type.EmptyTypes, new ParameterModifier[0]) != null)
            {
                this.Instance = Activator.CreateInstance<T>();
            }
        }

        /// <summary>
        /// Constructor with instance of T.
        /// </summary>
        /// <param name="instance"></param>
        public Builder(T instance)
        {
            this.Instance = instance;
        }


        /// <summary>
        /// Set properties for the underlying instance
        /// </summary>
        /// <param name="cb">Callback to execute to set properties of the underlying instance <see cref="Action{T}"/></param>
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
        /// <returns>Builder{T} <see cref="PdfBuilder.Builder{T}/> instance</returns>
        public Builder<T> Set(Action<T> cb)
        {
            cb(this.Instance);
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
        public object ReadProperty(Func<T, object> cb)
        {
            return cb(this.Instance);
        }
    }
}
