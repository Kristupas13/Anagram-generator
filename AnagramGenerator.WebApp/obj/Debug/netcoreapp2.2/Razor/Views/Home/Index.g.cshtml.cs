#pragma checksum "C:\Users\kristupas\Desktop\AnagramGenerator\Anagram-generator\AnagramGenerator.WebApp\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2a24f304b58ed79828575e54456e8ff9a7d4332c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "C:\Users\kristupas\Desktop\AnagramGenerator\Anagram-generator\AnagramGenerator.WebApp\Views\_ViewImports.cshtml"
using AnagramGenerator.WebApp;

#line default
#line hidden
#line 2 "C:\Users\kristupas\Desktop\AnagramGenerator\Anagram-generator\AnagramGenerator.WebApp\Views\_ViewImports.cshtml"
using AnagramGenerator.WebApp.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2a24f304b58ed79828575e54456e8ff9a7d4332c", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8403a213a789ede4ccf9b4ac936f518f5c4b3a09", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AnagramList>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(20, 37, true);
            WriteLiteral("\r\n    <ol>\r\n        <font size=\"6\">\r\n");
            EndContext();
#line 5 "C:\Users\kristupas\Desktop\AnagramGenerator\Anagram-generator\AnagramGenerator.WebApp\Views\Home\Index.cshtml"
             if (Model.Anagrams.Any())
            {
                foreach (var anagram in Model.Anagrams)
                {

#line default
#line hidden
            BeginContext(188, 24, true);
            WriteLiteral("                    <il>");
            EndContext();
            BeginContext(213, 7, false);
#line 9 "C:\Users\kristupas\Desktop\AnagramGenerator\Anagram-generator\AnagramGenerator.WebApp\Views\Home\Index.cshtml"
                   Write(anagram);

#line default
#line hidden
            EndContext();
            BeginContext(220, 35, true);
            WriteLiteral("</il>\r\n                    <br />\r\n");
            EndContext();
#line 11 "C:\Users\kristupas\Desktop\AnagramGenerator\Anagram-generator\AnagramGenerator.WebApp\Views\Home\Index.cshtml"
                }
            }

#line default
#line hidden
            BeginContext(289, 39, true);
            WriteLiteral("        </font>\r\n        <ol>\r\n\r\n\r\n<h3>");
            EndContext();
            BeginContext(329, 15, false);
#line 17 "C:\Users\kristupas\Desktop\AnagramGenerator\Anagram-generator\AnagramGenerator.WebApp\Views\Home\Index.cshtml"
Write(ViewBag.Message);

#line default
#line hidden
            EndContext();
            BeginContext(344, 7, true);
            WriteLiteral("</h3>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AnagramList> Html { get; private set; }
    }
}
#pragma warning restore 1591
