using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFConvertExe
{
  class Program
  {
    static void Main(string[] args)
    {
      string xpsPath = args[0];
      string pdfPath = args[1];

      var pdfProcessing = new PDFProcessing();
      pdfProcessing.convertXPSToPdf(xpsPath, pdfPath);
    }
  }
}
