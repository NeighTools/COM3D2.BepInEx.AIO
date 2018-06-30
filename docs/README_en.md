# Sybaris 2 to BepInEx migration pack

BepInEx is an open-source, free alternative to Sybaris 2. It works on any game and is actively supported by its developers.  

For more information about BepInEx, visit the [official project's wiki on GitHub](https://github.com/BepInEx/BepInEx/wiki).

## About this package

This is a basic AIO pack for COM3D2 that migrates your Sybaris 2 installation to BepInEx.  

The package contains

* BepInEx.SybarisLoader ([GitHub](https://github.com/BepInEx/BepInEx.SybarisLoader.Patcher)) -- a plug-in that emulates Sybaris' patching capabilities
* BepInEx.UnityInjectorLoader ([GitHub](https://github.com/BepInEx/BepInEx.UnityInjectorLoader)) -- a plug-in that emulates UnityInjector in BepInEx
* SybarisMigrator ([GitHub](https://github.com/NeighTools/COM3D2.BepInEx.AIO)) -- a tool that automatically migrates Sybaris to BepInEx

This package is optimized for COM3D2 and Sybaris 2, but it can be used on CM3D2 as well (as long as it uses Sybaris 2).

## How to install

1. Move the contents of this archive into COM3D2/CM3D2 root folder. Overwrite if asked.
2. Run `SybarisMigrator.exe`
3. Follow the migration guide in the opened window

## Restoring back to Sybaris 2

1. Remove all the files/folders you copied
2. Move all folders from `sybaris_old` folder into the game's root folder. Overwrite if needed.
3. Remove `sybaris_migrator.lock`

## Credits

BepInEx, UnityInjecorLoader and SybarisLoader are licensed to Bepis under the MIT licence.  
SybarisMigrator is licensed to Geoffrey Horsington under the MIT licence.