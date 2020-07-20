param([switch]$SkipAutoCreateAndLoadOfProfileScript)

<# Import-Module (Join-Path $PSScriptRoot "ProfileModules\Utilities.psm1") -Force -DisableNameChecking
Import-Module (Join-Path $PSScriptRoot "ProfileModules\CodingFunctions.psm1") -Force -DisableNameChecking
Import-Module (Join-Path $PSScriptRoot "ProfileModules\GitFunctions.psm1") -Force -DisableNameChecking
Import-Module (Join-Path $PSScriptRoot "ProfileModules\InstallFunctions.psm1") -Force -DisableNameChecking
Import-Module (Join-Path $PSScriptRoot "ProfileModules\Uncategorized.psm1") -Force -DisableNameChecking
Import-Module (Join-Path $PSScriptRoot "ProfileModules\Utilities.psm1") -Force -DisableNameChecking
Import-Module (Join-Path $PSScriptRoot "ProfileModules\GitFunctions.psm1") -Force -DisableNameChecking
Import-Module (Join-Path $PSScriptRoot "ProfileModules\TestingFunctions.psm1") -Force -DisableNameChecking #>

function script:GetPathToCustomProfileScript {
  param(
    [string]$UserName = $env:UserName,
    [string]$ScriptRoot = $PSScriptRoot
  )

  $pathToUserNameCustomProfile = Join-Path $ScriptRoot "$UserName.ps1"

  $userNameCustomProfileExists = Test-Path $pathToUserNameCustomProfile

  if ($userNameCustomProfileExists) {
    return $pathToUserNameCustomProfile
  }
  else {
    Write-Host "Hmm, can't find a profile for the user $UserName."
  }
}

$doAutoLoad = -not $SkipAutoCreateAndLoadOfProfileScript

if ($doAutoLoad) {
	Write-Host "Loading Anna's Custom Profile" -ForegroundColor Cyan
	
  $PathToCustomProfileScript = script:GetPathToCustomProfileScript

  . $PathToCustomProfileScript
}