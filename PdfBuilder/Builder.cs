using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using PdfBuilder.Interfaces;

namespace PdfBuilder
{
    /// <summary>
    /// Wrapper around and underlying instance of T where T is an IElement
    /// </summary>
    public class Builder<T> : IITextSharpInstance
    {
        private bool _pageBreakBefore = false;
        private bool _pageBreakAfter = false;

        /// <summary>
        /// The underlying instance of <see cref="T"/>
        /// </summary>
        public dynamic Instance { get; protected set; }


        /// <summary>
        /// Page break before the Builder instance
        /// The default is set to false (no page break)
        /// </summary>
        public bool PageBreakBefore
        {
            get { return this._pageBreakBefore; }
            set { this._pageBreakBefore = value; }
        }

        /// <summary>
        /// Page break after the Builder instance.
        /// The default is set to false (no page break)
        /// </summary>
        public bool PageBreakAfter
        {
            get { return this._pageBreakAfter; }
            set { this._pageBreakAfter = value; }
        }

        /// <summary>
        /// Default constructor. The underlying iTextSharp instance is attempted to be created using
        /// its default constructor.
        /// </summary>
        public Builder()
        {
            if (typeof(T).GetConstructor(Type.EmptyTypes) != null)
            {
                this.Instance = Activator.CreateInstance<T>();
            }
        }

        /// <summary>
        /// Constructor with parameters for instantiating instance of <typeparamref name="T"/>
        /// </summary>
        /// <param name="p"><see cref="System.Array"/>An array of objects used as constructor parameters</param>
        public Builder(params object[] p)
        {
            var underlyingTypes = p.Select(x => x.GetType().UnderlyingSystemType).ToArray();
            if (typeof(T).GetConstructor(underlyingTypes) != null)
            {
                this.Instance = (T)Activator.CreateInstance(typeof(T), p);
            }
            else
            {
                throw new TargetInvocationException(new Exception(
                string.Format(
                    "Cannot find constructor for type {0} with parameters {1}",
                    typeof(T).ToString(), String.Join(", ", underlyingTypes.Select(t => t.ToString()))
                )));
            }
        }

        /// <summary>
        /// Constructor from an instance of <typeparamref name="T"/>
        /// </summary>
        /// <param name="instance"><typeparamref name="T"/> instance</param>
        public Builder(T instance)
        {
            this.Instance = instance;
        }


        /// <summary>
        /// Execute an action on T
        /// </summary>
        /// <param name="cb">Callback to execute to set properties of the underlying instance <see cref="System.Action{T}"/></param>
        /// <typeparam name="T"></typeparam>
        /// <returns><see cref="Builder{T}"/> instance</returns>
        /// <example>
        /// <code>
        /// To set properties or perform actions:
        /// 
        /// var builder = new Builder<![CDATA[<Paragraph>]]>("Some text");
        /// builder.Set(p =>
        ///     {
        ///         p.FontSize = 12; // The font size for the paragraph has been set to 12
        ///     });
        /// </code>
        /// </example>
        public Builder<T> Set(Action<T> cb)
        {
            cb(this.Instance);
            return this;
        }

        /// <summary>
        /// Execute an action on <see cref="Builder{T}"/>
        /// </summary>
        /// <param name="cb">Callback to execute to set properties of the Builder instance <see cref="System.Action{Builder{T}}"/></param>
        /// <returns><see cref="Builder{T}"/></returns>
        /// <example>
        /// <code>
        /// To set properties or perform actions:
        /// 
        /// var builder = new Builder<![CDATA[<Paragraph>]]>("Some text");
        /// builder.SetBuilder(b =>
        ///     {
        ///         b.PageBreakBefore = true; // Will add a page break before rendering this element to Pdf
        ///     });
        /// </code>
        /// </example>
        public Builder<T> SetBuilder(Action<Builder<T>> cb)
        {
            cb(this);
            return this;
        }

        /// <summary>
        /// Read properties of T
        /// </summary>
        /// <param name="cb">Callback to execute on T <see cref="Func{T, TResult}"/></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns>The return value of the callback</returns>
        /// <example>
        /// <code>
        /// To return a value from T:
        /// 
        /// var builder = new Builder<![CDATA[<Paragraph>]]>("Some text");
        /// return builder.ReadProperty(p => p.Content); // Returns "Some text"
        /// </code>
        /// </example>
        public TResult ReadProperty<TResult>(Func<T, TResult> cb)
        {
            return cb(this.Instance);
        }

        /// <summary>
        /// Read properties of this <see cref="Builder{T}"/> instance
        /// </summary>
        /// <param name="cb">Callback to execute on <see cref="Func{Builder{T}, TResult}"/></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns>The return value of the callback</returns>
        /// <example>
        /// <code>
        /// To return a value from this Builder instance:
        /// 
        /// var builder = new Builder<![CDATA[<Paragraph>]]>("Some text");
        /// return builder.ReadBuilder(p => p.PageBreakAfter);
        /// </code>
        /// </example>
        public TResult ReadBuilder<TResult>(Func<Builder<T>, TResult> cb)
        {
            return cb(this);
        }
    }
}
