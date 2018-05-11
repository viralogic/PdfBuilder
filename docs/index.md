
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

	var pdf = new PdfBuilder()
		.Add(new Builder<Paragraph>("First paragraph"))
		.Add(new Builder<Paragraph>("Second paragraph")
			.Set(p => { p.setSpacingBefore(380f); })
		)
		.Render(new FileOutputStream(filename);

This is the sole purpose of PdfBuilder.

This project is still in its infancy and is currently not ready for release. If you would like to contribute, please fork and send me a PR when your changes have been completed. Any contributions would be greatly appreciated.

## Quick Start Notes:

No installation notes can be given at this time although the plan is to make this library available on Nuget.

## Documentation

API Documentation can be viewed [here](/api/toc.html). There are some very small code examples in the API documentation. Further examples are planned.
