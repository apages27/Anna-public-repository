<#	
	.NOTES
	===========================================================================
	 Created with: 	SAPIEN Technologies, Inc., PowerShell Studio 2017 v5.4.140
	 Created on:   	6/20/2017 1:35 PM
	 Created by:   	anna.pages
	 Organization: 	
	 Filename:     	ClearDisplay.ps1
	===========================================================================
	.DESCRIPTION
		Functions for clearing a display
#>

function Clear-Display
{
	[CmdletBinding(SupportsShouldProcess = $true)]
	param (
		[string]$Server = 'localhost',
		[Parameter(Mandatory = $True)]
		[string]$DisplayId
	)
	
	Get-CMFromEcpService -Server $Server -EndingUrl "Clear/$DisplayId"
}

Export-ModuleMember Clear-Display