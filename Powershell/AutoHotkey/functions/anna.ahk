#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;START Function Definitions;;;;;;;;;;;;;;;;;
#Include C:\Code\DevOps\AutoHotkey\common\LCFCommonLib\LCFCommonLib.ahk

test()
{
	;scriptFullPath := "C:\Code\DevOps\Powershell\Dev\Custom\loren.ps1"
	powershellCommand := "buildCommon"
	;commandSeparator := ";"
	;pauseForInput := "Write-Host -NoNewLine 'Press any key to continue...'; $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');"
	
	MsgBox, going to run powershell command "%powershellCommand%"
	
	;Run *RunAs powershell.exe -Executionpolicy Bypass .$PROFILE %commandSeparator% powershell.exe -Executionpolicy Bypass -command "& {%powershellCommand%}"
	
	;Run powershell.exe Write-Host -NoNewLine 'Press any key to continue...'; $null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');
	
	;Run powershell.exe -Executionpolicy Bypass -command "& {testCallFromAutoHotkey}"
	
	LCF_CL$ExecutePowerShellCommand(powershellCommand)
}

;thiis a test to see what I can get from the current windows
testLoop()
{
	Global G_b_runTest
	sleepTime := 250
	mouseSpeed := 15

	Loop
	{
		;Click SAVE AND CONTINUE to go to SETUP
		MouseClick, left, 2337, 1911
		Sleep, %sleepTime%

		;Click ADD/EDIT
		MouseClick, left, 1330, 608
		Sleep, %sleepTime%

		;Click Save AND CONTINUE to go to SETUP INDIVIDUAL DISPLAYS
		MouseClick, left, 2476, 671
		Sleep, %sleepTime%

		;Resize Display to 2x2 less
		MouseClick, left, 1453, 1134
		MouseClick, left, 2184, 1134, 1, 0, D
		MouseClick, left, 1453, 1134, 1, %mouseSpeed%, U

		;Click SAVE AND CONTINUE to go to SETUP
		MouseClick, left, 2337, 1911
		Sleep, %sleepTime%

		;Click ADD/EDIT
		MouseClick, left, 1330, 608
		Sleep, %sleepTime%

		;Click Save AND CONTINUE to go to SETUP INDIVIDUAL DISPLAYS
		MouseClick, left, 2476, 671
		Sleep, %sleepTime%

		;Resize Display to 2x2 more
		MouseClick, left, 1453, 1134
		MouseClick, left, 1661, 1134, 1, 0, D
		MouseClick, left, 2034, 1134, 1, %mouseSpeed%, U


		if (!G_b_runTest)
		{
			MsgBox, Loop stopped at Loop# %a_index%
			G_b_runTest := true
			break
		}
		else
		{
			Sleep, %sleepTime%
		}
	}
}

WaitForWindowWithTitle(title, timeoutSecs)
{
	Loop, %timeoutSecs%
	{
		WinGet, this_id, ID, A
		WinGetTitle, this_title, ahk_id %this_id%

		if( InStr(this_title, title) )
		{
			break
		}

		Sleep, 1000
	}
}

StartZoom(meetingId)
{
	if FileExist("C:\Program Files (x86)\Zoom\bin\Zoom.exe")
		Run "C:\Program Files (x86)\Zoom\bin\Zoom.exe" "--url=https://zoom.us/j/%meetingId%"
}

StartZoomHolodeck()
{
	StartZoom("5285149116")
}

StartZoomFarpoint()
{
	StartZoom("4465322665")
}

ReloadThisScript()
{
	Reload
	Sleep 1000 ; If successful, the reload will close this instance during the Sleep, so the line below will never be reached.
	MsgBox, 4,, The script could not be reloaded. Would you like to open it for editing?
	IfMsgBox, Yes, Edit
}

EditScriptWithVsCode()
{

	processName := "Code.exe"
	ISstartIfNotOpen := true
	pathToProcess := "C:\Program Files\Microsoft VS Code\"
	exeParameters := A_ScriptFullPath
	
	
	return BringWindowToTheFront(processName, ISstartIfNotOpen, pathToProcess, , exeParameters)
}

EditScriptWithNotpadPP()
{

	processName := "notepad++.exe"
	ISstartIfNotOpen := true
	exeParameters := "-lautoit " . A_ScriptFullPath
	
	
	return BringWindowToTheFront(processName, ISstartIfNotOpen, , , exeParameters)
}

BringWindowToTheFront(processName, ISstartIfNotOpen:=false, pathToProcess:="", ISGetReverseWindowOrder:=false, exeParameters:="")
{
	ISactivatedWindow := false
	ISalreadyFrontWindow := false
	ahkId :=

	WinGet, thisAhkId, ID, A
	WinGet, id, list,,,Program Manager

	if(ISGetReverseWindowOrder = true)
	{
		reverseOrderLoopNumber := Floor(id/2)
		currentFrontIndex := 1
		currentBackIndex := id
		
		Loop, %reverseOrderLoopNumber%
		{
			tempID := id%currentFrontIndex%
			id%currentFrontIndex% := id%currentBackIndex%
			id%currentBackIndex% := tempID
			
			currentFrontIndex++
			currentBackIndex--
		}
	}
	
	Loop, %id%
	{
		currentId := id%A_Index%
		WinGetTitle, currentTitle, ahk_id %currentId%
		WinGet, current_pname, ProcessName, ahk_id %currentId%
		
		if((currentId == thisAhkId) && (current_pname == processName))
		{
			ISalreadyFrontWindow := true
			ahkId := currentId
		}
		else if((currentId <> thisAhkId) && (currentTitle <> ""))
		{			
			if(current_pname = processName)
			{
				ahkId := currentId

				WinGet, currentIsMax, MinMax, ahk_id %currentId%
				
				if(currentIsMax <> -1)
				{
					WinRestore, ahk_id %currentId%
				}

				WinActivate, ahk_id %currentId%
				ISactivatedWindow := true
			}
		}
	}
	
	if((ISstartIfNotOpen == true) && (ISactivatedWindow == false) && (ISalreadyFrontWindow == false))
	{
		Run, *RunAs %pathToProcess%%processName% %exeParameters%, , , processId

		SetTitleMatchMode, RegEx
		processNamePart := RTrim(processName, ".ex")
		regExSearchString := "i)" . processNamePart . "."

		Loop, 1000
		{
			WinGet, ahkId, ID, ahk_pid %processId%

			if ( ahkId <> "")
			{
				break
			}
			
			WinGet, ahkId, ID, %regExSearchString%

			if ( ahkId <> "")
			{
				break
			}

			sleep, 100
		}
	}

	return ahkId
}

