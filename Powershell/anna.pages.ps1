	   
# Check if the enum exists, if it doesn't, create it.
# need the zzzz otherwise it will not reconize the device type
if (!("RepoType" -as [Type])) {
    Add-Type -TypeDefinition @'
    public enum RepoType {
		zzzzz,
        device, 
		cinenet, 
		networkmanager, 
		auth, 
		auth_legacy, 
		web, 
		devops, 
		admin, 
		bootstrapper, 
		canvas, 
		common, 
		room, 
		spike, 
		tools, 
		streaming, 
		guard, 
		one, 
		shared, 
		hal, 
		behaviors, 
		stress, 
		ecp, 
		screenShare, 
		connector, 
		licensing,
		webManager,
		serviceTemplate,
		alpha,
		asset,
		displays,
		install,
		dataPathLivePreview,
		identity,
		desktop,
		voice,
		singleInstall,
		wallAsset,
		devicemanager,
		dm,
		us,
		ac,
		ssl,
        it,
        wc,
        webcommon,
        activity,
        onvif,
        powershell,
        filetransporter,
        ft
    }
'@
}

function eh { sudo "C:\Program Files\Notepad++\notepad++.exe" "C:\Windows\System32\drivers\etc\hosts" -p };

function sn+ { Start-Process notepad++ };

function vpn($NewPassword) { 
    $pathToEncryptedPassword = Join-Path $env:HOME -ChildPath encrypted-password.txt

    if ($NewPassword) {
        if (Test-Path $pathToEncryptedPassword) {
            Move-Item $pathToEncryptedPassword "$pathToEncryptedPassword-replaced-on-$(Get-Date -Format {yyyy-dd-MM_hh-mm})"
        }
        Set-Content -Path $pathToEncryptedPassword -Value $($NewPassword | ConvertTo-SecureString -AsPlainText -Force | ConvertFrom-SecureString)
    }


    $netCred = (New-Object System.Management.Automation.PSCredential -ArgumentList anna.pages, (Get-Content $pathToEncryptedPassword | ConvertTo-SecureString)).GetNetworkCredential()
    disco
    rasdial.exe "CineMassive VPN" $netCred.UserName $netCred.Password
}

function gitmerge {
    git pull origin master
}

function disco {
    rasdial.exe /Disconnect
}

function cts([ValidateSet('Galactica', 'WhiteStar', 'Nostromo', 'TigersClaw', 'StarFury', 'NoSerenity', 'voyager')]$serverName) {
    Set-CmEnvironmentVariable -EnvironmentVariable CineTestServer -Value $serverName
    "Server changed to $env:CineTestServer"
}

function setenv {
    param(
        [string]$variableName,
        [string]$value
    )
	
    [Environment]::SetEnvironmentVariable($variableName, $value, "Machine")
	
    refreshenv
	
    "Environment variable $variableName set to $value"
}

function vnc($computer) {
    $computer = if ($computer) { $computer } else { $env:CineTestServer }
    $pathToTightVnc = Join-Path $env:ProgramFiles "TightVNC\tvnviewer.exe"
    Start-Process $pathToTightVnc $computer
}

function zoomHolodeck($meetingId = "5285149116") {
    $zoomArguments = "--url=zoommtg://zoom.us/join?action=join&confid=dGlkPWIwYzdjZTZiN2I0YTU2YmNhYmE2ZmJjM2Q4ZWY5ZmQz&confno=$meetingId&zc=0&pk=&mcv=0.92.11227.0929&browser=chrome"

    Start-Process $env:ZoomLocation $zoomArguments
}

function zoomFarpoint($meetingId = "4465322665") {
    $zoomArguments = "--url=zoommtg://zoom.us/join?action=join&confid=dGlkPWIwYzdjZTZiN2I0YTU2YmNhYmE2ZmJjM2Q4ZWY5ZmQz&confno=$meetingId&zc=0&pk=&mcv=0.92.11227.0929&browser=chrome"

    Start-Process $env:ZoomLocation $zoomArguments
}

