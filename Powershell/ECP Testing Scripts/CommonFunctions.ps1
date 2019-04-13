<#	
	.NOTES
	===========================================================================
	 Created with: 	SAPIEN Technologies, Inc., PowerShell Studio 2017 v5.4.140
	 Created on:   	6/20/2017 1:12 PM
	 Created by:   	anna.pages
	 Organization: 	
	 Filename:     	CommonFunctions.ps1
	===========================================================================
	.DESCRIPTION
		Common functions for testing ECP service integration
#>

<#function CallCineNetEndpoint()
{
	[CmdletBinding(SupportsShouldProcess = $True)]
	param (
		[Parameter(Mandatory = $True)]
		[int]$Port,
		[Parameter(Mandatory = $True)]
		[string]$Server,
		[Parameter(Mandatory = $True)]
		[string]$BaseUrl,
		[Parameter(Mandatory = $True)]
		[string]$EndingUrl,
		[string][ValidateSet("GET", "POST", "DELETE")]
		$WebMethod = "GET",
		[string]$Body
	)
	
	$fullUrl = "http://$($Server):$Port/$BaseUrl/$endingUrl"

	$AuthToken = Get-CmToken
	
	Write-Host "Calling url $fullUrl using token $AuthToken"
	
	
	
	if ($Body)
	{
		Invoke-RestMethod -Uri $fullUrl -Method $WebMethod -ContentType 'application/json; charset=utf-8' -Body $Body -Headers @{ Authorization = "Token $($AuthToken)" }
	}
	else
	{
		Invoke-RestMethod -Uri $fullUrl -Method $WebMethod -ContentType 'application/json; charset=utf-8' -Headers @{ Authorization = "Token $($AuthToken)" }
	}
}

function Get-CMFromNetworkManager()
{
	[CmdletBinding(SupportsShouldProcess = $True)]
	param (
		[Parameter(Mandatory = $True)]
		[string]$Server,
		[Parameter(Mandatory = $True)]
		[string]$EndingUrl
	)
	
	CallCineNetEndpoint -Port (Get-CmNetworkManagerPort) -Server $Server -BaseUrl "CineNet/NetworkManager" -EndingUrl $EndingUrl
}

Export-ModuleMember Get-CMFromNetworkManager#>

function Get-CMFromEcpService()
{
	[CmdletBinding(SupportsShouldProcess = $True)]
	param (
		[Parameter(Mandatory = $True)]
		[string]$Server,
		[Parameter(Mandatory = $True)]
		[string]$EndingUrl
	)
	
	CallCineNetEndpoint -Port 25777 -Server $Server -BaseUrl "CineNet/Ecp" -EndingUrl $EndingUrl
}

Export-ModuleMember Get-CMFromEcpService

<#function Send-CmToNetworkManager()
{
	[CmdletBinding(SupportsShouldProcess = $True)]
	param (
		[Parameter(Mandatory = $True)]
		[string]$Server,
		[Parameter(Mandatory = $True)]
		[string]$EndingUrl,
		[string]$Body
	)
	
	Write-Host "Posting to NetworkManager := $EndingUrl | Server := $Server"
	Write-Host (ConvertTo-Json $Body)
	
	CallCineNetEndpoint -Port (Get-CmNetworkManagerPort) -Server $Server -BaseUrl "CineNet/NetworkManager" -EndingUrl $EndingUrl -WebMethod "POST" -Body $Body
}

Export-ModuleMember Send-CmToNetworkManager#>

function Get-CmLayouts()
{
	[CmdletBinding(SupportsShouldProcess = $True)]
	param (
		[Parameter(Mandatory = $True)]
		[string]$Server,
		[Parameter(Mandatory = $True)]
		[Guid]$DisplayId
	)
	
	Get-CMFromNetworkManager -Server $Server -EndingUrl "Display/$DisplayId/Layout"
}

Export-ModuleMember Get-CmLayouts

function Get-CmDisplays()
{
	[CmdletBinding(SupportsShouldProcess = $True)]
	param (
		[Parameter(Mandatory = $True)]
		[string]$Server
	)
	
	Get-CMFromNetworkManager -Server $Server -EndingUrl "Display"
}

Export-ModuleMember Get-CmDisplays

function Get-CmAssets()
{
	[CmdletBinding(SupportsShouldProcess = $True)]
	param (
		[Parameter(Mandatory = $True)]
		[string]$Server,
		[string]$InstanceId
	)
	
	Get-CMFromNetworkManager -Server $Server -EndingUrl "Instance/$InstanceId/Assets"
}

Export-ModuleMember Get-CmAssets

<#function Get-CMInstance()
{
	[CmdletBinding(SupportsShouldProcess = $True)]
	param (
		[Parameter(Mandatory = $True)]
		[string]$Server,
		[string]$InstanceName,
		[string]$InstanceType
	)
	
	Write-Host "Getting Instances from Network Manager"
	
	$instances = Get-CMFromNetworkManager -Server $Server -EndingUrl "Instance"
	
	if ($InstanceName)
	{
		$instances | Where-Object { $_.InstanceName -eq $InstanceName } | Select-Object -First 1
	}
	elseIf ($InstanceType)
	{
		$instances | Where-Object { $_.InstanceType -eq $InstanceType }
	}
	else
	{
		$instances
	}
}

Export-ModuleMember Get-CMInstance#>