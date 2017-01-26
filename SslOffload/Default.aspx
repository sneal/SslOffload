<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SslOffload.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Original Url: <%= HttpContext.Current.Items["OriginalUrl"] %></h1>
        <h1>Request.Url: <%= Request.Url %></h1>
        <h1>Request.RawUrl: <%= Request.RawUrl %></h1>
        <h1>HTTP_X_FORWARDED_PROTO: <%= Request.ServerVariables["HTTP_X_FORWARDED_PROTO"] %></h1>
    </div>
    </form>
</body>
</html>
