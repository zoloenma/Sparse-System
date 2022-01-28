<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student.aspx.cs" Inherits="Sparse.Student.Student" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../wwwroot/css/app.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="flex items-center bg-ashblue p-3 flex-wrap">
            <span class="text-xl text-white font-thin tracking-wide">Sparse</span>
        </nav>
        <div class="flex items-center justify-center h-screen p-12">
          <div class="w-full h-full bg-lightgray p-10 rounded-md">
            <div class="flex flex-col lg:flex-row h-full w-full">
              <div class="h-full w-full lg:flex-1">
                <div class="w-full h-full flex items-center text-center">
                  <div class="outer">
                    <div class="inner"><p class="md:text-5xl text-lg mt-3 text-black-800">Malayan Colleges Laguna</p></div>
                    <div class="inner"><p class="md:text-5xl text-lg mt-3 text-black-800 py-6">Center of Learning and Information Resources</p></div>
                    <div class="inner">
                      <p class="md:text-5xl text-lg mt-3 text-black-800">Opens: 7:00 AM <br />and <br />Closes: 8:00PM</p>
                    </div>
                  </div>
                </div>
              </div>
              <div class="h-full w-full lg:flex-1">
                <div class="w-full h-full flex items-center justify-center">
                  <div class="outer">
                    <div class="inner  w-full h-full flex items-center justify-center">
                      <span class="inline-flex bg-orange text-black rounded-full h-24 w-36 md:text-3xl justify-center items-center">BUSY</span>
                    </div>
                    <div class="inner w-full h-full flex items-center justify-center py-8">
                      <svg class="transform -rotate-90 w-72 h-72">
                        <circle cx="145" cy="145" r="120" stroke="currentColor" stroke-width="30" fill="transparent" class="text-gray-700" />
                        <circle cx="145" cy="145" r="120" stroke="currentColor" stroke-width="30" fill="transparent" class="text-blue-500" stroke-dasharray="754.285714286" stroke-dashoffset="377.142857143" />
                      </svg>
                      <span class="absolute text-5xl">50%</span>
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
