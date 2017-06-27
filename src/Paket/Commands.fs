module Paket.Commands

open System

open Argu

type AddArgs =
    | [<Mandatory;MainCommandAttribute()>] NuGet of package_ID:string
    | [<Hidden;CustomCommandLine("nuget")>] NuGet_Legacy of package_ID:string

    | [<AltCommandLine("-V")>] Version of version_constraint:string
    | [<Hidden;CustomCommandLine("version")>] Version_Legacy of version_constraint:string

    | [<AltCommandLine("-p")>] Project of path:string
    | [<Hidden;CustomCommandLine("project")>] Project_Legacy of path:string

    | [<AltCommandLine("-g")>] Group of name:string
    | [<Hidden;CustomCommandLine("group")>] Group_Legacy of name:string

    | Create_New_Binding_Files
    | [<Hidden;CustomCommandLine("--createnewbindingfiles")>] Create_New_Binding_Files_Legacy

    | [<AltCommandLine("-f")>] Force
    | [<AltCommandLine("-i")>] Interactive
    | Redirects
    | Clean_Redirects
    | No_Install
    | Keep_Major
    | Keep_Minor
    | Keep_Patch
    | Touch_Affected_Refs
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | NuGet(_) -> "NuGet package ID"
            | NuGet_Legacy(_) -> "[obsolete]"

            | Group(_) -> "add the dependency to a group (default: Main group)"
            | Group_Legacy(_) -> "[obsolete]"

            | Version(_) -> "dependency version constraint"
            | Version_Legacy(_) -> "[obsolete]"

            | Project(_) -> "add the dependency to a single project only"
            | Project_Legacy(_) -> "[obsolete]"

            | Create_New_Binding_Files -> "create binding redirect files if needed"
            | Create_New_Binding_Files_Legacy -> "[obsolete]"

            | Force -> "force download and reinstallation of all dependencies"
            | Interactive -> "ask for every project whether to add the dependency"
            | Redirects -> "create binding redirects"
            | Clean_Redirects -> "remove binding redirects that were not created by Paket"
            | No_Install -> "do not add dependencies to projects"
            | Keep_Major -> "only allow updates that preserve the major version"
            | Keep_Minor -> "only allow updates that preserve the minor version"
            | Keep_Patch -> "only allow updates that preserve the patch version"
            | Touch_Affected_Refs -> "touch project files referencing affected dependencies to help incremental build tools detecting the change"

type ConfigArgs =
    | [<CustomCommandLine("add-credentials")>] AddCredentials of key_or_URL:string
    | [<CustomCommandLine("add-token")>] AddToken of key_or_URL:string * token:string
    | Username of username:string
    | Password of password:string
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | AddCredentials(_) -> "add credentials for URL or credential key"
            | AddToken(_) -> "add token for URL or credential key"
            | Username(_) -> "provide username"
            | Password(_) -> "provide password"

type ConvertFromNugetArgs =
    | [<AltCommandLine("-f")>] Force
    | No_Install
    | No_Auto_Restore

    | Migrate_Credentials of mode:string
    | [<Hidden;CustomCommandLine("--creds-migrations")>] Migrate_Credentials_Legacy of mode:string
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Force -> "force the conversion even if paket.dependencies or paket.references files are present"
            | No_Install -> "do not add dependencies to projects"
            | No_Auto_Restore -> "do not enable Paket's auto-restore"

            | Migrate_Credentials(_) -> "specify mode for NuGet source credential migration: encrypt|plaintext|selective (default: encrypt)"
            | Migrate_Credentials_Legacy(_) -> "[obsolete]"

type FindRefsArgs =
    | [<Mandatory;MainCommandAttribute()>] NuGets of package_ID:string list
    | [<Hidden;ExactlyOnce;CustomCommandLine("nuget")>] NuGets_Legacy of package_ID:string list

    | [<AltCommandLine("-g")>] Group of name:string
    | [<Hidden;CustomCommandLine("group")>] Group_Legacy of name:string
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | NuGets(_) -> "list of NuGet package IDs"
            | NuGets_Legacy(_) -> "[obsolete]"

            | Group(_) -> "specify dependency group (default: Main group)"
            | Group_Legacy(_) -> "[obsolete]"

