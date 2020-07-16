using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using PdfGenerate.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PdfGenerate.Reports
{
    public class StudentReport
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentReport(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        #region Declaration
        int _maxColum = 3;
        Document _document;
        Font _fontStyle;
        PdfPTable _pdfTable = new PdfPTable(3);
        PdfPCell _pdfCell;
        MemoryStream _memoryStream = new MemoryStream();
        StudentVM _oStudent = new StudentVM();
        #endregion

        public byte[] Report(StudentVM oStudent)
        {
            _oStudent = oStudent;
            _document = new Document();
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(5f, 5f, 20f, 5f);
            _pdfTable.WidthPercentage = 100;
            _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);

            PdfWriter docWriter = PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();

            float[] size = new float[_maxColum];
            for (int i = 0; i < _maxColum; i++)
            {
                if(i==0)
                {
                    size[i] = 20;
                }
                else
                {
                    size[i] = 100;
                }

            }

            _pdfTable.SetWidths(size);
            this.ReportHeader();
            this.EmptyRaw(2);
            this.ReportBody();
            _pdfTable.HeaderRows = 2;
            _document.Add(_pdfTable);

            _document.Close();

            return _memoryStream.ToArray();
        }

        private void ReportHeader()
        {
            _pdfCell = new PdfPCell(this.AddLogo());
            _pdfCell.Colspan = 1;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(this.SetPageTitle());
            _pdfCell.Colspan = _maxColum-1;
            _pdfCell.Border = 0;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();
        }

        private PdfPTable AddLogo()
        {
            int maxColum = 1;
            PdfPTable pdfPTable = new PdfPTable(maxColum);

            string path = _webHostEnvironment.WebRootPath + "/Images";
            string imgCombine = Path.Combine(path, "logo.jpg");
            Image img = Image.GetInstance(imgCombine);

            _pdfCell = new PdfPCell(img);
            _pdfCell.Colspan = maxColum;
            _pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);

            pdfPTable.CompleteRow();

            return pdfPTable;

        }

        private PdfPTable SetPageTitle()
        {
            int maxColum = 3;
            PdfPTable pdfPTable = new PdfPTable(maxColum);
            
            _fontStyle = FontFactory.GetFont("tahoma", 14f, 1);
            _pdfCell = new PdfPCell(new Phrase("Popular Pharmaceuticals Limited", _fontStyle));
            _pdfCell.Colspan = maxColum;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);
            pdfPTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("tahoma", 8f, 1);
            _pdfCell = new PdfPCell(new Phrase("Sheltech Panthokunjo, 17 Shukrabad ,West Panthopath , Dhaka 1207 ,Bangladesh", _fontStyle));
            _pdfCell.Colspan = maxColum;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);
            pdfPTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("tahoma", 6f, 1);
            _pdfCell = new PdfPCell(new Phrase("Printed on: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"), _fontStyle));
            _pdfCell.Colspan = maxColum;
            _pdfCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);

            _fontStyle = FontFactory.GetFont("tahoma", 14f, 1);
            _pdfCell = new PdfPCell(new Phrase("Credit Requisition", _fontStyle));
            _pdfCell.Colspan = maxColum;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            pdfPTable.AddCell(_pdfCell);

            
            pdfPTable.CompleteRow();

            return pdfPTable;

        }


        private void EmptyRaw(int nCount)
        {
            for (int i = 1; i <=nCount; i++)
            {
                _pdfCell = new PdfPCell(new Phrase("", _fontStyle));
                _pdfCell.Colspan = _maxColum;
                _pdfCell.Border = 0;
                _pdfCell.ExtraParagraphSpace = 10;
                _pdfTable.AddCell(_pdfCell);
                _pdfTable.CompleteRow();
            }
        }

        private void ReportBody()
        {
            var fontStyleBold = FontFactory.GetFont("Tahoma", 9f, 1);
            _fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);







            _pdfCell = new PdfPCell(new Phrase("ID", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Name", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Address", fontStyleBold));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable.AddCell(_pdfCell);

            _pdfTable.CompleteRow();


            foreach (var student in _oStudent.students)
            {
                _pdfCell = new PdfPCell(new Phrase(student.Id.ToString(), _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(student.Name, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);

                _pdfCell = new PdfPCell(new Phrase(student.Address, _fontStyle));
                _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfCell.BackgroundColor = BaseColor.White;
                _pdfTable.AddCell(_pdfCell);

                _pdfTable.CompleteRow();
            }

            

        }
    }


}
