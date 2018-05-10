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
    /// Wrapper around and underlying instance of T where T is an IElement
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
        /// Read properties of T
        /// </summary>
        /// <param name="cb">Callback to execute on T <see cref="Func{T, TResult}"/></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns><see cref="System.Object"/>An instance of an object. Please note that this object will need to explicitly cast into appropriate type</returns>
        /// <example>
        /// <code>
        /// To return a value from T:
        /// 
        /// var builder = new Builder<![CDATA[<Paragraph>]]>("Some text");
        /// return (string)builder.Read(p => p.Content); // Returns "Some text"
        /// </code>
        /// </example>
        public object ReadProperty(Func<T, object> cb)
        {
            return cb(this.Instance);
        }
    }
}
