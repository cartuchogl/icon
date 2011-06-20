namespace GameEngine

import System
import Horde3DNET
import Horde3DNET.Utils

Platform.Methods.setWindow("test events",640,480,0)

H3d.init()

Console.WriteLine(H3d.getVersionString())
Console.WriteLine(Platform.Methods.getPlatform())
Console.WriteLine(Platform.Methods.getCpuFlags())

running = true
while running:
  while evt = Platform.Methods.checkEvents():
    running = false if evt == 'quit'
    Console.WriteLine(evt) if evt

Horde3DUtils.dumpMessages()

H3d.release()

