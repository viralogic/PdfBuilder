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
            if (typeof(T).GetConstructor(Type.EmptyTypes) != null)
            {
                this.Instance = Activator.CreateInstance<T>();
            }
        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="p"><see cref="System.Array{Object}"/></param>
        public Builder(params object[] p)
        {
            var underlyingTypes = p.Select(x => x.GetType().UnderlyingSystemType).ToArray();
            if (typeof(T).GetConstructor(underlyingTypes) != null)
            {
                this.Instance = (T)Activator.CreateInstance(typeof(T), p);
            }
            throw new TargetInvocationException(new Exception(
                string.Format(
                    "Cannot find constructor for type {0} with parameters {1}",
                    typeof(T).ToString(), String.Join(", ", underlyingTypes.Select(t => t.ToString()))
                )));
        }


        public Builder(T instance)
        {
            this.Instance = instance;
        }


        /// <summary>
        /// Execute an action on T
        /// </summary>
        /// <param name="cb">Callback to execute to set properties of the underlying instance <see cref="Action{T}"/></param>
        /// <typeparam name="T"></typeparam>
        /// <code>
        /// To set properties or perform actions:
        /// 
        /// var builder = new Builder{Paragraph}("Some text");
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
        /// Read properties of T
        /// </summary>
        /// <param name="cb">Callback to execute on T <see cref="Func{T, TResult}"/></param>
        /// <typeparam name="T"><see cref="iTextSharp.text.Paragraph"/></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <code>
        /// To return a value from T:
        /// 
        /// var builder = new Builder{Paragraph}("Some text");
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
