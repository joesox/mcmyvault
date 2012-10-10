
-- Set 'require' to look in the program root folder first, then in the
-- current folder
package.path = eihort.ProgramPath .. "?.lua;?.lua;;";
MinecraftJar = eihort.MinecraftPath .. "bin/minecraft.jar";

require "mapview"
require "worldmenu"

------------------------------------------------------------------------------
-- Event dispatch system

EventDownAction = { };
EventUpAction = { };
EventState = { };

local function eventDown( evt )
	EventState[evt] = false;
	local action = EventDownAction[evt];
	if action then
		action();
	end
end
local function eventUp( evt )
	EventState[evt] = nil;
	local action = EventUpAction[evt];
	if action then
		action();
	end
end
Event = {
	-- The names of these events are (mostly) hard-coded in eihort.exe

	-- To be filled in later:
	-- redraw(): Called to draw the screen again
	-- mousemove( x, y ): The mouse just moved
	-- idle(): Called when there's nothing else to do.
	--         Return true to save CPU

	active = function( state, ... )
		local types = { ... };
		for _, type in ipairs( types ) do
			if type == 'app' then
				if state then
					Event.resize( eihort.getWindowDims() );
				end
			elseif type == 'mouse' then
				if state then
					eventDown( 'mfocus' );
				else
					eventUp( 'mfocus' );
				end
			end
		end
	end;
	resize = function( w, h )
		ScreenWidth = w;
		ScreenHeight = h;
		ScreenAspect = w / h;
		eventDown( 'resize' );
	end;
	keydown = eventDown;
	keyup = eventUp;
	mousedown = function( button, x, y )
		Event.mousemove( x, y );
		eventDown( 'mouse' .. button );
	end;
	mouseup = function( button, x, y )
		Event.mousemove( x, y );
		eventUp( 'mouse' .. button );
	end;
	quit = function()
		-- User closed the window
		QuitFlag = 0;
	end;
	error = function( msg )
		local ignore = eihort.errorDialogYesNo( "Runtime Error", debug.traceback( msg, 3 ) .. "\n\nAttempt to ignore the error?" );
		if not ignore then
			QuitFlag = 1;
		end
	end;
};

local function main()
	-- Helper to process a single event
	local function processEvent( evtStr, ... )
		if not evtStr then
			return false;
		end

		local handler = Event[evtStr];
		if handler then
			handler( ... );
		end

		return true;
	end

	-- The main loop
	assert( Event.redraw );
	assert( Event.idle );

	while not QuitFlag do
		if not processEvent( eihort.pollEvent() ) then
			local idle = Event.idle();
			if eihort.shouldRedraw( Config.disable_cpu_saver ) then
				Event.redraw();
			end
			if idle then
				eihort.yield(); -- Don't kill the CPU
			end
		end
	end
	return QuitFlag;
end

------------------------------------------------------------------------------
-- Read in the config

function tableMerge( t1, t2 )
	--deep-merges two tables.
	--if e.g. ~/.config/eihort.config has only some key bindings redefined,
	--the global eihort.config's "keys" table should still contain the others.
	for k, v in pairs( t2 ) do
		if type( v ) == "table" and type( t1[k] ) == "table" then
			tableMerge( t1[k], v )
		else
			t1[k] = v
		end
	end
end

Config = {
	getenv = os.getenv;
	minecraft_path = eihort.MinecraftPath;
	eihort_path = eihort.ProgramPath;
};
do
	local configChunk = assert( loadfile( eihort.ProgramPath .. "eihort.config" ) );
	setfenv( configChunk, Config );
	local success = xpcall( configChunk, function( msg )
		eihort.errorDialog( "Error in eihort.config", msg )
	end );
	if not success then
		return 1;
	end
	
	local function tryLoadConfig( path, filename )
		if not path or not filename then
			return;
		end
		local configFile = path .. filename;
		local f = io.open( configFile );
		if not f then
			return;
		end
		f:close();
		local userChunk = assert( loadfile( configFile ) );
		--if we have a readable user config, load and merge it.
		if userChunk then
			local userConfigMeta = { __index = Config; };
			local userConfig = {};
			setmetatable( userConfig, userConfigMeta );
			setfenv( userChunk, userConfig );
			success = xpcall( userChunk, function( msg )
				eihort.errorDialog( "Error in " .. configFile, msg )
			end );
			if success then
				--overwrite parts of the global config with the user config
				tableMerge( Config, userConfig );
			end
		end
	end

	--get XDG config directory or its default.
	local xdgConfig = os.getenv( "XDG_CONFIG_HOME" );
	if xdgConfig then
		tryLoadConfig( xdgConfig, "/eihort.config" );
	else
		tryLoadConfig( os.getenv( "HOME" ), "/.config/eihort.config" );
	end
	-- Win7 user config in %USERPROFILE%
	tryLoadConfig( os.getenv( "USERPROFILE" ), "/eihort.config" );

	local workerCount = Config.worker_threads or 0;
	if workerCount == 0 then
		workerCount = eihort.getProcessorCount();
	end
	eihort.initWorkers( workerCount );
end

------------------------------------------------------------------------------
-- World loading function

function loadWorld( path )
	local levelDatPath = path .. "/level.dat";
	local f = io.open( levelDatPath, "rb" );
	if f then
		f:close();
		local oName, dat = eihort.loadNBT( levelDatPath );
		if dat then
			local levelData = dat:get("Data");
			if levelData then
				local version = levelData:get("version");
				local name = levelData:get("name");
				return eihort.loadWorld( path, version == 0x4abd ), name;
			end
			dat:destroy();
		end
	end
end

------------------------------------------------------------------------------
-- Process the command line

local argv = { ... };
local startWorld, startWorldName;
if #argv > 0 then
	startWorld, startWorldName = loadWorld( argv[1] );
	if startWorld:getRegionCount() == 0 then
		eihort.errorDialog( "Eihort Error", "No regions were found at '" .. argv[1] .. "'.\n\nThere does not appear to be a Minecraft world at that path." );
		return 1;
	end
end

------------------------------------------------------------------------------
-- Initialize video

ScreenWidth = Config.screenwidth or 800;
ScreenHeight = Config.screenheight or 600;
ScreenAspect = ScreenWidth / ScreenHeight;
assert( eihort.initializeVideo( ScreenWidth, ScreenHeight, Config.fullscreen, Config.multisample or 0 ) );

if startWorld then
	-- World loaded - enter the main map view
	beginMapView( startWorld, startWorldName );
else
	-- Show the world selection menu first
	beginWorldMenu();
end

------------------------------------------------------------------------------
-- Error trap

while true do
	local success, ret = xpcall( main, function( ... ) return Event.error( ... ); end );
	if success then
		-- The return value of this function is the return value of eihort.exe
		return ret;
	end
end

