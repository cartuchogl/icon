namespace GameEngine

import System
import Horde3DNET.Utils

Platform.Methods.setWindow("test scene",640,480,0)

scene as Scene

CoreEvents.onpostinit += def(obj):
  Console.WriteLine(Horde3DNET.H3d.getVersionString())
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
    ),
    'Camera' : {
      'tx' : '59',
      'ty' : '11',
      'tz' : '-51',
      'rx' : '-20',
      'ry' : '135',
      'rz' : '0',
      'sx' : '1',
      'sy' : '1',
      'sz' : '1'
    }
  })
  scene.loadFull("./content")
  scene.addNode("env").update()
  sky = scene.addNode("skyBox")
  sky.sx = 210
  sky.sy = 50
  sky.sz = 210
  sky.update()
  
CoreEvents.onframe += def(obj):
  scene.render()

CoreEvents.onkeydown += def(obj,args):
  s = "onkeydown -> state:{0}, scancode:{1}, unicode:{2}, sym:{3}, mod:{4}"
  print s % (args['state'], args['scancode'], args['unicode'], args['sym'], args['mod'])
  if args['scancode'] == 13:
    scene.camera.moveForward(5)
  if args['scancode'] == 1:
    scene.camera.moveBackward(5)
  if args['scancode'] == 0:
    scene.camera.strafeLeft(5)
  if args['scancode'] == 2:
    scene.camera.strafeRight(5)
  scene.camera.update()

CoreEvents.onkeyup += def(obj,args):
  s = "onkeyup -> state:{0}, scancode:{1}, unicode:{2}, sym:{3}, mod:{4}"
  print s % (args['state'], args['scancode'], args['unicode'], args['sym'], args['mod'])
/*   if args['scancode'] == 53:
    Platform.Methods.quit() */

CoreEvents.onmousedown += def(obj,args):
  s = "onmousedown -> x:{0}, y:{1}, state: {2}, button: {3}"
  print s % (args['x'], args['y'], args['state'], args['button'])

CoreEvents.onmouseup += def(obj,args):
  s = "onmouseup -> x:{0}, y:{1}, state: {2}, button: {3}"
  print s % (args['x'], args['y'], args['state'], args['button'])

CoreEvents.onend += def(obj):
  Horde3DUtils.dumpMessages()
  print "onend"

kkk = 0

CoreEvents.onmousemove += def(obj,args):
  s = "onmousemove -> x:{0}, y:{1}, xrel:{2}, yrel:{3}"
  print s % (args['x'],args['y'],args['xrel'],args['yrel'])
  if kkk==0:
    kkk=1
  else:
    scene.camera.debug()
    scene.camera.rx -= int.Parse(args['yrel'].ToString())/2
    scene.camera.ry -= int.Parse(args['xrel'].ToString())/2
    scene.camera.update()
    scene.camera.debug()

