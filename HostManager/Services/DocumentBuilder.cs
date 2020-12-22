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
            return filePath;
        }
    }
}
