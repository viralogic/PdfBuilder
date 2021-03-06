﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;

namespace PdfBuilder.Core
{
    /// <summary>
    /// Handles PDF table rows
    /// </summary>
    public class TableRowBuilder : Builder<PdfPRow>
    {
        /// <summary>
        /// Constructs a PDF table row from a list of PdfPCell builders.
        /// The cells appear in the row in the order in which they exist in the list.
        /// </summary>
        /// <param name="cells">A list of PdfPCell builders <see cref="IEnumerable{Builder{PdfPCell}}"/></param>
        public TableRowBuilder(IEnumerable<Builder<PdfPCell>> cells)
        {
            var rowCells = new List<PdfPCell>();
            foreach (var c in cells)
            {
                rowCells.Add(c.Instance);
            }
            this.Instance = new PdfPRow(rowCells.ToArray());
        }

        /// <summary>
        /// Constructs a PDF table row from a list of PdfPCells.
        /// The cells appear in the row in the order in which they exist in the list.
        /// </summary>
        /// <param name="cells">A list of PdfPCells <see cref="iTextSharp.text.pdf.PdfPCell"/></param>
        public TableRowBuilder(IEnumerable<PdfPCell> cells)
        {
            this.Instance = new PdfPRow(cells.ToArray());
        }
    }
}
