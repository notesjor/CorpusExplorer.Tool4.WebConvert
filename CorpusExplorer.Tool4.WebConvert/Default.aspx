<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" Culture="de-DE" UICulture="de-DE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
  <link rel="stylesheet" href="https://notes.jan-oliver-ruediger.de/wp-includes/css/dist/block-library/style.min.css" />
  <style>
    #main-footer {
      font-family: Open Sans,Arial,sans-serif;
      color: #666;
      line-height: 1.7em;
      font-weight: 500;
      -webkit-font-smoothing: antialiased;
      font-size: 100%;
      -webkit-text-size-adjust: 100%;
      box-sizing: border-box;
      display: block;
      background-color: #222222;
    }

    #et-footer-nav {
      font-family: Open Sans,Arial,sans-serif;
      color: #666;
      line-height: 1.7em;
      font-weight: 500;
      -webkit-font-smoothing: antialiased;
      box-sizing: border-box;
      margin: 0;
      padding: 0;
      border: 0;
      outline: 0;
      font-size: 100%;
      -webkit-text-size-adjust: 100%;
      vertical-align: baseline;
      background: transparent;
      background-color: rgba(255,255,255,0.05);
    }

    .container {
      font-family: Open Sans,Arial,sans-serif;
      color: #666;
      line-height: 1.7em;
      font-weight: 500;
      -webkit-font-smoothing: antialiased;
      box-sizing: border-box;
      padding: 0;
      border: 0;
      outline: 0;
      font-size: 100%;
      -webkit-text-size-adjust: 100%;
      vertical-align: baseline;
      background: transparent;
      width: 80%;
      max-width: 1080px;
      margin: auto;
      text-align: left;
      position: relative;
    }

    .bottom-nav {
      font-family: Open Sans,Arial,sans-serif;
      color: #666;
      line-height: 1.7em;
      font-weight: 500;
      -webkit-font-smoothing: antialiased;
      text-align: left;
      box-sizing: border-box;
      margin: 0;
      border: 0;
      outline: 0;
      font-size: 100%;
      -webkit-text-size-adjust: 100%;
      vertical-align: baseline;
      background: transparent;
      list-style: none;
      overflow-wrap: break-word;
      padding: 15px 0;
    }

    .menu-item {
      font-family: Open Sans, Arial, sans-serif;
      line-height: 1.7em;
      -webkit-font-smoothing: antialiased;
      list-style: none;
      overflow-wrap: break-word;
      box-sizing: border-box;
      margin: 0;
      padding: 0;
      border: 0;
      outline: 0;
      -webkit-text-size-adjust: 100%;
      vertical-align: baseline;
      background: transparent;
      font-weight: 600;
      display: inline-block;
      font-size: 14px;
      padding-right: 22px;
    }

      .menu-item > a {
        color: #edb059;
        text-decoration: none;
      }

    #footer-bottom {
      font-family: Open Sans,Arial,sans-serif;
      color: #666;
      line-height: 1.7em;
      font-weight: 500;
      -webkit-font-smoothing: antialiased;
      box-sizing: border-box;
      margin: 0;
      border: 0;
      outline: 0;
      font-size: 100%;
      -webkit-text-size-adjust: 100%;
      vertical-align: baseline;
      background: transparent;
      background-color: rgba(0,0,0,0.32);
      padding: 15px 0 5px;
    }

    fieldset {
      padding: 10px 0 10px 0;
    }
  </style>
