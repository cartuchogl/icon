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
MONO = "dmcs"
BOO = "booc"
BOO_PATH = "/Library/Frameworks/Mono.framework/Versions/2.10.2/lib/mono/boo/"
RUBY_PATH = "/Library/Frameworks/Mono.framework/Versions/2.10.2/lib/ironruby/bin"
RUBY_DLL = "#{RUBY_PATH}/IronRuby.dll"
BOO_DLL = "#{BOO_PATH}/Boo.Lang.Interpreter.dll,#{BOO_PATH}/Boo.Lang.Compiler.dll,#{BOO_PATH}/Boo.Lang.dll"

exec "rm bin/*"
exec "cp -v #{RUBY_PATH}/*.dll bin/"

exec "#{GCC} engine/platform.c #{INC} -c -o bin/platform.o #{SDL_CFLAGS}"

exec "#{GPP} -o bin/icon bin/platform.o engine/main.cpp #{INC} -lm #{HORDE3D} #{HORDE3DUTILS} #{MONO_FLAGS[0..-2]} #{SDL_LIBS[0..-2]} #{SDL_CFLAGS}"

exec "#{MONO} -target:library -out:bin/engine.dll engine/*.cs horde3d/*.cs"

# exec "#{BOO} -target:library -o:bin/game_engine.dll game_engine/*.boo -r:bin/engine.dll"
exec "#{MONO} -target:library -out:bin/game_engine.dll game_engine/*.cs -r:bin/engine.dll,#{BOO_DLL} -out:bin/game_engine.dll"

exec "#{BOO} tests/test_events.boo -r:bin/engine.dll,bin/game_engine.dll -o:bin/test_events.exe"
exec "#{BOO} tests/test_scene.boo -r:bin/engine.dll,bin/game_engine.dll -o:bin/test_scene.exe"

exec "#{MONO} tests/test_events.cs -r:bin/engine.dll,bin/game_engine.dll,#{BOO_DLL} -out:bin/test_events_csharp.exe"
exec "#{MONO} tests/test_ruby.cs -r:bin/engine.dll,bin/game_engine.dll,#{BOO_DLL},#{RUBY_DLL} -out:bin/test_ruby.exe"

