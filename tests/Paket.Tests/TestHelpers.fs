﻿module Paket.TestHelpers

open Paket
open System

let DictionaryDiscovery(graph : seq<string * string * (string * VersionRange) list>) = 
    { new IDiscovery with
          
          member __.GetPackageDetails(force, sources, package, resolverStrategy, version) = 
              async { 
                  let dependencies =
                    graph
                         |> Seq.filter (fun (p, v, _) -> p = package && v = version)
                         |> Seq.map (fun (_, _, d) -> d)
                         |> Seq.head
                         |> List.map (fun (p, v) -> 
                                { Name = p
                                  VersionRange = v
                                  DirectDependencies = []
                                  ResolverStrategy = resolverStrategy
                                  Sources = sources })
                  return { Source = Seq.head sources; DownloadLink = ""; DirectDependencies = dependencies }
              }
          
          member __.GetVersions(sources, package) = 
              async { 
                  return [|
                            graph
                             |> Seq.filter (fun (p, _, _) -> p = package)
                             |> Seq.map (fun (_, v, _) -> v)|]
              } }

let resolve graph (dependencies: (string * VersionRange) seq) =
    let packages = dependencies |> Seq.map (fun (n,v) -> { Name = n; VersionRange = v; Sources = [Nuget ""]; DirectDependencies = []; ResolverStrategy = ResolverStrategy.Max })
    Resolver.Resolve(true, DictionaryDiscovery graph, packages).ResolvedVersionMap

let getVersion resolved =
    match resolved with
    | ResolvedDependency.Resolved x ->
        match x.Referenced.VersionRange with
        | Specific v -> v.ToString()

let getDefiningPackage resolved =
    match resolved with
    | ResolvedDependency.Resolved (FromPackage x) -> x.Defining.Name

let getDefiningVersion resolved =
    match resolved with
    | ResolvedDependency.Resolved (FromPackage x) -> 
        match x.Defining.VersionRange with
        | Specific v -> v.ToString()

let getSource resolved =
    match resolved with
    | ResolvedDependency.Resolved x -> x.Referenced.Sources |> List.head


let normalizeLineEndings (text:string) = text.Replace("\r\n","\n").Replace("\r","\n").Replace("\n",Environment.NewLine)

let toLines (text:string) = text.Replace("\r\n","\n").Replace("\r","\n").Split('\n')
