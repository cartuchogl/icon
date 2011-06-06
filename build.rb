def exec(str)
  puts(str)
  puts(`#{str}`)
end

SDL_CFLAGS = `sdl-config --cflags`
SDL_LIBS = `sdl-config --libs`
H3D = "../Horde3D/trunk/Horde3D"
HORDE3D = "#{H3D}/../CMake/Horde3D/Source/Horde3DEngine/Horde3D.framework/Versions/A/Horde3D"
HORDE3DUTILS = "#{H3D}/../CMake/Horde3D/Source/Horde3DUtils/Horde3DUtils.framework/Versions/A/Horde3DUtils"
INC = "-I/opt/local/include -I#{H3D}/Bindings/C++/ -Iengine"
GCC = "gcc -Wall -m32"
GPP = "g++ -Wall -m32"
MONO_FLAGS = `pkg-config --cflags --libs mono-2`
MONO = "mcs"
BOO = "booc"

exec "rm bin/*"

exec "#{GCC} engine/platform.c #{INC} -c -o bin/platform.o #{SDL_CFLAGS}"

exec "#{GPP} -o bin/icon bin/platform.o engine/main.cpp #{INC} -lm #{HORDE3D} #{HORDE3DUTILS} #{MONO_FLAGS[0..-2]} #{SDL_LIBS[0..-2]} #{SDL_CFLAGS}"

exec "#{MONO} -target:library -out:bin/engine.dll engine/*.cs horde3d/*.cs"

exec "#{BOO} tests/test_events.boo game_engine/core_events.boo -r:bin/engine.dll -o:bin/test_events.exe"
exec "#{BOO} tests/test_scene.boo game_engine/core_events.boo game_engine/scene.boo game_engine/object3d.boo -r:bin/engine.dll -o:bin/test_scene.exe"
