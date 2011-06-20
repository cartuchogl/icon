using System;
using System.Collections;
using Horde3DNET;
using Horde3DNET.Utils;

namespace GameEngine {
  public class TestEvents {
    private static int acum = 0;
    private static int frames = 0;
    
    public static int Main(string[] args) {
      Platform.Methods.setWindow("test events",640,480,0);
      
      H3d.init();

      Console.WriteLine(H3d.getVersionString());
      Console.WriteLine(Platform.Methods.getPlatform());
      Console.WriteLine(Platform.Methods.getCpuFlags());

      bool running = true;
      while(running) {
        string evt;
        while((evt = Platform.Methods.checkEvents())!=null) {
          if(evt == "quit") {
            running = false;
          }
          if(evt!=null) {
            Console.WriteLine(evt);
          }
        }
      }

      Horde3DUtils.dumpMessages();

      H3d.release();
      
      return 0;
    }
  }
}
