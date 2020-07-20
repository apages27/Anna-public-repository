#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

#Include, C:\Code\DevOps\AutoHotkey\Dev\Custom\anna\functions\anna.ahk

ahkId := BringTeamsToTheFront()

x := 0
y := 1200
width := 1920
height := 1200

WinMove, ahk_id %ahkId%,, x, y, width, height

