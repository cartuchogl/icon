require 'bin/engine.dll'
require 'bin/game_engine.dll'
include GameEngine
include Horde3DNET

def kk(hash)
  retval = System::Collections::Hashtable.new
  hash.each do |k,v|
    retval.Add(k.to_s.to_clr_string,v.to_s.to_clr_string)
  end
  retval
end

def kkk(ary)
  System::Array[System::String].new(ary.map { |s| s.to_s.to_clr_string })
end

Platform::Methods.setWindow("ruby -> c++ -> CLR -> csharp -> DLR -> ruby",640,480,0)

scene = Scene.new

CoreEvents.onpostinit {|i| puts H3d.getVersionString }
CoreEvents.onpostinit do |i|
  tmp = System::Collections::Hashtable.new
  tmp.Add('Material'.to_clr_string,kk({
    'font' => "overlays/font.material.xml",
    'light' => "materials/light.material.xml"
  }))
  tmp.Add('SceneGraph'.to_clr_string,kk({
    'skyBox' => "models/skybox/skybox.scene.xml",
    'env' => "environments/001.scene.xml"
  }))
  tmp.Add('Pipeline'.to_clr_string,kkk([
    "pipelines/forward.pipeline.xml",
    "pipelines/deferred.pipeline.xml",
    "pipelines/hdr.pipeline.xml",
    "pipelines/custom.pipeline.xml"
  ]))
  scene.load(
    tmp
  )
  scene.loadFull("./content")
  scene.addNode("skyBox").update
  scene.addNode("env").update
end

def pformat(evt,hash)
  puts "#{evt} -> "+hash.keys.map{|k| "#{k}:#{hash[k]}"}.join(",")
end

CoreEvents.onkeydown {|obj,args| pformat("onkeydown",args) }
CoreEvents.onkeyup {|obj,args| pformat("onkeyup",args) }
CoreEvents.onmousedown {|obj,args| pformat("onmousedown",args) }
CoreEvents.onmouseup {|obj,args| pformat("onmouseup",args) }
CoreEvents.onmousemove {|obj,args| pformat("onmousemove",args) }
CoreEvents.onframe {|i| scene.render }
CoreEvents.onend {|i| puts "onend" }
