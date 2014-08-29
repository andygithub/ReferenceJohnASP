
Write-host "Start Database Init Section"

$sqlexecpath = "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\120\SqlPackage.exe ";
$sqlexecArgs =" /a:Publish /pr:C:\Builds\1\DemoMeeting\SimpleTFSBuild\bin\Reference_John_UnitTestRun.publish.xml /sf:C:\Builds\1\DemoMeeting\SimpleTFSBuild\bin\Reference.John.Database.dacpac";

$ps = new-object System.Diagnostics.Process;
$pinfo = New-Object System.Diagnostics.ProcessStartInfo;
$pinfo.Filename = $sqlexecpath;
$pinfo.Arguments = "/a:Publish /pr:C:\Builds\1\DemoMeeting\SimpleTFSBuild\bin\Reference_John_UnitTestRun.publish.xml /sf:C:\Builds\1\DemoMeeting\SimpleTFSBuild\bin\Reference.John.Database.dacpac";
$pinfo.RedirectStandardOutput = $true;
$pinfo.RedirectStandardError = $true;
$pinfo.UseShellExecute = $false;
$pinfo.CreateNoWindow = $true;
$ps.StartInfo = $pinfo;
$ps.start() | out-null;

#$ps.BeginOutputReadLine();
[string] $Outvalue = $ps.StandardOutput.ReadToEnd();
$ps.WaitForExit();

#$stdout = $p.StandardOutput.ReadToEnd()
#$stderr = $p.StandardError.ReadToEnd()
#Write-Host "stdout: $stdout"
#Write-Host "stderr: $stderr"

Write-host $Outvalue

Write-host "End Database Init Section";

#& ping.exe localhost -n 1 | Tee-Object -Variable output;
#$output;
