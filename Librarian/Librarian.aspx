<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Librarian.aspx.cs" Inherits="Sparse.Librarian.Librarian" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../wwwroot/css/app.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/gh/alpinejs/alpine@v2.x.x/dist/alpine.min.js" defer></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- Navbar -->
            <nav x-data="{ open: false }" class="flex items-center bg-custom-ashblue h-16 px-6 flex-wrap justify-between">
                <span class="text-2xl text-white font-thin tracking-wide">Sparse</span>
                <div>
                    <button type="button" @click="open = !open" class="flex flex-row">
                        <p class="text-white">
                            <asp:Label ID="emailLbl" runat="server" Text=""></asp:Label>
                        </p>
                        <div class="flex flex-row ml-2 items-center">
                            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-white" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
                            </svg>
                            <svg xmlns="http://www.w3.org/2000/svg" class="h-4 w-4 text-white" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7" />
                            </svg>
                        </div>
                    </button>
                    <div x-show="open" x-transition:enter="transition ease-out duration-100" x-transition:enter-start="transform opacity-0 scale-95" x-transition:enter-end="transform opacity-100 scale-100" x-transition:leave="transition ease-in duration-75" x-transition:leave-start="transform opacity-100 scale-100" x-transition:leave-end="transform opacity-0 scale-95" class="absolute right-0 w-full mt-2 origin-top-right rounded shadow-lg sm:w-48">
                      <div class="px-2 py-2 bg-white rounded shadow">
                          <asp:Button ID="LogoutBtn" runat="server" Text="Logout" CssClass="block px-4 py-2 bg-transparent rounded hover:bg-custom-lightgray w-full text-left" OnClick="LogoutBtn_Click" />
                      </div>
                    </div>
                </div>
            </nav>

            <div>
                <div class="container mx-auto my-14 text-custom-black">
                    <!-- Warning -->
                    <div id="WarningAlert" runat="server" class="bg-red-100 border border-red-400 text-red-700 p-4 rounded" role="alert">
                        <p class="text-center text-xl"><b>Warning:</b> Effective Capacity Exceeded</p>
                    </div>

                    <!-- Room Occupancy & Effective Capacity -->
                    <div class="bg-custom-lightgray border border-custom-darkblue px-12 py-6 rounded mt-9 flex flex-col sm:flex-row justify-between">
                        <div class="flex flex-col">
                            <p class="text-center sm:text-left">Current Room Occupancy:</p>
                            <div class="flex flex-row items-center justify-center sm:justify-start mt-3">
                                <p class="text-3xl">
                                    <b><asp:Label ID="CurrentRoomOccupancyLbl" runat="server" Text=""></asp:Label></b>
                                </p>
                                <asp:Label ID="RoomStatusLbl" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="flex flex-col mt-8 sm:mt-0">
                            <p class="text-center sm:text-left">Effective Capacity:</p>
                            <div class="flex flex-row items-center justify-center sm:justify-start mt-3">
                                <p class="text-3xl">
                                    <asp:Label ID="EffectiveCapacityLbl" runat="server" Text=""></asp:Label>
                                </p>
                                <div class="ml-4 text-sm underline text-custom-ashblue">
                                    <asp:LinkButton ID="ChangeCapacity" runat="server">Change</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="flex flex-col lg:flex-row mt-9 gap-5">
                        <!-- Average Room Occupancy -->
                        <div class="w-full lg:w-3/5 flex-auto bg-custom-lightgray rounded p-12">
                            <h2 class="text-xl">Average Room Occupancy</h2>
                            <div class="mt-4">
                                <div>
                                    <ul class="flex flex-row space-x-6 text-custom-ashblue">
                                        <li><asp:Button ID="monBtn" runat="server" Text="MON" OnClick="monBtn_Click" CssClass="border border-custom-lightgray py-2 px-4 rounded bg-transparent" /></li>
                                        <li><asp:Button ID="tueBtn" runat="server" Text="TUE" OnClick="tueBtn_Click" CssClass="border border-custom-lightgray py-2 px-4 rounded bg-transparent" /></li>
                                        <li><asp:Button ID="wedBtn" runat="server" Text="WED" OnClick="wedBtn_Click" CssClass="border border-custom-lightgray py-2 px-4 rounded bg-transparent" /></li>
                                        <li><asp:Button ID="thuBtn" runat="server" Text="THU" OnClick="thuBtn_Click" CssClass="border border-custom-lightgray py-2 px-4 rounded bg-transparent" /></li>
                                        <li><asp:Button ID="friBtn" runat="server" Text="FRI" OnClick="friBtn_Click" CssClass="border border-custom-lightgray py-2 px-4 rounded bg-transparent" /></li>
                                        <li><asp:Button ID="satBtn" runat="server" Text="SAT" OnClick="satBtn_Click" CssClass="border border-custom-lightgray py-2 px-4 rounded bg-transparent" /></li>
                                    </ul>
                                </div>

                                <asp:Chart ID="AverageChart" runat="server" Height="250px" Width="500px" CssClass="mt-6">  
                                    <Series>  
                                        <asp:Series Name="Series1"  YValuesPerPoint="6">  
                                        </asp:Series>  
                                    </Series>  
                                    <ChartAreas>  
                                        <asp:ChartArea Name="ChartArea1">  
                                        </asp:ChartArea>  
                                    </ChartAreas>  
                                </asp:Chart>
                            </div>
                        </div>

                        <!-- Room Occupancy for the Past Hour -->
                        <div class="w-full lg:w-2/5 flex-auto bg-custom-lightgray rounded p-12">
                            <h2 class="text-xl">Room Occupancy for the Past Hour</h2>
                            <table class="mt-4 w-full">
                                <thead class="bg-custom-ashblue text-white">
                                    <tr class="h-10">
                                        <th>Time</th>
                                        <th>Occupancy</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:GridView ID="HistoryTable1" runat="server"></asp:GridView>
                                    <asp:Repeater ID="HistoryTable" runat="server">
                                        <ItemTemplate>
                                            <tr class="h-10 border-b border-custom-darkgray text-center">
                                                <td><%#DataBinder.Eval(Container,"DataItem.Time", "{0:hh:mm tt}")%></td>
                                                <td>
                                                    <%#String.Format("{0}%", (Convert.ToDouble(DataBinder.Eval(Container,"DataItem.Occupancy")) * 100).ToString("0.##")) %>
                                                </td>
                                                <!-- bar
                                                <td class="flex flex-row">
                                                    
                                                    <div class="w-full bg-custom-darkgray h-5 ml-2 rounded-full">
                                                      <div class="bg-custom-darkblue h-5 rounded-full" style="width: 99%"></div>
                                                    </div>
                                                </td>
                                                    -->
                                            </tr>                                            
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Change Capacity Modal -->
            <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ScriptManager>

            <asp:Panel ID="ModalPanel" runat="server" CssClass="p-12 relative bg-custom-lightgray border border-custom-darkblue rounded">
                <div class="flex flex-col">
                    <p>Enter new Effective Capacity:</p>
                    <asp:TextBox ID="capacityTB" runat="server" CssClass="border border-custom-ashblue mt-2 rounded"></asp:TextBox>
                    <div class="flex flex-row justify-between mt-8">
                        <asp:Button ID="ChangeBtn" runat="server" Text="Change" OnClick="ChangeBtn_Click" CssClass="bg-custom-darkblue rounded text-white font-bold px-4 py-2" />
                        <asp:Button ID="CancelBtn" runat="server" Text="Cancel" CssClass="bg-custom-darkgray rounded text-white font-bold px-4 py-2" />
                    </div>
                </div>
            </asp:Panel>
            
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="CancelBtn" PopupControlID="ModalPanel" TargetControlID="ChangeCapacity" BackgroundCssClass="bg-black opacity-60 z-20">
            </ajaxToolkit:ModalPopupExtender>
        </div>
    </form>
</body>
</html>
