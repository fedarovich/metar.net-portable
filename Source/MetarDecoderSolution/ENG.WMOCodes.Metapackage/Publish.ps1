$sourceNugetExe = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$targetNugetExe = [System.IO.Path]::GetFullPath([System.IO.Path]::Combine($PSScriptRoot, "..", "nuget.exe"))

Set-Alias nuget $targetNugetExe -Scope Script -Verbose

If ((Test-Path $targetNugetExe) -eq $true) {
	nuget update -self
} Else {
	Invoke-WebRequest $sourceNugetExe -OutFile $targetNugetExe
}

$version = [System.IO.Path]::GetFileName($PSScriptRoot)
nuget push "$PSScriptRoot\METAR.NET.Core.Portable.$version.nupkg" -Source https://www.nuget.org/api/v2/package
nuget push "$PSScriptRoot\METAR.NET.Decoders.Portable.$version.nupkg" -Source https://www.nuget.org/api/v2/package
nuget push "$PSScriptRoot\METAR.NET.Downloaders.Portable.$version.nupkg" -Source https://www.nuget.org/api/v2/package
nuget push "$PSScriptRoot\METAR.NET.Portable.$version.nupkg" -Source https://www.nuget.org/api/v2/package
