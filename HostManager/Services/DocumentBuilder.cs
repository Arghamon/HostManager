using System;
using System.IO;
using SautinSoft.Document;
using SelectPdf;

namespace HostManager.Services
{
    public static class DocumentBuilder
    {

        public static void CreatePdf(string filePath, string output)
        {
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertUrl(filePath);
            doc.Save(output);
            doc.Close();
        }

        public static string CreateDoc(string filePath)
        {
            var name = Guid.NewGuid();
            var output = $@"./wwwroot/docs/{name}-invoice.docx";
            var input = filePath;
            DocumentCore document = DocumentCore.Load(input);
            
            document.Save(output);

            return output;
        }
    }
}
