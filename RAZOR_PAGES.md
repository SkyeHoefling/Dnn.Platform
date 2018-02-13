*This is a WIP and is not complete yet.*

# DNN Razor Pages Prototype #

DNN Razor Pages is a prototype that is attempting to port dotnet core's Razor Pages implementation to the DNN Platform. 

## What is Razor Pages ? ##
There is a lot of conversation about Razor in the dotnet ecosystem and it really boils down to 2 main concepts.
* Razor Syntax
* Platform Using Razor 

The term Razor is used quite a bit in the web stack of Microsoft technologies so it is best to understand the terminology which will help us understand the rest of this document

| Technology                    | Definition                                                             |
|-------------------------------|------------------------------------------------------------------------|
| Razor Syntax or Razor         | The `.cshtml` file which is a combination of markup (html) and C# code. Microsoft has several articles documenting razor syntax since it is used in a variety of technologies, [Intro to Razor Syntax](https://docs.microsoft.com/en-us/aspnet/web-pages/overview/getting-started/introducing-razor-syntax-c) |
| ASP.NET MVC                   | The Model-View-Controller platform that uses the .NET Framework. The View is a `.cshtml` file which uses the `Razor Syntax` |
| ASP.NET Core MVC              | The Model-View-Controller platform that uses dotnet core. The View is a `.cshtml` file which uses the `Razor Syntax`        |
| ASP.NET Core Razor Pages      | A simplification of the MVC platform(s) built in dotnet core. Razor Pages uses a view and a code behind (or model) which greatly reduces the boiler plate code that is needed to build an ASP.NET Core Website. |
| ASP.NET WebPages (deprecated) | It is useful to mention WebPages since it used `Razor Syntax` and is very similar to Razor Pages but Microsoft has killed this off and has stated `Razor Pages` is not the same thing or the evolution. Looking at the fundamental usage the 2 technologies are very similar. |

### Razor Pages ###
ASP.NET Core Razor Pages was announced in 2017 with ASP.NET Core 2.0 which is a simplification from the MVC platform which is commonly used in both .NET Framework and dotnet core. Razor Pages removes the large amount of boiler plate code and just requires a View and a code behind file or Model. 

* [Intro to Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/mvc/razor-pages/?tabs=visual-studio)

One of the major benefits of Razor Pages is the simplicity of the platform. It is now very easy and simple for a developer to create a new website using Razor Pages with a simple view and code behind. This can also easily be expanded to use design patterns such as Model-View-ViewModel (MVVM) which Microsoft has been spending a lot of time and energy in. This now provides developers multiple options to choose the design pattern that works best for their needs and not being forced into something.

## Why DNN Razor Pages ? ##
For the purposes of this document DNN has a few major goals (this may need to be ammended)
* Upgrade to dotnet core
* Provide stable migration path from dotnet framework to dotnet core
* Provide feature parity with Microsoft technologies

<strong>DNN Razor Pages is built in dotnet core and DNN Platform is built in .NET Framework, why should we spend effort porting this to DNN Platform? </strong>
This is an important question and should be asked because we will not be leveraging dotnet core for the first version of DNN Razor Pages. To better understand the answer to this question we need to ask the follow up question: 
* How will we migrate modules from .NET Framework to dotnet core? 

Porting ASP.NET Core Razor Pages to DNN Platform now provides a migration path from DNN to DNN vNext (dotnet core). This is an important migration path for a few reasons:
* This is the new technology Microsoft is backing to get more developers into the Microsoft Ecosystem by removing boilerplate code
* DNN WebForm modules can now upgrade to DNN RazorPages which will take significantly less time. We estimate it may take 100 hours to migrate something to MVC and using that as a baseline it may take 20 hours to migrate to Razor Pages since they follow similar design patterns. 
* Abstracting away the DNN Razor Pages implementation allows us to support the current .NET Framework implementation of DNN and DNN vNext built in dotnet core

## Implementation Strategy ##
To support Razor Pages in DNN Platform we can leverage the existing codebase that implements Razor Syntax and Model Binding to Razor Pages, this is available through the DNN MVC implementation. 

### DNN Platform ###

### DNN vNext (dotnet core) ###
As we transition from DNN Platform to DNN vNext we will remove ported classes and reference the RazorPages assemblies/NuGets where applicable. This *should* not change anything as far as a DNN Razor Page is concerned since it will still be using the same API.

## Specification ##
(this should probably be moved to some design document in the future)

A DNN Razor Pages module will contain the following required files and folders
* Pages Folder
	* Index.cshtml (view)
	* IndexModel.cs (code behind)
* DNN Manifest File
	* A `moduleControl.controlSrc` specifying the index razor page in the following format: `Module/Index.razorpages`

### Pages ###
The Pages folder follows the Microsoft Convention that all of the razor files `.cshtml` and code behind files `.cs` files are located inside of the Pages folder. 

#### Index.cshtml ####
To define a DNN Razor Pages razor file you will specify the following markup at the top of your file:

ASP.NET Core Razor Pages
```cshtml
@page
@model MyModule.Pages.IndexModel

<div>@Model.WelcomeMessage</div>
```

DNN Razor Pages
```cshtml
@inherits DotNetNuke.Web.Mvc.RazorPages.Framework.DnnWebViewPage<MyModule.Pages.IndexModel>

<div>@Model.WelcomeMessage</div>
```

#### IndexModel.cs ####
As far as the developer is concerned the DNN Razor Pages and the ASP.NET Core Razor Pages code behind is identical except where the PageModel object comes from. See code sample below:

ASP.NET Core Razor Pages
```c#
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace MyModule.Pages
{
    public class IndexModel : PageModel
    {
        public string WelcomeMessage => "Hello World";
    }
}
```

DNN Razor Pages
```c#
using DotNetNuke.Web.Mvc.RazorPages;
using System;

namespace MyModule.Pages
{
    public class IndexModel : PageModel
    {
        public string WelcomeMessage => "Hello World";
    }
}
```

### DNN Manifest File ###
The Manifest file requires the pages be configured using the `.razorpages` extension.

#### XML Node ####
all items except required DNN Razor Pages nodes/attributes are ommitted

```xml
<dotnetnuke type="Package" version="5.0">
  <packages>
    <package>
      <component type="Module">
        <desktopModule>
          <moduleDefinitions>
            <moduleDefinition>
              <moduleControls>
                <!-- BEGIN DNN Razor Page moduleControl -->
                <moduleControl>				  
                  <controlKey />
                  <controlSrc>MyModule/Index.razorpages</controlSrc>
                  <supportsPartialRendering>False</supportsPartialRendering>
                  <controlTitle />
                  <controlType>View</controlType>
                  <iconFile />
                  <helpUrl />
                  <viewOrder>0</viewOrder>
                </moduleControl>
                <!-- END DNN Razor Page moduleControl -->
              </moduleControls>
            </moduleDefinition>
          </moduleDefinitions>
        </desktopModule>
      </component>
    </package>
  </packages>
</dotnetnuke>
```