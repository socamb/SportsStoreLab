#
# Replace_SetParameters.ps1
#
param(
    [string]$setParamsFilePath
)
Write-Verbose -Verbose "Entering script Replace-SetParameters.ps1"
Write-Verbose -Verbose ("Path to SetParametersFile: {0}" -f $setParamsFilePath)
 
# get the environment variables
$vars = gci -path env:*
 
# read in the setParameters file
$contents = gc -Path $setParamsFilePath
 
# perform a regex replacement
$newContents = "";
$contents | % {
    $line = $_
    if ($_ -match "__(\w+)__") {
        $setting = gci -path env:* | ? { $_.Name -eq $Matches[1]  }
        if ($setting) {
            Write-Verbose -Verbose ("Replacing key {0} with value from environment" -f $setting.Name)
            $line = $_ -replace "__(\w+)__", $setting.Value
        }
    }
    $newContents += $line + [Environment]::NewLine
}
 
Write-Verbose -Verbose "Overwriting SetParameters file with new values"
sc $setParamsFilePath -Value $newContents
 
Write-Verbose -Verbose "Exiting script Replace-SetParameters.ps1"
