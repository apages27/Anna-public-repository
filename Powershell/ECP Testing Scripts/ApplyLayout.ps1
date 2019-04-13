<#	
	.NOTES
	===========================================================================
	 Created with: 	SAPIEN Technologies, Inc., PowerShell Studio 2017 v5.4.140
	 Created on:   	6/20/2017 1:12 PM
	 Created by:   	anna.pages
	 Organization: 	
	 Filename:     	ApplyLayout.ps1
	===========================================================================
	.DESCRIPTION
		Functions for applying a layout
#>

function Apply-Layout {
	[CmdletBinding(SupportsShouldProcess = $true)]
	param (
		[string]$Server = 'localhost',
		[Parameter(Mandatory = $True)]
		[string]$DisplayId,
		[Parameter(Mandatory = $True)]
		[string]$LayoutId
	)
	
	Get-CMFromEcpService -Server $Server -EndingUrl "Layout/$DisplayId/$LayoutId"
}

Export-ModuleMember Apply-Layout