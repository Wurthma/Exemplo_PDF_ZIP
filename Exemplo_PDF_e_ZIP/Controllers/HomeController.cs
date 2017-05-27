using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.IO.Compression;
using System.Web.Mvc;

namespace Exemplo_PDF_e_ZIP.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetPdfFile()
        {
            var doc = new Document(PageSize.A4.Rotate());
            var stream = new MemoryStream();
            var pw = PdfWriter.GetInstance(doc, stream);
            var minhaStringHTML = @"<HTML><HEAD></HEAD><body><FORM method='post'><table><tr><td></td></tr></table></BODY></HTML>";

            doc.Open();
            Paragraph paragrafo = new Paragraph(minhaStringHTML, new Font(Font.NORMAL, 12));
            paragrafo.Alignment = Element.ALIGN_LEFT;
            doc.Add(paragrafo);
            doc.Close();

            return File(stream.ToArray(), "application/pdf", "result.pdf");
        }

        [HttpPost]
        public ActionResult GetPdfFileZiped()
        {
            var doc = new Document(PageSize.A4.Rotate());
            var stream = new MemoryStream();
            var pw = PdfWriter.GetInstance(doc, stream);
            var minhaStringHTML = @"<HTML><HEAD></HEAD><body><FORM method='post'><table><tr><td></td></tr></table></BODY></HTML>";

            doc.Open();
            Paragraph paragrafo = new Paragraph(minhaStringHTML, new Font(Font.NORMAL, 12));
            paragrafo.Alignment = Element.ALIGN_LEFT;
            doc.Add(paragrafo);
            doc.Close();

            using (var compressedFileStream = new MemoryStream())
            {
                //Cria um arquivo ZIP
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Update, false))
                {
                    //Criar a entrada do PDF para o arquivo ZIP
                    var zipEntry = zipArchive.CreateEntry("MeuPDFZipado.pdf");

                    //Pegando o stream do PDF em memória
                    using (var originalFileStream = new MemoryStream(stream.ToArray()))
                    {
                        using (var zipEntryStream = zipEntry.Open())
                        {
                            //Copia o stream do PDF para o ZIP
                            originalFileStream.CopyTo(zipEntryStream);
                        }
                    }

                }

                return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = "Filename.zip" };
            }
        }
    }
}