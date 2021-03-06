require 'rake/clean'

CLEAN.include('engine/*.o','bin/icon')
CLOBBER.include('bin/icon')

MSVC = "/DWIN32 /Gm /RTC1 /nologo /EHsc /MD /D_WIN32 /DUNICODE /D_UNICODE /Zi"
MONO_WIN = "/D_MONO_PATH"

GCC = "cl.exe #{MSVC} #{MONO_WIN}"
GPP = "cl.exe #{MSVC} #{MONO_WIN}"

H3D = "..\\Horde3D_SDK_1.0.0_Beta5\\Horde3D"
HORDE3D = "/I#{H3D}\\Bindings\\C++"
H3DLIB = "#{H3D}\\..\\CMake\\Horde3D\\Source\\Horde3DEngine\\Debug\\Horde3D.lib"
H3DUTLIB = "#{H3D}\\..\\CMake\\Horde3D\\Source\\Horde3DUtils\\Debug\\Horde3DUtils.lib"
HORDE3DUTILS = "#{H3D}/../CMake/Horde3D/Source/Horde3DUtils/Horde3DUtils.framework/Versions/A/Horde3DUtils"

INC = "-I/opt/local/include -I#{H3D}/Bindings/C++/ -Iengine"

SDL_CFLAGS = "/I..\\SDL-1.2.14\\include"
SDL_LIBS = "..\\SDL-1.2.14\\lib\\SDL.lib ..\\SDL-1.2.14\\lib\\SDLmain.lib"

MONO_PATH = "..\\Mono-2.10.2\\"
MONO_INCLUDE = "/I#{MONO_PATH}include\\mono-2.0"
MONOLIB = "..\\mono.lib"

MONO_FLAGS = ""#`pkg-config --cflags --libs mono-2`[0..-2]
MONO_CFLAGS = ""#`pkg-config --cflags mono-2`[0..-2]
MONO = "dmcs"
BOO = "booc"
BOO_PATH = "/Library/Frameworks/Mono.framework/Versions/2.10.2/lib/mono/boo/"
RUBY_PATH = "/Library/Frameworks/Mono.framework/Versions/2.10.2/lib/ironruby"
RUBY_DLL = "#{RUBY_PATH}/bin/IronRuby.dll"
BOO_DLL = "#{BOO_PATH}/Boo.Lang.Interpreter.dll,#{BOO_PATH}/Boo.Lang.Compiler.dll,#{BOO_PATH}/Boo.Lang.dll"
GAME_ENGINE = "-r:bin/engine.dll,bin/game_engine.dll"

desc "Build engine"
task :default => ["bin\\icon.exe"]

SRC = FileList['engine/*.c','engine/*.cpp']
OBJ = SRC.ext('o')

rule '.o' => '.c' do |t|
  sh "#{GCC} /TC /c /Fo#{t.name} #{t.source} #{SDL_CFLAGS}"
end

rule '.o' => '.cpp' do |t|
  sh "#{GPP} /c /Fo#{t.name} #{t.source} #{SDL_CFLAGS} #{MONO_INCLUDE} #{HORDE3D}"
end

file "bin\\icon.exe" => OBJ do
  kk = OBJ.uniq.to_s.gsub("/","\\")
  puts kk
  sh "#{GPP} #{kk} #{SDL_LIBS} #{H3DLIB} #{H3DUTLIB} #{MONOLIB} /MD /link /SUBSYSTEM:CONSOLE /out:bin\\icon.exe"
end
  
# File dependencies go here ...
file 'engine\\platform.o' => ['engine\\platform.c', 'engine\\platform.h']
file 'engine\\utils.o' => ['engine\\utils.cpp', 'engine\\utils.h']

desc "Construye el binding en c# de horde"
task :build_csharp_horde3d_binding do
  sh "#{MONO} -target:library -out:bin/engine.dll engine/*.cs horde3d/*.cs"
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