type InitArgs =
    | [<Hidden;NoCommandLine>] NoArgs
with
    interface IArgParserTemplate with
        member __.Usage = ""

type AutoRestoreFlags = On | Off

type AutoRestoreArgs =
    | [<MainCommand;Mandatory>] Flags of AutoRestoreFlags
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Flags(_) -> "enable or disable automatic package restore"

type InstallArgs =
    | [<AltCommandLine("-f")>] Force
    | Redirects

    | Create_New_Binding_Files
    | [<Hidden;CustomCommandLine("--createnewbindingfiles")>] Create_New_Binding_Files_Legacy

    | Clean_Redirects
    | Keep_Major
    | Keep_Minor
    | Keep_Patch
    | [<CustomCommandLine("--generate-load-scripts")>] Generate_Load_Scripts
    | [<CustomCommandLine("--only-referenced")>] Install_Only_Referenced
    | [<Hidden;CustomCommandLine("project-root")>] Project_Root of path:string

    | Load_Script_Framework of framework:string
    | [<Hidden;CustomCommandLine("load-script-framework")>] Load_Script_Framework_Legacy of framework:string

    | Load_Script_Type of script_type:string
    | [<Hidden;CustomCommandLine("load-script-type")>] Load_Script_Type_Legacy of script_type:string

    | Touch_Affected_Refs
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Force -> "force download and reinstallation of all dependencies"
            | Redirects -> "create binding redirects"

            | Create_New_Binding_Files -> "create binding redirect files if needed"
            | Create_New_Binding_Files_Legacy -> "[obsolete]"

            | Clean_Redirects -> "remove binding redirects that were not created by Paket"
            | Install_Only_Referenced -> "only install dependencies listed in paket.references files, instead of all packages in paket.dependencies"
            | Generate_Load_Scripts -> "generate F# and C# include scripts that reference installed packages in a interactive environment like F# Interactive or ScriptCS"
            | Keep_Major -> "only allow updates that preserve the major version"
            | Keep_Minor -> "only allow updates that preserve the minor version"
            | Keep_Patch -> "only allow updates that preserve the patch version"
            | Touch_Affected_Refs -> "touch project files referencing affected dependencies to help incremental build tools detecting the change"
            | Project_Root(_) -> "alternative project root (only used for tooling)"

            | Load_Script_Framework(_) -> "framework identifier to generate scripts for, such as net45 or netstandard1.6"
            | Load_Script_Framework_Legacy(_) -> "[obsolete]"

            | Load_Script_Type(_) -> "language to generate scripts for, must be one of 'fsx' or 'csx'"
            | Load_Script_Type_Legacy(_) -> "[obsolete]"

type OutdatedArgs =
    | Ignore_Constraints

    | [<AltCommandLine("-g")>] Group of name:string
    | [<Hidden;CustomCommandLine("group")>] Group_Legacy of name:string

    | [<AltCommandLine("--pre")>] Include_Prereleases
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Ignore_Constraints -> "ignore version constraints in the paket.dependencies file"

            | Group(_) -> "specify dependency group (default: all groups)"
            | Group_Legacy(_) -> "[obsolete]"

            | Include_Prereleases -> "consider prerelease versions as updates"

type RemoveArgs =
    | [<CustomCommandLine("nuget")>][<Mandatory>] Nuget of package_ID:string
    | [<CustomCommandLine("project")>] Project of name:string
    | [<CustomCommandLine("group")>] Group of name:string
    | [<AltCommandLine("-f")>] Force
    | [<AltCommandLine("-i")>] Interactive
    | No_Install
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Nuget(_) -> "NuGet package id."
            | Group(_) -> "Removes the package from the given group. If omitted the Main group is used."
            | Project(_) -> "Allows to remove the package from a single project only."
            | Force -> "Forces the download and reinstallation of all packages."
            | Interactive -> "Asks the user for every project if he or she wants to remove the package from the projects's paket.references file. By default every installation of the package is removed."
            | No_Install -> "Skips paket install process (patching of csproj, fsproj, ... files) after the generation of paket.lock file."

