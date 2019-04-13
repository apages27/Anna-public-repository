if (-not (Get-Command "choco.exe")) {
	throw "You need to install chocolatey from chocolatey.org"
}

$chocoPackagesToInstall = @(
    'notepadplusplus',
    'telnet',
    'python2',
    'OpenSSL.Light',
    '7zip',
    'baretail',
    'conemu',
    'ditto', 
    'git', 
    'gitextensions', 
    'poshgit',
    'netfx-4.7.1-devpack',
    'nuget.commandline',
    'tightvnc',
    'netfx-4.6.2-devpack',
    'webstorm',
    'zoom'
	'ffmpeg',
    'nodejs', 
    'sudo',
    'kdiff3',
    'lockhunter',
    'dotnetcore',
    'dotnetcore-sdk',
    'visualstudio2017-workload-netweb', 
    'visualstudio2017professional', 
    'microsoft-build-tools',
    'nodejs.install',
    'nunit',
    'putty',
    'libreoffice-fresh',
    'vlc',
    'adobereader',
    'openssh',
    'studio3t',
    'curl',
	'KB2999226',
	'KB2919355',
	'KB2919442',
	'fiddler',
	'teamviewer',
	'resharper'
)

Write-Host "Installing Packages: $chocoPackagesToInstall" -ForegroundColor Green
Write-Host "If there are any packages you don't want after this, just do 'choco uninstall <packageName>'"

foreach($package in $chocoPackagesToInstall) {

    choco install $package -y

}

Set-Location $env:CinemassiveCodeRoot

[Net.ServicePointManager]::SecurityProtocol = "tls12, tls11, tls"

$reposToClone = Invoke-RestMethod "https://api.github.com/orgs/cinemassive/repos?type=private" -Headers @{Authorization="token 87a5310762e02a51af27c7ea9415b4c5a0300136"}
foreach($repo in $reposToClone)
{
  if ($repo.name.StartsWith("CineNet") -or $repo.name -eq "DevOps")
  { 
    Write-Host "Cloning: $($repo.name)" -ForegroundColor Green
    git clone "https://github.com/Cinemassive/$($repo.name).git"
  }     
}

Enable-WindowsOptionalFeature -FeatureName IIS-WebServerRole