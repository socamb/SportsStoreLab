[CmdletBinding()]

param(
	[Parameter(Mandatory=$True, Position=1)]
	[string]$sourcesDirectory,
	[Parameter(Mandatory=$True, Position=2)]
	[string]$buildNumber
)

# Regular expression pattern to find the version in the build number 
# and then apply it to the assemblies
$versionRegex = "\d+\.\d+\.\d+\.\d+"

Write-Verbose -Verbose "Sources Directory: $sourcesDirectory"
Write-Verbose -Verbose "Build Number: $buildNumber"

# Get and validate the version data
$versionData = [regex]::matches($buildNumber,$versionRegex)
switch($versionData.Count)
{
   0        
      { 
         Write-Error "Could not find version number data in $buildNumber."
         exit 1
      }
   1 {}
   default 
      { 
         Write-Warning "Found more than instance of version data in buildNumber." 
         Write-Warning "Will assume first instance is version."
      }
}

$newVersion = $versionData[0]
Write-Verbose -Verbose "Version: $newVersion"

# Apply the version to the assembly property files
$files = gci $sourcesDirectory -recurse -include "*Properties*","My Project" | 
    ?{ $_.PSIsContainer } | 
    foreach { gci -Path $_.FullName -Recurse -include AssemblyInfo.* }
if($files)
{
    Write-Verbose -Verbose "Will apply $newVersion to $($files.count) files."

    foreach ($file in $files) {
        $filecontent = Get-Content($file)
        attrib $file -r
        $filecontent -replace $versionRegex, $newVersion | Out-File $file
        Write-Verbose -Verbose "$file.FullName - version applied"
    }
}
else
{
    Write-Warning -Verbose "Found no files."
}