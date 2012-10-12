// MCMyVault v1.4.1.1
// by JPSIII
// Copyright 2012. All Rights Reserved
// YOU CAN DOWNLOAD mcmapDZ here: 
// http://www.minecraftforum.net/topic/253696-mcmapdz-048-%C2%B7-simple-lightning-fast-interactive-web-maps/
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
the third party apps like mcmapDZ & eihort in your folder:
C:\Users\<USERAME>\Documents\MCMyVault
You will then be prompted for the locations; Remember to choose 32bit or
64bit depending upon your system. If you don't know what type of system you
have, in general if you receive errors when trying to run them, try the 
other.
"Show Hidden Files" setting must be enabled on your Windows computer
before running.

The host PC It needs the .NET 4.0 Runtime
http://www.microsoft.com/en-us/download/details.aspx?id=17718

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
mcmapDZ_renders: <fullpath to mcmapDZ\renders folder>
backup_loc:<fullpath to whereyou want the backups stored>
eihort:<fullpath toeihort.exe>

[myapps]
myappname = <FULLFILENAMEPATH>

****************************
***    Backup Files      ***
****************************
The backup file must have the following format:
<WORLDNAME>_<anythingelse>.zip

WHEN RESTORING, if a .minecraft\saved folder exists with the first part of the backup name
the folder will be deleted, and be replaced with the backup content.

****************************
***    Testing Notes     ***
****************************
Tested with Minecraft 1.3.2
Tested on Windows 7 64bit
Tested on Windows XP 32bit

****************************
***  Troubleshooting     ***
****************************
ERROR: "Eihort Fatal Error: Failed to locate terrain/moon_phases.png"
RESOLUTION:
You may not have run Minecraft.exe yet on this computer; close MCMyVault and run Minecraft.exe and login; select Yes to download updates, then exit and run MCMyVault again
***
