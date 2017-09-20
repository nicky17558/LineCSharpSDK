using System;
using LineCSharpSDK;

namespace LineSample
{
    public partial class UserProfile : System.Web.UI.Page
    {


        const string clientId = "<clientId>";//
        const string password = "<screct>";
        const string transUrl = "<callback url>";
        LineLoginClient loginClient = new LineLoginClient(clientId, password, transUrl);
        protected void Page_Load(object sender, EventArgs e)
        {

            var code = Request.QueryString["code"];


            if (string.IsNullOrEmpty(code))
            {
                Response.Redirect(loginClient.GetOAuthUrl("abcd1234"));
            }
            else
            {

                var token = loginClient.GetLineLoginAccessToken(code);
                var lineUser = loginClient.GetLineLoginUserProfile(token);

                Label2.Text = lineUser.userId;
                Label1.Text = lineUser.displayName;
                Image1.ImageUrl = lineUser.pictureUrl;
            }
        }
    }

}