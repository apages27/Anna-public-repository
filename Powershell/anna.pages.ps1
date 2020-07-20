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
    $pathToTightVnc = Join-Path $env:ProgramFiles "TightVNC\tvnviewer.exe"
    Start-Process $pathToTightVnc $computer
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
        [string]$Server = "",
        [string]$ServerFolder,
        [string]$LocalFolder,
        [string]$Username = "",
        [string]$Password = "",
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

function Explore-Functions {
	$list = Get-ChildItem function:

	$searchList = @()

	foreach ($item in $list) {
		$moduleName = $item.ModuleName.PadRight(20, ' ')

		if ([string]::IsNullOrWhiteSpace($moduleName)) {
			continue
		}

		$searchString = "$($moduleName.PadRight(40, ' '))  $($item.Name)"

		Write-Host $searchString

		$searchList += $searchString
	}

	$result = $searchList | Invoke-Fzf

	$functionToCall = $result.Remove(0, 42)

	Write-Host "$functionToCall" -ForegroundColor Yellow
}

function Clean-Code {
	[CmdletBinding()]
	param(
		[switch]$ForPR = $false
	)

	if (-not (Get-Command "cleanupcode.exe")) {
		choco install resharper-clt
	}

	$solutionLocation = Get-ChildItem -Filter *.sln | Select-Object -first 1

	if (-not $solutionLocation) {
		Write-Host "No Solution Found"
		return;
	}

	$branch = git rev-parse --abbrev-ref HEAD
	$currentDir = ((Get-Item -Path ".\").Name)
	$touchedFiles = @()

	if ($ForPR) {
		Write-Verbose "Looking into unmerged files"
		$touchedFiles = (git diff --name-only $branch $(git merge-base $branch master) ) -split '\n'
	} else {
		Write-Verbose "Looking at modified and new files"
		$newFiles = (git ls-files -o --exclude-standard) -split '\n'
		$modifiedFiles = (git ls-files -m) -split '\n'
		$touchedFiles = $newFiles + $modifiedFiles
	}

	$touchedFiles = $touchedFiles | Where { [System.IO.File]::Exists(  [System.IO.Path]::Combine($env:CodeRoot, $currentDir, $_) )}

	if ($touchedFiles -gt 0) {
		$include = $touchedFiles -join ";"
		Write-Verbose "Files to clean: $include"
		cleanupcode --include=$include $solutionLocation.FullName
	} else {
		Write-Verbose "No files to clean."
	}
}

function Get-Branches() {
    [CmdletBinding(SupportsShouldProcess = $true)]
    param (
    )

    $branches = git branch -a

    $remoteOnlyBranches = @();
    $localBranches = @();

    $branches | ForEach-Object {
        $normalName = $_ -replace "remotes/origin/", ""

        if ($_ -like "*remotes/origin/*") {
            $remoteOnlyBranches += $normalName;
        }
        else {
            $localBranches += $normalName;
        }
    }

    Write-Host "----------------- Local Branches -------------------------" -ForegroundColor Magenta

    $localBranches | ForEach-Object {
        Write-Host "      $_" -ForegroundColor Magenta
    }

    Write-Host ""
    Write-Host "----------------- Remote Branches -------------------------" -ForegroundColor DarkCyan

    $remoteOnlyBranches | ForEach-Object {
        Write-Host "      $_" -ForegroundColor DarkCyan
    }
}

function Create-PR {
    param($repositoryId, $fromBranch, $title, $toBranch = "master")
    $mutation = @"
    mutation {
        createPullRequest( input: {repositoryId:"$repositoryId",baseRefName:"$toBranch", headRefName:"$fromBranch",title:"$title"}){
          clientMutationId,
          pullRequest{
            url,
            id
          }
        }
      }
"@
    return  InvokeGitHubGraphQL -query $mutation
}

function Request-PRReview {
    param($pullRequestId, $reviewer)
    $mutation = @"
    mutation {
        requestReviews(input: {pullRequestId:"$pullRequestId",userIds:["$reviewer"]}){
            pullRequest{
              url,
              id
            }
        }
    }
"@
    return  InvokeGitHubGraphQL -query $mutation
}

function Get-GitHubUser {
    param($login)
    $query = @"
    query{
      user(login: "$login"){
        id,
        url,
        avatarUrl,
        bio
      }
    }
"@
    return  InvokeGitHubGraphQL -query $query
}

function InvokeGitHubGraphQL {
    param($query)
    $jsonBody = @{query = $query} | ConvertTo-Json
    $res = Invoke-RestMethod -Headers @{Authorization = "bearer " + $env:GitHubPersonalAccessToken} -Uri "https://api.github.com/graphql" -Method Post -Body $jsonBody
    Write-Host $res.errors
    return $res.data
}

function New-PullRequest() {
    [CmdletBinding(SupportsShouldProcess = $true)]
    param (
        [Parameter(Mandatory = $True)][string]$title,
        [string]$reviewer = ""
    )

    $reviewerDetails = Get-GitHubUser -login $reviewer
    $branch = git rev-parse --abbrev-ref HEAD
    $currentRepo = Current-GitHubRepo

    $pr = Create-PR -repositoryId $currentRepo.id -fromBranch $branch -title $title -toBranch "master"
    $reviewRequest = Request-PRReview -pullRequestId $pr.createPullRequest.pullRequest.id -reviewer $reviewerDetails.user.id
    Write-Host $pr.createPullRequest.pullRequest.url
}

function Current-GitHubRepo() {
    $repoUrl = (git config --get remote.origin.url).Split('/')
    $repoOwner = $repoUrl[-2]
    $repoName = $repoUrl[-1].Replace(".git", "")
    $query = "query {repository(owner: ""$repoOwner"", name:""$repoName""){id,url}}"
    $res = InvokeGitHubGraphQL -query $query
    return $res.repository
}

function Switch-Branch() {
    [CmdletBinding()]
    Param(
        # Any other parameters can go here
    )
 
    DynamicParam {
        # Set the dynamic parameters' name
        $ParameterName = 'Name'

        # Create the dictionary
        $RuntimeParameterDictionary = New-Object System.Management.Automation.RuntimeDefinedParameterDictionary

        # Create the collection of attributes
        $AttributeCollection = New-Object System.Collections.ObjectModel.Collection[System.Attribute]

        # Create and set the parameters' attributes
        $ParameterAttribute = New-Object System.Management.Automation.ParameterAttribute
        $ParameterAttribute.Mandatory = $false
        $ParameterAttribute.Position = 1

        # Add the attributes to the attributes collection
        $AttributeCollection.Add($ParameterAttribute)

        # Generate and set the ValidateSet
        $arrSet = Get-AListOfBranches

        $ValidateSetAttribute = New-Object System.Management.Automation.ValidateSetAttribute($arrSet)

        # Add the ValidateSet to the attributes collection
        $AttributeCollection.Add($ValidateSetAttribute)

        # Create and return the dynamic parameter
        $RuntimeParameter = New-Object System.Management.Automation.RuntimeDefinedParameter($ParameterName, [string], $AttributeCollection)
        $RuntimeParameterDictionary.Add($ParameterName, $RuntimeParameter)

        return $RuntimeParameterDictionary
    }

    begin {
        # Bind the parameter to a friendly variable
        $Name = $PsBoundParameters[$ParameterName]
    }

    process {
        if (-not $Name) {
            $branches = Get-AListOfBranches

            $Name = $branches | Invoke-Fzf
        }

        git checkout $Name
        pp
    }
}

function Get-AListOfBranches {
    $finalList = @(
        "master"
    )

    $branches = git branch -a

    $branches | ForEach-Object {
        $normalName = $_ -replace "remotes/origin/", ""
        $normalName = $normalName -replace "\*", ""

        $normalName = $normalName.Trim()

        $isCurrentlyInList = Get-IsInList $finalList $normalName

        if (-not $isCurrentlyInList) {
            $finalList += $normalName
        }
    }

    $finalList
}

function Get-PersonalRepo {
Set-Location (Join-Path $env:CodeRoot "Anna-public-repository")
}

split