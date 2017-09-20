# LineCSharpSDK
使用此SDK非常簡單
Line Notify 申請位置
https://notify-bot.line.me/zh_TW/
Line Login 申請位置
https://business.line.me/zh-hant/

取得  ClientId Secret CallBackUrl 就可以開始囉

------------------------------------------------------------------------------------------------------------------------------

Line Login 使用方法

轉導至Line OAuth 網址 => 驗證後將導回 CallBack URL =>取得 兌換Token的 Code => 使用 Token 取得 user Profile

LineLoginClient loginClient = new LineLoginClient(ClientId, Secret, CallBackUrl);
       
var code = Request.QueryString["code"];

if (string.IsNullOrEmpty(code))
{
    Response.Redirect(loginClient.GetOAuthUrl("abcd1234"));
}
else
{
   var token = loginClient.GetLineLoginAccessToken(code);
   var lineUser = loginClient.GetLineLoginUserProfile(token);
 }
}

------------------------------------------------------------------------------------------------------------------------------


Line Notify 使用方法

轉導至Line OAuth 網址 => 驗證後將導回 CallBack URL =>取得 兌換Token的 Code => 使用 Token 發送Notify

LineNotifyClient client = new LineNotifyClient(ClientId, Secret, CallBackUrl);
       
var code = Request.QueryString["code"];

if (string.IsNullOrEmpty(code))
{
   Response.Redirect(client.GetLineNotifyOAuthUrl("abcd1234"));
}
else
{
   var token = client.GetLineAccessToken(code);
   client.SendLineNotifiy(token, "Hello");
 }
}







