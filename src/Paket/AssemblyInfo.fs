﻿namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Paket")>]
[<assembly: AssemblyProductAttribute("Paket")>]
[<assembly: AssemblyCompanyAttribute("Paket team")>]
[<assembly: AssemblyDescriptionAttribute("A dependency manager for .NET with support for NuGet packages and git repositories.")>]
[<assembly: AssemblyVersionAttribute("3.1.7")>]
[<assembly: AssemblyFileVersionAttribute("3.1.7")>]
[<assembly: AssemblyInformationalVersionAttribute("3.1.7")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "3.1.7"
    let [<Literal>] InformationalVersion = "3.1.7"