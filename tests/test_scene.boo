namespace GameEngine

import System
import Horde3DNET
import Horde3DNET.Utils

Platform.Methods.setWindow("test events",640,480,0)

H3d.init()

Console.WriteLine(H3d.getVersionString())
Console.WriteLine(Platform.Methods.getPlatform())
Console.WriteLine(Platform.Methods.getCpuFlags())

scene as Scene = Scene()
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

running = true
while running:
  while evt = Platform.Methods.checkEvents():
    running = false if evt == 'quit'
    Console.WriteLine(evt) if evt
  scene.render()
  Platform.Methods.swapBuffers()

Horde3DUtils.dumpMessages()

H3d.release()






