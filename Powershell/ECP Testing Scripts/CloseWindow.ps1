<#	
	.NOTES
	===========================================================================
	 Created with: 	SAPIEN Technologies, Inc., PowerShell Studio 2017 v5.4.140
	 Created on:   	6/20/2017 2:18 PM
	 Created by:   	anna.pages
	 Organization: 	
	 Filename:     	CloseWindow.ps1
	===========================================================================
	.DESCRIPTION
		Functions for closing a window
#>

function Close-Window
{
	[CmdletBinding(SupportsShouldProcess = $true)]
	param (
		[string]$Server = 'localhost',
		[Parameter(Mandatory = $True)]
		[string]$DisplayId,
		[Parameter(Mandatory = $True)]
		[string]$WindowId
	)
	
	Get-CMFromEcpService -Server $Server -EndingUrl "Close/$DisplayId/$WindowId"
}

Export-ModuleMember Close-Window