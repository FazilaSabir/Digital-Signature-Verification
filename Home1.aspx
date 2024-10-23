<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Home1.aspx.cs" Inherits="EService.Home1" Title="E-Service-Home Page" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<asp:LinkButton ID="LinkButton1" runat="server" target="_blank" OnClick="Button1_Click">LinkButton</asp:LinkButton>--%>

    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" EnablePartialRendering="true" runat="server">
    </asp:ScriptManager>

    <table width="80%" align="center">
        <tr>
            <td>
                <%--<asp:Button Text="Visual Comparison" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server" OnClick="Tab1_Click" />--%>
                <asp:Button Text="Online Verification" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
                    OnClick="Tab2_Click" />
                <asp:MultiView ID="MainView" runat="server" >
                    <asp:View ID="View1" runat="server">
                        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                            <tr>
                                <td>
                                    <br />
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 135px">&nbsp;</td>
                                            <td style="width: 285px">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                      <%--  <tr>
                                           <td style="width: 135px; text-align: right;">Enter Reference ID</td>
                                            <td style="width: 285px">
                                                <asp:TextBox ID="txt_Search" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <img src="images/button_download.png" />
                                                <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />

                                                <div id="fileuploader">Upload</div>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td style="width: 135px">&nbsp;</td>
                                            <td style="width: 285px"><strong>
                                                <asp:Label Class="blink_me" ID="lbl_errormsg" runat="server"></asp:Label>
                                            </strong></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>

                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="style1" colspan="3">
                                                <asp:GridView ID="GV_Cert" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="SignedDocumentId" DataSourceID="ObjDS_Cert" EnableModelValidation="True" ForeColor="#333333" GridLines="None" OnRowCommand="GV_Cert_RowCommand" OnSelectedIndexChanged="GV_Cert_SelectedIndexChanged">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SignedDocumentId" HeaderText="SignedDocumentId" InsertVisible="False" ReadOnly="True" SortExpression="SignedDocumentId" Visible="False" />
                                                        <asp:BoundField DataField="ReferenceID" HeaderText="ReferenceID" SortExpression="ReferenceID" />
                                                        <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" SortExpression="DepartmentName">
                                                            <ItemStyle Width="200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DateofSignature" DataFormatString="dd/MM/yyyy" HeaderText="DateofSignature" SortExpression="DateofSignature">
                                                            <ItemStyle Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TypeofSignature" HeaderText="TypeofSignature" SortExpression="TypeofSignature" Visible="False" />
                                                        <asp:BoundField DataField="ApplicationName" HeaderText="ApplicationName" SortExpression="ApplicationName" Visible="False" />
                                                        <asp:BoundField DataField="TransactionIdentifier" HeaderText="TransactionIdentifier" SortExpression="TransactionIdentifier" Visible="False" />
                                                        <asp:BoundField DataField="TypeofTransaction" HeaderText="TypeofTransaction" SortExpression="TypeofTransaction" Visible="False" />
                                                        <asp:BoundField DataField="SignedDocumentId" HeaderText="SignedDocumentId" InsertVisible="False" ReadOnly="True" SortExpression="SignedDocumentId" Visible="False" />
                                                        <asp:BoundField DataField="ElectronicDocument" HeaderText="ElectronicDocument" SortExpression="ElectronicDocument" Visible="False" />
                                                        <asp:BoundField DataField="SignedElectonicDocument" HeaderText="SignedElectonicDocument" SortExpression="SignedElectonicDocument" Visible="False" />
                                                        <asp:ButtonField ButtonType="Button" Text="Download" CommandName="Download" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                </asp:GridView>
                                                <asp:ObjectDataSource ID="ObjDS_Cert" runat="server" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" SelectMethod="GetSignedDocByRefID" TypeName="EServiceTemplate.DAL.Ds_SignedDocsTableAdapters.SignedDocumentTableAdapter">
                                                    <DeleteParameters>
                                                        <asp:Parameter Name="Original_SignedDocumentId" Type="Int32" />
                                                    </DeleteParameters>
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="" Name="RefID" Type="String" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">&nbsp;</td>
                                            <td>
                                                <a id="downloadbtn" visible="false" runat="server" download="">
                                                    <%--<input type="file" runat="server" accept="application/pdf" name="FileUpload" style="width: 326px" />--%>
                                                </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style6" style="width: 135px">&nbsp;</td>
                                            <td style="width: 261px">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                            <tr>
                                <td>

                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 164px">&nbsp;</td>
                                            <td style="width: 345px">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 164px; height: 33px;">Upload Signed Document</td>
                                            <td style="width: 345px; height: 33px;">
                                                <%-- <div id="fileuploader">Upload</div>--%>
                                                <asp:FileUpload ID="FileUpload1" accept="application/pdf" runat="server" onchange="ShowButton();" Height="22px" Width="225px" />
                                                <%--<asp:Button ID="Button1" runat="server" EnableViewState="False" OnClick="Button1_Click1" Text="Refresh" />--%>
                                            </td>
                                            <td style="height: 33px">
                                                <asp:Button ID="btn_Verify" runat="server" OnClick="btn_Verify_Click" Text="Verify" Width="93px" />
                                            </td>
                                            <td style="height: 33px">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px;" colspan="3">
                                                <%-- <div id="fileuploader">Upload</div>--%>
                                                <strong>
                                                    <asp:Label ID="lbl_result" runat="server" Text="result" Visible="False"></asp:Label>
                                                </strong>
                                                <rsweb:ReportViewer ID="ReportViewer1" Width="100%" runat="server" Visible="False" SizeToReportContent="true">
                                                </rsweb:ReportViewer>
                                                <iframe id="frmPrint" name="IframeName" width="500" height="200" runat="server" style="display: none"></iframe>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: right">
                                                <%--<input id="PrintButton" title="Print" style="width: 16px; height: 16px;" type="image" alt="Print" runat="server" src="~/Reserved.ReportViewerWebControl.axd?OpType=Resource&amp;Version=11.0.3442.2&amp;Name=Microsoft.Reporting.WebForms.Icons.Print.gif" />--%>
                                                <asp:Button ID="printreport" EnableViewState="false" runat="server" Text="Print" OnClick="printreport_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>

    <%--    <div id="tabs">
  <ul>
    <li><a href="#tabs-1">Visual Comparison</a></li>
    <li><a href="#tabs-2">Online Verification</a></li>
  </ul>
  <div id="tabs-1">
  </div>
  <div id="tabs-2">--%>

    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
     <ContentTemplate>--%>

    <%-- </ContentTemplate>
      <Triggers>
          <asp:AsyncPostBackTrigger ControlID="btn_Verify"  EventName="Click" />
    </Triggers>
     </asp:UpdatePanel>--%>

    <%--  </div>
