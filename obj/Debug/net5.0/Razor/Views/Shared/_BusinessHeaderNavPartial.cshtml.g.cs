#pragma checksum "C:\Users\tjenk\Documents\CSProjects\Anerme\Views\Shared\_BusinessHeaderNavPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "56eeb529c1d22058fd899275b299326728a03511"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__BusinessHeaderNavPartial), @"mvc.1.0.view", @"/Views/Shared/_BusinessHeaderNavPartial.cshtml")]
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
#line 1 "C:\Users\tjenk\Documents\CSProjects\Anerme\Views\_ViewImports.cshtml"
using Anerme;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\tjenk\Documents\CSProjects\Anerme\Views\_ViewImports.cshtml"
using Anerme.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"56eeb529c1d22058fd899275b299326728a03511", @"/Views/Shared/_BusinessHeaderNavPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5983994e3c352288babd8d9b328f48a4d9d25194", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__BusinessHeaderNavPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Administrator>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<header class=""business-header"">
    <a href=""/"">
        <div class=""logo"">
            <div class=""logo-letter logo-letter-a"">a</div>
            <div class=""logo-letter logo-letter-n"">N</div>
            <div class=""logo-letter logo-letter-e logo-letter-ne"">e</div>
            <div class=""logo-letter logo-letter-r"">r</div>
            <div class=""logo-letter logo-letter-m"">M</div>
            <div class=""logo-letter logo-letter-e"">e</div>
        </div>
    </a>        

    <ul>
        <li><a>");
#nullable restore
#line 16 "C:\Users\tjenk\Documents\CSProjects\Anerme\Views\Shared\_BusinessHeaderNavPartial.cshtml"
          Write(Model.User.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 16 "C:\Users\tjenk\Documents\CSProjects\Anerme\Views\Shared\_BusinessHeaderNavPartial.cshtml"
                                Write(Model.User.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>\r\n        <li><a href=\"/Logout\">Logout</a></li>\r\n        <li><a><i class=\"fa fa-cogs\" aria-hidden=\"true\"></i></a></li>\r\n    </ul>\r\n</header>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Administrator> Html { get; private set; }
    }
}
#pragma warning restore 1591