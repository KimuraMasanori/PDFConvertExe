using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace PDFConvertExe
{
  public class PDFProcessing
  {
    private string EXEC_PATH;
    private string GS_PATH;

    public PDFProcessing()
    {
      this.EXEC_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
      this.GS_PATH = Path.Combine(EXEC_PATH, "ghostxps-9.56.1-win32", "gxpswin32.exe");
    }

    /*
    public void convertXPSToPdf(List<string> xpsPathList, string pdfFilePath)
    {
      var tempPdfList = new List<string>();

      foreach(var xpsPath in xpsPathList)
      {
        var tempPdfPath = string.Concat(xpsPath,".pdf");
        convertXPSToPdf(xpsPath,tempPdfPath);
        tempPdfList.Add(tempPdfPath);
      }

      mergePdfFiles(tempPdfList,pdfFilePath);
    }
    */

    public void convertXPSToPdf(string xpsPath, string pdfFilePath)
    {
    
      StringBuilder argsBuilder = new StringBuilder();
      argsBuilder.Append(" -sDEVICE=pdfwrite");
      argsBuilder.Append(" -sOutputFile=");
      argsBuilder.Append(pdfFilePath);
      argsBuilder.Append(" -dNOPAUSE ");
      argsBuilder.Append(xpsPath);
        
      //コマンド実行文字列作成
      var processInfo = new ProcessStartInfo(GS_PATH,argsBuilder.ToString());
        
      //コマンドウィンドウを表示しない
      processInfo.CreateNoWindow = true;
      processInfo.UseShellExecute = false;

      Process.Start(processInfo);
 
      //pdfFilePathで指定したファイルが作成されているかをcheckCounter回チェック
      int checkCounter = 10;
      while(!File.Exists(pdfFilePath) && checkCounter != 0)
      {
        System.Threading.Thread.Sleep(1000);
        checkCounter--;
        Console.WriteLine(pdfFilePath + " NOT FOUND");
      }
    }


    //public void mergePdfFiles(List<string> pdfPathList, string mergedPdfPath)
    //{
    //  PdfDocument document = new PdfDocument();

    //  foreach (var pdffile in pdfPathList)
    //  {
    //    /* 
    //      pdffileが別のプロセスで使用されていないかをcheckCounter回チェックする
    //    */
    //    int checkCounter = 10;
    //    bool isOKFlg = false;
    //    while (checkCounter != 0 && !isOKFlg)
    //    {
    //      try
    //      {
    //        using (var pdfsr = File.OpenRead(pdffile))
    //        {
    //          isOKFlg = true;
    //          pdfsr.Close();
    //        }
    //      }
    //      catch (IOException ioex)
    //      {
    //        Console.WriteLine(pdffile + ":" + ioex.Message);
    //        System.Threading.Thread.Sleep(1000);
    //        checkCounter--;
    //      }
    //    }

    //    PdfDocument mergedDocument = PdfReader.Open(pdffile, PdfDocumentOpenMode.Import);

    //    foreach (var page in mergedDocument.Pages)
    //    {
    //      document.AddPage(page);
    //    }

    //    mergedDocument.Close();


    //    document.Save(mergedPdfPath);

    //  }
    //}
  }
}