type ClearCacheArgs =
    | [<Hidden;NoCommandLine>] NoArgs
with
    interface IArgParserTemplate with
        member __.Usage = ""

type RestoreArgs =
    | [<AltCommandLine("-f")>] Force
    | [<CustomCommandLine("--only-referenced")>] Install_Only_Referenced
    | [<CustomCommandLine("--touch-affected-refs")>] Touch_Affected_Refs
    | [<CustomCommandLine("--ignore-checks")>] Ignore_Checks
    | [<CustomCommandLine("--fail-on-checks")>] Fail_On_Checks
    | [<CustomCommandLine("group")>] Group of name:string
    | [<Unique>] Project of file_name:string
    | [<Unique>] References_Files of file_name:string list
    | [<Unique>] Target_Framework of target_framework:string
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Force -> "Forces the download of all packages."
            | Group(_) -> "Allows to restore a single group."
            | Install_Only_Referenced -> "Allows to restore packages that are referenced in paket.references files, instead of all packages in paket.dependencies."
            | Touch_Affected_Refs -> "Touches project files referencing packages which are being restored, to help incremental build tools detecting the change."
            | Ignore_Checks -> "Skips the test if paket.dependencies and paket.lock are in sync."
            | Fail_On_Checks -> "Causes the restore to fail if any of the checks fail."
            | Project(_) -> "Allows to restore dependencies for a project."
            | References_Files(_) -> "Allows to restore all packages from the given paket.references files."
            | Target_Framework(_) -> "Allows to restore only for a specified target framework."

type SimplifyArgs =
    | [<AltCommandLine("-i")>] Interactive
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Interactive -> "confirm deletion of every transitive dependency"

type UpdateArgs =
    | [<Mandatory;MainCommandAttribute()>] NuGet of package_id:string
    | [<Hidden;CustomCommandLine("nuget")>] NuGet_Legacy of package_id:string

    | [<AltCommandLine("-V")>] Version of version_constraint:string
    | [<Hidden;CustomCommandLine("version")>] Version_Legacy of version_constraint:string

    | [<AltCommandLine("-g")>] Group of name:string
    | [<Hidden;CustomCommandLine("group")>] Group_Legacy of name:string

    | Create_New_Binding_Files
    | [<Hidden;CustomCommandLine("--createnewbindingfiles")>] Create_New_Binding_Files_Legacy

    | [<AltCommandLine("-f")>] Force
    | Redirects
    | Clean_Redirects
    | No_Install
    | Keep_Major
    | Keep_Minor
    | Keep_Patch
    | Filter
    | Touch_Affected_Refs
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | NuGet(_) -> "NuGet package ID"
            | NuGet_Legacy(_) -> "[obsolete]"

            | Group(_) -> "specify dependency group to update (default: all groups)"
            | Group_Legacy(_) -> "[obsolete]"

            | Version(_) -> "dependency version constraint"
            | Version_Legacy(_) -> "[obsolete]"

            | Create_New_Binding_Files -> "create binding redirect files if needed"
            | Create_New_Binding_Files_Legacy -> "[obsolete]"

            | Force -> "force download and reinstallation of all dependencies"
            | Redirects -> "create binding redirects"
            | Clean_Redirects -> "remove binding redirects that were not created by Paket"
            | No_Install -> "do not add dependencies to projects"
            | Keep_Major -> "only allow updates that preserve the major version"
            | Keep_Minor -> "only allow updates that preserve the minor version"
            | Keep_Patch -> "only allow updates that preserve the patch version"
            | Touch_Affected_Refs -> "touch project files referencing affected dependencies to help incremental build tools detecting the change"
            | Filter -> "treat the NuGet package ID as a regex to filter packages"

