require 'bin/engine.dll'
require 'bin/game_engine.dll'
include GameEngine
include Horde3DNET
include Horde3DNET::Utils
require 'yaml'
require 'ruby/helpers.rb'
puts Platform::Methods.getTime.to_s+"ms to ruby."

conf = YAML::load( File.read("ruby/config.yml") )
scene_load = YAML::load( File.read("ruby/scene02.yml") )

Platform::Methods.setWindow(
  conf['window_title'],
  conf['width'], conf['height'],
  conf['fullscreen']
)

H3d.init
class Object3D
  def to_yaml
    {
      'tx'=>tx.to_f,
      'ty'=>ty.to_f,
      'tz'=>tz.to_f,
      'rx'=>rx.to_f,
      'ry'=>ry.to_f,
      'rz'=>rz.to_f,
      'sx'=>sx.to_f,
      'sy'=>sy.to_f,
      'sz'=>sz.to_f
    }.to_yaml
  end
end

def onloaded_content(scene)
  sky = scene.addNode "skyBox"
  sky.sx = 210
  sky.sy = 50
  sky.sz = 210
  sky.update
  scene.addNode("env").update
  puts sky.to_yaml
end

puts H3d.getVersionString
puts Platform::Methods.getPlatform
puts Platform::Methods.getCpuFlags
scene = Scene.new
loaded = false
scene.load( convert_load( scene_load ) )
# scene.onloaded { onloaded_content(scene); loaded = true }
init = Platform::Methods.getTime
scene.loadFull "./content"
puts (Platform::Methods.getTime-init).to_s+" ms"
onloaded_content(scene)
scene.addLight(nil)
loaded = true

H3d.setOption(H3d::H3DOptions.DebugViewMode,0)

# Platform::Methods.setRelaunchAssembly("bin/test_events_csharp.exe")

running = true
while running do
  CoreEvents.fireEvent("onframe","")
  while evt = Platform::Methods.checkEvents do
    running = false if evt == 'quit'
    puts evt if evt
  end
  if loaded then
    scene.render
    Platform::Methods.swapBuffers
  end
end

Horde3DUtils.dumpMessages

H3d.release

