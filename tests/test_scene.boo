namespace GameEngine

import System

Platform.methods.setWindow("test scene",640,480,0)

scene as Scene

CoreEvents.onpostinit += def(obj,args):
  Console.WriteLine(Horde3DNET.h3d.getVersionString())
  scene = Scene()
  scene.load({
    'Material' : {
      'font' : "overlays/font.material.xml",
      'light' : "materials/light.material.xml"
    },
    'SceneGraph' : {
      'skyBox' : "models/skybox/skybox.scene.xml",
      'env' : "environments/001.scene.xml"
    },
    'Pipeline' : (
      "pipelines/forward.pipeline.xml",
      "pipelines/deferred.pipeline.xml",
      "pipelines/hdr.pipeline.xml",
      "pipelines/custom.pipeline.xml"
    )
  })
  scene.loadFull("./content")
  scene.addNode("env").update()
  sky = scene.addNode("skyBox")
  sky.sx = 210
  sky.sy = 50
  sky.sz = 210
  sky.update()
  
CoreEvents.onframe += def(obj,args):
  scene.render()

CoreEvents.onkeydown += def(obj,args):
  s = "onkeydown -> state:{0}, scancode:{1}, unicode:{2}, sym:{3}, mod:{4}"
  print s % (args['state'], args['scancode'], args['unicode'], args['sym'], args['mod'])

CoreEvents.onkeyup += def(obj,args):
  s = "onkeyup -> state:{0}, scancode:{1}, unicode:{2}, sym:{3}, mod:{4}"
  print s % (args['state'], args['scancode'], args['unicode'], args['sym'], args['mod'])
  if args['scancode'] == 53:
    Platform.methods.quit()

CoreEvents.onmousedown += def(obj,args):
  s = "onmousedown -> x:{0}, y:{1}, state: {2}, button: {3}"
  print s % (args['x'], args['y'], args['state'], args['button'])

CoreEvents.onmouseup += def(obj,args):
  s = "onmouseup -> x:{0}, y:{1}, state: {2}, button: {3}"
  print s % (args['x'], args['y'], args['state'], args['button'])

CoreEvents.onend += def(obj,args):
  print "onend"