function zoomgs($meetingId = "5742885281") {
    $zoomArguments = "--url=zoommtg://zoom.us/join?action=join&confid=dGlkPWIwYzdjZTZiN2I0YTU2YmNhYmE2ZmJjM2Q4ZWY5ZmQz&confno=$meetingId&zc=0&pk=&mcv=0.92.11227.0929&browser=chrome"

    Start-Process $env:ZoomLocation $zoomArguments
}

function zoomBridgeOfAwesome($meetingId = "7398400979") {
    $zoomArguments = "--url=zoommtg://zoom.us/join?action=join&confid=dGlkPWIwYzdjZTZiN2I0YTU2YmNhYmE2ZmJjM2Q4ZWY5ZmQz&confno=$meetingId&zc=0&pk=&mcv=0.92.11227.0929&browser=chrome"

    Start-Process $env:ZoomLocation $zoomArguments
}

function zoff {
    Get-Process zoom -ErrorAction SilentlyContinue | Stop-Process
}

function zwitchF {
    zoff
 
    zoomFarpoint
}

function zwitchH {
    zoff
 
    zoomHolodeck
}

function pullAllRepos {
    Clear-Host

    $currentLocation = Get-Location

    Step-Repos {
		git.exe fetch
        $status = Get-GitStatus -ForegroundColor Yellow
        Write-Host "Git Status: $status"
		$hasStuffToPull = $status.BehindBy -ge 1
		$noLocalChanges = (-not $status.HasWorking) -and (-not $status.HasIndex)
		if ($hasStuffToPull) {
			git.exe pull
        }

		Write-VcsStatus
		Write-Host "$_" -ForegroundColor Green
	}

    . $PROFILE

    Set-Location $currentLocation
}

function trello {
    Start-Process https://trello.com/b/xyKA1vXn/1-cm-dev-weekly-wip
}

function gitc { 
    param (
        [string]$Message = "",
        [switch]$DoNotPush = $false
    )
	
    if ($Message) {
        git add -A
        git commit -m $Message
		
        if (!$DoNotPush) {
            pp
        }
    }
    else {
        Start-Process "C:\Program Files (x86)\GitExtensions\GitExtensions.exe" commit
    }
}

function gite { 
    Start-Process "C:\Program Files (x86)\GitExtensions\GitExtensions.exe"
}

function tc($serverName) { if (Test-Connection $serverName -Count 1 -Quiet) { Write-Host "$serverName is there" } else { Write-Host "$serverName is down" }}