</head>
<body>
  <header id="main-header" data-height-onload="80" data-height-loaded="true" data-fixed-height-onload="80" style="top: 0px;">
    <div class="clearfix et_menu_container">
      <div class="logo_container" style="padding-bottom: 20px">
        <span class="logo_helper"></span>
        <a href="https://notes.jan-oliver-ruediger.de/">
          <img src="https://notes.jan-oliver-ruediger.de/wp-content/uploads/Logo_Blog_Neu_2.png" width="336" height="90" alt="Notes - Jan Oliver Rüdiger" id="logo" data-height-percentage="54" data-actual-width="336" data-actual-height="90">
        </a>
      </div>
    </div>
  </header>
  <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
      <Scripts>
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
      </Scripts>
    </telerik:RadScriptManager>
    <script type="text/javascript">
        //Put your JavaScript code here.
    </script>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <div>

      <telerik:RadFormDecorator RenderMode="Lightweight" ID="FormDecorator1" runat="server" DecoratedControls="all" DecorationZoneID="decorationZone"></telerik:RadFormDecorator>
      <div>
        <div id="decorationZone">
          <fieldset>
            <legend><asp:Localize meta:resourcekey="step1_head" runat="server">1. Dateiformat (Input)</asp:Localize></legend>
            <p><asp:Localize meta:resourcekey="step1_desc" runat="server">Bitte wählen Sie zuerst aus, welches Dateiformat eingelesen werden soll. Die unter Schritt 2 (Upload) ausgewählten Dateien müssen diesem Format entsprechen.</asp:Localize></p>
            <telerik:RadComboBox ID="format_input" runat="server" Culture="de-DE" Width="300px">
            </telerik:RadComboBox>
          </fieldset>
          <fieldset>
            <legend><asp:Localize meta:resourcekey="step2_head" runat="server">2. Upload</asp:Localize></legend>
            <p>
              <asp:Localize meta:resourcekey="step2_desc" runat="server">Sie können max. 100 Dateien mit einer Gesamtgröße von max. 50 MB hochladen. Wenn Sie eine größere Menge (Anzahl/Dateigesamtgröße) konvertieren möchten, nutzen Sie bitte eine lokal installierte CorpusExplorer-Installation (siehe <a href="https://notes.jan-oliver-ruediger.de/korpora/">'Korpora konvertieren'</a>) - Download, Installation und Nutzung des CorpusExplorers sind kostenfrei.</asp:Localize><br />
            </p>
            <telerik:RadAsyncUpload ID="upload_files" runat="server" MultipleFileSelection="Automatic" MaxFileInputsCount="100"></telerik:RadAsyncUpload>
          </fieldset>
          <fieldset>
            <legend><asp:Localize meta:resourcekey="step3_head" runat="server">3. Dateiformat (Output)</asp:Localize></legend>
            <p><asp:Localize meta:resourcekey="step3_desc" runat="server">Geben Sie bitte an, in welches Dateiformat die Dateie(en) konvertiert werden sollen.</asp:Localize></p>
            <telerik:RadComboBox ID="format_output" runat="server" Culture="de-DE" Width="300px">
            </telerik:RadComboBox>
          </fieldset>
          <fieldset>
            <legend><asp:Localize meta:resourcekey="step4_head" runat="server">4. Ausführen</asp:Localize></legend>
            <p><asp:Localize meta:resourcekey="step4_desc" runat="server">Bevor Sie auf &#39;Ausführen&#39; klicken, überprüfen Sie bitte noch einmal ihre Eingaben. Außerdem stellen Sie bitte sicher, dass alle unter 2. (Upload) gewählten Dateien mit einem grünen Punkt versehen sind - dies zeigt einen erfolgreichen Upload an. Klicken Sie auf den "Ausführen"-Button und warten Sie die Konvertierung ab. Je nach Umfang kann dies einige Zeit in Anspruch nehmen.</asp:Localize></p>
            <telerik:RadButton ID="btn_execute" runat="server" Text="<%$ Resources:Btn_Execute.Text %>" OnClick="btn_execute_Click"></telerik:RadButton>
            <telerik:RadProgressBar ID="progress_convert" runat="server" Visible="False"></telerik:RadProgressBar>
          </fieldset>
        </div>
      </div>
    </div>
  </form>
  <div>
  </div>
  <footer id="main-footer">
    <div id="et-footer-nav">
      <div class="container">
        <ul id="menu-impressum-rechtliches" class="bottom-nav">
          <li class="menu-item menu-item-type-post_type menu-item-object-page"><a href="https://notes.jan-oliver-ruediger.de/"><asp:Localize meta:resourcekey="foot_blog" runat="server">Zum Web-Blog</asp:Localize></a></li>
          <li class="menu-item menu-item-type-post_type menu-item-object-page"><a href="https://notes.jan-oliver-ruediger.de/software/"><asp:Localize meta:resourcekey="foot_software" runat="server">Weitere Software</asp:Localize></a></li>
          <li class="menu-item menu-item-type-post_type menu-item-object-page"><a href="https://notes.jan-oliver-ruediger.de/korpora/"><asp:Localize meta:resourcekey="foot_corpora" runat="server">Kostenfreie Korpora</asp:Localize></a></li>
          <li class="menu-item menu-item-type-post_type menu-item-object-page"><a href="https://notes.jan-oliver-ruediger.de/impressum/"><asp:Localize meta:resourcekey="foot_impressum" runat="server">Impressum</asp:Localize></a></li>
          <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-privacy-policy"><a href="https://notes.jan-oliver-ruediger.de/impressum/datenschutzerklaerung/"><asp:Localize meta:resourcekey="foot_dsgvo" runat="server">Datenschutzerklärung</asp:Localize></a></li>
          <li class="menu-item menu-item-type-post_type menu-item-object-page"><a href="https://notes.jan-oliver-ruediger.de/impressum/haftungsausschluss-disclaimer/"><asp:Localize meta:resourcekey="foot_protect" runat="server">Haftungsausschluss</asp:Localize></a></li>
        </ul>
      </div>
    </div>
    <div id="footer-bottom">
      <div class="container clearfix">
      </div>
    </div>
  </footer>
</body>
</html>