IfZoomIsRunningCloseItIfNotReturnTrue()
{
	returnVal := false
	
	processName := "Zoom.exe"
	
	Process, Exist, %processName%
	if(ErrorLevel == 0)
	{
		returnVal := true
	}
	else
	{
		CloseProcess(processName)
	}
	
	return returnVal
}

CloseProcess(processName)
{
	Process, Close, %processName%
	sleep, 100
	Loop, 10
	{
		Process, Exist, %processName%
		if(ErrorLevel != 0)
		{
			Process, Close, %processName%
		}
		else
		{
			;MsgBox, Process %processName% Doesn't Exist
			break
		}
	}
}

EditPowerShellWithVsCode()
{

	processName := "Code.exe"
	ISstartIfNotOpen := true
	pathToProcess := "C:\Program Files\Microsoft VS Code\"
	exeParameters := "C:\Code\DevOps\Powershell\Dev\Custom\anna.ps1"
	
	
	return BringWindowToTheFront(processName, ISstartIfNotOpen, pathToProcess, , exeParameters)
}

BringVsCodeToFront()
{
	processName := "Code.exe"
	ISstartIfNotOpen := true
	pathToProcess := "C:\Program Files\Microsoft VS Code\"
	
	return BringWindowToTheFront(processName, ISstartIfNotOpen, pathToProcess)
}

BringBrowserToTheFront()
{
	processName := "chrome.exe"
	ISstartIfNotOpen := true
	
	return BringWindowToTheFront(processName, ISstartIfNotOpen)
}

BringCodeWriterToTheFront()
{
	;processName := "devenv.exe" ;VS
	processName := "rider64.exe"
	ISstartIfNotOpen := true
	pathToProcess := ""
	ISGetReverseWindowOrder := false
	
	return BringWindowToTheFront(processName, ISstartIfNotOpen, pathToProcess, ISGetReverseWindowOrder)
}

BringConEmuToTheFront()
{
	processName := "ConEmu64.exe"
	ISstartIfNotOpen := true
	
	return BringWindowToTheFront(processName, ISstartIfNotOpen)
}

BringNotepadPPToTheFront()
{
	processName := "notepad++.exe"
	ISstartIfNotOpen := true
	
	return BringWindowToTheFront(processName, ISstartIfNotOpen)
}

BringTeamsToTheFront()
{
	processName := "Teams.exe"
	ISstartIfNotOpen := true
	pathToProcess := "C:\Users\anna.pages\AppData\Local\Microsoft\Teams\current\"
	
	return BringWindowToTheFront(processName, ISstartIfNotOpen, pathToProcess)
}

BringWebStormToTheFront()
{
	processName := "webstorm64.exe"
	ISstartIfNotOpen := true
	pathToProcess := "C:\Users\loren\AppData\Local\JetBrains\Toolbox\apps\WebStorm\ch-0\183.4284.130\bin\"
	
	return BringWindowToTheFront(processName, ISstartIfNotOpen, pathToProcess)
}

RestartService(serviceName)
{
	RunWait,sc stop %serviceName%
	if (ErrorLevel = 0)
	{
		MsgBox Success!
	}
	else
	{
		MsgBox could not stop %serviceName%. Errorlevel: %Errorlevel%
	}

	RunWait,sc start %serviceName%
	if (ErrorLevel = 0)
	{
		MsgBox Success!
	}
	else
	{
		MsgBox could not start %serviceName%. Errorlevel: %Errorlevel%
	}
}

OpenLcfCommonLibrary()
{	
	pathToLcfCommonLibrary := "C:\Code\DevOps\AutoHotkey\common\LCFCommonLib\LCFCommonLib.ahk"

	LCF_CL$TempSaveClipboardAll()   ; Save the entire clipboard

	if(LCF_CL$CopyHilightedToClipboard(0.5))
	{
		;MsgBox, Nothing was Hilighted
		Run, notepad++.exe -lautoit C:\Code\DevOps\AutoHotkey\common\LCFCommonLib\LCFCommonLib.ahk
	}
	else
	{
		Run, notepad++.exe -lautoit C:\Code\DevOps\AutoHotkey\common\LCFCommonLib\LCFCommonLib.ahk

		Sleep, 500
		
		SendInput, ^{END}
		SendInput, ^f
		Sleep, 100
		LCF_CL$SendCtrlvWithTime()	;Paste
		SendInput, {TAB}
		Sleep, 100
		SendInput, {TAB}
		Sleep, 100
		SendInput, {LEFT}
		SendInput, {ENTER}
	}

	LCF_CL$RestoreClipboardAll()   ; Restore the original clipboard.
}
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;END   Function Definitions;;;;;;;;;;;;;;;;;

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;END   SCRIPT;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
