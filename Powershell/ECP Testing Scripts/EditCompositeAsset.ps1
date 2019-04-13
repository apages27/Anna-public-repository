function Edit-CompositeAsset
{
	[CmdletBinding(SupportsShouldProcess = $true)]
	param (
		[string]$Server = 'localhost',
		[Parameter(Mandatory = $True)]
		[string]$AssetProperty,
		[Parameter(Mandatory = $True)]
		[string]$AssetValue,
		[Parameter(Mandatory = $True)]
		[string]$AssetId
	)
	
	Get-CMFromEcpService -Server $Server -EndingUrl "Asset/$AssetId/$AssetProperty/$AssetValue"
}

Export-ModuleMember Edit-CompositeAsset