function repo([RepoType]$repoName) {

    $rootDirectory = $env:CineMassiveCodeRoot
       
    $endingFolder = "";
    
    switch ($repoName) {
        "ac" { $endingFolder = "CineNet.AlphaControl" }
        "alpha" { $endingFolder = "CineNet.AlphaControl" }
        "dc" { $endingFolder = "CineNet.DeviceControl" }
        "device" { $endingFolder = "CineNet.DeviceControl" }
        "cinenet" { $endingFolder = "CineNet\CineNet" }
        "cn" { $endingFolder = "CineNet" }
        "networkmanager" { $endingFolder = "CineNet.NetworkManager" }
        "nm" { $endingFolder = "CineNet.NetworkManager" }
        "auth" { $endingFolder = "CineNet.AuthenticationService" }
        "as" { $endingFolder = "CineNet.AuthenticationService" }
        "authservice" { $endingFolder = "CineNet.AuthenticationService" }
        "web" { $endingFolder = "CineNetWeb" }
        "devops" { $endingFolder = "DevOps" }
        "admin" { $endingFolder = "CineNet.Administration" }
        "administration" { $endingFolder = "CineNet.Administration" }
        "bootstrapper" { $endingFolder = "CineNet.Bootstrapper" }
        "boot" { $endingFolder = "CineNet.Bootstrapper" }
        "canvas" { $endingFolder = "CineNet.Graphics.Canvas" }
        "common" { $endingFolder = "CineNet.Common" }
        "room" { $endingFolder = "CineNet.RoomService" }
        "spike" { $endingFolder = "Spikes" }
        "test" { $endingFolder = "CineNet.Tests" }
        "networking" { $endingFolder = "CineNet.Communication.Networking" }
        "tools" { $endingFolder = "CineNet.Tools" }
        "streaming" { $endingFolder = "CineNet.Streaming" }
        "guard" { $endingFolder = "CineNet.GuardianCare" }
        "behaviors" { $endingFolder = "CineNet.Behaviors" }
        "ecp" { $endingFolder = "CineNet.Ecp" }
        "hal" { $endingFolder = "CineNet.Graphics.HardwareAbstraction" }
        "licensing" { $endingFolder = "CineNet.Licensing" }
        "one" { $endingFolder = "CineNet.OneService" }
        "asset" { $endingFolder = "CineNet.AssetManager" }
        "devicemanager" { $endingFolder = "CineNet.DeviceManager" }
        "dm" { $endingFolder = "CineNet.DeviceManager" }
        "us" { $endingFolder = "CineNet.UserState" }
        "displays" { $endingFolder = "CineNet.Displays" }
        "wallasset" { $endingFolder = "CineNet.WallAsset" }
        "install" { $endingFolder = "CineNet.Installer" }
        "ssl" { $endingFolder = "CineNet.Ssl" }
        "it" { $endingFolder = "CineNet.Integration.Tests" }
        "wc" { $endingFolder = "CineNet.Web.Common" }
        "webcommon" { $endingFolder = "CineNet.Web.Common" }
        "dataPathLivePreview" {$endingFolder = "CineNet.DataPathLivePreview"}
        "activity" {$endingFolder = "CineNet.Activity"}
        "onvif" {$endingFolder = "CineNet.OnvifWsdl"}
        "powershell" {$endingFolder = "CineNet.Powershell"}
        "filetransporter" {$endingFolder = "CineNet.FileTransporter"}
        "ft" {$endingFolder = "CineNet.FileTransporter"}
    }

    $finalDestination = $rootDirectory + $endingFolder

    Write-Host "Going to Repo "  $finalDestination -ForegroundColor Magenta

    Set-Location $finalDestination

}

function openSolution() {

    $solutionLocation = Get-ChildItem -Filter *.sln | Select-Object -first 1

    Write-Host "Opening solution " $solutionLocation -ForegroundColor Blue

    Invoke-Item $solutionLocation.FullName
}

function code([RepoType]$repoName) {
    repo $repoName
    openSolution
}

function pow() {
    Start-Process powershell ise
}

function mongo { 
        &("C:\Users\anna.pages\AppData\Local\MongoDBCompassCommunity\MongoDBCompassCommunity.exe")
}

function branch() {
    [CmdletBinding(SupportsShouldProcess = $true)]
    param (
        [string]$Name,
        [switch]$PushBranchToRemote = $true
    )
	
    $branches = git branch -a
	
    if (-not $Name) {
        $branches | ForEach-Object {
            $normalName = $_ -replace "remotes/origin/", ""
			
            Write-Host $normalName -ForegroundColor Cyan
        }
    }
    else {
        $foundBranch = isInList $branches $Name
		
        if ($foundBranch) {
            Write-Host "Found Branch $Name" -ForegroundColor Green
            git checkout $Name
            pp
        }
        else {
            Write-Host "Did not find branch $Name creating" -ForegroundColor Yellow
            git fetch
            git checkout -B $Name
            git push --set-upstream origin $Name
        }
    }
}

function isInList() {
    param (
        [Parameter(Mandatory = $True)][System.Array]$array,
        [Parameter(Mandatory = $True)][string]$matchValue
    )
	
    foreach ($arr in $array) {
        if ($arr -match $matchValue) {
            return $TRUE
        }
    }
	
    $false
}

