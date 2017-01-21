param (
   [string] $AzureWebsiteName,
   [hashtable] $appsettings
)


Write-Host 'SetAzureWebsite Executing .... '
Set-AzureWebsite -Name $AzureWebsiteName -AppSettings $appsettings

Write-Host 'SetAzureWebsite Done  .... '