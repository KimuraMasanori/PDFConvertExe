using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;

namespace PDFConvertExe.Tests
{
  [TestClass()]
  public class PDFProcessingTests
  {
    [TestMethod]
    public void convertXPSToPdfTest1()
    {
      var rootPath = @"C:\Users\kimura\Documents\Mikoshi\pdf\";
      var xpsFile = Path.Combine(rootPath, "h01_1a_20220708024423.xps");
      var pdfPath = Path.Combine(rootPath, "h01_1a_20220708024423.pdf");


      if (File.Exists(pdfPath))
      {
        File.Delete(pdfPath);
      }

      var converter = new PDFProcessing();
     
      converter.convertXPSToPdf(xpsFile, pdfPath);

      Assert.AreEqual(File.Exists(pdfPath), true);
    }

    [TestMethod]
    public void convertXPSToPdfTest2()
    {
      var rootPath = @"C:\Users\kimura\Documents\Mikoshi\pdf\";
      var xpsFile = Path.Combine(rootPath, "h01_1a_20220708024423.xps");
      var pdfPath = Path.Combine(rootPath, "h01_1a_20220708024423.pdf");


      if (File.Exists(pdfPath))
      {
        File.Delete(pdfPath);
      }

      PDFConvertExe.Program.Main(new string[]{xpsFile,pdfPath});
       
      Assert.AreEqual(File.Exists(pdfPath), true);
    }


    [TestMethod]
    public void convertXPSToPdfTest3()
    {
      var rootPath = @"C:\Users\kimura\Documents\Mikoshi\pdf\";
      var xpsFile = Path.Combine(rootPath, "h01_1a_20220708024423.xps");
      var pdfPath = Path.Combine(rootPath, "h01_1a_20220708024423.pdf");


      if (File.Exists(pdfPath))
      {
        File.Delete(pdfPath);
      }

      var processInfo = new ProcessStartInfo(@"C:\Users\kimura\source\repos\PDFConvertExe\PDFConvertExe\bin\Debug\PDFConvertExe.exe", xpsFile + " " + pdfPath);
      processInfo.UseShellExecute = true;

      var process = Process.Start(processInfo);

      process.CloseMainWindow();
      process.Close();

      int checkCounter = 3;
      while(!File.Exists(pdfPath) && checkCounter != 0)
      {
        System.Threading.Thread.Sleep(1000);
        checkCounter--;
      } 

      Assert.AreEqual(File.Exists(pdfPath), true);
    }
  }
}