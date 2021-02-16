#pragma checksum "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\Counter.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fe3928d539703514f5488902c1896e1cdf2705bc"
// <auto-generated/>
#pragma warning disable 1591
namespace HomeMQ.FirstBlazor.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\_Imports.razor"
using HomeMQ.FirstBlazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\_Imports.razor"
using HomeMQ.FirstBlazor.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\Counter.razor"
using System.Timers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\Counter.razor"
using HomeMQ.Core.ViewModels;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/counter")]
    public partial class Counter : Microsoft.AspNetCore.Components.ComponentBase, IDisposable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Counter</h1>\r\n\r\n");
            __builder.OpenElement(1, "p");
            __builder.AddContent(2, "Current count: ");
            __builder.AddContent(3, 
#nullable restore
#line 9 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\Counter.razor"
                   CounterViewModel.CounterService.CurrentCount

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 13 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\Counter.razor"
       
    //private int currentCount = 0;

    //private void IncrementCount()
    //{
    //    currentCount++;

    //    Console.WriteLine($"Count incremented: {currentCount}");
    //}

    private Timer timer;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += OnTimerInterval;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        base.OnAfterRender(firstRender);
    }

    private void OnTimerInterval(object sender, ElapsedEventArgs e)
    {
        CounterViewModel.CounterService.IncrementCount();
        InvokeAsync(() => StateHasChanged());
    }

    public void Dispose()
    {
        timer?.Dispose();
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private CounterViewModel CounterViewModel { get; set; }
    }
}
#pragma warning restore 1591
