require 'rake/clean'

CLEAN.include('engine/*.o','bin/icon')
CLOBBER.include('bin/icon')

GCC = "gcc -Wall"
GPP = "g++ -Wall"

H3D = "../Horde3D_SDK_1.0.0_Beta5/Horde3D"
HORDE3D = "-lHorde3D"
HORDE3DUTILS = "-lHorde3DUtils"

INC = "-I/opt/local/include -I#{H3D}/Bindings/C++/ -Iengine"

SDL_CFLAGS = `sdl-config --cflags`[0..-2]
SDL_LIBS = `sdl-config --libs`[0..-2]

MONO_FLAGS = `pkg-config --cflags --libs mono-2`[0..-2]
MONO_CFLAGS = `pkg-config --cflags mono-2`[0..-2]
MONO = "dmcs"
BOO = "booc"
BOO_PATH = "/usr/lib/cli"
RUBY_PATH = "../IronRuby"
RUBY_DLL = "#{RUBY_PATH}/bin/IronRuby.dll"
BOO_DLL = "#{BOO_PATH}/Boo.Lang.Interpreter-2.0.9/Boo.Lang.Interpreter.dll,#{BOO_PATH}/Boo.Lang.Compiler-2.0.9/Boo.Lang.Compiler.dll,#{BOO_PATH}/Boo.Lang-2.0.9/Boo.Lang.dll"
GAME_ENGINE = "-r:bin/engine.dll,bin/game_engine.dll"

desc "Build engine"
task :default => ["bin/icon"]

SRC = FileList['engine/*.c','engine/*.cpp']
OBJ = SRC.ext('o')

rule '.o' => '.c' do |t|
  sh "#{GCC} #{INC} -c -o #{t.name} #{t.source} #{SDL_CFLAGS}"
end

rule '.o' => '.cpp' do |t|
  sh "#{GPP} #{INC} -c -o #{t.name} #{t.source} #{SDL_CFLAGS} #{MONO_CFLAGS}"
end

file "bin/icon" => OBJ do
  puts OBJ
  sh "#{GPP} -o bin/icon -rdynamic #{OBJ.uniq} #{INC} -lm #{HORDE3D} #{HORDE3DUTILS} #{MONO_FLAGS} #{SDL_LIBS} #{SDL_CFLAGS}"
end
  
# File dependencies go here ...
file 'engine/platform.o' => ['engine/platform.c', 'engine/platform.h']
file 'engine/utils.o' => ['engine/utils.cpp', 'engine/utils.h']

desc "Construye el binding en c# de horde"
task :build_csharp_horde3d_binding do
  sh "resgen /compile horde3d/Horde3D_Properties.resx"
  sh "#{MONO} -target:library -resource:horde3d/Horde3D_Properties.resources -out:bin/engine.dll engine/*.cs horde3d/*.cs"
end

desc "Construye el API del motor c#"
task :build_dotnet_game_engine do
  sh "#{MONO} -target:library -out:bin/game_engine.dll game_engine/*.cs -r:bin/engine.dll,#{BOO_DLL} -out:bin/game_engine.dll"
end

desc "Construye los tests"
task :build_tests do
  sh "#{BOO} tests/test_events.boo #{GAME_ENGINE} -o:bin/test_events.exe"
  sh "#{BOO} tests/test_scene.boo #{GAME_ENGINE} -o:bin/test_scene.exe"

  sh "#{MONO} tests/test_events.cs #{GAME_ENGINE},#{BOO_DLL} -out:bin/test_events_csharp.exe"
  sh "#{MONO} tests/test_ruby.cs #{GAME_ENGINE},#{BOO_DLL},#{RUBY_DLL} -out:bin/test_ruby.exe"
end

desc "Borra el contenido de bin"
task :clean_bin do
  sh "rm bin/*"
end

desc "Copia desde mono las librerias necesarias para el scripeo en ruby"
task :copy_needed do
  sh "cp -v #{RUBY_PATH}/bin/*.dll bin/"
  sh "cp -rv #{RUBY_PATH}/Lib ."
end

desc "All - :copy_needed"
task :all => ["bin/icon",:build_csharp_horde3d_binding,:build_dotnet_game_engine,:build_tests]
