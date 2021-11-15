<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
</head>
<body>
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
            <legend>1. Dateiformat (Input)</legend>
            <p>Geben Sie zuerst an, welches Dateiformat eingelesen werden soll.</p>
            <telerik:RadComboBox ID="format_input" runat="server" Culture="de-DE" Width="300px">
            </telerik:RadComboBox>
          </fieldset>
          <fieldset>
            <legend>2. Upload</legend>
            <p>Laden Sie dann beliebige Dateien hoch. Diese müssen mit dem unter "1. Dateiformat (Input)" übereinstimmen.</p>
            <telerik:RadAsyncUpload ID="upload_files" runat="server" MultipleFileSelection="Automatic" MaxFileInputsCount="100"></telerik:RadAsyncUpload>
          </fieldset>
          <fieldset>
            <legend>3. Dateiformat (Output)</legend>
            <p>Geben Sie an, in welches Dateiformat konvertiert werden sollen.</p>
            <telerik:RadComboBox ID="format_output" runat="server" Culture="de-DE" Width="300px">
            </telerik:RadComboBox>
          </fieldset>
          <fieldset>
            <legend>4. Ausführen</legend>
            <p>Klicken Sie abschließend auf den "Ausführen"-Button und warten Sie die Konvertierung ab.</p>
            <telerik:RadButton ID="btn_execute" runat="server" Text="Ausführen" OnClick="btn_execute_Click"></telerik:RadButton>
            <telerik:RadProgressBar ID="progress_convert" runat="server" Visible="False"></telerik:RadProgressBar>
          </fieldset>
        </div>
      </div>

    </div>
  </form>
</body>
</html>
