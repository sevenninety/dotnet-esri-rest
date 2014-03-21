<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="GetPost.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Send Request Example</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Example of GET/POST request</h2>
        <asp:Button ID="sendGET" runat="server" Text="Send GET" 
            onclick="sendGET_Click" />  
        <asp:Button ID="sendPOST" runat="server" Text="Send POST" 
            onclick="sendPOST_Click" />    
    </div>
    <div>
     <asp:Literal ID="responseFromServer" runat="server" />
    </div>
    </form>
</body>
</html>