function bare {
    Push-Location $env:CineNetInstallRoot

    Get-ChildItem *.log -Recurse -Exclude CineNet.OneService.log, WebServer.*.log, CineNet.SingleInstall.log, mongo.log, CineNet.Bootstrapper.log, CineNet.Ecp.log | Sort-Object -Property Name | ForEach-Object { $list += "$_ " }

    Write-Host "Launching baretail with these files: $list"
    Start-Process baretail.exe $list.Trim()
    Pop-Location
}

function resetForTesting() {
    param (
        [string[]]$ServicesToDelete,
        [switch]$DeleteMongo,
        [switch]$DeleteNode,
        [switch]$CineNetToOpen,
        [switch]$KeepInstallData
    )
	
    Reset-CineNetForTesting -ServicesToDelete $ServicesToDelete -DeleteMongo:$DeleteMongo -DeleteNode:$DeleteNode -CineNetToOpen:$CineNetToOpen -KeepInstallData:$KeepInstallData
}

function deployCodeBuilder {
    robocopy $env:CineMassiveCodeRoot\CineNet.Administration\CineNet.CodeBuilder\bin\Debug\ \\tigersclaw\testing\cinenetcodebuilder\
}

function runBuildLocally {
    [CmdletBinding(SupportsShouldProcess = $true)]
    param(
        [Parameter(Mandatory = $True)][int]$buildNumber, 
        [string]$reposAndBranches,
        [string]$configuration = "Release"
    )
  
    #Sample usage of reposAndBranches

    Push-Location (Join-Path $env:CineMassiveCodeRoot "CineNet.Administration")
    $deleteResult = Remove-Item "*\bin\*" -Recurse -Force -ErrorAction SilentlyContinue
    Write-Verbose "Results of delete: $deleteResult"

    build -configuration $configuration 
    Push-Location ".\CineNet.CodeBuilder\bin\Release"
    $argumentList = "-d E:\Code\Builds -b $buildNumber --leaveDirectories --outToConsole"

    if ($reposAndBranches) {
        $argumentList += " --reposAndBranches $reposAndBranches"
        Write-Verbose "Using Repos And Branches: ($reposAndBranches | Out-String)"
    }

    Write-Verbose "Running CodeBuilder with Arguments: $argumentList"
    Invoke-Expression "& .\CineNetCodeBuilder.exe $argumentList"
    Copy-Item C:\dev\Builds\Build_3_0_$buildNumber\CineNet.cm C:\dev\CineNet.Administration\FilesToEmbed\ -Force

    Pop-Location
}

function build {
    param (
        $pathToSolution,
        $configuration = "Debug",
        $target = "Build",
        [ValidateSet("quiet", "minimal", "detailed", "normal")]$verbosity = "normal",
        [switch]$restorePackages
    )
	
    if ($restorePackages) {
        Restore-NugetPackages
    }
	
    Start-MsBuildOrDotNetExe $pathToSolution
}

function webUI {
    repo 'web'
	
    webStorm
}

function webCommon {
    repo 'wc'
	
    webStorm
}

function UIDemo {
    Set-Location C:\Code\DisplayLayoutDemo

    pull

    webStorm
}

function webStorm {
    param(
        [switch] $Blank
    )

    $webStormPath = "C:\Program Files (x86)\JetBrains\WebStorm 2018.2.2\bin\webstorm64.exe"

    if ($Blank) {
        Invoke-Expression $webStormPath
    }
    else {
        $currentDirectory = Get-Location

        $fullCommand = "& '$webStormPath' $currentDirectory"
    
        Invoke-Expression $fullCommand   
    }
}

function pss {
    Start-Process "C:\Program Files\SAPIEN Technologies, Inc\PowerShell Studio 2017\PowerShell Studio.exe"
	
    repo 'power'
}

