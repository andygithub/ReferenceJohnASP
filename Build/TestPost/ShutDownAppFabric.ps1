# shut down the configured appfabric instance
# need to load all the modules into the prompt before shutting down

Import-Module ApplicationServer
Import-Module distributedcacheconfiguration
Import-Module distributedcacheadministration

Write-Verbose "Imported AppFabric Modules"
use-cachecluster
stop-cachecluster
Write-Verbose "Shutdown cache instance"