type FindPackagesArgs =
    | [<MainCommandAttribute()>] Search of package_ID:string
    | [<Hidden;CustomCommandLine("searchtext")>] Search_Legacy of package_ID:string

    | Source of source_URL:string
    | [<Hidden;CustomCommandLine("source")>] Source_Legacy of source_URL:string

    | [<CustomCommandLine("--max")>] Max_Results of int
    | [<Hidden;CustomCommandLine("max")>] Max_Results_Legacy of int
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Search(_) -> "search for NuGet package ID"
            | Search_Legacy(_) -> "[obsolete]"

            | Source(_) -> "specify source URL"
            | Source_Legacy(_) -> "[obsolete]"

            | Max_Results(_) -> "limit maximum number of results"
            | Max_Results_Legacy(_) -> "[obsolete]"

type FixNuspecArgs =
    | [<Mandatory>][<CustomCommandLine("file")>] File of text:string
    | [<Mandatory>][<CustomCommandLine("references-file")>] ReferencesFile of text:string
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | File _ -> ".nuspec file to fix transitive dependencies within"
            | ReferencesFile _ -> "paket.references to use"

type FixNuspecsArgs =
    | [<Mandatory>][<CustomCommandLine("files")>] Files of nuspecPaths:string list
    | [<Mandatory>][<CustomCommandLine("references-file")>] ReferencesFile of referencePath:string
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Files _ -> ".nuspec files to fix transitive dependencies within"
            | ReferencesFile _ -> "paket.references to use"

type GenerateNuspecArgs =
    | [<CustomCommandLine "project">][<Mandatory>] Project of project:string
    | [<CustomCommandLine "dependencies">][<Mandatory>] DependenciesFile of dependenciesPath:string
    | [<CustomCommandLine "output">][<Mandatory>] Output of output:string
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Project _ -> "generate .nuspec for project"
            | DependenciesFile _ -> "paket.dependencies file used to populate .nuspec file"
            | Output _ -> "output directory of the .nuspec file"

type ShowInstalledPackagesArgs =
    | [<AltCommandLine("-a")>] All

    | [<AltCommandLine("-p")>] Project of path:string
    | [<Hidden;CustomCommandLine("project")>] Project_Legacy of path:string
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | All -> "include transitive dependencies"

            | Project(_) -> "specify project to show dependencies for"
            | Project_Legacy(_) -> "[obsolete]"

type ShowGroupsArgs =
    | [<Hidden;NoCommandLine>] NoArgs
with
    interface IArgParserTemplate with
        member __.Usage = ""

type FindPackageVersionsArgs =
    | [<Mandatory;MainCommandAttribute()>] NuGet of package_ID:string
    | [<Hidden;CustomCommandLine("nuget", "name")>] NuGet_Legacy of package_ID:string

    | Source of source_URL:string
    | [<Hidden;CustomCommandLine("source")>] Source_Legacy of source_URL:string

    | [<CustomCommandLine("--max")>] Max_Results of int
    | [<Hidden;CustomCommandLine("max")>] Max_Results_Legacy of int
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | NuGet(_) -> "NuGet package ID"
            | NuGet_Legacy(_) -> "[obsolete]"

            | Source(_) -> "specify source URL"
            | Source_Legacy(_) -> "[obsolete]"

            | Max_Results(_) -> "limit maximum number of results"
            | Max_Results_Legacy(_) -> "[obsolete]"

