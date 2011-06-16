using System;
using System.Collections;

namespace GameEngine {
  public class TestEvents {
    private static int acum = 0;
    private static int frames = 0;
    
    public static int Main(string[] args) {
      Platform.Methods.setWindow("test events",640,480,0);
      
      CoreEvents.onpostinit += new CoreEvent(delegate(object caller) {
        Console.WriteLine(Horde3DNET.H3d.getVersionString());
      });
      
      CoreEvents.onkeydown += new CoreEventData(delegate(object caller, Hashtable arg) {
        Console.WriteLine(
          "onkeydown -> state:{0}, scancode:{1}, unicode:{2}, sym:{3}, mod:{4}",
          arg["state"], arg["scancode"], arg["unicode"], arg["sym"], arg["mod"]
        );
      });
      
      CoreEvents.onkeyup += new CoreEventData(delegate(object caller, Hashtable arg) {
        Console.WriteLine(
          "onkeyup -> state:{0}, scancode:{1}, unicode:{2}, sym:{3}, mod:{4}",
          arg["state"], arg["scancode"], arg["unicode"], arg["sym"], arg["mod"]
        );
      });

      CoreEvents.onmousedown += new CoreEventData(delegate(object caller, Hashtable arg) {
        Console.WriteLine(
          "onmousedown -> x:{0}, y:{1}, state: {2}, button: {3}",
          arg["x"], arg["y"], arg["state"], arg["button"]
        );
      });

      CoreEvents.onmouseup += new CoreEventData(delegate(object caller, Hashtable arg) {
        Console.WriteLine(
          "onmouseup -> x:{0}, y:{1}, state: {2}, button: {3}",
          arg["x"], arg["y"], arg["state"], arg["button"]
        );
      });

      CoreEvents.onmousemove += new CoreEventData(delegate(object caller, Hashtable arg) {
        Console.WriteLine(
          "onmousemove -> x:{0}, y:{1}, xrel:{2}, yrel:{3}",
          arg["x"], arg["y"], arg["xrel"], arg["yrel"]
        );
      });
      
      CoreEvents.onframe += new CoreEvent(delegate(object caller) {
        acum += 1;
        frames += 1;
        if( acum>1000)
          Console.WriteLine( "frame " + frames );
      });

      CoreEvents.onendframe += new CoreEvent(delegate(object caller) {
        if( acum>1000 ) acum = 0;
      });

      CoreEvents.onend += new CoreEvent(delegate(object caller) {
        Console.WriteLine( "onend" );
      });
      
      for(int c=0;c<100000;c++) {
        Console.WriteLine(c);
      }
      
      return 0;
    }
  }
}
