<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="Sparse.Librarian.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../wwwroot/css/app.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="flex items-center bg-custom-ashblue p-3 flex-wrap">
            <span class="text-xl text-white font-thin tracking-wide">Sparse</span>
        </nav>
        <div class="flex items-center justify-center h-screen -mt-20">
          <div class="w-full p-8 lg:w-1/2 ">
            <div class="mt-4">
              <label class="block text-black text-sm font-bold mb-2">Email</label>
                <asp:TextBox ID="EmailTxt" runat="server" class="bg-custom-lightgray text-black focus:outline-none focus:shadow-outline border rounded py-2 px-4 block w-full appearance-none" type="email" MaxLength="100" />
                <asp:RequiredFieldValidator ErrorMessage="Please enter your Email" CssClass="text-red" ControlToValidate="EmailTxt" runat="server" Display="Dynamic" />
            </div>
            <div class="mt-4">
              <div class="flex justify-between">
                <label class="block text-black text-sm font-bold mb-2">Password</label>
              </div>
              <asp:TextBox ID="PasswordTxt" runat="server" class="bg-custom-lightgray  text-black focus:outline-none focus:shadow-outline border rounded py-2 px-4 block w-full appearance-none" type="password" MaxLength="100" />
              <asp:RequiredFieldValidator ErrorMessage="Please enter your password" CssClass="text-red" ControlToValidate="PasswordTxt" runat="server" Display="Dynamic" />

            </div>
            <div class="mt-8">
                <asp:Button ID="SubmitBtn" Text="LOGIN" runat="server" CssClass="bg-custom-ashblue text-white font-bold py-2 px-4 w-full rounded hover:bg-gray-600" OnClick="SubmitBtn_Click" />
            </div>
          </div>
        </div>
    </form>
</body>
</html>
