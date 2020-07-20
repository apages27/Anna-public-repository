function eh { sudo "C:\Program Files\Notepad++\notepad++.exe" "C:\Windows\System32\drivers\etc\hosts" -p };

function sn+ { Start-Process notepad++ };

function gitmerge {
    git pull origin master
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

function pullAllRepos {
    Clear-Host

    $currentLocation = Get-Location

    Step-Repo {
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

function openSolution() {

    $solutionLocation = Get-ChildItem -Filter *.sln | Select-Object -first 1

    Write-Host "Opening solution " $solutionLocation -ForegroundColor Blue

    Invoke-Item $solutionLocation.FullName
}

function code($repoName) {
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

function webUI {
    repo 'web'
	
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

function gcd {
    param (
        [string]$commandName
    )

    (Get-Command $commandName).Definition
}


function Stream-VLC {
    param (
        [string]$url
    )

    Start-Process vlc.exe $url
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