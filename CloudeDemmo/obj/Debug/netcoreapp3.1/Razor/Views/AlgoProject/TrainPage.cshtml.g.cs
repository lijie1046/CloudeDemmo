#pragma checksum "C:\Users\kwlover\Desktop\wlks\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "392f3764fff576d82c6fe244284dc6f9329c027a"
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
#line 1 "C:\Users\kwlover\Desktop\wlks\CloudeDemmo\Views\_ViewImports.cshtml"
using CloudeDemmo;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\kwlover\Desktop\wlks\CloudeDemmo\Views\_ViewImports.cshtml"
using CloudeDemmo.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"392f3764fff576d82c6fe244284dc6f9329c027a", @"/Views/AlgoProject/TrainPage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"39ca2ec5ec5b03b7c915dd8d94c53ff898234fb3", @"/Views/_ViewImports.cshtml")]
    public class Views_AlgoProject_TrainPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CloudeDemmo.ViewModels.TrainPageViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
            WriteLiteral("\n\n<h1>");
#nullable restore
#line 9 "C:\Users\kwlover\Desktop\wlks\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml"
Write(Model.PageTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>

<div class=""container"">
    <div class=""row justify-content-center"">
        <div class=""col-12 text-center align-self-center text-intro"">
            <div class=""row justify-content-center"">
                <div class=""col-lg-8"">
                    <div>
                        ===============================================
                    </div>

                    <div>
");
#nullable restore
#line 21 "C:\Users\kwlover\Desktop\wlks\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml"
                         if (Model.Result != null)
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "C:\Users\kwlover\Desktop\wlks\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml"
                             foreach (var result in Model.Result)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div>\n                                    ！！！");
#nullable restore
#line 26 "C:\Users\kwlover\Desktop\wlks\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml"
                                  Write(result);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                                </div>\n");
#nullable restore
#line 28 "C:\Users\kwlover\Desktop\wlks\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml"
                             }

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "C:\Users\kwlover\Desktop\wlks\CloudeDemmo\Views\AlgoProject\TrainPage.cshtml"
                              
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\n                </div>\n            </div>\n        </div>\n    </div>\n</div>\n");
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
