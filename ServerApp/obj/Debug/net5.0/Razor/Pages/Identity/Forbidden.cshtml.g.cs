#pragma checksum "D:\Programming\Angular\Angular_Dotnet\QuranAnalysis\ServerApp\Pages\Identity\Forbidden.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54c15176dff74117535be05a836075088105ba44"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Pages_Identity_Forbidden), @"mvc.1.0.razor-page", @"/Pages/Identity/Forbidden.cshtml")]
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
#line 2 "D:\Programming\Angular\Angular_Dotnet\QuranAnalysis\ServerApp\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Mvc.RazorPages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Programming\Angular\Angular_Dotnet\QuranAnalysis\ServerApp\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Programming\Angular\Angular_Dotnet\QuranAnalysis\ServerApp\Pages\_ViewImports.cshtml"
using System.Security.Claims;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Programming\Angular\Angular_Dotnet\QuranAnalysis\ServerApp\Pages\_ViewImports.cshtml"
using ServerApp.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Programming\Angular\Angular_Dotnet\QuranAnalysis\ServerApp\Pages\_ViewImports.cshtml"
using ServerApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"54c15176dff74117535be05a836075088105ba44", @"/Pages/Identity/Forbidden.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a673c0fd388eb614b20218e211c0c454af563d40", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Identity_Forbidden : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\Programming\Angular\Angular_Dotnet\QuranAnalysis\ServerApp\Pages\Identity\Forbidden.cshtml"
  
    ViewData["showNav"] = false;
    ViewData["banner"] = "Access Denied";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h6 class=\"text-center\">You do not have access to this content</h6>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ServerApp.Pages.Identity.ForbiddenModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ServerApp.Pages.Identity.ForbiddenModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ServerApp.Pages.Identity.ForbiddenModel>)PageContext?.ViewData;
        public ServerApp.Pages.Identity.ForbiddenModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
