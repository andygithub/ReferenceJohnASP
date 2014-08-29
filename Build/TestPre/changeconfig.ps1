$files = gci C:\Builds\1\DemoMeeting\SimpleTFSBuild\bin  -filter "*.Fixture.dll.config" | Select-Object FullName
if($files)
{
	Write-Verbose "Will apply customization to $($files.count) files."
	
	foreach ($file in $files) {
			
			
		if(-not $Disable)
		{
			$filecontent = Get-Content($file.FullName)
			#attrib $file -r
			$filecontent -replace "=Reference_John;", "=" + $sqlGeneratedDb + ";" | Out-File $file
			Write-Verbose "$file.FullName - database name updated"
		}
	}
}
else
{
	Write-Warning "Found no files."
}