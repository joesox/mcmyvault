
-- Block ID -> Geometry description for Eihort 0.3.0+

-- The first part of this file contains helper functions used to construct
-- the various block geometry objects.
-- The actual Block ID -> Geometry list is at the end of the file

require "texpack";

function loadBlockDesc()
	local blocks = eihort.newBlockDesc();

	-- Load terrain.png
	local terrainImage = loadTexture( "", "terrain.png", true );
	if math.fmod( terrainImage.width, 16 ) ~= 0 or math.fmod( terrainImage.height, 16 ) ~= 0 then
		error( "Both of terrain.png's dimensions must be multiples of 16." );
	end

	-- Easy access to the tiles of terrain.png
	local loadedTextures = { };
	local tw, th = terrainImage.width / 16, terrainImage.height / 16;
	local defAniso = 'aniso_' .. tostring( Config.anisotropy or 0 );
	local function TerrainPNG( x, y, ... )
		local pos = y * 16 + x;
		local tex = loadedTextures[pos];
		if not tex then
			tex = terrainImage:sub( tw * x, th * y, tw, th ):uploadToGL( 'repeat', 'mag_nearest', 'min_linear', 'mip_linear', 'mipgen_alphawt_box', defAniso, ... );
			loadedTextures[pos] = tex;
		end
		return tex;
	end
	local function Tex_AlphaFromGray( t1, t2, ... )
		t1:grayToAlpha( t2 );
		local tex = t1:uploadToGL( 'repeat', 'mag_nearest', 'min_linear', 'mip_linear', 'mipgen_box', defAniso, ... );
		t1:destroy();
		t2:destroy();
		return tex;
	end
	local function TerrainPNG_AlphaFromGray( x, y, grayX, grayY, ... )
		return Tex_AlphaFromGray( terrainImage:sub( tw * x, th * y, tw, th ):copy(), terrainImage:sub( tw * grayX, th * grayY, tw, th ), ... );
	end
	local function TerrainPNG_InAlpha( x, y, ... )
		return Tex_AlphaFromGray( eihort.newImage( tw, th, 0, 0, 0, 0 ), terrainImage:sub( tw * x, th * y, tw, th ), ... );
	end
	local function TerrainPNG_NoAlpha( x, y, ... )
		return Tex_AlphaFromGray( terrainImage:sub( tw * x, th * y, tw, th ):copy(), eihort.newImage( tw, th, 0, 0, 0, 0 ), ... );
	end

	-- Load chest.png
	local chestImage = loadTexture( "item/", "chest.png", true );
	if math.fmod( chestImage.width, 64 ) ~= 0 or math.fmod( chestImage.height, 64 ) ~= 0 then
		error( "chest.png's dimensions must be multiples of 64." );
	end
	-- Access to the chest textures
	function ChestPNG( x, y, ... )
		local tw = chestImage.width/4;
		local th = chestImage.height/4;
		local tex = eihort.newImage( tw, th, 0, 0, 0, 0 );
		assert(y<4);
		if y == 0 then
			local t1 = chestImage:sub( tw*x*14/16, 0, tw*14/16, th*14/16 );
			tex:put( t1, tw*1/16, th*1/16 );
			t1:destroy();
		elseif y == 1 then
			local t1 = chestImage:sub( x*tw*14/16, th*14/16, tw*14/16, th*5/16 );
			local t2 = chestImage:sub( x*tw*14/16, th*34/16, tw*14/16, th*9/16 );
			tex:put( t1, tw*1/16, th*2/16 );
			tex:put( t2, tw*1/16, th*7/16 );
			t1:destroy();
			t2:destroy();
		else
			local t1 = chestImage:sub( tw*x*14/16, th*19/16, tw*14/16, th*14/16 );
			tex:put( t1, tw*1/16, th*1/16 );
			t1:destroy();
		end
		return tex:uploadToGL( 'repeat', 'mag_nearest', 'min_linear', 'mip_linear', 'mipgen_alphawt_box', defAniso, ... );
	end
	chestSide = ChestPNG( 0, 1 );
	chestTop = ChestPNG( 1, 0 );

	-- Simple color texture generator
	local function SingleColor( r, g, b, a )
		local tempTex = eihort.newImage( 1, 1, r, g, b, a );
		local tex = tempTex:uploadToGL( 'repeat', 'mag_nearest', 'min_nearest', 'mip_none' );
		tempTex:destroy();
		return tex;
	end

	-- Block geometry adapters
	local function DataAdapter( mask, ... ) -- "mask" is the bitmask for the data value.
		local geoms = { ... };
		local geomList = { };
		local solidity = 0x3f;
		for i, v in ipairs( geoms ) do
			if type( v ) == "number" then
				geomList[i] = geomList[v+1];
			else
				geomList[i] = v[1];
				solidity = eihort.intAnd( solidity, v[2] );
			end
		end
		return { eihort.geom.dataAdapter( mask, unpack( geomList ) ), solidity };
	end
	local function RotatingAdapter( normalGeom, faceGeom )
		return { eihort.geom.rotatingAdapter( normalGeom[1], faceGeom[1] ),
			     eihort.intAnd( normalGeom[2], eihort.intOr( 0x60, faceGeom[2] ) ) };
	end
	local function FaceBitAdapter( faceGeom )
		return { eihort.geom.faceBitAdapter( faceGeom[1] ), 0x00 };
	end
	local function FacingAdapter( normalGeom, faceGeom )
		if not normalGeom then
			normalGeom = { false, 0x00 };
		end
		return { eihort.geom.facingAdapter( normalGeom[1], faceGeom[1] ),
			     eihort.intAnd( normalGeom[2], eihort.intOr( 0x60, faceGeom[2] ) ) };
	end
	local function TopDifferentAdapter( normalGeom, diffGeom, topId, topGeom )
		return { eihort.geom.topDifferentAdapter( normalGeom[1], diffGeom[1], topId, topGeom and topGeom[1] ),
		         eihort.intAnd( normalGeom[2], diffGeom[2] ) };
	end
	local function SetTexScale( geom, s, t )
		geom[1]:setTexScale( s, t );
		return geom;
	end
	local function DelayRender( geom, delta )
		geom[1]:renderGroupAdd( delta or 1 );
		return geom;
	end

	-- Block Geometry creators
	-- In all the following, ... represents a list of textures with different
	-- meaning depending on how many textures are given:
	--     One texture: All sides of the block are covered with the same texture
	--     Two textures: The four sides take the first texture, the top and
	--                   bottom take the second
	--     Three textures: Sides, bottom, top
	--     Four textures: First two textures are the sides, which go on alternate
	--                    faces, last two textures are bottom and top
	--     Six textures: All 6 sides in the order X- X+ Z- Z+ Y- Y+
	local function OpaqueBlock( ... )
		return { eihort.geom.opaqueBlock( ... ), 0x3f };
	end
	local function HollowOpaqueBlock( ... )
		return { eihort.geom.opaqueBlock( ... ), 0x00 };
	end
	local function BrightOpaqueBlock( ... )
		return { eihort.geom.brightOpaqueBlock( ... ), 0x3f };
	end
	local function TransparentBlock( order, ... )
		return { eihort.geom.transparentBlock( order, ... ), 0x00 };
	end
	local function Slab( topOffset, bottomOffset, ... )
		local solidity = 0;
		if topOffset == 0 then
			solidity = solidity + 0x10;
		end
		if bottomOffset == 0 then
			solidity = solidity + 0x20;
		end
		return { eihort.geom.squashedBlock( topOffset, bottomOffset, ... ), solidity };
	end
	local function CompactedBlock( offsetXn, offsetXp, offsetZn, offsetZp, offsetYn, offsetYp, ... )
		return { eihort.geom.compactedBlock( offsetXn, offsetXp, offsetZn, offsetZp, offsetYn, offsetYp, ... ), 0x00 };
	end
	local function MultiCompactedBlock( x, y, z, ... )
		return { eihort.geom.multiCompactedBlock( x, y, z, ... ) , 0x00 };
	end
	local function BiomeOpaqueBlock( biomeChannel, ... )
		return { eihort.geom.biomeOpaqueBlock( biomeChannel, ... ), 0x3f };
	end
	local function BiomeHollowOpaqueBlock( biomeChannel, ... )
		return { eihort.geom.biomeOpaqueBlock( biomeChannel, ... ), 0x00 };
	end
	local function BiomeAlphaOpaqueBlock( biomeChannel, ... )
		return { eihort.geom.biomeAlphaOpaqueBlock( biomeChannel, ... ), 0x3f };
	end
	local function Portal( tex )
		return { eihort.geom.portal( tex ), 0x00 };
	end
	local function Cactus( offset, ... )
		return { eihort.geom.cactus( offset, ... ), 0x00 };
	end
	local function BiomeCactus( biomeChannel, offset, ... )
		return { eihort.geom.biomeCactus( biomeChannel, offset, ... ), 0x00 };
	end
	local function Rail( straight, turn )
		return { eihort.geom.rail( straight, turn ), 0x00 };
	end
	local function Door( bottom, top )
		return DataAdapter( 0x8,
			{ eihort.geom.door( bottom ), 0x00 },
			{ eihort.geom.door( top ), 0x00 } );
	end
	local function Stairs( tex )
		return { eihort.geom.stairs( tex ), 0x00 };
	end
	local function Torch( tex )
		return { eihort.geom.torch( tex ), 0x00 };
	end
	local function Flower( tex )
		return { eihort.geom.flower( tex ), 0x00 };
	end
	local function BiomeFlower( biomeChannel, tex )
		return { eihort.geom.biomeFlower( biomeChannel, tex ), 0x00 };
	end
	local function Fence( tex )
		return MultiCompactedBlock(
		          {  10,  10,  -7,  -7, -11,  -2,
				     10,  10,  -7,  -7,  -5,  -8 },
				  {  -7,  -7,  10,  10, -11,  -2,
				     -7,  -7,  10,  10,  -5,  -8 },
				  {  -6,  -6,  -6,  -6,  -0,  -0 },
				  tex );
	end

	-------------------------------------------------------------------
	-- The actual block geometry list
	FurnaceBody = OpaqueBlock( TerrainPNG( 13, 2 ), TerrainPNG( 14, 3 ) );
	MinecraftBlocks = {
		-- [<block id>] = <geometry>;
		[1]   = OpaqueBlock( TerrainPNG( 1, 0 ) ); -- Stone
		[2]   = TopDifferentAdapter( -- Grass
		            BiomeAlphaOpaqueBlock( 0, TerrainPNG_AlphaFromGray( 3, 0, 6, 2 ), TerrainPNG_NoAlpha( 2, 0 ), TerrainPNG_InAlpha( 0, 0 ) ), -- Normal grass
		            OpaqueBlock( TerrainPNG( 4, 4 ), TerrainPNG( 2, 0 ) ), 78 ); -- Snowy grass
		[3]   = OpaqueBlock( TerrainPNG( 2, 0 ) ); -- Dirt
		[4]   = OpaqueBlock( TerrainPNG( 0, 1 ) ); -- Cobblestone
		[5]   = DataAdapter( 0x3, -- Wooden Plank
		          OpaqueBlock( TerrainPNG( 4, 0 ) ),
		          OpaqueBlock( TerrainPNG( 6, 12 ) ),
		          OpaqueBlock( TerrainPNG( 6, 13 ) ),
		          OpaqueBlock( TerrainPNG( 7, 12 ) ));
		[6]   = DataAdapter( 0x3, -- Sapling
		          Flower( TerrainPNG( 15, 0 ) ),
		          Flower( TerrainPNG( 15, 3 ) ),
		          Flower( TerrainPNG( 15, 4 ) ),
		          Flower( TerrainPNG( 14, 1 ) ));
		[7]   = OpaqueBlock( TerrainPNG( 1, 1 ) ); -- Bedrock
		[8]   = TransparentBlock( 1, TerrainPNG( 15, 13 ) ); -- Water
		[9]   = TransparentBlock( 2, TerrainPNG( 15, 13 ) ); -- Stationary Water
		[10]  = BrightOpaqueBlock( TerrainPNG( 15, 15 ) ); -- Lava
		[11]  = BrightOpaqueBlock( TerrainPNG( 15, 15 ) ); -- Stationary Lava
		[12]  = OpaqueBlock( TerrainPNG( 2, 1 ) ); -- Sand
		[13]  = OpaqueBlock( TerrainPNG( 3, 1 ) ); -- Gravel
		[14]  = OpaqueBlock( TerrainPNG( 0, 2 ) ); -- Gold Ore
		[15]  = OpaqueBlock( TerrainPNG( 1, 2 ) ); -- Iron Ore
		[16]  = OpaqueBlock( TerrainPNG( 2, 2 ) ); -- Coal Ore
		[17]  = DataAdapter( 0x3, -- Wood
		          OpaqueBlock( TerrainPNG( 4, 1 ), TerrainPNG( 5, 1 ) ),
		          OpaqueBlock( TerrainPNG( 4, 7 ), TerrainPNG( 5, 1 ) ),
		          OpaqueBlock( TerrainPNG( 5, 7 ), TerrainPNG( 5, 1 ) ),
		          OpaqueBlock( TerrainPNG( 9, 9 ), TerrainPNG( 5, 1 ) ));
		[18]  = DataAdapter( 0x3, -- Leaves
		          BiomeHollowOpaqueBlock( 2, TerrainPNG( 4, 3 ) ),
		          BiomeHollowOpaqueBlock( 2, TerrainPNG( 4, 8 ) ),
		          BiomeHollowOpaqueBlock( 1, TerrainPNG( 4, 3 ) ),
		          BiomeHollowOpaqueBlock( 1, TerrainPNG( 4, 12 ) ));
		[19]  = OpaqueBlock( TerrainPNG( 0, 3 ) ); -- Sponge
		[20]  = HollowOpaqueBlock( TerrainPNG( 1, 3 ) ); -- Glass
		[21]  = OpaqueBlock( TerrainPNG( 0, 10 ) ); -- Lapis Lazuli Ore
		[22]  = OpaqueBlock( TerrainPNG( 0, 9 ) ); -- Lapis Lazuli Block
		[23]  = FacingAdapter( FurnaceBody, OpaqueBlock( TerrainPNG( 14, 2 ) ) ); -- Dispenser
		[24]  = DataAdapter( 0x3, -- Sandstone
		          OpaqueBlock( TerrainPNG( 0, 12 ), TerrainPNG( 0, 13 ), TerrainPNG( 0, 11 ) ), -- normal
		          OpaqueBlock( TerrainPNG( 5, 14 ), TerrainPNG( 0, 11 ), TerrainPNG( 0, 11 ) ), -- chiseled
		          OpaqueBlock( TerrainPNG( 6, 14 ), TerrainPNG( 0, 11 ), TerrainPNG( 0, 11 ) ), -- smooth
		          0 );
		[25]  = OpaqueBlock( TerrainPNG( 10, 4 ) ); -- Note block
		-- [26] -- Bed
		[27]  = DataAdapter( 0x8, -- Powered rails
		          Rail( TerrainPNG( 3, 10 ) ),
		          Rail( TerrainPNG( 3, 11 ) ) );
		[28]  = Rail( TerrainPNG( 3, 12 ) ); -- Detector rails
		-- Sticky Piston needs special code: the trick is that the side tiles need to rotate to face the front piece TODO
		[29]  = OpaqueBlock( TerrainPNG( 12, 6 ), TerrainPNG( 13, 6 ), TerrainPNG( 10, 6 ) ); -- Sticky Piston up
		[30]  = Flower( TerrainPNG( 11, 0 ) ); -- Web
		[31]  = DataAdapter( 0x3, -- Tall grass
		          Flower( TerrainPNG( 7, 3 ) ),
		          BiomeFlower( 0, TerrainPNG( 7, 2 ) ),
		          BiomeFlower( 0, TerrainPNG( 8, 3 ) ), 0 );
		[32]  = Flower( TerrainPNG( 7, 3 ) ), -- Dead shrubs
		-- Piston needs special code: the trick is that the side tiles need to rotate to face the front piece TODO
		[33]  = OpaqueBlock( TerrainPNG( 12, 6 ), TerrainPNG( 13, 6 ), TerrainPNG( 11, 6 ) ); -- Piston up
		-- [34] -- Piston Extension
		[35]  = DataAdapter( 0xf, -- Wool
		          OpaqueBlock( TerrainPNG( 0, 4 ) ),
		          OpaqueBlock( TerrainPNG( 2, 13 ) ),
		          OpaqueBlock( TerrainPNG( 2, 12 ) ),
		          OpaqueBlock( TerrainPNG( 2, 11 ) ),
		          OpaqueBlock( TerrainPNG( 2, 10 ) ),
		          OpaqueBlock( TerrainPNG( 2, 9 ) ),
		          OpaqueBlock( TerrainPNG( 2, 8 ) ),
		          OpaqueBlock( TerrainPNG( 2, 7 ) ),
		          OpaqueBlock( TerrainPNG( 1, 14 ) ),
		          OpaqueBlock( TerrainPNG( 1, 13 ) ),
		          OpaqueBlock( TerrainPNG( 1, 12 ) ),
		          OpaqueBlock( TerrainPNG( 1, 11 ) ),
		          OpaqueBlock( TerrainPNG( 1, 10 ) ),
		          OpaqueBlock( TerrainPNG( 1, 9 ) ),
		          OpaqueBlock( TerrainPNG( 1, 8 ) ),
		          OpaqueBlock( TerrainPNG( 1, 7 ) ) );
		[37]  = Flower( TerrainPNG( 13, 0 ) ); -- Yellow flower
		[38]  = Flower( TerrainPNG( 12, 0 ) ); -- Red rose
		[39]  = Flower( TerrainPNG( 13, 1 ) ); -- Brown mushroom
		[40]  = Flower( TerrainPNG( 12, 1 ) ); -- Red mushroom
		[41]  = OpaqueBlock( TerrainPNG( 7, 1 ) ); -- Gold block
		[42]  = OpaqueBlock( TerrainPNG( 6, 1 ) ); -- Iron block
		[43]  = DataAdapter( 0x7, -- Double Slab
		          OpaqueBlock( TerrainPNG( 5, 0 ), TerrainPNG( 6, 0 ) ), -- Normal double slab
		          OpaqueBlock( TerrainPNG( 0, 12 ), TerrainPNG( 0, 13 ) ), -- Sandstone
		          OpaqueBlock( TerrainPNG( 4, 0 ) ), -- Wooden planks
		          OpaqueBlock( TerrainPNG( 0, 1 ) ), -- Cobblestone
		          OpaqueBlock( TerrainPNG( 7, 0 ) ), -- Brick
		          OpaqueBlock( TerrainPNG( 6, 3 ) ), -- Stone brick
		          0, 0 );
		[44]  = DataAdapter( 0x8, -- Slabs
		          DataAdapter( 0x7, -- Normal Slabs
		            Slab( -8, 0, TerrainPNG( 5, 0 ), TerrainPNG( 6, 0 ) ), -- Normal slab
		            Slab( -8, 0, TerrainPNG( 0, 12 ), TerrainPNG( 0, 13 ), TerrainPNG( 0, 11 ) ), -- Sandstone slab
		            Slab( -8, 0, TerrainPNG( 4, 0 ) ), -- Wooden plank slab
		            Slab( -8, 0, TerrainPNG( 0, 1 ) ), -- Cobblestone slab
		            Slab( -8, 0, TerrainPNG( 7, 0 ) ), -- Brick slab
		            Slab( -8, 0, TerrainPNG( 6, 3 ) ), -- Stone brick
		            0, 0 ),
		          DataAdapter( 0x7, -- Upside-down slabs
		            Slab( 0, -8, TerrainPNG( 5, 0 ), TerrainPNG( 6, 0 ) ), -- Normal slab
		            Slab( 0, -8, TerrainPNG( 0, 12 ), TerrainPNG( 0, 13 ), TerrainPNG( 0, 11 ) ), -- Sandstone slab
		            Slab( 0, -8, TerrainPNG( 4, 0 ) ), -- Wooden plank slab
		            Slab( 0, -8, TerrainPNG( 0, 1 ) ), -- Cobblestone slab
		            Slab( 0, -8, TerrainPNG( 7, 0 ) ), -- Brick slab
		            Slab( 0, -8, TerrainPNG( 6, 3 ) ), -- Stone brick
		            0, 0 ) );
		[45]  = OpaqueBlock( TerrainPNG( 7, 0 ) ); -- Brick
		[46]  = OpaqueBlock( TerrainPNG( 8, 0 ), TerrainPNG( 10, 0 ), TerrainPNG( 9, 0 ) ); -- TNT
		[47]  = OpaqueBlock( TerrainPNG( 3, 2 ), TerrainPNG( 4, 0 ) ); -- Bookshelf
		[48]  = OpaqueBlock( TerrainPNG( 4, 2 ) ); -- Mossy cobblestone
		[49]  = OpaqueBlock( TerrainPNG( 5, 2 ) ); -- Obsidian
		[50]  = Torch( TerrainPNG( 0, 5, 'clamp' ) ); -- Torch
		-- [51] -- Fire
		[52]  = HollowOpaqueBlock( TerrainPNG( 1, 4 ) ); -- Monster spawner
		[53]  = Stairs( TerrainPNG( 4, 0 ) ); -- Wooden stairs
		[54]  = CompactedBlock( -1, -1, -1, -1, 0, -2, chestSide, chestTop ); -- Chest
		-- [55] -- Redstone Wire
		[56]  = OpaqueBlock( TerrainPNG( 2, 3 ) ); -- Diamond ore
		[57]  = OpaqueBlock( TerrainPNG( 8, 1 ) ); -- Diamond block
		[58]  = OpaqueBlock( TerrainPNG( 12, 3 ), TerrainPNG( 11, 3 ), TerrainPNG( 4, 0 ), TerrainPNG( 11, 2 ) ); -- Crafting table
		[59]  = DataAdapter( 0x7, -- Wheat Crops
		          Cactus( -4, TerrainPNG( 8, 5 ), 0 ),
		          Cactus( -4, TerrainPNG( 9, 5 ), 0 ),
		          Cactus( -4, TerrainPNG( 10, 5 ), 0 ),
		          Cactus( -4, TerrainPNG( 11, 5 ), 0 ),
		          Cactus( -4, TerrainPNG( 12, 5 ), 0 ),
		          Cactus( -4, TerrainPNG( 13, 5 ), 0 ),
		          Cactus( -4, TerrainPNG( 14, 5 ), 0 ),
		          Cactus( -4, TerrainPNG( 15, 5 ), 0 ) );
		[60]  = CompactedBlock( 0, 0, 0, 0, 0, -1, TerrainPNG( 7, 5 ) ); -- Farmland
		[61]  = FacingAdapter( FurnaceBody, OpaqueBlock( TerrainPNG( 12, 2 ) ) ); -- Furnace
		[62]  = FacingAdapter( FurnaceBody, OpaqueBlock( TerrainPNG( 13, 3 ) ) ); -- Burning furnace
		-- [63] -- Sign Post
		[64]  = Door( TerrainPNG( 1, 6 ), TerrainPNG( 1, 5 ) ); -- Wooden door
		[65]  = FacingAdapter( false, Cactus( -15, TerrainPNG( 3, 5 ) ) ); -- Ladder
		[66]  = Rail( TerrainPNG( 0, 8 ), TerrainPNG( 0, 7 ) ); -- Rails
		[67]  = Stairs( TerrainPNG( 0, 1 ) ); -- Cobblestone stairs
		[68]  = DataAdapter( 0x7, -- Wall Sign
		          SetTexScale( CompactedBlock( 0, 0, -14, 0, -4, -4, TerrainPNG( 4, 0 ) ), 2, 2 ),
		          0, 0,
		          SetTexScale( CompactedBlock( 0, 0, 0, -14, -4, -4, TerrainPNG( 4, 0 ) ), 2, 2 ),
		          SetTexScale( CompactedBlock( -14, 0, 0, 0, -4, -4, TerrainPNG( 4, 0 ) ), 2, 2 ),
		          SetTexScale( CompactedBlock( 0, -14, 0, 0, -4, -4, TerrainPNG( 4, 0 ) ), 2, 2 ),
		          0, 0 );
		-- [69] -- Lever
		[70]  = CompactedBlock( -1, -1, -1, -1, 0, -15, TerrainPNG( 1, 0 ) ); -- Stone pressure plate
		[71]  = Door( TerrainPNG( 2, 6 ), TerrainPNG( 2, 5 ) ); -- Iron door
		[72]  = CompactedBlock( -1, -1, -1, -1, 0, -15, TerrainPNG( 4, 0 ) ); -- Wooden pressure plate
		[73]  = OpaqueBlock( TerrainPNG( 3, 3 ) ); -- Redstone ore
		[74]  = OpaqueBlock( TerrainPNG( 3, 3 ) ); -- Glowing redstone ore
		[75]  = Torch( TerrainPNG( 3, 7, 'clamp' ) ); -- Redstone torch (off)
		[76]  = Torch( TerrainPNG( 3, 6, 'clamp' ) ); -- Redstone torch (on)
		-- [77] -- Stone Button
		[78]  = DataAdapter( 0x7, -- Snow
		          Slab( -14, 0, TerrainPNG( 2, 4 ) ),
		          Slab( -12, 0, TerrainPNG( 2, 4 ) ),
		          Slab( -10, 0, TerrainPNG( 2, 4 ) ),
		          Slab( -8, 0, TerrainPNG( 2, 4 ) ),
		          Slab( -6, 0, TerrainPNG( 2, 4 ) ),
		          Slab( -4, 0, TerrainPNG( 2, 4 ) ),
		          Slab( -2, 0, TerrainPNG( 2, 4 ) ),
		          OpaqueBlock( TerrainPNG( 2, 4 ) ) );
		[79]  = DelayRender( TransparentBlock( 0, TerrainPNG( 3, 4 ) ), 5 ); -- Ice
		[80]  = OpaqueBlock( TerrainPNG( 2, 4 ) ); -- Snow block
		[81]  = Cactus( -1, TerrainPNG( 6, 4 ), TerrainPNG( 7, 4 ), TerrainPNG( 5, 4 ) ); -- Cactus
		[82]  = OpaqueBlock( TerrainPNG( 8, 4 ) ); -- Clay
		[83]  = Flower( TerrainPNG( 9, 4 ) ); -- Sugar Cane
		[84]  = OpaqueBlock( TerrainPNG( 11, 4 ) ); -- Jukebox
		[85]  = Fence( TerrainPNG( 4, 0 ) ); -- Wooden Fence
		[86]  = RotatingAdapter( OpaqueBlock( TerrainPNG( 6, 7 ), TerrainPNG( 6, 6 ) ), OpaqueBlock( TerrainPNG( 7, 7 ) ) ); -- Pumpkin
		[87]  = OpaqueBlock( TerrainPNG( 7, 6 ) ); -- Netherrack
		[88]  = OpaqueBlock( TerrainPNG( 8, 6 ) ); -- Soul sand
		[89]  = OpaqueBlock( TerrainPNG( 9, 6 ) ); -- Glowstone block
		[90]  = DelayRender( Portal( SingleColor( 0x93/255, 0x31/255, 0x89/255, 0x80/255 ) ), 10 ); -- Portal
		[91]  = RotatingAdapter( OpaqueBlock( TerrainPNG( 6, 7 ), TerrainPNG( 6, 6 ) ), OpaqueBlock( TerrainPNG( 8, 7 ) ) ); -- Jack-o-lantern
		[92]  = DataAdapter( 0x7, -- Cake
		          CompactedBlock( -1, -1, -1, -1, 0, -8, TerrainPNG( 10, 7 ), TerrainPNG( 12, 7 ), TerrainPNG( 9, 7 ) ),
		          CompactedBlock( -3, -1, -1, -1, 0, -8, TerrainPNG( 11, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 12, 7 ), TerrainPNG( 9, 7 ) ),
		          CompactedBlock( -5, -1, -1, -1, 0, -8, TerrainPNG( 11, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 12, 7 ), TerrainPNG( 9, 7 ) ),
		          CompactedBlock( -7, -1, -1, -1, 0, -8, TerrainPNG( 11, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 12, 7 ), TerrainPNG( 9, 7 ) ),
		          CompactedBlock( -9, -1, -1, -1, 0, -8, TerrainPNG( 11, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 12, 7 ), TerrainPNG( 9, 7 ) ),
		          CompactedBlock( -11, -1, -1, -1, 0, -8, TerrainPNG( 11, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 10, 7 ), TerrainPNG( 12, 7 ), TerrainPNG( 9, 7 ) ), 0, 0 );
		-- [93] -- Redstone repeater (off)
		-- [94] -- Redstone repeater (on)
		[95]  = OpaqueBlock( TerrainPNG( 11, 1 ), TerrainPNG( 9, 1 ) ); -- Locked Chest
		[96]  = DataAdapter( 0x4, -- Trapdoor
		          Slab( -13, 0, TerrainPNG( 4, 5 ) ),
		          DataAdapter( 0x3,
		            CompactedBlock( 0, 0, -13, 0, 0, 0, TerrainPNG( 4, 5 ) ),
		            CompactedBlock( 0, 0, 0, -13, 0, 0, TerrainPNG( 4, 5 ) ),
		            CompactedBlock( -13, 0, 0, 0, 0, 0, TerrainPNG( 4, 5 ) ),
		            CompactedBlock( 0, -13, 0, 0, 0, 0, TerrainPNG( 4, 5 ) ) ) );
		[97]  = DataAdapter( 0x3, -- Hidden Silverfish
		          OpaqueBlock( TerrainPNG( 1, 0 ) ),
		          OpaqueBlock( TerrainPNG( 0, 1 ) ),
		          OpaqueBlock( TerrainPNG( 6, 3 ) ), 0 );
		[98]  = DataAdapter( 0x3, -- Stone brick block
		          OpaqueBlock( TerrainPNG( 6, 3 ) ),
		          OpaqueBlock( TerrainPNG( 4, 6 ) ),
		          OpaqueBlock( TerrainPNG( 5, 6 ) ),
		          OpaqueBlock( TerrainPNG( 5, 13 ) ) );
		[99] = DataAdapter( 0xf, -- Brown Mushroom
		          OpaqueBlock( TerrainPNG( 14, 8 ) ), -- pores
		          OpaqueBlock( TerrainPNG( 14, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ) ), -- west + north
		          OpaqueBlock( TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ) ), -- north
		          OpaqueBlock( TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ), TerrainPNG( 14, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ) ), -- east + north
		          OpaqueBlock( TerrainPNG( 14, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ) ), -- west
		          OpaqueBlock( TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ) ), -- top
		          OpaqueBlock( TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ) ), -- east
		          OpaqueBlock( TerrainPNG( 14, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ) ), -- west + south
		          OpaqueBlock( TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ) ), -- south
		          OpaqueBlock( TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 7 ) ), -- east + south
		          OpaqueBlock( TerrainPNG( 13, 8 ), TerrainPNG( 14, 8 ) ), -- stem
		          0, 0, 0, 0, 0 );
		[100] = DataAdapter( 0xf, -- Red Mushroom
		          OpaqueBlock( TerrainPNG( 14, 8 ) ), -- pores
		          OpaqueBlock( TerrainPNG( 13, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ) ), -- west + north
		          OpaqueBlock( TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ) ), -- north
		          OpaqueBlock( TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ), TerrainPNG( 13, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ) ), -- east + north
		          OpaqueBlock( TerrainPNG( 13, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ) ), -- west
		          OpaqueBlock( TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ) ), -- top
		          OpaqueBlock( TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ) ), -- east
		          OpaqueBlock( TerrainPNG( 13, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ) ), -- west + south
		          OpaqueBlock( TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ) ), -- south
		          OpaqueBlock( TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ), TerrainPNG( 14, 8 ), TerrainPNG( 13, 7 ) ), -- east + south
		          OpaqueBlock( TerrainPNG( 13, 8 ), TerrainPNG( 14, 8 ) ), -- stem
		          0, 0, 0, 0, 0 );
		-- [101] -- Iron Bars TODO
		[102] = HollowOpaqueBlock( TerrainPNG( 1, 3 ) ); -- Glass Panes (for now, a solid glass block - TODO)
		[103] = OpaqueBlock( TerrainPNG( 8, 8 ), TerrainPNG( 9, 8 ) ); -- Melon
		-- [104] -- Pumpkin Stem
		-- [105] -- Melon Stem
		[106] = FaceBitAdapter( BiomeCactus( 1, {-15}, TerrainPNG( 15, 8 ), TerrainPNG( 15, 8 ), 0 ) ); -- Vines
		-- [107] -- Fence Gate TODO
		[108] = Stairs( TerrainPNG( 7, 0 ) ); -- Brick Stairs
		[109] = Stairs( TerrainPNG( 6, 3 ) ); -- Stone Brick Stairs
		[110] = OpaqueBlock( TerrainPNG( 13, 4 ), TerrainPNG( 2, 0 ), TerrainPNG( 14, 4 ) ); -- Mycelium
		[111] = BiomeCactus( 0, {-15}, 0, 0, TerrainPNG( 12, 4 ) ); -- Lily pad
		[112] = OpaqueBlock( TerrainPNG( 0, 14 ) ); -- Nether Brick
		[113] = Fence( TerrainPNG( 0, 14 ) ); -- Nether Brick Fence
		[114] = Stairs( TerrainPNG( 0, 14 ) ); -- Nether Brick Stairs
		[115] = DataAdapter( 0x3, -- Nether Wart
		          Cactus( -4, TerrainPNG( 2, 14 ), 0 ),
		          Cactus( -4, TerrainPNG( 3, 14 ), 0 ), 1,
		          Cactus( -4, TerrainPNG( 4, 14 ), 0 ) );
		[116] = Slab( -4, 0, TerrainPNG( 6, 11 ), TerrainPNG( 7, 11 ), TerrainPNG( 6, 10 ) ); -- Enchantment Table
		-- [117] -- Brewing Stand
		-- [118] -- Cauldron
		-- [119] -- End Portal
		-- [120] -- End Portal Frame
		[121] = OpaqueBlock( TerrainPNG( 15, 10 ) ); -- End Stone
		-- [122] -- Dragon Egg
		[123] = OpaqueBlock( TerrainPNG( 3, 13 ) ); -- Redstone Lamp (off)
		[124] = OpaqueBlock( TerrainPNG( 4, 13 ) ); -- Redstone Lamp (on)
		[125] = DataAdapter( 0x3, -- Wooden Double-Slab
		          OpaqueBlock( TerrainPNG( 4, 0 ) ),
		          OpaqueBlock( TerrainPNG( 6, 12 ) ),
		          OpaqueBlock( TerrainPNG( 6, 13 ) ),
		          OpaqueBlock( TerrainPNG( 7, 12 ) ));
		[126] = DataAdapter( 0x3+0x8, -- Wooden Slab
		          Slab( -8, 0, TerrainPNG( 4, 0 ) ),
		          Slab( -8, 0, TerrainPNG( 6, 12 ) ),
		          Slab( -8, 0, TerrainPNG( 6, 13 ) ),
		          Slab( -8, 0, TerrainPNG( 7, 12 ) ),
		          Slab( 0, -8, TerrainPNG( 4, 0 ) ),
		          Slab( 0, -8, TerrainPNG( 6, 12 ) ),
		          Slab( 0, -8, TerrainPNG( 6, 13 ) ),
		          Slab( 0, -8, TerrainPNG( 7, 12 ) ));
		-- [127] -- Cocoa Plant
		[128] = Stairs( TerrainPNG( 0, 12 ), TerrainPNG( 0, 13 ), TerrainPNG( 0, 11 ) ); -- Sandstone Stairs
		[129] = OpaqueBlock( TerrainPNG( 11, 10 ) ); -- Emerald Ore
		-- [130] -- Ender Chest
		-- [131] -- Tripwire Hook
		-- [132] -- Tripwire
		[133] = OpaqueBlock( TerrainPNG( 9, 1 ) ); -- Block of Emerald
		[134] = Stairs( TerrainPNG( 6, 12 ) ); -- Spruce Wood Stairs
		[135] = Stairs( TerrainPNG( 6, 13 ) ); -- Birch Wood Stairs
		[136] = Stairs( TerrainPNG( 7, 12 ) ); -- Jungle Wood Stairs
		[137] = OpaqueBlock( TerrainPNG( 8, 11 ) ); -- Command Block
		[138] = HollowOpaqueBlock( TerrainPNG( 9, 2 ) ); -- Beacon Block
		-- [139] -- Cobblestone Wall
		-- [140] -- Flower Pot
		[141] = DataAdapter( 0x3, -- Carrots
		          Cactus( -4, TerrainPNG( 8, 12 ), 0 ),
				  Cactus( -4, TerrainPNG( 9, 12 ), 0 ),
				  Cactus( -4, TerrainPNG( 10, 12 ), 0 ),
				  Cactus( -4, TerrainPNG( 11, 12 ), 0 ) );
		[142] = DataAdapter( 0x3, -- Potatoes
		          Cactus( -4, TerrainPNG( 8, 12 ), 0 ),
				  Cactus( -4, TerrainPNG( 9, 12 ), 0 ),
				  Cactus( -4, TerrainPNG( 10, 12 ), 0 ),
				  Cactus( -4, TerrainPNG( 12, 12 ), 0 ) );
		-- [143] -- Wooden Button
	};

	for id, blk in pairs( MinecraftBlocks ) do
		blocks:setGeometry( id, blk[1] );
		blocks:setSolidity( id, blk[2] );
	end
	terrainImage:destroy();

	return blocks;
end




