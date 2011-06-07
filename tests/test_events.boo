namespace GameEngine

import System

Platform.Methods.setWindow("test events",640,480,0)

CoreEvents.onpostinit += def(obj):
  Console.WriteLine(Horde3DNET.H3d.getVersionString())

CoreEvents.onkeydown += def(obj,args):
  s = "onkeydown -> state:{0}, scancode:{1}, unicode:{2}, sym:{3}, mod:{4}"
  print s % (args['state'], args['scancode'], args['unicode'], args['sym'], args['mod'])

CoreEvents.onkeyup += def(obj,args):
  s = "onkeyup -> state:{0}, scancode:{1}, unicode:{2}, sym:{3}, mod:{4}"
  print s % (args['state'], args['scancode'], args['unicode'], args['sym'], args['mod'])

CoreEvents.onmousedown += def(obj,args):
  s = "onmousedown -> x:{0}, y:{1}, state: {2}, button: {3}"
  print s % (args['x'], args['y'], args['state'], args['button'])

CoreEvents.onmouseup += def(obj,args):
  s = "onmouseup -> x:{0}, y:{1}, state: {2}, button: {3}"
  print s % (args['x'], args['y'], args['state'], args['button'])

CoreEvents.onmousemove += def(obj,args):
  s = "onmousemove -> x:{0}, y:{1}, xrel:{2}, yrel:{3}"
  print s % (args['x'],args['y'],args['xrel'],args['yrel'])
  
acum = 0
frames = 0

CoreEvents.onframe += def(obj):
  acum += 1
  frames += 1
  print "frame ${frames}" if acum>1000

CoreEvents.onendframe += def(obj):
  acum = 0 if acum>1000

CoreEvents.onend += def(obj):
  print "onend"

