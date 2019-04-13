<#	
	.NOTES
	===========================================================================
	 Created with: 	SAPIEN Technologies, Inc., PowerShell Studio 2017 v5.4.140
	 Created on:   	6/20/2017 1:12 PM
	 Created by:   	anna.pages
	 Organization: 	
	 Filename:     	AssignExternalIds.ps1
	===========================================================================
	.DESCRIPTION
		Functions for assigning external IDs to displays, layouts and assets
#>

function Assign-Ids {
  [CmdletBinding(SupportsShouldProcess=$true)]
  param(
	  [string]$Server = 'localhost'
	)
	
	AssignIds-Displays -Server $Server
	
	AssignIds-Assets -Server $Server
}

Export-ModuleMember Assign-Ids

function AssignIds-Displays
{
	[CmdletBinding(SupportsShouldProcess = $true)]
	param (
		[string]$Server
	)
	
	$displays = (Get-CmDisplays -Server $Server)
	
	Write-Host 'Displays: ' + ($displays | Out-String)
	
	foreach ($display in $displays)
	{
		$assignIdRequest = @{
			ObjectId   = $display.DisplayId;
			ObjectType = 'Display'
		}
		
		$endingUrl = 'Ecp/' + $displays.IndexOf($display)
		
		Write-Host ($assignIdRequest | Out-String)
		Write-Host $endingUrl
		
		Send-CmToNetworkManager -Server $Server -EndingUrl $endingUrl -Body (ConvertTo-Json $assignIdRequest)
		
		AssignIds-Layouts -Server $Server -DisplayId $display.DisplayId -DisplayCount $displays.Count
	}
}

function AssignIds-Layouts
{
	[CmdletBinding(SupportsShouldProcess = $true)]
	param (
		[string]$Server,
		[string]$DisplayId,
		[int]$DisplayCount
	)
	$layouts = (Get-CmLayouts -Server $Server -DisplayId $DisplayId)
	
	Write-Host 'Layouts: ' + ($layouts | Out-String)
	
	foreach ($layout in $layouts)
	{
		$assignIdRequest = @{
			ObjectId   = $layout.LayoutId;
			ObjectType = 'Layout'
		}
		
		$endingUrl = 'Ecp/' + ($DisplayCount + $layouts.IndexOf($layout))
		
		Write-Host ($assignIdRequest | Out-String)
		Write-Host $endingUrl
		
		Send-CmToNetworkManager -Server $Server -EndingUrl $endingUrl -Body (ConvertTo-Json $assignIdRequest)
	}
}

function AssignIds-Assets
{
	[CmdletBinding(SupportsShouldProcess = $true)]
	param (
		[string]$Server
	)
	
	$videoWallInstance = (Get-CMInstance -Server $Server -InstanceType 'VideoWall') | Select-Object -First 1
	
	Write-Host ($videoWallInstance | Out-String)
	
	$assets = (Get-CmAssets -Server $Server -InstanceId $videoWallInstance.InstanceId)
	
	Write-Host 'Assets: ' + ($assets | Out-String)
	
	foreach ($asset in $assets)
	{
		$assignIdRequest = @{
			ObjectId   = $asset.AssetId;
			ObjectType = 'Asset'
		}
		
		$endingUrl = 'Ecp/' + (100 + $assets.IndexOf($asset))
		
		Write-Host ($assignIdRequest | Out-String)
		Write-Host $endingUrl
		
		Send-CmToNetworkManager -Server $Server -EndingUrl $endingUrl -Body (ConvertTo-Json $assignIdRequest)
	}
}