type PackArgs =
    | [<ExactlyOnce;MainCommand>] Output of path:string
    | [<Hidden;ExactlyOnce;CustomCommandLine("output")>] Output_Legacy of path:string

    | Build_Config of configuration:string
    | [<Hidden;CustomCommandLine("buildconfig")>] Build_Config_Legacy of configuration:string

    | Build_Platform of platform:string
    | [<Hidden;CustomCommandLine("buildplatform")>] Build_Platform_Legacy of platform:string

    | Version of version:string
    | [<Hidden;CustomCommandLine("version")>] Version_Legacy of version:string

    | [<CustomCommandLine("--template")>] Template_File of path:string
    | [<Hidden;CustomCommandLine("templatefile")>] Template_File_Legacy of path:string

    | [<CustomCommandLine("--exclude")>] Exclude_Template of package_ID:string
    | [<Hidden;CustomCommandLine("exclude")>] Exclude_Template_Legacy of package_ID:string

    | Specific_Version of package_ID:string * version:string
    | [<Hidden;CustomCommandLine("specific-version")>] Specific_Version_Legacy of package_ID:string * version:string

    | Release_Notes of text:string
    | [<Hidden;CustomCommandLine("releaseNotes")>] Release_Notes_Legacy of text:string

    | Lock_Dependencies
    | [<Hidden;CustomCommandLine("lock-dependencies")>] Lock_Dependencies_Legacy

    | [<CustomCommandLine("--minimum-from-lock-file")>] Lock_Dependencies_To_Minimum
    | [<Hidden;CustomCommandLine("minimum-from-lock-file")>] Lock_Dependencies_To_Minimum_Legacy

    | Pin_Project_References
    | [<Hidden;CustomCommandLine("pin-project-references")>] Pin_Project_References_Legacy

    | Symbols
    | [<Hidden;CustomCommandLine("symbols")>] Symbols_Legacy

    | Include_Referenced_Projects
    | [<Hidden;CustomCommandLine("include-referenced-projects")>] Include_Referenced_Projects_Legacy

    | Project_Url of URL:string
    | [<Hidden;CustomCommandLine("project-url")>] Project_Url_Legacy of URL:string
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Output(_) -> "output directory for .nupkg files"
            | Output_Legacy(_) -> "[obsolete]"

            | Build_Config(_) -> "build configuration that should be packaged (default: Release)"
            | Build_Config_Legacy(_) -> "[obsolete]"

            | Build_Platform(_) -> "build platform that should be packaged (default: check all known platform targets)"
            | Build_Platform_Legacy(_) -> "[obsolete]"

            | Version(_) -> "version of the package"
            | Version_Legacy(_) -> "[obsolete]"

            | Template_File(_) -> "pack a single paket.template file"
            | Template_File_Legacy(_) -> "[obsolete]"

            | Exclude_Template(_) -> "exclude paket.template file by package ID"
            | Exclude_Template_Legacy(_) -> "[obsolete]"

            | Specific_Version(_) -> "version number to use for package ID"
            | Specific_Version_Legacy(_) -> "[obsolete]"

            | Release_Notes(_) -> "release notes"
            | Release_Notes_Legacy(_) -> "[obsolete]"

            | Lock_Dependencies -> "use version constraints from paket.lock instead of paket.dependencies"
            | Lock_Dependencies_Legacy(_) -> "[obsolete]"

            | Lock_Dependencies_To_Minimum -> "use version constraints from paket.lock instead of paket.dependencies and add them as a minimum version; --lock-dependencies overrides this option"
            | Lock_Dependencies_To_Minimum_Legacy(_) -> "[obsolete]"

            | Pin_Project_References -> "pin dependencies generated from project references to exact versions (=) instead of using minimum versions (>=); with --lock-dependencies project references will be pinned even if this option is not specified"
            | Pin_Project_References_Legacy(_) -> "[obsolete]"

            | Symbols -> "create symbol and source packages in addition to library and content packages"
            | Symbols_Legacy(_) -> "[obsolete]"

            | Include_Referenced_Projects -> "include symbols and source from referenced projects"
            | Include_Referenced_Projects_Legacy(_) -> "[obsolete]"

            | Project_Url(_) -> "homepage URL for the package"
            | Project_Url_Legacy(_) -> "[obsolete]"

type PushArgs =
    | [<CustomCommandLine("url")>][<Mandatory>] Url of url:string
    | [<CustomCommandLine("file")>][<Mandatory>] FileName of path:string
    | [<CustomCommandLine("apikey")>] ApiKey of key:string
    | [<CustomCommandLine("endpoint")>] EndPoint of path:string
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Url(_) -> "Url of the NuGet feed."
            | FileName(_) -> "Path to the package."
            | ApiKey(_) -> "Optionally specify your API key on the command line. Otherwise uses the value of the `nugetkey` environment variable."
            | EndPoint(_) -> "Optionally specify a custom api endpoint to push to. Defaults to `/api/v2/package`."