function devOps {
    repo 'devops'
	
    vscode
}

function vscode {
    param(
        [string] $CodeArgs,
        [switch] $Blank
    )

    $vsCodePath = "& 'C:\Program Files\Microsoft VS Code\Code.exe'"

    if ($CodeArgs) {
        $fullCommand = "& 'C:\Program Files\Microsoft VS Code\bin\code.cmd' $CodeArgs"

        Invoke-Expression $fullCommand
    }
    else {
        if ($Blank) {
            Invoke-Expression $vsCodePath
        }
        else {
            $currentDirectory = Get-Location

            $fullCommand = "& 'C:\Program Files\Microsoft VS Code\bin\code.cmd' $currentDirectory"
    
            Invoke-Expression $fullCommand    
        }
    }
}

function clone([switch]$Elevate, [switch] $Vertical) {
    $orientation = "V"
    if ($Vertical) {
        $orientation = "H"
    }

    $elevateOption = ""
    if ($Elevate) {
        $elevateOption = "a"
    }

    powershell.exe "-new_console:s50$($orientation)$($elevateOption)"
}

function runService {
    param(
        [switch] $Release
    )
    if ($Release) {
        Write-Host "Running service in Release configuration" -ForegroundColor Yellow
        Start-CineNetServiceInReleaseFolder
    }
    else {
        Write-Host "Running service in Debug configuration" -ForegroundColor Yellow
        Start-CineNetServiceInDebugFolder
    }
}

