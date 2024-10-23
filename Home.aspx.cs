using EServiceTemplate.BLL;
using EServiceTemplate.Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf.security;
using Microsoft.Reporting.WebForms;
using System;

//using WISeKey.CID.SignatureSemantics;
//using WISeKey.CID.SignatureSemantics.SharedTypes;
//using WISeKey.CID.SignatureSemantics.Constant;
//using WISeKey.CID.SignatureSemantics.InternalServices;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace EService
{
    public partial class Home : System.Web.UI.Page
    {
        private byte[] fileContent = null;
        private string filename = "";
        private struct ValidationResult
        {
            public bool isvalid;
            public string reason;
            public string CN_signer;
            public string O_issuer;
            public DateTime signingDate;
        }

        private DataTable DT_Docs
        {
            get { return ViewState["DT_Docs"] as DataTable; }
            set { ViewState["DT_Docs"] = value; }
        }

        public object FileUploadControl { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            btnprint.Visible = false;
            //printreport.Visible = false;
            if (!IsPostBack)
            {
                //if (Application["EServiceUserToken"] == null)
                //if (Session["EServiceUserToken"] == null)
                //{
                //    //Response.Redirect("http://localhost/SeygovEServiceGateway", false);
                //    if (Default.eserviceGatewayUrl != "")
                //    {
                //        Response.Redirect(Default.eserviceGatewayUrl, false);
                //    }
                //}

                //Tab1.CssClass = "Clicked";
                MainView.ActiveViewIndex = 1;
            }
            //if(FileUpload1.HasFile)
            //{
            //   // FileUpload1.FileName = filename;
            //    FileUpload1.Visible = false;
            //    //lbl_upload.Visible = false;
            //    btn_Verify.Visible = false;
            //  //  filename = FileUpload1.FileName;
            //    //FileUpload1.Attributes.Add(filename, FileUpload1.FileName);
               
            //}
            //    btn_Verify.Visible = FileUpload1.HasFile;
        }
        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private void setmsg(bool ispositive, string msg)
        {
            if (ispositive)
            {
                lbl_errormsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lbl_errormsg.ForeColor = System.Drawing.Color.Red;
            }

            lbl_errormsg.Text = msg;
            downloadbtn.Visible = ispositive;
        }

       //protected void Button1_Click(object sender, EventArgs e)
       // {
       //     HiddenField hidden_seltab = (HiddenField)Master.FindControl("hidden_seltab");
       //     if (hidden_seltab != null)
       //     {
       //         hidden_seltab.Value = "0";
       //     }

       //     lbl_errormsg.Text = "";
       //     SignedDocument bllsignedDocs = new SignedDocument();

       //     if (txt_Search.Text.Trim() != "")
       //     {
       //         DT_Docs = bllsignedDocs.GetSignedDocumnetByRefID(txt_Search.Text.Trim());

       //         if (DT_Docs.Rows.Count > 0)
       //         {
       //             GV_Cert.DataSourceID = null;
       //             GV_Cert.DataSource = DT_Docs;

       //             //if (DT_Docs.Rows.Count == 1)
       //             //{
       //             //    fileContent = (byte[])DT_Docs.Rows[0]["SignedElectonicDocument"];

       //             //    string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
       //             //    string filename = "Document_" + System.DateTime.Now.ToString("dd-MMM-yyyy-HH-mm", CultureInfo.InvariantCulture) + ".pdf";
       //             //    string fullpath = path + "\\" + filename;

       //             //    String File2save = Server.MapPath("~/Files/" + filename);
       //             //    FileStream ff = new FileStream(File2save, FileMode.OpenOrCreate, FileAccess.Write);
       //             //    ff.Write(fileContent, 0, fileContent.Length);
       //             //    ff.Close();

       //             //    setmsg(true, "File found! Click download to view the file");

       //             //        downloadbtn.Attributes["href"] = "~/Files/" + filename;
       //             //        downloadbtn.Attributes["download"] = filename;

       //             //}
       //         }
       //         else
       //         {
       //             setmsg(false, "No records found");
       //         }
       //     }
       //     else
       //     {
       //         setmsg(false, "Enter a referenceID");
       //     }
       // }

        protected void GV_Cert_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.Trim().ToLower())
            {
                case "download":
                    fileContent = (byte[])
                        DT_Docs.Rows[0]["SignedElectonicDocument"];

                    string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string filename = "Document_" + System.DateTime.Now.ToString("dd-MMM-yyyy-HH-mm", CultureInfo.InvariantCulture) + ".pdf";
                    string fullpath = path + "\\" + filename;

                    String File2save = Server.MapPath("~/Files/" + filename);
                    FileStream ff = new FileStream(File2save, FileMode.OpenOrCreate, FileAccess.Write);
                    ff.Write(fileContent, 0, fileContent.Length);
                    ff.Close();

                    string fName = "~/Files/" + filename;
                    FileInfo fi = new FileInfo(fName);
                    //long sz = fi.Length;

                    Response.ClearContent();
                    Response.ContentType = MimeType(System.IO.Path.GetExtension(fName));
                    Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", System.IO.Path.GetFileName(fName)));
                    // Response.AddHeader("Content-Length", sz.ToString("F0"));
                    Response.TransmitFile(fName);
                    Response.End();

                    //downloadbtn.Attributes["href"] = "~/Files/" + filename;
                    //downloadbtn.Attributes["download"] = filename;

                    break;

                default:
                    break;
            }
        }

        private static string MimeType(string Extension)
        {
            string mime = "application/octetstream";
            if (string.IsNullOrEmpty(Extension))
                return mime;
            string ext = Extension.ToLower();
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            return mime;
        }

        protected void GV_Cert_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void FileUpload1_DataBinding(object sender, EventArgs e)
        {
        }

        //private bool wisekey_HasValidSignature(byte[] fileData)
        //{
        //    try
        //    {
        //        SignatureType sigType = SignatureType.PDF;

        //        byte[] docContent = fileData;

        //        int nParams = 1;

        //        WkCertificateValidator certValidator = new WkCertificateValidator();

        //        certValidator.UseAiaOcsp = false;
        //        certValidator.CrlCachedFolder = "";

        //        X509RevocationMode checkMode = X509RevocationMode.NoCheck;
        //        checkMode = X509RevocationMode.Offline;
        //        //checkMode = X509RevocationMode.Online;
        //        certValidator.SetRevocationCheckMode(checkMode);

        //        WkVerifier verifier = new WkVerifier(certValidator);
        //        verifier.ValidateTSACertificate(false);
        //        //verifier.ValidateTSACertificate(true);
        //        //verifier.TsaCertValidator = certValidator;

        //        bool bResult = false;

        //        try
        //        {
        //            bResult = verifier.Verify(docContent, false, DigestAlgorithm.SHA1, null,
        //                                        sigType, null, CertificateChainType.PKCS7, null, null);
        //        }
        //        catch (Exception ex)
        //        {
        //            bResult = false;
        //        }

        //        return bResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is ExceptionService)
        //        {
        //            ExceptionService es = (ExceptionService)ex;
        //            string errorMessage = es.Message;

        //            if (es.Parameters != null && es.Parameters.Length > 0)
        //                ; //logError(string.Format(errorMessage, es.Parameters));
        //            else
        //                ;//logError(errorMessage);
        //        }
        //        else
        //        {
        //            //logError(ex.Message);
        //        }

        //        return false;
        //    }
        //}

        private ValidationResult itext_Verify(byte[] pdf)
        {
            iTextSharp.text.pdf.PdfReader pdfReader = new PdfReader(pdf);

            List<string> names = pdfReader.AcroFields.GetSignatureNames();

            //List<string, bool> sigVals = new Dictionary<string, bool>();
            ValidationResult result = new ValidationResult();

            foreach (string name in names)
            {
                PdfPKCS7 pk = pdfReader.AcroFields.VerifySignature(name);
                
                //Verify the Signature
                bool signatureValid = pk.Verify();

                if (signatureValid)
                {
                    result.signingDate = pk.SignDate;
                    result.CN_signer = iTextSharp.text.pdf.security.CertificateInfo.GetSubjectFields(pk.SigningCertificate).GetField("CN");
                    result.O_issuer = iTextSharp.text.pdf.security.CertificateInfo.GetIssuerFields(pk.SigningCertificate).GetField("O");
                    //sigValidation.Valid = true;
                    result.isvalid = true;
                }
                else
                {
                    //sigValidation.Valid = false;
                    result.isvalid = false;
                    // sigValidation.InvalidReason = "The signature is not valid";
                    string j = pk.Reason;
                    result.reason = "";
                }

                //sigVals.Add(name, sigValidation);
            }

            // return sigVals;
            return result;
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[input.Length];
            //byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        protected void btn_Verify_Click(object sender, EventArgs e)
        {
            byte[] docContent = null;
            lbl_result.Text = "File Uploaded Successfully: " + FileUpload1.FileName;
            filename =FileUpload1.FileName;
            HiddenField hidden_seltab = (HiddenField)Master.FindControl("hidden_seltab");
            if (hidden_seltab != null)
            {
                hidden_seltab.Value = "1";
            }

            try
            {
                //HttpPostedFile postedFile = Request.Files["FileUpload"];

                //if (postedFile != null)
                //{
                if (FileUpload1.HasFile)
                {
                   // string filename = System.IO.Path.GetFileName(FileUpload1.FileName);
                    docContent = ReadFully(FileUpload1.PostedFile.InputStream);
                    filename = FileUpload1.FileName;
                    
                    string hashedData = ComputeSha256Hash(filename);
                    //FileInfo fi = new FileInfo(filename);
                    //var hashedData = fi.GetHashCode();
                    //fi.Refresh();
                    //DateTime dtCreationTime = fi.CreationTime;
                    int bytes = FileUpload1.PostedFile.ContentLength;
                    int Gbs = bytes / (1024 * 1024 * 1024);
                    int temp = bytes % (1024 * 1024 * 1024);

                    int MBs = temp / (1024 * 1024);
                    temp = temp % (1024 * 1024);

                    int KBs = temp / 1024;
                    temp = temp % 1024;
                    //Tech.Utility.WebPageUtility.MsgBox(filename, Page);
                    //using (FileStream fs = new FileStream(FileUpload1.PostedFile.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    //{
                    //    docContent = new byte[fs.Length];

                    //   // filename = FileUpload1.FileName;

                    //    int iBytesRead = fs.Read(docContent, 0, (int)fs.Length);
                    //}

                    // string filepath = postedFile.FileName;

                    //  SignatureType sigType = SignatureType.PDF;

                    //FileStream f = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                    //long n = f.Length;
                    //f.Position = 0;
                    //docContent = new byte[n];
                    //f.Read(docContent, 0, (int)n);
                    //f.Dispose();

                    #region validate using iTextSharp

                    ValidationResult result = new ValidationResult();
                    result = itext_Verify(docContent);

                    ReportViewer1.ShowPrintButton = true;
                    ReportViewer1.ShowRefreshButton = false;
                    ReportViewer1.ShowPageNavigationControls = false;
                    ReportViewer1.ShowBackButton = false;
                    ReportViewer1.ShowFindControls = false;
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                  //  ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report1.rdlc");
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DSV.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    DSV ds = new EServiceTemplate.Reports.DSV();
                   
                    //X509Certificate cert = new X509Certificate();
                    //cert = X509Certificate.CreateFromSignedFile(filename);
                    DataTable gg = ds.DataTable1;
                    //gg.Rows.Add(new Object[] { filename, result.isvalid });
                    gg.Rows.Add(new Object[] { filename, result.isvalid,KBs, result.CN_signer, result.O_issuer, hashedData,result.signingDate });
                   // gg.Rows.Add(new Object[] { filename, result.isvalid , KBs , dtCreationTime.ToString()}); 

                     //if (gg.Rows.Count > 0)
                     //{
                     //    string ffilename = gg.Rows[0]["Filename"].ToString();

                     //    Tech.Utility.WebPageUtility.MsgBox("filename: "+ffilename + " isvalid", Page);
                     //}
                     //else
                     //{
                     //    Tech.Utility.WebPageUtility.MsgBox(gg.Rows.Count.ToString(), Page);
                     //}

                     ReportDataSource reportDataSource = new ReportDataSource("DataSet1", gg);

                    ReportViewer1.LocalReport.DataSources.Add(reportDataSource);

                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.Refresh();
                }
                btnprint.Visible = FileUpload1.HasFile;
                btnprint.Visible = true;
                //if (result.reason != "")
                //{
                //    Type cstype = this.GetType();

                //    // Get a ClientScriptManager reference from the Page class.
                //    ClientScriptManager cs = Page.ClientScript;

                //    // Check to see if the startup script is already registered.
                //    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                //    {
                //        String cstext = "alert('test " + result.reason + "');";
                //        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                //    }
                //}

                #endregion validate using iTextSharp

                //  bool b = wisekey_HasValidSignature(docContent);

                //if (sigType != SignatureType.PDF)
                //{
                //    f = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                //    n = f.Length;
                //    f.Position = 0;
                //    sigContent = new byte[n];
                //    f.Read(sigContent, 0, (int)n);
                //    f.Dispose();
                //}

                //  WkCertificateValidator certValidator = new WkCertificateValidator();

                // X509RevocationMode checkMode = X509RevocationMode.NoCheck;
                //checkMode = X509RevocationMode.Offline;
                //checkMode = X509RevocationMode.Online;

                // certValidator.SetRevocationCheckMode(checkMode);

                //  WkVerifier verifier = new WkVerifier(certValidator);

                //   WISeKey.CID.SignatureSemantics.SharedTypes.Pair[] inputParams = new WISeKey.CID.SignatureSemantics.SharedTypes.Pair[1];
                //   inputParams[0] = new WISeKey.CID.SignatureSemantics.SharedTypes.Pair();
                //   inputParams[0].Parameter = XmlSignatureParameter.UriDocumentDigest.ToString();
                //   inputParams[0].ValueAsString = "";

                //   bool bResult = false;

                //    bResult = verifier.Verify(docContent, false, DigestAlgorithm.SHA1, null,sigType, null, CertificateChainType.PKCS7, null, null);

                //if (bResult)
                //{
                //  //  MessageBox.Show("Signature Is OK");
                //}
                //else
                //{
                //    //MessageBox.Show("Signature Is Invalid, ErrorCode: " +
                //    //    WISeKey.CID.SignatureSemantics.Constant.ErrorCode.GetDefaultMessage(verifier.ErrorCode));
                //   // MessageBox.Show(verifier.ErrorMessage);
                //}
                //}
                //else
                //{
                //    btn_Verify.Text = "er";
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ReadPdfFile(string fileName)
        {
            StringBuilder text = new StringBuilder();

            PdfReader pdfReader = new PdfReader(fileName);

            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            {
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)));
                text.Append(currentText);
                pdfReader.Close();
            }

           // TextBox1.Text = text.ToString();
        }
        protected void Tab1_Click(object sender, EventArgs e)
        {
           // Tab1.CssClass = "Clicked";
            Tab2.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
           // Tab1.CssClass = "Initial";
            Tab2.CssClass = "Clicked"; 
            MainView.ActiveViewIndex = 1;
        }

        protected void printreport_Click(object sender, EventArgs e)
        {
            
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("certi.pdf"), FileMode.Create);
            
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            //Open exsisting pdf
            Document document = new Document(PageSize.LETTER);
            PdfReader reader = new PdfReader(HttpContext.Current.Server.MapPath("certi.pdf"));

            //Getting a instance of new pdf wrtiter
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(HttpContext.Current.Server.MapPath("Print.pdf"), FileMode.Create));

            document.Open();

            PdfContentByte cb = writer.DirectContent;

            int i = 0;
            int p = 0;
            int n = reader.NumberOfPages;
            Rectangle psize = reader.GetPageSize(1);
            float width = psize.Width;
            float height = psize.Height;

            //Add Page to new document

            while (i < n)
            {
                document.NewPage();
                p++;
                i++;
                PdfImportedPage page1 = writer.GetImportedPage(reader, i);
                cb.AddTemplate(page1, 0, 0);
            }

            //Attach javascript to the document

            PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", writer);
            writer.AddJavaScript(jAction);

            document.Close();

            //Attach pdf to the iframe

            frmPrint.Attributes["src"] = "Print.pdf";
        }

        
    }
}