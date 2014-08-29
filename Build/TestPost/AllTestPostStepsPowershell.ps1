#all sections are in the same file because breaking out into seperate files had path issues
#using host for these status messages so they show up in the log by default.  change to verbose to turn down to normal level.

Write-host "Start PostTest Script $(Get-Date -Format o)";

Write-host "Start AppFrabric Section $(Get-Date -Format o)";

# start the configured appfabric instance
# need to load all the modules into the prompt before starting

Import-Module ApplicationServer
Import-Module distributedcacheconfiguration
Import-Module distributedcacheadministration

Write-Verbose "Imported AppFabric Modules";
use-cachecluster
stop-cachecluster
Write-Verbose "Shutdown cache instance";

Write-host "End AppFrabric Section $(Get-Date -Format o)";

Write-host "Start Database Teardown Section $(Get-Date -Format o)";

#teardown script deletes any unit test database names on the configured server

$sqlexecpath = "sqlcmd";
$sqlexecArgs = "-S . -E -Q ""declare @dbnames nvarchar(max) declare @statement nvarchar(max) set @dbnames = '' set @statement = '' select @dbnames = @dbnames + ',[' + name + ']' from sys.databases where name like 'Reference_John_UnitTestRun_%%' if len(@dbnames) = 0     begin      print 'no databases to drop'  end else  begin     set @statement = 'drop database ' + substring(@dbnames, 2, len(@dbnames)) print @statement exec sp_executesql @statement end""" ;

$ps = new-object System.Diagnostics.Process;
$pinfo = New-Object System.Diagnostics.ProcessStartInfo;
$pinfo.Filename = $sqlexecpath;
$pinfo.Arguments = $sqlexecArgs;
$pinfo.RedirectStandardOutput = $true;
$pinfo.RedirectStandardError = $true;
$pinfo.UseShellExecute = $false;
$pinfo.CreateNoWindow = $true;
$ps.StartInfo = $pinfo;
$ps.start();
[string] $OutSQLValue = $ps.StandardOutput.ReadToEnd();
$ps.WaitForExit();

Write-host $OutSQLValue;

Write-host "End Database Teardown Section $(Get-Date -Format o)";

Write-host "End PostTest Script $(Get-Date -Format o)";
