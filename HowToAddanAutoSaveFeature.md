# Introduction #

Decision has been made not to add an auto-save feature built into MCMyVault mainly because there are so many good programs out there that perform Minecraft auto-saves. So we'll just go with a recommended auto-save program that works well with MCMyVault; NOTE: MCSave doesn't restore world blocks, only player data/inventory.

Recommended auto-save program: MCSave
http://www.minecraftforum.net/topic/1281193-mcsave-powerful-lightweight-multiple-save-utility/

# HOW TO SETUP #

  1. Download MCSave from the link above and store at your favorite Minecraft tools folder.
  1. Open MCMyvault and add MCSave as a favorite program in the MyApps tab.
  1. Click on the MCSave to execute and you will need to set it up for your current Minecraft environment. Here is an example of a Technicpack.net 'Attack of the B-Team' environment below.
  1. MCSave will start auto saving if you configured MCSave properly.
![http://farm4.staticflickr.com/3685/13311756573_d18edb18e0.jpg](http://farm4.staticflickr.com/3685/13311756573_d18edb18e0.jpg)

# HOW IT WORKS #

MCSave creates its files in (your minecraft path)\saves\WORLDNAME\MCSave
This folder is included in MCMyVault backups and snapshots.
Use MCSave to revert to your last auto-save, which should be a newer restore than any of your MCMyVault backups. Then you can make a MCMyVault backup after restoring from a MCSave. It is recommended to use snapshots in the beginning of your first use with MCSave just as a precautionary measure.
NOTE: MCSave doesn't restore world blocks, only player data.

# HOW TO RESTORE WORLD BLOCKS AND PLAYER DATA USING MCSave and MCMyVault #
So you just died and your nice house is blown up by creepers.
  1. Close MCSave if you have it open.
  1. Manually copy your "(your minecraft path)\saves\WORLDNAME\MCSave" folder to your desktop
  1. Using MCMyVault, restore your last backup (this will get your world back to that point in time of your backup).
  1. Manually copy and paste the desktop "MCSave" folder to overwrite the "(your minecraft path)\saves\WORLDNAME\MCSave" folder.
  1. Using MCSave, revert back to the time just before you died.

This restores your world blocks from the last MCMyVault backup, and then your inventory and location with MCSave.

Please let me know if another auto-backup program is better suited to be recommended with MCMyVault. The requirements are:
  * Must work with Technicpack.net modpacks
  * System tray while running is preferred
  * Storing the auto-save info in the world subfolders is preferred