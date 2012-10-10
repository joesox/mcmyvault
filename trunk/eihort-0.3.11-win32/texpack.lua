
function loadTexture( subpath, filename, necessary )
	local tx = eihort.loadImage( eihort.ProgramPath .. filename );
	if not tx then
		if Config.texture_pack then
			if string.match( Config.texture_pack, "%.zip$" ) then
				tx = eihort.loadImageZip( Config.texture_pack, subpath .. filename );
			else
				tx = eihort.loadImage( Config.texture_pack .. "/" .. subpath .. filename );
			end
		end
		if not tx then
			tx = eihort.loadImageZip( Config.minecraft_jar or MinecraftJar, subpath .. filename );
			if not tx and necessary then
				error( "Failed to locate " .. subpath .. filename .. ".\n\nCheck your minecraft_jar path in eihort.config or place this file in the folder with the Eihort application." );
			end
		end
	end
	return tx;
end


