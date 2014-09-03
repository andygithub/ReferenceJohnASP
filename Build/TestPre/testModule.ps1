Import-Module (Join-Path (Split-Path -Parent $MyInvocation.MyCommand.Definition) 'Pathing.psm1') 

Write-host "Start Test "
Write-host "SQL Package path: $(Get-SQLPackagePath) "
Write-host "Base Database name: $(Get-DatabaseNameBase) "
Write-host "Unit Test database name: $(Get-DatabaseNameUnitTest) "
Write-host "End Test "

Remove-Module -name Pathing