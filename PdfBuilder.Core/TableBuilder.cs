using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.pdf;

namespace PdfBuilder.Core
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

        /// <summary>
        /// Adds a row to the table
        /// </summary>
        /// <param name="row">An instance of a Builder row <see cref="Builder{PdfPRow}"/></param>
        /// <returns><see cref="TableBuilder"/></returns>
        public TableBuilder Add(TableRowBuilder row)
        {
            return this.Add(row.Instance);
        }

        /// <summary>
        /// Adds a row to the table
        /// </summary>
        /// <param name="row">An instance of a PdfPRow <see cref="PdfPRow"/></param>
        /// <returns><see cref="TableBuilder"/></returns>
        public TableBuilder Add(PdfPRow row)
        {
            this.Set(t => { t.Rows.Add(row); });
            return this;
        }
    }
}
