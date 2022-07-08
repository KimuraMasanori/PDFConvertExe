using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace PDFConvertExe.Tests
{
  [TestClass()]
  public class PDFProcessingTests
  {
    [TestMethod]
    public void convertXPSToPdfTest()
    {
      var rootPath = @"C:\Users\kimura\Documents\Mikoshi\pdf\";
      var xpsFile = Path.Combine(rootPath, "h01_1a_20220701042045.xps");
      var pdfPath = Path.Combine(rootPath, "h01_1a_20220701042045.pdf");


      if (File.Exists(pdfPath))
      {
        File.Delete(pdfPath);
      }

      var converter = new PDFProcessing();
      converter.convertXPSToPdf(xpsFile, pdfPath);

      Assert.AreEqual(File.Exists(pdfPath), true);
    }
  }
}