type GenerateLoadScriptsArgs =
    | [<AltCommandLine("-g")>] Groups of name:string list
    | [<Hidden;CustomCommandLine("groups")>] Groups_Legacy of name:string list

    | [<AltCommandLine("-f")>] Framework of framework:string
    | [<Hidden;CustomCommandLine("framework")>] Framework_Legacy of framework:string

    | [<AltCommandLine("-t")>] Type of script_type:string
    | [<Hidden;CustomCommandLine("type")>] Type_Legacy of script_type:string
with
  interface IArgParserTemplate with
      member this.Usage =
        match this with
        | Groups(_) -> "groups to generate scripts for (default: all groups)"
        | Groups_Legacy(_) -> "[obsolete]"

        | Framework(_) -> "framework identifier to generate scripts for, such as net45 or netstandard1.6"
        | Framework_Legacy(_) -> "[obsolete]"

        | Type(_) -> "language to generate scripts for, must be one of 'fsx' or 'csx'"
        | Type_Legacy(_) -> "[obsolete]"

type WhyArgs =
    | [<Mandatory;MainCommandAttribute()>] NuGet of package_ID:string
    | [<Hidden;CustomCommandLine("nuget")>] NuGet_Legacy of package_ID:string

    | [<AltCommandLine("-g")>] Group of name:string
    | [<Hidden;CustomCommandLine("group")>] Group_Legacy of name:string

    | Details
with
  interface IArgParserTemplate with
      member this.Usage =
        match this with
        | NuGet(_) -> "NuGet package ID"
        | NuGet_Legacy(_) -> "[obsolete]"

        | Group(_) -> "specify dependency group (default: Main group)"
        | Group_Legacy(_) -> "[obsolete]"

        | Details -> "display detailed information with all paths, versions and framework restrictions"

type Command =
    // global options
    |                                                   Version
    | [<AltCommandLine("-s");Inherit>]                  Silent
    | [<AltCommandLine("-v");Inherit>]                  Verbose
    | [<Inherit>]                                       Log_File of path:string
    | [<Inherit;Hidden>]                                From_Bootstrapper
    // subcommands
    | [<CustomCommandLine("add")>]                      Add of ParseResults<AddArgs>
    | [<CustomCommandLine("clear-cache")>]              ClearCache of ParseResults<ClearCacheArgs>
    | [<CustomCommandLine("config")>]                   Config of ParseResults<ConfigArgs>
    | [<CustomCommandLine("convert-from-nuget")>]       ConvertFromNuget of ParseResults<ConvertFromNugetArgs>
    | [<CustomCommandLine("find-refs")>]                FindRefs of ParseResults<FindRefsArgs>
    | [<CustomCommandLine("init")>]                     Init of ParseResults<InitArgs>
    | [<CustomCommandLine("auto-restore")>]             AutoRestore of ParseResults<AutoRestoreArgs>
    | [<CustomCommandLine("install")>]                  Install of ParseResults<InstallArgs>
    | [<CustomCommandLine("outdated")>]                 Outdated of ParseResults<OutdatedArgs>
    | [<CustomCommandLine("remove")>]                   Remove of ParseResults<RemoveArgs>
    | [<CustomCommandLine("restore")>]                  Restore of ParseResults<RestoreArgs>
    | [<CustomCommandLine("simplify")>]                 Simplify of ParseResults<SimplifyArgs>
    | [<CustomCommandLine("update")>]                   Update of ParseResults<UpdateArgs>
    | [<CustomCommandLine("find-packages")>]            FindPackages of ParseResults<FindPackagesArgs>
    | [<CustomCommandLine("find-package-versions")>]    FindPackageVersions of ParseResults<FindPackageVersionsArgs>
    | [<Hidden;CustomCommandLine("fix-nuspec")>]        FixNuspec of ParseResults<FixNuspecArgs>
    | [<CustomCommandLine("fix-nuspecs")>]              FixNuspecs of ParseResults<FixNuspecsArgs>
    | [<CustomCommandLine("generate-nuspec")>]          GenerateNuspec of ParseResults<GenerateNuspecArgs>
    | [<CustomCommandLine("show-installed-packages")>]  ShowInstalledPackages of ParseResults<ShowInstalledPackagesArgs>
    | [<CustomCommandLine("show-groups")>]              ShowGroups of ParseResults<ShowGroupsArgs>
    | [<CustomCommandLine("pack")>]                     Pack of ParseResults<PackArgs>
    | [<CustomCommandLine("push")>]                     Push of ParseResults<PushArgs>
    | [<Hidden;CustomCommandLine("generate-include-scripts")>] GenerateIncludeScripts of ParseResults<GenerateLoadScriptsArgs>
    | [<CustomCommandLine("generate-load-scripts")>]    GenerateLoadScripts of ParseResults<GenerateLoadScriptsArgs>
    | [<CustomCommandLine("why")>]                      Why of ParseResults<WhyArgs>
