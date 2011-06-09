require 'bin/engine.dll'
require 'bin/game_engine.dll'
include GameEngine
include Horde3DNET
require 'yaml'
require 'ruby/helpers.rb'

conf = YAML::load( File.read("ruby/config.yml") )
scene_load = YAML::load( File.read("ruby/scene01.yml") )
puts scene_load

Platform::Methods.setWindow(
  conf['window_title'],
  conf['width'], conf['height'],
  conf['fullscreen']
)

scene = Scene.new
dirty = false

CoreEvents.onpostinit do |i|
  puts H3d.getVersionString
  puts Platform::Methods.getPlatform
  puts Platform::Methods.getCpuFlags
end
CoreEvents.onpostinit do |i|
  scene.load( convert_load( scene_load ) )
  scene.loadFull "./content"
  sky = scene.addNode "skyBox"
  sky.sx = 210
  sky.sy = 50
  sky.sz = 210
  sky.update
  scene.addNode("env").update
end
CoreEvents.ondirtyinit {|i| dirty = true }
CoreEvents.onkeydown do |obj,args|
  pformat("onkeydown",args)
  unless dirty
    if args['scancode'] == 13 then
      scene.camera.moveForward(5)
    elsif args['scancode'] == 1 then
      scene.camera.moveBackward(5)
    elsif args['scancode'] == 0 then
      scene.camera.strafeLeft(5)
    elsif args['scancode'] == 2 then
      scene.camera.strafeRight(5)
    end
    scene.camera.update()
  end
end
CoreEvents.onkeyup do |obj,args|
  pformat("onkeyup",args)
  if args['scancode']==53 then
    Platform::Methods.quit()
  end
end
CoreEvents.onmousedown { |obj,args| pformat( "onmousedown", args ) }
CoreEvents.onmouseup { |obj,args| pformat( "onmouseup", args ) }
CoreEvents.onmousemove { |obj,args| pformat( "onmousemove", args ) }
CoreEvents.onframe { |i| scene.render }
CoreEvents.onend { |i| puts "onend" }
