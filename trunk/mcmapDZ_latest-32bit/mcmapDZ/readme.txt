usage: mcmapDZ.exe [-h] [-day] [-night] [-cave] [-nether] [-end] [-skylight]
                   [-biomes] [-texture FILE] [-colours FILE]
                   [-biomecolours FOLDER] [-orientation {N,E,S,W}]
                   [-height VAL] [-limit fromX fromY toX toY] [-noise VAL]
                   [-mem VAL] [-quality VAL] [-extension NAME] [-optimise]
                   [-dir DIR] [-savemap] [-saveinfo] [-labels FILE]
                   [-cache DIR] [-debug]
                   WORLDPATH

mcmapDZ v0.4 - Renders Deep Zoom Minecraft maps using the mcmap isometric
mapping tool and hdmake.

positional arguments:
  WORLDPATH             the path to the world you want to render.

optional arguments:
  -h, --help            show this help message and exit
  -day                  renders a regular daylight map. If no other map flags
                        are used, this is default.
  -night                renders night map using blocklight/torches.
  -cave                 renders a cave map, showing all caves that have been
                        explored by players.
  -nether               renders a map of the nether associated with the world
                        (if present).
  -end                  renders a map of The End associated with the world (if
                        present).
  -skylight             use the skylight when rendering the map (such as
                        shadows below trees, etc).
  -biomes               apply biome colours to grass and leaves (requires
                        Donkey Kong's biome extractor)
  -texture FILE         extract block colours from a PNG texture file, such as
                        terrain.png
  -colours FILE         extract block colours from a text file, such as
                        colors.txt
  -biomecolours FOLDER  loads grasscolor.png and foliagecolor.png from the
                        specified folder for biome colours.
  -orientation {N,E,S,W}
                        the map orientation of the top left corner. Default is
                        E.
  -height VAL           maxiumum height at which blocks will be rendered on
                        the map (1-256).
  -limit fromX fromY toX toY
                        define coordinate boundaries for the render.
  -noise VAL            adds selective noise to certain blocks to simulate
                        texture (sane values are 1-20)
  -mem VAL              sets the amount of memory (in MiB) used for rendering.
                        mcmap will use incremental rendering or disk caching
                        to stick to this limit.
  -quality VAL          adjusts the compression quality of the output tiles.
                        Applies for JPG only. Default 0.92.
  -extension NAME       the extension to use for the output tiles. Default
                        jpg.
  -optimise             optimises PNG tiles using quantinisation. This will
                        increase render time but will greatly reduce file
                        size.
  -dir DIR              sets the directory to output the render. Default is
                        the name of the world in the 'renders' folder.
  -savemap              saves the original mcmap PNGs in the maps directory.
  -saveinfo             saves information about the world in the render
                        directory.
  -labels FILE          file containing list of labels to display on the
                        render
  -cache DIR            sets the directory used to store temporary cache
                        files. Default is the 'working' folder.
  -debug                prints commands sent to mcmap and hdmake.
