#region

using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using CorpusExplorer.Sdk.Compatibility;
using CorpusExplorer.Sdk.Ecosystem;
using CorpusExplorer.Sdk.Extern.Json.SimpleStandoff;
using CorpusExplorer.Sdk.Extern.Json.Speedy;
using CorpusExplorer.Sdk.Extern.Plaintext.ClanChildes;
using CorpusExplorer.Sdk.Extern.Xml.Bnc;
using CorpusExplorer.Sdk.Extern.Xml.Catma._5._0;
using CorpusExplorer.Sdk.Extern.Xml.Catma._6._0;
using CorpusExplorer.Sdk.Extern.Xml.CoraXml._0._8;
using CorpusExplorer.Sdk.Extern.Xml.CoraXml._1._0;
using CorpusExplorer.Sdk.Extern.Xml.Dta.Tcf2017;
using CorpusExplorer.Sdk.Extern.Xml.FnhdC;
using CorpusExplorer.Sdk.Extern.Xml.Folker.Fln;
using CorpusExplorer.Sdk.Extern.Xml.Ids.KorAP;
using CorpusExplorer.Sdk.Extern.Xml.Opus;
using CorpusExplorer.Sdk.Extern.Xml.Tei.Dwds;
using CorpusExplorer.Sdk.Extern.Xml.Tiger.Importer;
using CorpusExplorer.Sdk.Extern.Xml.Txm;
using CorpusExplorer.Sdk.Extern.Xml.Weblicht;
using CorpusExplorer.Sdk.Terminal;
using CorpusExplorer.Sdk.Utils.DocumentProcessing.Exporter;
using CorpusExplorer.Sdk.Utils.DocumentProcessing.Exporter.Abstract;
using CorpusExplorer.Sdk.Utils.DocumentProcessing.Exporter.Tlv;
using CorpusExplorer.Sdk.Utils.DocumentProcessing.Importer;
using CorpusExplorer.Sdk.Utils.DocumentProcessing.Importer.Abstract;
using CorpusExplorer.Sdk.Utils.DocumentProcessing.Importer.CorpusExplorerV6;
using Ionic.Zip;
using Telerik.Web.UI;

#endregion

public partial class Default : Page
{
  private FileFormatExporter[] _exporter;

  private FileFormatImporter[] _importer;
  private TerminalController _terminal;

  protected RadButton btn_execute;

  protected RadComboBox format_input;

  protected RadComboBox format_output;

  protected RadFormDecorator FormDecorator1;

  protected RadProgressBar progress_convert;

  protected RadAsyncUpload upload_files;

