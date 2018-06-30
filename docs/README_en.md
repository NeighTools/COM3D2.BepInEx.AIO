# Sybaris 2 to BepInEx migration pack

BepInEx is an open-source, free alternative to Sybaris 2. It works on any game and is actively supported by its developers.  

For more information about BepInEx, visit the [official project's wiki on GitHub](https://github.com/BepInEx/BepInEx/wiki).

## About this package

This is a basic AIO pack for COM3D2 that migrates your Sybaris 2 installation to BepInEx.  

The package contains

* BepInEx.SybarisLoader ([GitHub](https://github.com/BepInEx/BepInEx.SybarisLoader.Patcher)) -- a plug-in that emulates Sybaris' patching capabilities
* BepInEx.UnityInjectorLoader ([GitHub](https://github.com/BepInEx/BepInEx.UnityInjectorLoader)) -- a plug-in that emulates UnityInjector in BepInEx
* SybarisMigrator ([GitHub](https://github.com/NeighTools/COM3D2.BepInEx.AIO)) -- a tool that automatically migrates Sybaris to BepInEx

This package is optimized for COM3D2 and Sybaris 2, but it can be used on CM3D2 as well.

## How to install

1. Move the contents of this archive into COM3D2/CM3D2 root folder. Overwrite if asked.
2. Run `SybarisMigrator.exe`
3. Follow the migration guide in the opened window


## Credits

Licence for BepInEx, SybarisLoader and UnityInjectorLoader:

```
MIT License

Copyright (c) 2018 Bepis

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

Licence for SybarisMigrator

```
MIT License

Copyright (c) 2018 Geoffrey Horsington

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```