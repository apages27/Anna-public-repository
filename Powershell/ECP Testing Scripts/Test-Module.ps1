<#	
	.NOTES
	===========================================================================
	 Created with: 	SAPIEN Technologies, Inc., PowerShell Studio 2017 v5.4.140
	 Created on:   	6/19/2017 12:22 PM
	 Created by:   	anna.pages
	 Organization: 	
	 Filename:     	Test-Module.ps1
	===========================================================================
	.DESCRIPTION
	The Test-Module.ps1 script lets you test the functions and other features of
	your module in your PowerShell Studio module project. It's part of your project,
	but it is not included in your module.

	In this test script, import the module (be careful to import the correct version)
	and write commands that test the module features. You can include Pester
	tests, too.

	To run the script, click Run or Run in Console. Or, when working on any file
	in the project, click Home\Run or Home\Run in Console, or in the Project pane, 
	right-click the project name, and then click Run Project.
#>


#Explicitly import the module for testing
Import-Module '..\CineNet.psm1' -Force
Import-Module '.\EcpTester.psd1' -Force

#Run each module function

$Server = 'localhost'

$AuthToken = Get-CmAccessToken -Server $Server

<#$displays = New-CmDisplayForEachGpu -Server $Server -AuthToken $AuthToken

Write-Host $displays
	
Write-Host "Waiting for display info to fully Populate (Workspace - Canvas)" -ForegroundColor Green

Start-Sleep -Seconds 15

Write-Host "Done waiting for display info to fully Populate (Workspace - Canvas)" -ForegroundColor Green

$displays | ForEach-Object {
	SetUpDisplayForLayoutTest -Server $Server -DisplayId $_.DisplayId -AuthToken $AuthToken
}

Assign-Ids

Apply-Layout -DisplayId 0 -LayoutId 2

Clear-Display -DisplayId 0

Close-Window -DisplayId 0 -WindowId 0

Update-Window -DisplayId 0 -WindowId 1 -AssetID 105#>

Edit-CompositeAsset -AssetID 200 -AssetProperty "Border" -AssetValue "Red"

#Sample Pester Test
#Describe "Test EcpTester" {
#	It "tests Write-HellowWorld" {
#		Write-HelloWorld | Should BeExactly "Hello World"
#	}	
#}