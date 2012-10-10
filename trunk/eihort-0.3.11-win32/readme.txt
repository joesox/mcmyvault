Eihort v0.3.11
by Lloigor

A long view-distance OpenGL Minecraft world viewer


 :: Disclaimer ::

This program is distributed as is without ANY warranty whatsoever.
This is unfinished, experimental software - there will be bugs.


 :: Installation ::

  Windows, Mac and Linux (tgz):
Unzip and run the exe/app.

  Linux (package):
Install the package with:
	sudo dpkg -i eihort_<version>.deb


 :: Running ::

  Windows and Mac:
Drag and drop the world folder onto eihort.exe (the file you downloaded,
not the running program).

  All platforms:
You can run Eihort from the command line with the path to the world folder
as the first argument. If your path contains spaces, make sure you put quotes
around it. E.g.:

eihort "C:\Users\???\AppData\Roaming\.minecraft\saves\World1"

If no path is given, Eihort will try to find some worlds to load and
let you choose between them. Eihort will search for worlds in
the Minecraft saves folder and in ./ . More paths can be added in
eihort.config.

Eihort will merge eihort.config files in $USERPROFILE (e.g. C:\Users\???\
on Windows 7), and $XSD_CONFIG_HOME or $HOME/.config with the main config
file with the Eihort executable. User-defined settings can be placed here
if you do not have easy access to the eihort.config with the Eihort
executable.

Enjoy!


 :: Experimental Feature ::

Starting with Eihort 0.3.7, the camera can be controlled with a crude
spline system. Default controls for this experimental feature are:

SPACE - Set a control point in the spline
1 - Play the spline forward (speed is affected by ctrl/shift)
2 - Play the spline backward (speed is affected by ctrl/shift)
3 - Jump to the end of the spline
4 - Jump to the start of the spline
` - Erase the last control point in the spline
0 - Erase all control points in the spline

Better controls will be included in a future version.


 :: Texture Packs ::

You can load your favourite texture pack by pointing the texture_pack
option in eihort.config at the zip or folder containing the pack.


 :: Biomes ::

Minecraft worlds stored in the Anvil format will have biomes automatically
enabled. To have Eihort draw biomes correctly for maps stored in the MCRegion
format, you will need Donkey Kong's Minecraft Biome Extractor from
http://www.minecraftforum.net/viewtopic.php?f=1022&t=80902 .
Run MBE on your world and Eihort will automatically use the extracted
biome information.


 :: Controls ::

Controls can be modified in eihort.config. The default controls are:

Esc - Release the mouse (as in Minecraft)
	  Click in the window to look around again
W,A,S,D - Move around
X/Z - Move up/down
L - Change the light mode (Overworld, Nether, Full Bright)
T/Y - Move the time of day forward/backwards
[ / ] - Change the view distance (left bracket lowers the view distance while
		right bracket increases it)
F1 - Toggle the corner display
F2 - Take a screenshot - it will be saved in the current directory
R - Completely reload the world
O - Move back to the origin of the world.. for when you get lost
P - Move to the single player's position
M - Monitor the world folder for changes (off by default)
N - Change the phase of the moon

Shift - Speed up movement, lighting changes and view distance changes
Ctrl - Slow down movement, lighting changes and view distance changes


 :: Bugs ::

If you find a bug in Eihort, please report it to the issues section of
https://bitbucket.org/lloigor/eihort/, or in the Eihort thread on the
Minecraft forums.
For rendering bugs, please include a screenshot. For crashes, please
describe how you got it to crash, along with your system specs (platform,
CPU, GPU). Crash dumps are welcome from those able to produce them.


 :: Acknowledgements ::

This program would not be possible without:
    Minecraft (of course)
	zlib (http://zlib.net/)
    SDL and SDL_image (http://www.libsdl.org/)
	libpng (http://www.libpng.org/pub/png/libpng.html)
    GLEW (http://glew.sourceforge.net/)
    Triangle (http://www.cs.cmu.edu/~quake/triangle.html)
    Lua (http://www.lua.org/)


