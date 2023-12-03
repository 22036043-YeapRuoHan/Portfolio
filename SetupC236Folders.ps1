#
# C236 Powershell Script
#
# This script will create your C236 solution and 13 projects
# Please run in either D:\C236 or C:\C236
#
# Usage: 1. Open windows search and type powershell
#        2. Change directory to D:\C236 or C:\C236
#        3. Type the name of this script (SetupC236Folders.ps1)
#
# SOI Republic Polytechnic 2023
#
$version = dotnet --version | Out-String
Write-Output $version
if ($version.Substring(0,1) -ne '7')
{
    Write-Output 'Dotnet Version 7 is needed.'
    Write-Output 'Please update to the latest Visual Studio.'
    cmd /c 'pause'
	Exit
}
dotnet new sln 
for ($x = 1 ; $x -le 13 ; $x++) { 
    $dir = "Lesson{0:d2}" -f $x
    mkdir $dir
    cd $dir
    dotnet new mvc --no-https
    # Remove-Item Controllers\HomeController.cs 
	Set-Content -Path Controllers\HomeController.cs -Value "using System;`r`nusing Microsoft.AspNetCore.Mvc;`r`nnamespace $dir.Controllers`r`n{`r`n  public class HomeController : Controller`r`n  {`r`n    public String Index() => ""$dir Home"";`r`n  }`r`n}`r`n"
	Set-Content -Path Views\Shared\_Layout.cshtml -Value "@RenderBody();"
    Remove-Item Views\_ViewImports.cshtml 
    Remove-Item Views\Shared\_ValidationScriptsPartial.cshtml
    Remove-Item Views\Shared\Error.cshtml
    Remove-Item Views\Home -Recurse -Force -Confirm:$false
	Set-Content -Path wwwroot\index.html -Value "$dir index.html"
    Remove-Item wwwroot\favicon.ico
    Remove-Item wwwroot\css -Recurse -Force -Confirm:$false
    Remove-Item wwwroot\js -Recurse -Force -Confirm:$false
    Remove-Item wwwroot\lib -Recurse -Force -Confirm:$false
    cd ..
    dotnet sln add $dir
}