</div>--%>

    <script type="text/javascript">
        function blinker() {
            $('#txt_Search').fadeOut(500);
            $('#txt_Search').fadeIn(500);
        }
        function ShowButton() {
                        document.getElementById('<%=btn_Verify.ClientID %>').style.display = "inline";
        }

        setInterval(blinker, 1000);

        // Print function (require the reportviewer client ID)
        function printReport(ReportViewer1) {
            var rv1 = $('#' + ReportViewer1);
            var iDoc = rv1.parents('html');

            // Reading the report styles
            var styles = iDoc.find("head style[id$='ReportControl_styles']").html();
            if ((styles == undefined) || (styles == '')) {
                iDoc.find('head script').each(function () {
                    var cnt = $(this).html();
                    var p1 = cnt.indexOf('ReportStyles":"');
                    if (p1 > 0) {
                        p1 += 15;
                        var p2 = cnt.indexOf('"', p1);
                        styles = cnt.substr(p1, p2 - p1);
                    }
                });
            }
            if (styles == '') { alert("Cannot generate styles, Displaying without styles.."); }
            styles = '<style type="text/css">' + styles + "</style>";

            // Reading the report html
            var table = rv1.find("div[id$='_oReportDiv']");
            if (table == undefined) {
                alert("Report source not found.");
                return;
            }

            // Generating a copy of the report in a new window
            var docType = '<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/loose.dtd">';
            var docCnt = styles + table.parent().html();
            var docHead = '<head><title>Printing ...</title><style>body{margin:5;padding:0;}</style></head>';
            var winAttr = "location=yes, statusbar=no, directories=no, menubar=no, titlebar=no, toolbar=no, dependent=no, width=720, height=600, resizable=yes, screenX=200, screenY=200, personalbar=no, scrollbars=yes";;
            var newWin = window.open("", "_blank", winAttr);
            writeDoc = newWin.document;
            writeDoc.open();
            writeDoc.write(docType + '<html>' + docHead + '<body onload="window.print();">' + docCnt + '</body></html>');
            writeDoc.close();

            // The print event will fire as soon as the window loads
            newWin.focus();
            // uncomment to autoclose the preview window when printing is confirmed or canceled.
            // newWin.close();
        };

        // Linking the print function to the print button
        $('#printreport').click(function () {
            printReport('rv1');
        });
    </script>
</asp:Content>