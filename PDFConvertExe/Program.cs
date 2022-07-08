using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFConvertExe
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine(args[0]);
      Console.WriteLine(args[1]);

      string xpsPath = args[0];
      string pdfPath = args[1];

      var pdfProcessing = new PDFProcessing();
      pdfProcessing.convertXPSToPdf(xpsPath, pdfPath);
    }
  }
}
