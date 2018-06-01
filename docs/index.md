
# PdfBuilder Documentation.

Creating PDFs in code is a dull, repetitive task. Yet we are constantly asked to create PDFs for our business users.

In .NET, a common PDF creating library is iTextSharp. As you can [see](https://developers.itextpdf.com/examples/itext5-building-blocks/phrase-and-paragraph-examples), the code to create even a simple document is quite long and verbose.

    public void createPdf(String filename) throws IOException, DocumentException {
        Document document = new Document();
        PdfWriter.getInstance(document, new FileOutputStream(filename));
        document.open();
        Paragraph paragraph1 = new Paragraph("First paragraph");
        Paragraph paragraph2 = new Paragraph("Second paragraph");
        paragraph2.setSpacingBefore(380f);
        document.add(paragraph1);
        document.add(paragraph2);
        document.close();
    }

For me, using a fluent style syntax would help take the verbosity and drudgery of creating PDFs by hand using iTextSharp. For example, to create the same PDF as above, I would want to write:

	var pdf = new Pdf(PageSize.A4)
		.Add(new Builder<Paragraph>("First paragraph"))
		.Add(new Builder<Paragraph>("Second paragraph")
			.Set(p => { p.setSpacingBefore(380f); })
		)
		.Save("myPdf.pdf");

This is the sole purpose of PdfBuilder.

This project is still in its infancy and is currently not ready for release. If you would like to contribute, please fork and send me a PR when your changes have been completed. Any contributions would be greatly appreciated.

## Quick Start Notes:

PdfBuilder has been recently released on NuGet as an alpha release.

    Install-Package PdfBuilder -Version 1.0.1-alpha

## Contributing

The easiest way to contribute is to send me a message on GitHub telling me what useful feature would make PDF generation easier for you. If you have the time or inclination, please fork and send me a PR with your new feature. 

I started this project just to serve my needs for a work project. So really the features on top of iTextSharp are pretty minimal. Its greatest feature is just being able to code in the fluent style.

Some ideas include:

- ability to apply styles to a document from a common "style" class. Would be especially useful for tables to avoid having to style each every table cell.
- Render from Markdown templates. The fluent style really seems to make the code appear to just be a really verbose template. It doesn't seem to far of leap to be able to generate a pdf from a Markdown template and a data model.  
