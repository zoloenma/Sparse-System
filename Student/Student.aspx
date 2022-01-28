<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="Sparse.Student.Student" %>

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
        
        <div class="h-auto mt-10 p-6 flex items-center justify-center">
            <div class="container max-w-screen-lg mx-auto">
                <div>
                    <div class="bg-custom-lightgray rounded shadow-lg p-4 px-4 md:p-8 mb-6">
                        <div class="grid gap-4 gap-y-2 text-sm grid-cols-1 lg:grid-cols-5">
                            <div class="lg:col-span-3">
                                <div class="grid gap-4 gap-y-2 text-sm grid-cols-1 md:grid-cols-5 pt-3">
                                    <div class="md:col-span-5 text-center">
                                        <div>
                                            <p class="md:text-5xl text-lg mt-3 text-black pt-6">Malayan Colleges Laguna</p>
                                        </div>
                                    </div>
                                    <div class="md:col-span-5 text-center">
                                        <div>
                                            <p class="md:text-4xl text-lg mt-3 text-black py-6">Center of Learning and Information Resources</p>
                                        </div>
                                    </div>
                                    <div class="md:col-span-1"></div>
                                    <div class="md:col-span-3 text-center">
                                        <div>
                                            <p class="md:text-4xl text-lg mt-3 text-black pb-3">Opens: 7:00 AM <br/>and <br/>Closes: 8:00PM</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="lg:col-span-2">
                                <div class="grid gap-4 gap-y-2 text-sm grid-cols-1 md:grid-cols-5">
                                    <div class="md:col-span-5 text-center pt-7">
                                        <asp:Label ID="RoomStatus" runat="server"></asp:Label>
                                    </div>
                                    <div class="md:col-span-5 text-center">
                                        <div>
                                            <div class="inner w-full h-full flex items-center justify-center py-8">
                                                <svg class="transform -rotate-90 w-72 h-72">
                                                    <circle id="firstCircle" runat="server" cx="145" cy="145" r="120" stroke="darkgray" stroke-width="30" fill="transparent"  />
                                                    <circle id="circlePercentage" runat="server" cx="145" cy="145" r="120" stroke="darkblue" stroke-width="30" fill="transparent" />
                                                </svg>
                                                <asp:Label id="percentage" runat="server" class="absolute text-5xl"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
