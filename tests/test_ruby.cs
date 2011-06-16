using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronRuby;
using IronRuby.Builtins;
using IronRuby.Runtime;

namespace GameEngine {
  public class TestRuby {
    public static int Main(string[] args) {
      var engine = IronRuby.Ruby.CreateEngine();

      var paths = engine.GetSearchPaths().ToList();
      foreach(string path in paths) {
        Console.WriteLine(path);
      }

      engine.ExecuteFile("ruby/test01.rb");

      return 0;
    }
  }
}


