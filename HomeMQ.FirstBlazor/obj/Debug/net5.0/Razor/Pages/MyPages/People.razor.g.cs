#pragma checksum "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f825676e759361eb1a7dbbcefba8b229f01de054"
// <auto-generated/>
#pragma warning disable 1591
namespace HomeMQ.FirstBlazor.Pages.MyPages
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
#line 3 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
using HomeMQ.DapperCore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
using HomeMQ.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
using HomeMQ.FirstBlazor.Models;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Data/People")]
    public partial class People : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>People Page</h1>\r\n\r\n");
            __builder.AddMarkupContent(1, "<h4>Insert New Person</h4>\r\n");
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Forms.EditForm>(2);
            __builder.AddAttribute(3, "Model", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Object>(
#nullable restore
#line 12 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
                  newPerson

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "OnValidSubmit", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Forms.EditContext>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Forms.EditContext>(this, 
#nullable restore
#line 12 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
                                             InsertPerson

#line default
#line hidden
#nullable disable
            )));
            __builder.AddAttribute(5, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder2) => {
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.DataAnnotationsValidator>(6);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(7, "\r\n    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.ValidationSummary>(8);
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(9, "\r\n\r\n    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(10);
                __builder2.AddAttribute(11, "id", "firstName");
                __builder2.AddAttribute(12, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 16 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
                                           newPerson.FirstName

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(13, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => newPerson.FirstName = __value, newPerson.FirstName))));
                __builder2.AddAttribute(14, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => newPerson.FirstName));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(15, "\r\n    ");
                __builder2.OpenComponent<Microsoft.AspNetCore.Components.Forms.InputText>(16);
                __builder2.AddAttribute(17, "id", "lastName");
                __builder2.AddAttribute(18, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 17 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
                                          newPerson.LastName

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(19, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => newPerson.LastName = __value, newPerson.LastName))));
                __builder2.AddAttribute(20, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => newPerson.LastName));
                __builder2.CloseComponent();
                __builder2.AddMarkupContent(21, "\r\n    ");
                __builder2.AddMarkupContent(22, "<button type=\"submit\" class=\"btn btn-primary\">Submit</button>");
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(23, "\r\n\r\n");
            __builder.AddMarkupContent(24, "<h4>Current People</h4>");
#nullable restore
#line 22 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
 if(people is null)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(25, "<p><em>Loading...</em></p>");
#nullable restore
#line 25 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(26, "table");
            __builder.AddAttribute(27, "class", "table table-striped");
            __builder.AddMarkupContent(28, "<thead><tr><th>First Name</th>\r\n                <th>Last Name</th></tr></thead>\r\n        ");
            __builder.OpenElement(29, "tbody");
#nullable restore
#line 36 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
             foreach (var item in people)
            {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(30, "tr");
            __builder.OpenElement(31, "td");
            __builder.AddContent(32, 
#nullable restore
#line 39 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
                         item.FirstName

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(33, "\r\n                    ");
            __builder.OpenElement(34, "td");
            __builder.AddContent(35, 
#nullable restore
#line 40 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
                         item.LastName

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 43 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 46 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 48 "C:\Users\devin\source\repos\HomeMQ\HomeMQ.FirstBlazor\Pages\MyPages\People.razor"
       
    private List<Person> people;
    private DisplayPerson newPerson = new DisplayPerson();

    protected override async Task OnInitializedAsync()
    {
        people = await database.GetPeople();
    }

    private async Task InsertPerson()
    {
        var pp = new Person
        {
            FirstName = newPerson.FirstName,
            LastName = newPerson.LastName
        };
        await database.InsertPerson(pp);

        people.Add(pp);

        newPerson = new DisplayPerson();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IPeopleData database { get; set; }
    }
}
#pragma warning restore 1591