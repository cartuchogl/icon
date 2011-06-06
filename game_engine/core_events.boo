namespace GameEngine

import System

import Boo.Lang.Interpreter from Boo.Lang.Interpreter

class CoreEvents:
  static event onpostinit as callable(object, object)
  static event onframe as callable(object, object)
  static event onendframe as callable(object, object)
  static event onend as callable(object, object)
  static event onkeydown as callable(object, Boo.Lang.Hash)
  static event onmousemove as callable(object, Boo.Lang.Hash)
  static event onkeyup as callable(object, Boo.Lang.Hash)
  static event onmousedown as callable(object, Boo.Lang.Hash)
  static event onmouseup as callable(object, Boo.Lang.Hash)
  
  static interpreter as InteractiveInterpreter
  
  static def fireEvent(str as string, args as string):
    try:
      interpreter = InteractiveInterpreter(RememberLastValue: true) if not interpreter
      interpreter.Eval(args)
      hash = interpreter.LastValue
      if str == 'onpostinit':
        CoreEvents.onpostinit(CoreEvents,hash) if CoreEvents.onpostinit
      elif str == 'onframe':
        CoreEvents.onframe(CoreEvents,hash) if CoreEvents.onframe
      elif str == 'onendframe':
        CoreEvents.onendframe(CoreEvents,hash) if CoreEvents.onendframe
      elif str == 'onend':
        CoreEvents.onend(CoreEvents,hash) if CoreEvents.onend
      elif str == 'onkeydown':
        CoreEvents.onkeydown(CoreEvents,hash) if CoreEvents.onkeydown
      elif str == 'onmousemove':
        CoreEvents.onmousemove(CoreEvents,hash) if CoreEvents.onmousemove
      elif str == 'onkeyup':
        CoreEvents.onkeyup(CoreEvents,hash) if CoreEvents.onkeyup
      elif str == 'onmousedown':
        CoreEvents.onmousedown(CoreEvents,hash) if CoreEvents.onmousedown
      elif str == 'onmouseup':
        CoreEvents.onmouseup(CoreEvents,hash) if CoreEvents.onmouseup
      else:
        print "unknow event ${str}"
    except e:
      print e
