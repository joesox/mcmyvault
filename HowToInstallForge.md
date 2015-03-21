# HOW TO INSTALL MINECRAFT FORGE ON WINDOWS #

These instructions work for Minecraft version 1.5.1

**PREREQUISITE:** download and install http://www.7-zip.org/
this program is to open the minecraft.jar file to mod it

(If you have a new computer from 2010 or later you will probably want the 64bit installation
check your OS if 64 bit)

## FIRST STEP: ##

  1. Download https://dl.dropbox.com/u/60712778/minecraftforge-universal-1.5.1-7.7.0.600.zip
  1. Using Windows Explorer, right-click on minecraftforge-universal-1.5.1-7.7.0.600.zip and choose "7-Zip" then "Open Archive"
a new window should have opened showing some folders named "paulscode" etc, place that off to the side for now.

## FINAL STEP: ##

  1. Open a new Windows Explorer, and navigate to
```
C:\Users\<YOURWINDOWSUSERNAME>\AppData\Roaming\.minecraft\bin\
```
  1. Right-click on "minecraft" or "minecraft.jar" and choose "7-Zip" then "Open Archive"
a new window should have opened showing some folders named "achievement" etc, place that off to the other side so they are side-by-side.
  1. In "minecraft.jar", right-click, DELETE the "META-INF" folder **MUST BE DONE FIRST** (press ok to confirm)
  1. In "minecraftforge-universal-1.5.1-7.7.0.600.zip", on the menu click "Edit" -> "Select All"
  1. Left-click on all the highlighted files and drag them over to "minecraft.jar" (press Yes to confirm)
  1. Press File -> Exit on the open 7-Zip of both "minecraft.jar" and "minecraftforge-universal-1.5.1-7.7.0.600.zip"
  1. Start your minecraft.exe and sign-in
It is successfully installed if you see a "FML is setting up your minecraft" message while logging in.

# TROUBLESHOOTING: #
  * TIP1: If you get stuck in a "Done loading" screen, close Minecraft; delete the '\AppData\Roaming\.minecraft\bin\' folder and start minecraft.exe again.
  * TIP2: After seeing the "FML is setting up your minecraft", if it sits at a blank screen, delete all your previous mods and try again.