  protected void btn_execute_Click(object sender, EventArgs e)
  {
    try
    {
      if (format_input.SelectedIndex < 0 || format_input.SelectedIndex >= _importer.Length) return;
      if (format_output.SelectedIndex < 0 || format_output.SelectedIndex >= _exporter.Length) return;
      if (_importer[format_input.SelectedIndex].DisplayName == _exporter[format_output.SelectedIndex].DisplayName)
      {
        Response.Write("<script>alert('Ein- und Ausgabeformat sind identisch. Keine Konvertierung notwendig.');</script>");
        return;
      }

      var guid = Guid.NewGuid();
      var dirBase = Server.MapPath("~/temp");

      var dirInput = Path.Combine(dirBase, guid.ToString("N"), "input");
      Directory.CreateDirectory(dirInput);

      var dirOutput = Path.Combine(dirBase, guid.ToString("N"), "CorpusExplorerWebConvert");
      Directory.CreateDirectory(dirOutput);

      progress_convert.Value = 0;
      progress_convert.Visible = true;

      progress_convert.Label = string.Format("Import 1/{0}", upload_files.UploadedFiles.Count);
      var num = 1;
      var count = 50 / (double)upload_files.UploadedFiles.Count;

      _terminal.ProjectNew();
      foreach (UploadedFile uploadedFile in upload_files.UploadedFiles)
      {
        var inputGuid = Guid.NewGuid();
        var inputFile = Path.Combine(dirInput, string.Concat(inputGuid.ToString("N"), _importer[format_input.SelectedIndex].DefaultExtension));
        uploadedFile.SaveAs(inputFile);

        var abstractCorpusAdapter = _importer[format_input.SelectedIndex].Importer.Execute(new[] { inputFile }).FirstOrDefault();
        if (abstractCorpusAdapter == null)
          continue;

        _terminal.Project.Add(abstractCorpusAdapter);

        var progressConvert = progress_convert;
        progressConvert.Value = progressConvert.Value + count;

        progress_convert.Label = string.Format("Import {0}/{1}", num++, upload_files.UploadedFiles.Count);
      }
      progress_convert.Value = 50;
      progress_convert.Label = "Export";

      _exporter[format_output.SelectedIndex].Exporter.Export(_terminal.Project, Path.Combine(dirOutput, $"corpus{_exporter[format_output.SelectedIndex].DefaultExtension}"));

      progress_convert.Value = 75;
      progress_convert.Label = "Download";

      Response.Clear();
      Response.BufferOutput = false;

      Response.ContentType = "application/zip";
      Response.AddHeader("content-disposition", $"attachment; filename=CorpusExplorerWebConvert-{DateTime.Now:yyyy-MM-dd-HHmmss}.zip");
      using (var zipFile = new ZipFile())
      {
        zipFile.AddDirectory(dirOutput);
        zipFile.Save(Response.OutputStream);
      }

      Response.Flush();
      progress_convert.Value = 100;
      progress_convert.Label = "Fertig!";
    }
    catch
    {
      progress_convert.Value = 0;
      progress_convert.Label = "Fehler!";
    }
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    int i;
    _terminal = CorpusExplorerEcosystem.Initialize();
    _importer = new[]
    {
      new FileFormatImporter
      {
        DisplayName = "CorpusExplorer v6 (*.cec6)",
        Importer = new ImporterCec6(),
        DefaultExtension = ".cec6"
      },
      new FileFormatImporter
      {
        DisplayName = "CorpusExplorer v5 (*.cec5)",
        Importer = new ImporterCorpusExplorerV1toV5(),
        DefaultExtension = ".cec5"
      },
      new FileFormatImporter
      {
        DisplayName = "CorpusExplorer v1-v4 (*.zip)",
        Importer = new ImporterCorpusExplorerV1toV5(),
        DefaultExtension = ".zip"
      },
      new FileFormatImporter
      {
        DisplayName = "BNC TEI (*.xml)",
        Importer = new ImporterBnc(),
        DefaultExtension = ".xml"
      },
      new FileFormatImporter
      {
        DisplayName = "CATMA 5.0 (*.xml)",
        Importer = new ImporterCatma(),
        DefaultExtension = ".xml"
      },
      new FileFormatImporter
      {
        DisplayName = "CLAN/Childes (*.cex)",
        Importer = new ImporterClanChildes(),
        DefaultExtension = ".cex"
      },
      new FileFormatImporter
      {
        DisplayName = "CoNLL (*.conll)",
        Importer = new ImporterConll(),
        DefaultExtension = ".conll"
      },
      new FileFormatImporter
      {
        DisplayName = "CoraXML 0.8 (*.xml)",
        Importer = new ImporterCoraXml08(),
        DefaultExtension = ".xml"
      },
      new FileFormatImporter
      {
        DisplayName = "CoraXML 1.0 (*.xml)",
        Importer = new ImporterCoraXml10(),
        DefaultExtension = ".xml"
      },
      new FileFormatImporter
      {
        DisplayName = "DTA-TCF (*.tcf.xml)",
        Importer = new ImporterDta2017(),
        DefaultExtension = ".tcf.xml"
      },
      new FileFormatImporter
      {
        DisplayName = "FnhdC (*.xml)",
        Importer = new ImporterFnhdC(),
        DefaultExtension = ".tcf.xml"
      },
      new FileFormatImporter
      {
        DisplayName = "FOLKER / OrthoNormal (*.fln)",
        Importer = new ImporterFolkerFln(),
        DefaultExtension = ".fln"
      },
      new FileFormatImporter
      {
        DisplayName = "IDS KorAP (*.zip)",
        Importer = new ImporterKorap(),
        DefaultExtension = ".zip"
      },
      new FileFormatImporter
      {
        DisplayName = "IMS Open Corpus Workbench (*.vrt)",
        Importer = new ImporterCorpusWorkBench(),
        DefaultExtension = ".vrt"
      },
      new FileFormatImporter
      {
        DisplayName = "JSON Standoff Universal (*.json)",
        Importer = new ImporterSimpleJsonStandoff(),
        DefaultExtension = ".json"
      },
      new FileFormatImporter
      {
        DisplayName = "OPUS Corpus Collection (*.xml)",
        Importer = new ImporterOpusXces(),
        DefaultExtension = ".xml"
      },
      new FileFormatImporter
      {
        DisplayName = "Sketch Engine VERT (*.vert)",
        Importer = new ImporterSketchEngine(),
        DefaultExtension = ".vert"
      },
      new FileFormatImporter
      {
        DisplayName = "SPEEDy/CODEX (*.json)",
        Importer = new ImporterSpeedy(),
        DefaultExtension = ".json"
      },
      new FileFormatImporter
      {
        DisplayName = "TiGER-XML (*.xml)",
        Importer = new ImporterTiger(),
        DefaultExtension = ".xml"
      },
      new FileFormatImporter
      {
        DisplayName = "TLV-XML (*.xml)",
        Importer = new ImporterTlv(),
        DefaultExtension = ".xml"
      },
      new FileFormatImporter
      {
        DisplayName = "TreeTagger (*.txt)",
        Importer = new ImporterTreeTagger(),
        DefaultExtension = ".txt"
      },
      new FileFormatImporter
      {
        DisplayName = "TXM (*.xml)",
        Importer = new ImporterTxm(),
        DefaultExtension = ".xml"
      },
      new FileFormatImporter
      {
        DisplayName = "WebLicht (*.xml)",
        Importer = new ImporterWeblicht(),
        DefaultExtension = ".xml"
      }
    };
    var fileFormatImporterArray = _importer;
    for (i = 0; i < fileFormatImporterArray.Length; i++)
    {
      var fileFormatImporter = fileFormatImporterArray[i];
      format_input.Items.Add(fileFormatImporter.DisplayName);
    }

    _exporter = new[]
    {
      new FileFormatExporter
      {
        DisplayName = "CorpusExplorer v6 (*.cec6)",
        Exporter = new ExporterCec6(),
        DefaultExtension = ".cec6"
      },
      new FileFormatExporter
      {
        DisplayName = "CATMA v6 (*.xml)",
        Exporter = new ExporterCatma(),
        DefaultExtension = ".xml"
      },
      new FileFormatExporter
      {
        DisplayName = "CoNLL (*.conll)",
        Exporter = new ExporterConll(),
        DefaultExtension = ".conll"
      },
      new FileFormatExporter
      {
        DisplayName = "CSV (*.csv)",
        Exporter = new ExporterCsv(),
        DefaultExtension = ".csv"
      },
      new FileFormatExporter
      {
        DisplayName = "CSV nur Metadaten (*.csv)",
        Exporter = new ExporterCsvMetadataOnly(),
        DefaultExtension = ".csv"
      },
      new FileFormatExporter
      {
        DisplayName = "DTA-TCF (*.tcf.xml)",
        Exporter = new ExporterDta2017(),
        DefaultExtension = ".tcf.xml"
      },
      new FileFormatExporter
      {
        DisplayName = "DWDS-TEI (*.xml)",
        Exporter = new ExporterDwdsTei(),
        DefaultExtension = ".xml"
      },
      new FileFormatExporter
      {
        DisplayName = "HTML (*.html)",
        Exporter = new ExporterHtmlPure(),
        DefaultExtension = ".html"
      },
      new FileFormatExporter
      {
        DisplayName = "IMS Open Corpus Workbench (*.vrt)",
        Exporter = new ExporterCorpusWorkBench{ UseSentenceTag = true },
        DefaultExtension = ".vrt"
      },
      new FileFormatExporter
      {
        DisplayName = "OPUS Corpus Collection (*.xml)",
        Exporter = new ExporterOpusXces(),
        DefaultExtension = ".xml"
      },
      new FileFormatExporter
      {
        DisplayName = "Sketch Engine VERT (*.vert)",
        Exporter = new ExporterSketchEngine(),
        DefaultExtension = ".vert"
      },
      new FileFormatExporter
      {
        DisplayName = "Simple JSON (*.json)",
        Exporter = new ExporterJson(),
        DefaultExtension = ".json"
      },
      new FileFormatExporter
      {
        DisplayName = "Plaintext (*.txt)",
        Exporter = new ExporterPlaintext(),
        DefaultExtension = ".txt"
      },
      new FileFormatExporter
      {
        DisplayName = "Plaintext vereinfacht (*.txt)",
        Exporter = new ExporterPlaintextPure(),
        DefaultExtension = ".txt"
      },
      new FileFormatExporter
      {
        DisplayName = "SPEEDy/CODEX (*.json)",
        Exporter = new ExporterSpeedy(),
        DefaultExtension = ".json"
      },
      new FileFormatExporter
      {
        DisplayName = "TLV-XML (*.xml)",
        Exporter = new ExporterTlv(),
        DefaultExtension = ".xml"
      },
      new FileFormatExporter
      {
        DisplayName = "TreeTagger (*.txt)",
        Exporter = new ExporterTreeTagger{ UseSentenceTag = true},
        DefaultExtension = ".txt"
      },
      new FileFormatExporter
      {
        DisplayName = "TXM (*.xml)",
        Exporter = new ExporterTxm(),
        DefaultExtension = ".xml"
      },
      new FileFormatExporter
      {
        DisplayName = "WebLicht (*.xml)",
        Exporter = new ExporterWeblicht(),
        DefaultExtension = ".xml"
      },
      new FileFormatExporter
      {
        DisplayName = "XML (*.xml)",
        Exporter = new ExporterXml(),
        DefaultExtension = ".xml"
      }
    };
    var fileFormatExporterArray = _exporter;
    for (i = 0; i < fileFormatExporterArray.Length; i++)
    {
      var fileFormatExporter = fileFormatExporterArray[i];
      format_output.Items.Add(fileFormatExporter.DisplayName);
    }
  }

  private abstract class AbstractFileFormat
  {
    public string DefaultExtension { get; set; }

    public string DisplayName { get; set; }
  }

  private class FileFormatExporter : AbstractFileFormat
  {
    public AbstractExporter Exporter { get; set; }
  }

  private class FileFormatImporter : AbstractFileFormat
  {
    public AbstractImporter Importer { get; set; }
  }
}