with
    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Add _ -> "add a new dependency"
            | ClearCache _ -> "clear the NuGet and git cache directories"
            | Config _ -> "store global configuration values like NuGet credentials"
            | ConvertFromNuget _ -> "convert projects from NuGet to Paket"
            | FindRefs _ -> "find all project files that have a dependency installed"
            | Init _ -> "create an empty paket.dependencies file in the current working directory"
            | AutoRestore _ -> "manage automatic package restore during the build process inside Visual Studio"
            | Install _ -> "download dependencies and update projects"
            | Outdated _ -> "find dependencies that have newer versions available"
            | Remove _ -> "remove a dependency"
            | Restore _ -> "download the computed dependency graph"
            | Simplify _ -> "simplify declared dependencies by removing transitive dependencies"
            | Update _ -> "update dependencies to their latest version"
            | FindPackages _ -> "search for NuGet packages"
            | FindPackageVersions _ -> "search for dependency versions"
            | FixNuspec _ -> "[obsolete]"
            | FixNuspecs _ -> "patch a list of .nuspec files to correct transitive dependencies"
            | GenerateNuspec _ -> "generate a default nuspec for a project including its direct dependencies"
            | ShowInstalledPackages _ -> "show installed top-level packages"
            | ShowGroups _ -> "show groups"
            | Pack _ -> "create NuGet packages from paket.template files"
            | Push _ -> "push a NuGet package"
            | GenerateIncludeScripts _ -> "[obsolete]"
            | GenerateLoadScripts _ -> "generate F# and C# include scripts that reference installed packages in a interactive environment like F# Interactive or ScriptCS"
            | Why _ -> "determine why a dependency is required"
            | Log_File _ -> "print output to a file"
            | Silent -> "suppress console output"
            | Verbose -> "print detailed information to the console"
            | Version -> "show Paket version"
            | From_Bootstrapper -> "call coming from the '--run' feature of the bootstrapper"

let commandParser = ArgumentParser.Create<Command>(programName = "paket", errorHandler = new ProcessExiter())

let markdown (subParser : ArgumentParser) (width : int) (additionalText : string) =
    let ensureLineBreak (text : string) = if String.IsNullOrEmpty(text) then text else text + Environment.NewLine + Environment.NewLine
    let cleanUp (text : string) = text.Trim('\r', '\n') |> ensureLineBreak

    let parentMetadata = subParser.ParentInfo |> Option.get

    let syntax =
        subParser.PrintCommandLineSyntax(usageStringCharacterWidth = width)

    let options = subParser.PrintUsage(hideSyntax=true, usageStringCharacterWidth = width)

    let makeSentence t =
        let upcase (s:string) =
            s.Substring(0,1).ToUpper() + s.Substring(1)

        sprintf "%s." (upcase t)

    System.Text.StringBuilder()
        .Append("# paket ")
        .AppendLine(parentMetadata.Name)
        .AppendLine()
        .AppendLine(parentMetadata.Description |> makeSentence)
        .AppendLine()
        .AppendLine("```sh")
        .AppendLine(syntax)
        .AppendLine()
        .Append(options)
        .AppendLine("```")
        .AppendLine()
        .Append(additionalText |> cleanUp)
        .ToString()

let getAllCommands () = commandParser.GetSubCommandParsers()
