# Sybaris 2 to BepInEx migration pack

BepInEx is an open-source, free alternative to Sybaris 2. It works on any game and is actively supported by its developers.  

For more information about BepInEx, visit the [official project's wiki on GitHub](https://github.com/BepInEx/BepInEx/wiki).

## About this package

This is a basic AIO pack for COM3D2 that migrates your Sybaris 2 installation to BepInEx.  

The package contains

* BepInEx.SybarisLoader ([GitHub](https://github.com/BepInEx/BepInEx.SybarisLoader.Patcher)) -- a plug-in that emulates Sybaris' patching capabilities
* BepInEx.UnityInjectorLoader ([GitHub](https://github.com/BepInEx/BepInEx.UnityInjectorLoader)) -- a plug-in that emulates UnityInjector in BepInEx
* SybarisMigrator ([GitHub](https://github.com/NeighTools/COM3D2.BepInEx.AIO)) -- a tool that automatically migrates Sybaris to BepInEx

This package is optimized for COM3D2 and Sybaris 2.

## How to install

1. Move the contents of this archive into COM3D2/CM3D2 root folder. Overwrite if asked.

    > ⚠️ **IMPORTANT**
    >
    > It is **absolutely** crucial that you also move `SybarisMigrator.exe` to the game folder.

2. Run `SybarisMigrator.exe` **from the game's folder**.
3. Follow the migration guide in the opened window.

## How to install plug-ins

**NOTE**: Some plug-ins are only made for Sybaris/UnityInjector and some only for BepInEx. Please refer to the plug-in's README on how to install.

### BepInEx plug-ins and patchers

To install BepInEx plug-ins, drop them into `BepInEx` folder.  
To install BepInEx patchers, drop them into `BepInEx\patchers` folder.

You can configure plug-ins after you run the game once. The configuration file is `BepInEx\config.ini`.


### Sybaris/UnityInjector

To install UnityInjector plug-ins, drop them into `Sybaris\UnityInjector` folder.  
To intall Sybaris patchers, drop them into `Sybaris` folder.

You can configure plug-ins after you run the game once. The configuration file is located in `Sybaris\UnityInjector\Config` folder.

## Restoring back to Sybaris 2

1. Remove all the files/folders you copied
2. Move all folders from `sybaris_old` folder into the game's root folder. Overwrite if needed.
3. Remove `sybaris_migrator.lock`

## Credits

BepInEx, UnityInjecorLoader and SybarisLoader are licensed to Bepis under the MIT licence.  
SybarisMigrator is licensed to Geoffrey Horsington under the MIT licence.