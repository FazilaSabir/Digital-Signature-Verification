using SeygovEService.ServiceAgent;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EService
{
    public partial class Default : System.Web.UI.MasterPage
    {
        public static string eserviceGatewayUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["EServiceUserToken"] != null)
            //{
            //    //Displaying login user account settings
            //    if (((EServiceUserToken)Session["EServiceUserToken"]).OrganisationSBN.Trim() != "")
            //    {
            //        string firstname = ((EServiceUserToken)Session["EServiceUserToken"]).OnlineUserOtherName;
            //        string lastname = ((EServiceUserToken)Session["EServiceUserToken"]).OnlineUserLastName;

            //        string organisationName = ""; //retrieve from SRC
            //        if (((EServiceUserToken)Session["EServiceUserToken"]).OrganisationSBN.Trim() != "")
            //        {
            //            organisationName = EService.ServiceAgent.ESSubscriptionInfo.GetOrganisationTradeName(((EServiceUserToken)Session["EServiceUserToken"]).OrganisationSBN.Trim());
            //        }

            //        EService.wuc.ucOrgUserSettings ucOrgUserSettings = (EService.wuc.ucOrgUserSettings)Page.LoadControl("~/wuc/ucOrgUserSettings.ascx");
            //        ucOrgUserSettings.SetLoginUserName = "Welcome " + firstname + " " + lastname + ", " + organisationName;
                    
            //        phAccountSettings.Controls.Add(ucOrgUserSettings);
            //    }
            //    else
            //    {
            //        if (((EServiceUserToken)Session["EServiceUserToken"]).OnlineUserId > 0 || ((EServiceUserToken)Session["EServiceUserToken"]).ForeignOnlineUserId > 0)
            //        {
            //            EService.wuc.ucAccountSettings ucAccountSettings = (EService.wuc.ucAccountSettings)Page.LoadControl("~/wuc/ucAccountSettings.ascx");
            //            ucAccountSettings.SetLoginUserName = "Welcome " + ((EServiceUserToken)Session["EServiceUserToken"]).OnlineUserOtherName + " " + ((EServiceUserToken)Session["EServiceUserToken"]).OnlineUserLastName;
            //            phAccountSettings.Controls.Add(ucAccountSettings);
            //        }
            //    }
            //}

            LoadUserControls();

            //if (Session["EServiceUserToken"] == null)
            //{
            //    if (Default.eserviceGatewayUrl == "")
            //    {
            //        DataTable dtEservice = new DataTable();
            //        dtEservice = SeygovEService.ServiceAgent.EService.GetEServiceByEServiceId(1);
            //        if (dtEservice.Rows.Count > 0)
            //        {
            //            Default.eserviceGatewayUrl = dtEservice.Rows[0]["URL"].ToString();
            //        }
            //    }

            //    Response.Redirect(Default.eserviceGatewayUrl, false);
            //}
        }

        private void LoadUserControls()
        {
            EService.wuc.ucSubHeader ucSubHeader = (EService.wuc.ucSubHeader)Page.LoadControl("~/wuc/ucSubHeader.ascx");
            phSubHeader.Controls.Add(ucSubHeader);

            //EService.wuc.ucAccount ucAccount = (EService.wuc.ucAccount)Page.LoadControl("~/wuc/ucAccount.ascx");
            //phAccount.Controls.Add(ucAccount);

            EService.wuc.ucGatewayLinks ucGatewayLinks = (EService.wuc.ucGatewayLinks)Page.LoadControl("~/wuc/ucGatewayLinks.ascx");
            PlaceHolder phGatewayLinks = (PlaceHolder)this.FindControl("phGatewayLinks");
            phGatewayLinks.Controls.Add(ucGatewayLinks);
        }
    }
}