#pragma checksum "E:\project\CloudeDemmo\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7b7db5cb49c7e3ec054efbced2b5d90ebd18c4a6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_AlgoProject_TrainPage), @"mvc.1.0.view", @"/Views/AlgoProject/TrainPage.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\project\CloudeDemmo\CloudeDemmo\Views\_ViewImports.cshtml"
using CloudeDemmo;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\project\CloudeDemmo\CloudeDemmo\Views\_ViewImports.cshtml"
using CloudeDemmo.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7b7db5cb49c7e3ec054efbced2b5d90ebd18c4a6", @"/Views/AlgoProject/TrainPage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f7d9640f6fa5df49acdde7cb281546ed9d8d876d", @"/Views/_ViewImports.cshtml")]
    public class Views_AlgoProject_TrainPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CloudeDemmo.ViewModels.TrainPageViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n\r\n<h1>");
#nullable restore
#line 10 "E:\project\CloudeDemmo\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml"
Write(Model.PageTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n<div>\r\n    ===============================================\r\n</div>\r\n\r\n<div>\r\n");
#nullable restore
#line 16 "E:\project\CloudeDemmo\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml"
 if (Model.Result != null)
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "E:\project\CloudeDemmo\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml"
     foreach (var result in Model.Result)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div>\r\n            ！！！");
#nullable restore
#line 21 "E:\project\CloudeDemmo\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml"
          Write(result);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n");
#nullable restore
#line 23 "E:\project\CloudeDemmo\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "E:\project\CloudeDemmo\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml"
     
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CloudeDemmo.ViewModels.TrainPageViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
