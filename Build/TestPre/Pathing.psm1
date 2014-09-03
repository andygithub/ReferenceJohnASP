function Get-SQLPackagePath
{
    return "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\120\SqlPackage.exe";
}

function Get-SQLPackageArguements
{
    return "/a:Publish /pr:$($Env:TF_BUILD_BINARIESDIRECTORY)\Reference_John_UnitTestRun.publish.xml /sf:$($Env:TF_BUILD_BINARIESDIRECTORY)\Reference.John.Database.dacpac /tdn:$(Get-DatabaseNameUnitTestAndBuildNumber)";
}

function Get-DatabaseNameBase
{
    return "Reference_John";
}

function Get-DatabaseNameUnitTest
{
    return "$(Get-DatabaseNameBase)_UnitTestRun_";
}

function Get-DatabaseNameUnitTestAndBuildNumber
{
    return "$(Get-DatabaseNameUnitTest)$Env:TF_BUILD_BUILDNUMBER";
}

function Get-ConnectionStringDatabaseTokenToReplace
{
    return "=$(Get-DatabaseNameUnitTestAndBuildNumber);";
}

function Get-ConnectionStringDatabaseToken
{
    return "=$(Get-DatabaseNameBase);";
}

Export-ModuleMember -function Get-SQLPackagePath;
Export-ModuleMember -function Get-SQLPackageArguements;
Export-ModuleMember -function Get-DatabaseNameBase;
Export-ModuleMember -function Get-DatabaseNameUnitTest;
Export-ModuleMember -function Get-DatabaseNameUnitTestAndBuildNumber;
Export-ModuleMember -function Get-ConnectionStringDatabaseTokenToReplace;
Export-ModuleMember -function Get-ConnectionStringDatabaseToken;