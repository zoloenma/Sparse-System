<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Librarian.aspx.cs" Inherits="Sparse.Librarian.Librarian" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../wwwroot/css/app.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <nav class="flex items-center bg-custom-ashblue h-16 pl-5 flex-wrap">
                <span class="text-2xl text-white font-thin tracking-wide">Sparse</span>
            </nav>
            <div>
                <div class="container mx-auto my-14">
                    <div class="bg-red-100 border border-red-400 text-red-700 p-4 rounded" role="alert">
                        <p class="text-center text-xl"><b>Warning:</b> Effective Capacity Exceeded</p>
                    </div>

                    <div class="bg-custom-lightgray border border-custom-darkblue text-custom-black px-12 py-6 rounded mt-9 flex flex-col sm:flex-row justify-between">
                        <div class="flex flex-col">
                            <p class="text-center sm:text-left">Current Room Occupancy:</p>
                            <div class="flex flex-row items-end justify-center sm:justify-start mt-3">
                                <p class="text-3xl">
                                    <b><asp:Label ID="CurrentRoomOccupancy" runat="server" Text=""></asp:Label>%</b>
                                </p>
                                <div class="bg-custom-red px-6 py-1 rounded-full ml-4">
                                    <p class="text-2xl">FULL</p>
                                </div>
                            </div>
                        </div>
                        <div class="flex flex-col mt-8 sm:mt-0">
                            <p class="text-center sm:text-left">Effective Capacity:</p>
                            <div class="flex flex-row items-center justify-center sm:justify-start mt-3">
                                <p class="text-3xl">100</p>
                                <div class="ml-4 text-sm underline text-custom-ashblue">
                                    <asp:LinkButton ID="ChangeCapacity" runat="server">Change</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="flex flex-col lg:flex-row mt-9 gap-5">
                        <div class="w-full lg:w-3/5 flex-auto bg-custom-lightgray rounded px-12 py-12">
                            <h2 class="text-xl">Average Room Occupancy</h2>
                        </div>
                        <div class="w-full lg:w-2/5 flex-auto bg-custom-lightgray rounded px-12 py-12">
                            <h2 class="text-xl">Room Occupancy for the Past Hour</h2>
                            <table class="mt-4 w-full">
                                <thead class="bg-custom-ashblue text-white">
                                    <tr class="h-10">
                                        <th>Time</th>
                                        <th>Occupancy</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="h-10 border-b border-custom-darkgray text-center">
                                        <td>11:50 AM</td>
                                        <td>80%</td>
                                        <td>[bar]</td>
                                    </tr>
                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
