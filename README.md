# icon engine

Try to build a flexible and powefull 3d game engine to build amazing games :D

License: MIT

Version: 0.1

Copyright: 2011 [Carlos Bolaños aka cartuchogl](http://urbin.es).

## Inspiration

- [blendelf](https://github.com/centralnoise/BlendELF)
- [udk](http://www.udk.com/)
- [unity3d](http://unity3d.com/unity/)
- [love](http://love2d.org/)
- [shoes](http://shoesrb.com/)
- [mono](http://www.mono-project.com/Embedding_Mono)
- [ruby](http://www.ruby-lang.org/)

## Dependencies

- [mono](http://mono-project.com/Main_Page)
- [horde3d](http://www.horde3d.org/)
- [sdl](http://www.libsdl.org/)
- OpenGL 2.0 capable graphics hardware (you don't have glsl, you don't have graphics)
  
## Compilation

### MacOSX

Download horde3d fron svn and build it.

Download mono and install it.

Use macport to build libsdl as universal (sudo macport install libsdl +universal)

Revisa el archivo Rakefile para comprobar que las rutas son correctas

    rake copy_needed # para copiar el motor de scripting desde mono
    rake all # para compilar el motor y los tests.

Ahora necesitas datos, hay un paquete que puedes descomprimir en el raiz en 
[github](https://github.com/downloads/cartuchogl/icon/content.zip)

Puedes ejecutar los tests usando desde el raiz

    ./bin/icon bin/test_[ruby|events|scene|events_csharp].exe bin/game_engine.dll
    
### Linux

Tested on Ubuntu 12.04

Need

- horde3d http://www.horde3d.org/
- ruby
- mono-complete
- boo
- IronRuby

WIP

### Windows

Needed

- Visual Studio C++ (tested on Visual C++ 2008 Express 9.0.3.729.1 SP)
- Download and install [git](http://code.google.com/p/msysgit/downloads/list)
- Download and install [ruby](http://rubyinstaller.org/)
- Download [Horde3D 1.0.0beta5](http://www.horde3d.org/download.html)
- Download [SDL](http://www.libsdl.org/download-1.2.php)
- Download [Mono](http://www.go-mono.com/mono-downloads/download.html)

Crea una carpeta para todo lo necesario, por ejemplo voy a crear una carpeta en el escritorio
que se llame icon_project. Dentro descomprimimos Horde3d y SDL, copiamos la carpeta que creó el
instalador de mono en "Program Files" Mono-2.10.2 tambien aquí. Usando la consola de git desde
la carpeta que hemos creado clonamos el repo

    git clone https://github.com/cartuchogl/icon.git

Vamos a SDL-1.2.14/include/SDL\_config_win32.h y entre las lineas 33 y 40 quitamos la palabra
signed de las lineas, quedaria asi:

    typedef __int8      int8_t;
    typedef unsigned __int8      uint8_t;
    typedef __int16      int16_t;
    typedef unsigned __int16   uint16_t;
    typedef __int32      int32_t;
    typedef unsigned __int32   uint32_t;
    typedef __int64      int64_t;
    typedef unsigned __int64   uint64_t;

[Referencia](http://ffmpeg.arrozcru.org/forum/viewtopic.php?f=1&t=598)

Para linkar con msvc necesitamos el lib de mono, podemos contruirlo descargando
https://github.com/mono/mono/blob/master/msvc/mono.def y desde la consola de VS ejecutando

    lib /machine:x86 /def:mono.def /out:mono.lib

[Referencia](http://mono.1490590.n4.nabble.com/Embedding-Mono-on-Windows-td3515710.html)

Deberiamos tener dentro de nuestra carpeta:

- Horde3D\_SDK_1.0.0_Beta5/
- SDL-1.2.14/
- Mono-2.10.2/
- icon/
- mono.lib

Ahora deberiamos poder compilar el ejecutable, creamos una carpeta bin en icon y usando la
consola de VS desde la carpeta del codigo, donde estan los Rakefiles.*, podemos ejecutar:

    rake

deberia compilarnos el código y dejarnos el ejecutable en bin. Ahora necesitamos:

- Horde3D.dll
- Horde3DUtils.dll
- mono-2.0.dll
- SDL.dll

Deberiamos poder ejecutar el programa en consola y nos mostraria un mensaje de error 
invitandonos a pasarle un assembly de dotNet.

Por ahora debes copiarlos desde otra instalación, necesitas:

- engine.dll
- game_engine.dll
- los ejecutables de los tests test_*.exe
- las librerias que copia la tarea copy_needed

El test_events.exe funciona, no he podido probar mas por que la maquina windows que tengo no
soporta glsl :(

## Notes

- On test_ruby the ruby code are just in time compiled usind DLR

## Features

- Cross-platform Windows, Linux, MacOSx
- Horde3D flexible rendering pipeline
- .NET scripting engine across mono posibility to write programs at csharp or ruby or whatever?

## TODO

- Compile on linux, WIP
- <del>Compile on Windows</del>
- Compile assemblies on windows
- Build packages
- Minimal package
- Mono packaged into app
