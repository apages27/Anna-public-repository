<#	
	.NOTES
	===========================================================================
	 Created with: 	SAPIEN Technologies, Inc., PowerShell Studio 2017 v5.4.140
	 Created on:   	6/20/2017 2:33 PM
	 Created by:   	anna.pages
	 Organization: 	
	 Filename:     	UpdateWindow.ps1
	===========================================================================
	.DESCRIPTION
		Functions for updating a window
#>

function Update-Window
{
	[CmdletBinding(SupportsShouldProcess = $true)]
	param (
		[string]$Server = 'localhost',
		[Parameter(Mandatory = $True)]
		[string]$DisplayId,
		[Parameter(Mandatory = $True)]
		[string]$WindowId,
		[Parameter(Mandatory = $True)]
		[string]$AssetId
	)
	
	Get-CMFromEcpService -Server $Server -EndingUrl "Update/$DisplayId/$WindowId/$AssetId"
}

Export-ModuleMember Update-Window