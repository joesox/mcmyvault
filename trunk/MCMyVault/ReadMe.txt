// MCMyVault v1.7.4.1
// by JPSIII
// Copyright 2012-2014. All Rights Reserved
// YOU CAN DOWNLOAD mcmap2 here:
// http://wrim.pl/mcmap/
// YOU CAN DOWNLOAD eihort here: 
// https://bitbucket.org/lloigor/eihort/
///////////////////////////////////////////////////////////////////////////
//LICENSE 
//BY DOWNLOADING AND USING, YOU AGREE TO THE FOLLOWING TERMS: 
//If it is your intent to use this software for non-commercial purposes,  
//such as in academic research, this software is free and is covered under  
//the GNU GPL License, given here: <http://www.gnu.org/licenses/gpl.txt>  
//You agree with 3RDPARTY's Terms Of Service 
//found with their software. 
// 'Minecraft Icon Version III.ico' for personal use only from
// http://www.softicons.com/free-icons/game-icons/minecraft-icons-by-paul-schulerr/minecraft-iii-icon
///////////////////////////////////////////////////////////////////////////

****************************
***    Quick Start       ***
****************************
On first time use, let the config.ini be created. The installer will place
the third party apps like mcmap2 & eihort in your folder:
C:\Users\<USERAME>\Documents\MCMyVault
You will then be prompted for the locations; Remember to choose 32bit or
64bit depending upon your system. If you don't know what type of system you
have, in general if you receive errors when trying to run them, try the 
other.

The host PC It needs the .NET 4.0 Runtime
http://www.microsoft.com/en-us/download/details.aspx?id=17718

Starting with v1.5.1.9, Windows non-administrator & administrator accounts, 
MCMyVault default install path is
c:\Users\<USERNAME>\AppData\Local\MCMyVault
[NOTE: v1.6.1.x+ Now stores all settings files in this local so multiple installations 
to configure for any additional launchers like Tekkit etc. are no longer needed.]
Non-administrators can install v1.5.1.9+
****************************
***     config.ini       ***
****************************
If no config.ini is created, MCMyVault will create a default generic 
config.ini. 
*** SECTIONS AND KEYS ***
[settings]
minecraft: <fullpath to minecraft.exe>
minecraft_server: <fullpath to Minecraft_Server.exe>
minecraft_saved: Will be auto detected; typically 'C:\Users\<USERAME>\AppData\Roaming\.minecraft\saves'
mcmapDZ_renders: <fullpath to mcmap2 folder>
backup_loc:<fullpath to where you want the backups stored>
eihort:<fullpath toeihort.exe>
backup_loc_textpacks=typically 'C:\Users\<USERAME>\Documents\MCMyVault\backups-texturepacks'
minecraft_textpacks=typically 'C:\Users\<USERAME>\AppData\Roaming\.minecraft\texturepacks'
snapshots_loc:<fullpath to where you want the snapshots stored>
cloud_backup_loc: example: C:\Users\<USERAME>\Documents\Dropbox\mc_backups
lastbackup_col_width: MCMyVault will handle
name_col_width: MCMyVault will handle
filename_col_width: MCMyVault will handle
map_col_width: MCMyVault will handle
debug: true  <set to 'true' for additional log entries>
cleanupdays: <number of days to keep the oldest backup for each day in that period and send the rest to recyclebin>

[myapps]
myappname = <FULLFILENAMEPATH>

NOTE: You can add Windows folder locations or internet URLs here by 
creating a shortcut, then pointing to the shortcut itself. For Example:
minecraft_tools= C:\Users\<USERAME>\Documents\minecraft tools\minecraft tools.lnk
RedstoneSimulator=C:\Users\<USERAME>\Documents\minecraft tools\RedstoneSimulator.url

***********************************
***     startupconfig.ini       ***
***********************************
currentconfig: Tell MCMyVault what config to load at startup. Each profile will have its own ini file settings.
The default is 'config.ini'

****************************
***    Backup Files      ***
****************************
The backup file must have the following format:
<WORLDNAME>_<anythingelse>.zip

WHEN RESTORING, if a .minecraft\saved folder exists with the first part of the backup name
the folder will be deleted, and be replaced with the backup content.

New in version 1.7.0.0+, after an extraction of a backup zip, it will make sure the 
folders are in the correct location in the .minecraft\saved\<mapname>\ folder.

****************************
***    Testing Notes     ***
****************************
Tested with Minecraft 1.7.4
Tested with Minecraft 1.6.4
Tested on Windows 7 64bit
Tested on Windows 8.1 Pro N

Tested 'Send copy to cloud storage' feature with Dropbox and Google Drive
 
****************************
***  TROUBLESHOOTING     ***
****************************
ERROR: "Eihort Fatal Error: Failed to locate terrain/moon_phases.png"
RESOLUTION:
You may not have run Minecraft.exe yet on this computer; close MCMyVault and run Minecraft.exe and login; select Yes to download updates, then exit and run MCMyVault again
***
ERROR: "Missing MSVCR100.dll" when running eihort.
RESOLUTION:
eihort requires Microsoft Visual C++ 2010 SP1 Redistributable Package so please download from Microsoft and install.
64bit: http://www.microsoft.com/en-us/download/details.aspx?id=5555
32bit: http://www.microsoft.com/en-us/download/details.aspx?id=8328
***
ERROR: "Path not found" 
RESOLUTION: 
Please check your Settings tab and validate each location path. Click Save button and File -> Restart, then try again.
***
"Show Hidden Files" setting may need to be enabled on your Windows 
computer before running.
***
