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

* TODO: Finish razor pages documentation