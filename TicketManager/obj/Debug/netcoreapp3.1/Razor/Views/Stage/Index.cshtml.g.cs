#pragma checksum "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b348e70dbf85712d0f0bda97de117918e1b5893d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Stage_Index), @"mvc.1.0.view", @"/Views/Stage/Index.cshtml")]
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
#line 1 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/_ViewImports.cshtml"
using TicketManager;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/_ViewImports.cshtml"
using TicketManager.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b348e70dbf85712d0f0bda97de117918e1b5893d", @"/Views/Stage/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"83845285f4665d81c7e28bd45cd06f771096a1f0", @"/Views/_ViewImports.cshtml")]
    public class Views_Stage_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TicketManager.Models.StageViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "MemberReservation", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "OutsideReservation", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("methohd", new global::Microsoft.AspNetCore.Html.HtmlString("post"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Stage", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Export", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<div class=\"row\">\n    <div class=\"col-2\">\n        <h2>");
#nullable restore
#line 5 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
       Write(Model.Stage.DramaName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\n    </div>\n    <div class=\"col-2\">\n        <h3>");
#nullable restore
#line 8 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
       Write(Model.Stage.Num);

#line default
#line hidden
#nullable disable
            WriteLiteral(" st</h3>\n    </div>\n</div>\n\n<h6>");
#nullable restore
#line 12 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
Write(Model.Stage.Time);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\n\n<dl class=\"row\">\n    <dt class=\"col-1\">予約数</dt>\n    <dd class=\"col-1\">");
#nullable restore
#line 16 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                 Write(Model.Stage.CountOfGuests);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\n    <dt class=\"col-1\">キャパ</dt>\n    <dd class=\"col-1\">");
#nullable restore
#line 18 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                 Write(Model.Stage.Max);

#line default
#line hidden
#nullable disable
            WriteLiteral("</dd>\n</dl>\n\n<table class=\"table\">\n    <thead>\n        <tr>\n            <th>名前</th>\n            <th>フリガナ</th>\n");
#nullable restore
#line 26 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
             if ((bool)(ViewData["IsShinkan"]))
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <th>新入生</th>\n                <th>新入生以外</th>\n");
#nullable restore
#line 30 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <th>人数</th>\n");
#nullable restore
#line 34 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <th>団員名</th>\n            <th></th>\n        </tr>\n    </thead>\n    <tbody>\n");
#nullable restore
#line 40 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
         foreach (var reservation in Model.MemberReservations)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\n                <td>\n                    ");
#nullable restore
#line 44 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
               Write(reservation.GuestName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 47 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
               Write(reservation.Furigana);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n");
#nullable restore
#line 49 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                 if ((bool)(ViewData["IsShinkan"]))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\n                        ");
#nullable restore
#line 52 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                   Write(reservation.NumOfFreshmen);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </td>\n                    <td>\n                        ");
#nullable restore
#line 55 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                   Write(reservation.NumOfOthers);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </td>\n");
#nullable restore
#line 57 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\n                        ");
#nullable restore
#line 61 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                   Write(reservation.NumOfGuests);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </td>\n");
#nullable restore
#line 63 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>\n                    ");
#nullable restore
#line 65 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
               Write(reservation.MemberName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b348e70dbf85712d0f0bda97de117918e1b5893d10707", async() => {
                WriteLiteral("\n                        変更\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 70 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                         WriteLiteral(Model.Stage.DramaName);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 71 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                                    WriteLiteral(reservation.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["reservationId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-reservationId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["reservationId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b348e70dbf85712d0f0bda97de117918e1b5893d13813", async() => {
                WriteLiteral("\n                        削除\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 76 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                         WriteLiteral(Model.Stage.DramaName);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 77 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                                    WriteLiteral(reservation.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["reservationId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-reservationId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["reservationId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                </td>\n            </tr>\n");
#nullable restore
#line 82 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\n</table>\n\n<table class=\"table\">\n    <thead>\n        <tr>\n            <th>名前</th>\n            <th>フリガナ</th>\n");
#nullable restore
#line 91 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
             if ((bool)(ViewData["IsShinkan"]))
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <th>新入生</th>\n                <th>新入生以外</th>\n");
#nullable restore
#line 95 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <th>人数</th>\n");
#nullable restore
#line 99 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <th>メールアドレス</th>\n            <th>連絡先</th>\n            <th>備考</th>\n            <th></th>\n        </tr>\n    </thead>\n    <tbody>\n");
#nullable restore
#line 107 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
         foreach (var reservation in Model.OutsideReservations)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\n                <td>\n                    ");
#nullable restore
#line 111 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
               Write(reservation.GuestName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 114 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
               Write(reservation.Furigana);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n");
#nullable restore
#line 116 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                 if ((bool)(ViewData["IsShinkan"]))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\n                        ");
#nullable restore
#line 119 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                   Write(reservation.NumOfFreshmen);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </td>\n                    <td>\n                        ");
#nullable restore
#line 122 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                   Write(reservation.NumOfOthers);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </td>\n");
#nullable restore
#line 124 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\n                        ");
#nullable restore
#line 128 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                   Write(reservation.NumOfGuests);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </td>\n");
#nullable restore
#line 130 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>\n                    ");
#nullable restore
#line 132 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
               Write(reservation.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 135 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
               Write(reservation.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 138 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
               Write(reservation.Remarks);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b348e70dbf85712d0f0bda97de117918e1b5893d21603", async() => {
                WriteLiteral("\n                        変更\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 143 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                         WriteLiteral(Model.Stage.DramaName);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 144 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                                    WriteLiteral(reservation.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["reservationId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-reservationId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["reservationId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b348e70dbf85712d0f0bda97de117918e1b5893d24711", async() => {
                WriteLiteral("\n                        削除\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 149 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                         WriteLiteral(Model.Stage.DramaName);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 150 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                                    WriteLiteral(reservation.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["reservationId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-reservationId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["reservationId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                </td>\n            </tr>\n");
#nullable restore
#line 155 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\n</table>\n\n<div class=\"form-group\">\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b348e70dbf85712d0f0bda97de117918e1b5893d28091", async() => {
                WriteLiteral("\n        <input type=\"submit\" value=\"CSVを出力\" class=\"btn btn-primary\" />\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 161 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
            WriteLiteral(Model.Stage.DramaName);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 161 "/Users/soichiro/Projects/TicketManager/TicketManager/Views/Stage/Index.cshtml"
                                                 WriteLiteral(Model.Stage.Num);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["n"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-n", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["n"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TicketManager.Models.StageViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591