using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using SeygovEService.ServiceAgent;
using EService.ServiceAgent;

namespace EService
{
    public partial class VerifySubscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                            if (!IsPostBack)
                {
                    if (Session["EServiceUserToken"] == null)
                    {
                       
                        if (Default.eserviceGatewayUrl != "")
                        {
                            Response.Redirect(Default.eserviceGatewayUrl, false);
                        }
                    }
                    else
                    {
                        bool isAccessAuthorized = false;
                        string result = "";
                        EServiceUserToken eServiceUserToken = (EServiceUserToken)Session["EServiceUserToken"];
                        int currentUserId = 0;
                        if (eServiceUserToken.OnlineUserId > 0)
                        {
                            currentUserId = eServiceUserToken.OnlineUserId;
                        }
                        else if (eServiceUserToken.ForeignOnlineUserId > 0)
                        {
                            currentUserId = eServiceUserToken.ForeignOnlineUserId;
                        }
                        isAccessAuthorized = EService.ServiceAgent.ESSubscriptionInfo.VerifyOnlineUserSubscription(eServiceUserToken.EServiceId, eServiceUserToken.OnlineUserId, eServiceUserToken.ForeignOnlineUserId, eServiceUserToken.OrganisationSBN, currentUserId, ref result);
                        lblUserInfo.Text = result;
                       
                        if (isAccessAuthorized)
                        {
                            Response.Redirect("Home.aspx", false);
                        }
                        else
                        {
                            DataTable dtOnlineUserSubs = new DataTable();
                            dtOnlineUserSubs = EService.ServiceAgent.ESSubscriptionInfo.SearchOnlineUserSubs(eServiceUserToken.OnlineUserId, eServiceUserToken.ForeignOnlineUserId, ESSubscriptionInfo.subscriptionStatus.Null, eServiceUserToken.OrganisationSBN);
                            if (dtOnlineUserSubs.Rows.Count > 0)
                            {
                                #region Displaying subscription status
                                if (dtOnlineUserSubs.Rows[0]["SUBSCRIPTIONSTATUS"] != null && dtOnlineUserSubs.Rows[0]["SUBSCRIPTIONSTATUS"].ToString().Trim() != "")
                                {
                                    string subsStatus = dtOnlineUserSubs.Rows[0]["SUBSCRIPTIONSTATUS"].ToString().Trim().ToLower();
                                    switch (subsStatus)
                                    {
                                        case "a":
                                            lblSubsStatus.Text = "Active";
                                            break;
                                        case "d":
                                            lblSubsStatus.Text = "Deactive";
                                            break;
                                        case "p":
                                            lblSubsStatus.Text = "Pending";
                                            break;
                                        default:
                                            lblSubsStatus.Text = "Not Available";
                                            break;
                                    }
                                }
                                #endregion

                                lblSubsDate.Text = dtOnlineUserSubs.Rows[0]["SUBSCRIBEDDATE"] != null ? Common.DateInDateTimeFormat(dtOnlineUserSubs.Rows[0]["SUBSCRIBEDDATE"].ToString().Trim()).ToString("dd/MM/yyyy") : "";
                                lblExpiryDate.Text = dtOnlineUserSubs.Rows[0]["EXPIRYDATE"] != null ? Common.DateInDateTimeFormat(dtOnlineUserSubs.Rows[0]["EXPIRYDATE"].ToString().Trim()).ToString("dd/MM/yyyy") : "N/A";

                            }
                            else
                            {
                                rowAddress.Visible = false;
                                rowSubsDetailsLabel.Visible = false;
                                rowSubsStatus.Visible = false;
                                rowSubsDate.Visible = false;
                                rowExpiryDate.Visible = false;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Tech.Utility.WebPageUtility.MsgBox("Error!Please contact System Administrator", Page);
            }
        }                
    }
}
