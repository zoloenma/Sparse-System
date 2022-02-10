<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="Sparse.Student.Student" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../wwwroot/css/app.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <nav class="flex items-center bg-custom-ashblue h-16 px-6 flex-wrap">
            <span class="text-2xl text-white font-thin tracking-wide">Sparse</span>
        </nav>

        <br />
        <br />

        <!-- HCI -->

        <!--
        <div class="h-auto mt-10 p-6 flex items-center justify-center">
            <div class="container max-w-screen-lg mx-auto">
                <div>
                    <div class="bg-custom-lightgray rounded shadow-lg p-4 px-4 md:p-8 mb-6" id="statusContainers" runat="server">
                        <div class="grid gap-4 gap-y-2 text-sm grid-cols-1 lg:grid-cols-5">
                            <div class="md:col-span-5 text-center">
                                <div>
                                    <asp:Label class="md:text-4xl text-md mt-3 text-black pt-6 font-bold" ID="sdstatusHCI_1" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label class="md:text-1xl text-lg mt-3 text-black pt-6 font" ID="sdstatusHCI_2" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        -->

        <!-- End of HCI -->

        <div class="w-auto grid lg:grid-cols-2 lg:grid-rows-2 sm:grid-cols-1 gap-10 px-24 justify-center">

            <div class="container md bg-custom-lightgray rounded shadow-lg p-4 sm:p-2">
                <div class="md:col-span-5 text-center justify-center items-center lg:pt-8">
                    <p class="md:text-5xl sm:text-5x1 text-lg mt-3 font-bold text-black py-6">
                        Center for Learning and<br />
                        Information Resources
                    </p>
                    <p class="md:text-3xl text-lg mt-3 text-black">Einstein Bldg.</p>
                </div>

                <div class="md:col-span-1"></div>
                <div class="md:col-span-3 text-center py-12">
                    <div class="lg:pt-12">
                        <p>
                            <asp:Label class="md:text-4xl text-lg mt-3 mb-2 text-black pb-3" ID="openingHourLabel" runat="server">[time:AM]</asp:Label>
                            <asp:Label class="md:text-4xl text-lg mt-3 mb-2 text-black pb-3" ID="Label1" runat="server">&nbsp;-&nbsp;</asp:Label>
                            <asp:Label class="md:text-4xl text-lg mt-3 mb-2 text-black pb-3" ID="closingHourLabel" runat="server">[time:PM]</asp:Label>
                        </p>
                        <br />
                        <p>
                            <asp:Label class="md:text-4xl text-lg mt-3 mb-2 text-black pb-3" ID="openingDayLabel" runat="server">[openingDay]</asp:Label>
                            <asp:Label class="md:text-4xl text-lg mt-3 mb-2 text-black pb-3" ID="Label2" runat="server">&nbsp;-&nbsp;</asp:Label>
                            <asp:Label class="md:text-4xl text-lg mt-3 mb-2 text-black pb-3" ID="closingDayLabel" runat="server">[closingDay]</asp:Label>
                        </p>
                    </div>
                </div>
            </div>


            <div class="container bg-custom-lightgray rounded shadow-lg row-span-2 lg:pt-24">
                <div class="md:col-span-5 text-center pt-12 pl-24 pr-24 pb-12 lg:pt-24">
                    <asp:Label ID="RoomStatus" runat="server"></asp:Label>
                </div>
                <div class="md:col-span-5 text-center">
                    <div>
                        <div class="inner w-full h-full flex items-center justify-center py-8">
                            <svg class="transform -rotate-90" height="500" width="500">
                                <circle id="firstCircle" runat="server" cx="250" cy="250" r="210" class="text-custom-darkgray" stroke="currentColor" stroke-linecap="round" stroke-width="60" fill="transparent" />
                                <circle id="circlePercentage" runat="server" cx="250" cy="250" r="210" stroke="currentColor" class="text-custom-darkblue" stroke-linecap="round" stroke-width="60" fill="transparent" />
                            </svg>
                            <asp:Label ID="percentage" runat="server" class="absolute text-5xl"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="grid grid-rows-3 min-w-screen p-2 gap-4">

                <div class="container mx-auto rounded p-12 text-center shadow-lg" id="statusContainer" runat="server">
                    <asp:Label class="md:text-4xl sm:text-5x1 text-md mt-3 text-black pt-6 font-bold" ID="statusHCI_1" runat="server"></asp:Label>
                    <br />
                    <asp:Label class="md:text-1xl sm:text-2x1 text-lg mt-3 text-black pt-6 font" ID="statusHCI_2" runat="server"></asp:Label>
                </div>

                <div class="container bg-custom-lightgray p-12 mx-auto rounded text-center shadow-lg" id="busyContainer" runat="server">
                    <asp:Label class="md:text-2xl sm:text-4x1 text-sm mt-3 text-black pt-6" id="busyHolderLabel" runat="server">Today's room is</asp:Label>
                    <br />
                    <asp:Label class="md:text-4xl sm:text-5x1 text-md mt-3 text-black pt-6 font-bold text-custom-ashblue" ID="busyLabel" runat="server"></asp:Label>
                </div>

                <div class="container bg-custom-lightgray p-12 mx-auto rounded text-center shadow-lg" id="peakHoursContainer" runat="server">
                    <asp:Label class="md:text-2xl sm:text-4x1 text-sm mt-3 text-black pt-6" id="peakHolderLabel" runat="server">Peak hours are</asp:Label>
                    <br />
                    <asp:Label class="md:text-4xl sm:text-5x1 text-md mt-3 text-black pt-6 font-bold text-custom-darkblue" ID="peakhoursLabel" runat="server"></asp:Label>
                </div>

            </div>

        </div>

        <br />
        <br />
        <br />

    </form>
</body>
</html>
