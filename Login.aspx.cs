using SeygovEService.ServiceAgent;
using System;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace EService
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        //    if (!IsPostBack)
        //    {
        //        string egatewayName = ConfigurationManager.AppSettings["EGateWayName"].ToString();
        //        DataTable dtEservice = new DataTable();
        //        dtEservice = SeygovEService.ServiceAgent.EService.GetEServiceByFullName(egatewayName);
        //        if (dtEservice.Rows.Count > 0)
        //        {
        //            Default.eserviceGatewayUrl = dtEservice.Rows[0]["URL"].ToString();
        //        }

        //        EServiceUserToken eserviceUserToken = new EServiceUserToken();
        //        eserviceUserToken = SeygovEService.ServiceAgent.EService.VerifyEServiceUserToken(this.Page);

        //        if (eserviceUserToken.OnlineUserNin.Trim() != "" || eserviceUserToken.ForeignOnlineUserId > 0)
        //        {
        //            SeygovEService.ServiceAgent.EService.IncrementEServiceHitCount(eserviceUserToken.EServiceId, eserviceUserToken.OnlineUserNin, eserviceUserToken.ForeignOnlineUserId, eserviceUserToken.OrganisationSBN, Common.GetClientIPAddress());

        //            Session["EServiceUserToken"] = eserviceUserToken;

        //            AuthenticateEventArgs args = new AuthenticateEventArgs(true);
        //            args.Authenticated = true;
        //            if (eserviceUserToken.OrganisationSBN != "" && eserviceUserToken.OnlineUserAccessCode != "")
        //            {
        //                //Creating and adding an authentication cookie to the cookie collection
        //                //FormsAuthentication.SetAuthCookie(eserviceUserToken.OnlineUserAccessCode, true);
        //                //HttpCookie cookie = FormsAuthentication.GetAuthCookie(eserviceUserToken.OnlineUserAccessCode, true);
        //                //cookie.Name = "loginCookie";
        //                //Response.Cookies.Add(cookie);

        //                FormsAuthentication.RedirectFromLoginPage(eserviceUserToken.OnlineUserAccessCode, false);
        //            }
        //            else if (eserviceUserToken.OnlineUserNin.Trim() != "")
        //            {
        //                //Creating and adding an authentication cookie to the cookie collection
        //                //FormsAuthentication.SetAuthCookie(eserviceUserToken.OnlineUserNin, true);
        //                //HttpCookie cookie = FormsAuthentication.GetAuthCookie(eserviceUserToken.OnlineUserNin, true);
        //                //cookie.Name = "loginCookie";
        //                //Response.Cookies.Add(cookie);

        //                FormsAuthentication.RedirectFromLoginPage(eserviceUserToken.OnlineUserNin, false);
        //            }
        //        }
        //        else
        //        {
        //            if (System.Configuration.ConfigurationManager.AppSettings["EServiceName"] != null)
        //            {
        //                SeygovEService.ServiceAgent.EService.IncrementEServiceHitCount(System.Configuration.ConfigurationManager.AppSettings["EServiceName"], "00000000000", null, null, Common.GetClientIPAddress());
        //            }

        //            if (Default.eserviceGatewayUrl != "")
        //            {
        //                Response.Redirect(Default.eserviceGatewayUrl, false);
        //            }
        //        }
        //    }
        }
    }
}