function Start-CineNetServiceInReleaseFolder {
    [CmdletBinding()]
    param ()
    $gitIgnore = FindFileInDirectoryOrGoUp -currentDirectory $(Get-Item $PWD) -pattern ".gitIgnore"
	
    if ($gitIgnore) {
		
        $nameOfService = $gitIgnore.Directory.Name
		
        $pathToServiceDirectory = Join-Path $gitIgnore.Directory.FullName "$nameOfService\bin\Release\net471"
		
        if (-not (Test-Path $pathToServiceDirectory)) {
            $pathToServiceDirectory = Join-Path $gitIgnore.Directory.FullName "$nameOfService\bin\Release"
        }
		
        Write-Host "Path to Service Directory $pathToServiceDirectory" -ForegroundColor Cyan`
		
        Write-Verbose $($gitIgnore.Directory.FullName)
        Write-Verbose "Running CineNet.OneService in: $pathToService"
		
        try {
            Push-Location $pathToServiceDirectory
            Copy-Item ".\$nameOfService.dll.config" ".\CineNet.OneService.exe.config" -Force
            Start-Process -FilePath ".\CineNet.OneService.exe" -NoNewWindow -Wait -ErrorAction Continue
        }
        finally {
            Pop-Location
        }
    }
}

function br() {
    build
	
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Build succeeded" -ForegroundColor Green
        runService
    }
    else {
        Write-Host "Build Failed not running service" -ForegroundColor Red
    }
}

function killTheProcess() {
    [CmdletBinding(SupportsShouldProcess = $true)]
    param (
        [Parameter(Mandatory = $True)][string]$ProcessToStop
    )
    Write-Host "Stopping $ProcessToStop" -ForegroundColor Yellow
	
    Stop-Process -Name "$ProcessToStop" -ErrorAction SilentlyContinue
	
    Write-Host "Done stopping $ProcessToStop" -ForegroundColor Green
}


function resetToMaster() {
    git fetch
    git checkout -B master
    git reset --hard origin/master
}

function deleteNonRemoteBranches() {
    git branch --merged | ForEach-Object { $_.trim() } | Where-Object { $_ -notmatch 'master' } | ForEach-Object { git branch -d $_ }
    pp
}

function cleanUpGitBranches() {
    resetToMaster
    pull
    deleteNonRemoteBranches
}

Set-Alias cgb cleanUpGitBranches

function pp {
    pull
    push
}

function pull {
    git pull
}

function push {
    git push
}

function cloneRepo {
    param (
        [Parameter(Mandatory = $True)][string]$GitRepo,
        [string]$Folder,
        [switch]$DoNotMoveToNewLocation
    )
	
    if ($Folder) {
        New-GitClone -GitRepo $GitRepo -Folder $Folder -DoNotMoveToNewLocation:$DoNotMoveToNewLocation
    }
    else {
        New-GitClone -GitRepo $GitRepo -DoNotMoveToNewLocation:$DoNotMoveToNewLocation
    }
}

function updateNuget {
    param (
        [switch]$DoNotCommit,
        [string]$verbosity = "normal"
    )
	
    Start-DotNetUpdatePackages

    Write-Host "Running Build" -ForegroundColor Cyan
    build -verbosity $verbosity

    if ($LASTEXITCODE -ne 0) {
        Write-Host "Build failed with exit code $LASTEXITCODE, not committing changes" -ForegroundColor Yellow
    }
	
    elseif (!$DoNotCommit) {
        gitc "Updated Packages"
    }
}

function wiki() {
    Start-Process https://github.com/Cinemassive/DevOps/wiki
}

function test() {
    param (
        [ValidateSet("quiet", "minimal", "detailed", "normal")]$verbosity = "normal",
        [switch]$runBuild,
        [string]$name
    )
        
    Clear-Host
    
    if ($runBuild) {
        Write-Host "Running Build" -ForegroundColor Cyan
        build -verbosity $verbosity
    }
    else {
        $LASTEXITCODE = 0
    }
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Build failed with exit code $LASTEXITCODE, not running tests" -ForegroundColor Yellow
    }
    else {
        
        if ($name) {
            
            Write-Host "Build succeeded, running test with name filter $name" -ForegroundColor Yellow
            
            Start-NUnitTestsFromDebugFolder -where "test=~$name"
        }
        else {
            Write-Host "Build succeeded, running tests" -ForegroundColor Cyan

            Start-NUnitTestsFromDebugFolder
        }
    }
}

function bt {
    param (
        [string]$testName
    )
	
    test -runBuild -name $testName -verbosity "detailed"
}

function btWeb {
    repo 'web'
    ng build --prod
    ng test
}

function resetColor () {
    [CmdletBinding()]
    param()
    
    Write-Verbose ($PSBoundParameters | Out-String)

    [Console]::ResetColor()
}

function Get-CineNetVersion () {
    [CmdletBinding()]
    param()
    
    Write-Verbose ($PSBoundParameters | Out-String)

    Push-Location $env:CineNetInstallRoot

    Get-ChildItem **\version.cm | ForEach-Object { $_.FullName; Get-Content $_ }

    Pop-Location
}

function Add-FolderToPath {
    param(
        [Parameter(Mandatory = $true)]
        [string] $Path,

        [ValidateSet('Machine', 'User', 'Session')]
        [string] $Container = 'Session'
    )

    if ($Container -ne 'Session') {
        $containerMapping = @{
            Machine = [EnvironmentVariableTarget]::Machine
            User    = [EnvironmentVariableTarget]::User
        }
        $containerType = $containerMapping[$Container]

        $persistedPaths = [Environment]::GetEnvironmentVariable('Path', $containerType) -split ';'
        if ($persistedPaths -notcontains $Path) {
            $persistedPaths = $persistedPaths + $Path | Where-Object { $_ }
            [Environment]::SetEnvironmentVariable('Path', $persistedPaths -join ';', $containerType)
        }
    }

    $envPaths = $env:Path -split ';'
    if ($envPaths -notcontains $Path) {
        $envPaths = $envPaths + $Path | Where-Object { $_ }
        $env:Path = $envPaths -join ';'
    }
}

function Update-Latest {
    [CmdletBinding()]
    param()
    Install-Latest -SkipDataReset -SkipDisplaySetUp -SkipAdminUser -UseFakeWall
}

function Display-Setup {
    # We need to wait for the wall to actually start up
    Start-Sleep -Seconds 5

    Write-Host "Auto Creating display" -ForegroundColor Cyan

    New-CmDisplayForEachCanvas -Server "Anna-Dev"

    Write-Host "Done creating displays"

    $room = New-CmRoom -Name "Auto Generated Room"

    $roomId = $room.RoomId

    $displays = Get-CmDisplay

    $displays | ForEach-Object {
        $displayId = $_.DisplayId

        New-CmAddDisplayToRoom -RoomId $roomId -DisplayId $displayId
    }

    New-CmActivity -RoomId $roomId -ActivityName "Auto Generated Activity"
}

$env:AlphaSettingsJson = @"
{
  "CanvasSettings":  [
            {
              "HorizontalPanels":2,
              "VerticalPanels":2,
              "Bounds":"0, 0, 1920, 1200"
          }
          ],
  "DefaultVericalPanels":  2,
  "OutputType":  "AlphaFx",
  "DefaultHorizontalPanels":  2
}
"@

Set-Alias ll Set-LogLevel

function gcd {
    param (
        [string]$commandName
    )

    (Get-Command $commandName).Definition
}

function buildCommon {
    Set-Location (Join-Path $env:CineMassiveCodeRoot "CineNet.Web.Common")
    ng build @cinemassive/common --prod
}

function pubCommon {
    Run-NpmPublishCommon
}

function testCommon {
    Set-Location (Join-Path $env:CineMassiveCodeRoot "CineNet.Web.Common")
    ng test @cinemassive/common --watch=false
}

function btCommon {
    Clear-Host

    Write-Host "Running Build" -ForegroundColor Green
    buildCommon
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Build failed with exit code $LASTEXITCODE, not running tests" -ForegroundColor Yellow
    }
    else {
        Write-Host "Build succeeded, running tests" -ForegroundColor Green

        testCommon
    }
}

function updateWebCommon {
    repo 'web'

    npm install @cinemassive/common
}

function runIT {
    param (
        [Switch]$local = $False,
        [string]$Branch
    )
    pullAllRepos

    . $PROFILE

    refreshenv

    Clean-CineNetInstall

    refreshenv

    if ($local) {
        Test-CineNetIntergration -RunInstallLocaly -PackageLocally -Branch $Branch
    }
    else {
        Test-CineNetIntergration   
    }
}

function Install-CineNet34 {
    Clean-CineNetInstall

    refreshenv

	Push-Location "C:\CineBuilds\OlderInstalls"

	.\CineNetInstaller_3.4.2491.89 install all
}

function Fresh-Install {
    Clean-CineNetInstall

    Clean-CineNetInstall

    refreshenv

    pullAllRepos

    . $PROFILE

    Install-Latest -EnableSSL -CreateInstallData
}

function Stream-VLC {
    param (
        [string]$url
    )

    Start-Process vlc.exe $url
}

function Dev-Cam {
    param (
        [string]$camNumber
    )

    switch ($camNumber) {
    "1" {Stream-VLC rtsp://root:cinemassive@10.111.13.59/axis-media/media.amp}
    "2" {Stream-VLC rtsp://root:cinemassive@10.111.13.239/axis-media/media.amp}
    }
}

function StopRemoteCineNetProcess() {
    [CmdletBinding(SupportsShouldProcess = $true)]
    param (
        [string]$ProcessName,
        [string]$Server,
        [string]$Username = "localadmin",
        [string]$Password = "cinemassive"
    )

    if ($Password) {
        net.exe use \\$Server  /user:$Username $Password    
    }
    else {
        net.exe use \\$Server  /user:$Username   
    } 
    
    if ($ProcessName) {
        
        Write-Host "Looking to stop Process $ProcessName" -ForegroundColor DarkYellow

     
            $foundProcess = Get-Process -ComputerName $Server -Name $ProcessName -ErrorAction SilentlyContinue
        
            if ($null -ne $foundProcess) {
                Write-Host "Stopping Process => $ProcessName"
                
                Stop-Process $foundProcess -Force
                
                Write-Host "Waiting for $ProcessName to exit"
                
                Wait-Process -Name $ProcessName -Timeout 20 -ErrorAction Ignore
                
                Write-Host "Waiting" -ForegroundColor Yellow
                
                Start-Sleep -Seconds 7
            }
            else {
                Write-Host "Couldn't find a process named $ProcessName on the server $Server!" -ForegroundColor DarkYellow
                $foundProcesses = Get-Process -ComputerName $Server
                Write-Host "Found these processes: $foundProcesses" -ForegroundColor Green
            }
        
    }
}

function StartRemoteCineNetProcess() {
    [CmdletBinding(SupportsShouldProcess = $true)]
    param (
        [string]$ProcessName,
        [string]$Server
    )
    
    if ($ProcessName) {
        
        Write-Host "Looking to stop Process $ProcessName" -ForegroundColor DarkYellow
        
        $foundProcess = Get-Process -ComputerName $Server -Name $ProcessName -ErrorAction SilentlyContinue
        
        if ($null -ne $foundProcess) {
            Write-Host "Stopping Process => $ProcessName"
            
            Stop-Process $foundProcess -Force
            
            Write-Host "Waiting for $ProcessName to exit"
            
            Wait-Process -Name $ProcessName -Timeout 20 -ErrorAction Ignore
            
            Write-Host "Waiting" -ForegroundColor Yellow
            
            Start-Sleep -Seconds 7
        }
    }
}

function CopyWallCodeToRemoteAlpha {
    CopyToServer -ServerFolder "C$\cinemassive\AlphaControl\CineNetWall" -LocalFolder "C:\Code\CineNet.AlphaControl\CineNet.Wall\bin\Release"
}

function CopyToServer {
    param (
        [string]$Server = "Cassini",
        [string]$ServerFolder,
        [string]$LocalFolder,
        [string]$Username = "localadmin",
        [string]$Password = "cinemassive",
        [switch]$SkipLogin
    )

    if (-not $SkipLogin) {
        if ($Password) {
            net.exe use \\$Server  /user:$Username $Password    
        }
        else {
            net.exe use \\$Server  /user:$Username   
        }    
    }

    if (-not $LocalFolder) {
        Write-Host "Did not provide local folder" -ForegroundColor Cyan

        $LocalFolder = Get-Location

        Write-Host "Going to copy '$LocalFolder'" -ForegroundColor Cyan
    }

    if (-not $ServerFolder) {
        Write-Host "Did not provide server folder" -ForegroundColor Magenta

        $serverEndingPath = Split-Path $LocalFolder -Leaf

        $ServerFolder = "Testing\$serverEndingPath"

        Write-Host "Going to copy '$ServerFolder'" -ForegroundColor Magenta
    }

    $destination = "\\$server\$ServerFolder"

    Write-Host "Copying '$LocalFolder' to '$destination'" -ForegroundColor Yellow
	
    Robocopy.exe /S $LocalFolder $destination  /R:0 /XD Logs /IS /IT

    Write-Host "Done copying '$LocalFolder' to '$destination'" -ForegroundColor Green
}

function split {

    $ConEmuConsoleCount = getConEmuConsoleCount

    if (1 -eq $ConEmuConsoleCount ) {

        powershell.exe -new_console:sH

        Start-Sleep -Seconds 5
        
        powershell.exe -new_console:sV
    }
}

function getConEmuConsoleCount() {
    @(get-process -ea silentlycontinue "ConEmuC64").count
}

function killProcessOnPort() {
    param (
        [string]$Port
    )
    Stop-Process -Id (Get-NetTCPConnection -LocalPort $Port).OwningProcess -Force
}

split