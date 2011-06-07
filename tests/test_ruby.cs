using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronRuby;
using IronRuby.Builtins;
using IronRuby.Runtime;

namespace GameEngine {
  class TestRuby {
    static int Main(string[] args) {
      var runtime = Ruby.CreateRuntime();
      var engine = runtime.GetRubyEngine();

      engine.Execute(@"
      require 'ruby/test01.rb'
      ");

/*      engine.Execute("class Foo; def bar; puts 'hello from bar'; end; end");
      object o = engine.Execute("Foo.new");
      var operations = engine.CreateOperations();
      string s2 = operations.InvokeMember(o, "bar") as string; 
      Console.WriteLine(s2);*/
      // outputs "hello from bar"
      return 0;
    }
  }
}


