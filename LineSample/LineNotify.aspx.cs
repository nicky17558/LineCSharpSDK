using System;
using LineCSharpSDK;

namespace LineSample
{
    public partial class LineNotify : System.Web.UI.Page
    {
        const string clientId = "<clientId>";
        const string password = "<secrect>";
        const string transUrl = "<callBackUrl>";
        LineNotifyClient client = new LineNotifyClient(clientId, password, transUrl);
        protected void Page_Load(object sender, EventArgs e)
        {
           

            var code = Request.QueryString["code"];


            if (string.IsNullOrEmpty(code))
            {
                Response.Redirect(client.GetLineNotifyOAuthUrl("abcd1234"));
            }
            else
            {
                var token = client.GetLineAccessToken(code);

                Session["LineToken"] = token;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

             client = new LineNotifyClient(clientId, password, transUrl);

            if (Session["LineToken"] != null)
            {
                var token = Session["LineToken"].ToString();
                var res = client.SendLineNotifiy(token, TextBox1.Text);
                Response.Write(res.status);
            }
            else
            {
                Response.Write("Token Miss");
            }





        }

        protected void Button2_Click(object sender, EventArgs e)
        {

             client = new LineNotifyClient(clientId, password, transUrl);

            if (Session["LineToken"] != null)
            {
                var token = Session["LineToken"].ToString();
                var res = client.GetLineNotifyStatus(token);
                Response.Write(res.status);
            }
            else
            {
                Response.Write("Token Miss");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            client = new LineNotifyClient(clientId, password, transUrl);

            if (Session["LineToken"] != null)
            {
                var token = Session["LineToken"].ToString();
                var res = client.RevokeLineNotifyStatus(token);
                Response.Write(res.status);
            }
            else
            {
                Response.Write("Token Miss");
            }
        }
    }
}