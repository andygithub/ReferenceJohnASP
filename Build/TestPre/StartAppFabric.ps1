# start the configured appfabric instance
# need to load all the modules into the prompt before starting

Import-Module ApplicationServer
Import-Module distributedcacheconfiguration
Import-Module distributedcacheadministration

Write-Verbose "Imported AppFabric Modules"
use-cachecluster
start-cachecluster
Write-Verbose "Start cache instance"