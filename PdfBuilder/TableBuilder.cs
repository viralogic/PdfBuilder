using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;

namespace PdfBuilder
{
    /// <summary>
    /// Handles building PDF tables
    /// </summary>
    public class TableBuilder : Builder<PdfPTable>
    {
        /// <summary>
        /// Constructor to create a table with given number of columns
        /// </summary>
        /// <param name="numColumns">The number of columns in the table <see cref="System.Int32"/></param>
        public TableBuilder(int numColumns)
        {
            this.Instance = new PdfPTable(numColumns);
        }
    }
}
