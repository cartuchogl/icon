using System;
using System.Collections;

namespace GameEngine {
  public delegate void CoreEvent(object caller);
  public delegate void CoreEventData(object caller, Hashtable arguments);
  
  public class CoreEvents {
    public static event CoreEvent onpostinit;
    public static event CoreEvent ondirtyinit;
    public static event CoreEvent onframe;
    public static event CoreEvent onendframe;
    public static event CoreEvent onend;
    public static event CoreEventData onkeydown;
    public static event CoreEventData onmousemove;
    public static event CoreEventData onkeyup;
    public static event CoreEventData onmousedown;
    public static event CoreEventData onmouseup;
    
    private static Boo.Lang.Interpreter.InteractiveInterpreter interpreter;
    
    public static void fireEvent(string str,string args) {
      try {
        if(interpreter==null) {
          interpreter = new Boo.Lang.Interpreter.InteractiveInterpreter();
          interpreter.RememberLastValue = true;
        }
        interpreter.Eval(args);
        object data = interpreter.LastValue;
        switch(str) {
          case "onpostinit":
            if(CoreEvents.onpostinit!=null) CoreEvents.onpostinit(null);
            break;
          case "ondirtyinit":
            if(CoreEvents.ondirtyinit!=null) CoreEvents.ondirtyinit(null);
            break;
          case "onframe":
            if(CoreEvents.onframe!=null) CoreEvents.onframe(null);
            break;
          case "onendframe":
            if(CoreEvents.onendframe!=null) CoreEvents.onendframe(null);
            break;
          case "onend":
            if(CoreEvents.onend!=null) CoreEvents.onend(null);
            break;
          case "onkeydown":
            if(CoreEvents.onkeydown!=null) CoreEvents.onkeydown(null,(Hashtable)data);
            break;
          case "onmousemove":
            if(CoreEvents.onmousemove!=null) CoreEvents.onmousemove(null,(Hashtable)data);
            break;
          case "onkeyup":
            if(CoreEvents.onkeyup!=null) CoreEvents.onkeyup(null,(Hashtable)data);
            break;
          case "onmousedown":
            if(CoreEvents.onmousedown!=null) CoreEvents.onmousedown(null,(Hashtable)data);
            break;
          case "onmouseup":
            if(CoreEvents.onmouseup!=null) CoreEvents.onmouseup(null,(Hashtable)data);
            break;
          default:
            Console.WriteLine("unknow event "+str);
            break;
        }
      } catch(Exception ex) {
        Console.WriteLine(ex);
      }
    }
  }
}