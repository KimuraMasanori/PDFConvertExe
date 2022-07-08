using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace PDFConvertExe.Tests
{
  [TestClass()]
  public class PDFProcessingTests
  {

    string rootPath;
    string xpsFile;
    string pdfPath;

    [TestInitialize]
    public void initSettings()
    {
      rootPath = @"C:\Users\kimura\Documents\Mikoshi\pdf\";
      xpsFile = Path.Combine(rootPath, "h01_1a_20220708040117.xps");
      pdfPath = Path.Combine(rootPath, "h01_1a_20220708040117.pdf");
    }

    [TestMethod]
    public void convertXPSToPdfTest1()
    {
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

    [TestMethod]
    public void convertXPSToPdfTest4()
    {
      var pdfPathList = new List<string>();
      

      for (int i = 0 ; i < System.IO.Directory.GetFiles(rootPath).Length;i++)
      {
        var targetFile = Path.GetExtension(Directory.GetFiles(rootPath)[i]);

        if (Path.GetExtension(targetFile) == ".pdf")
        {
          File.Delete(targetFile);
        }
      }

      foreach (var targetFile in Directory.EnumerateFiles(rootPath))
      {
        if (System.IO.Path.GetExtension(targetFile) == ".xps")
        {
          var outPdfFile = targetFile + ".pdf";
          var processInfo = new ProcessStartInfo(@"C:\Users\kimura\source\repos\PDFConvertExe\PDFConvertExe\bin\Debug\PDFConvertExe.exe",
            targetFile + " " + outPdfFile);
          processInfo.UseShellExecute = true;

          var process = Process.Start(processInfo);

          process.CloseMainWindow();
          process.Close();

          int checkCounter = 3;
          while (!File.Exists(outPdfFile) && checkCounter != 0)
          {
            System.Threading.Thread.Sleep(1000);
            checkCounter--;
          }

          if(File.Exists(outPdfFile))
          {
            pdfPathList.Add(outPdfFile);
          }
        }

      }

      int xpsCount = Directory.EnumerateFiles(rootPath).Where(w => Path.GetExtension(w) == ".xps").Count();
      Assert.AreEqual(pdfPathList.Count == xpsCount, true);

    }
  }
}