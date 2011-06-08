/*
Cuando usas lenguajes dinámicos un tiempo, y vas al mundo del tipado estático, cuando el compilador
se queja de los tipos, es como, "Oh, lo siento, no nos habiamos presentado, permitame que le pegue
esta pegatina en la frente".
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using Horde3DNET;
using Horde3DNET.Utils;

namespace GameEngine {
  public class Scene {
    private List<int> pipelines;
    private Dictionary<string,int> materials;
    private Dictionary<string,int> scene_graph;
    private Dictionary<string,float> camera_init;
    
    public Object3D camera;
    
    public Scene() {}
    
    public void load(Hashtable hash) {
      pipelines = new List<int>();
      materials = new Dictionary<string,int>();
      scene_graph = new Dictionary<string,int>();
      camera_init = new Dictionary<string,float>();
      
      if(hash.ContainsKey("Material")) {
        IDictionaryEnumerator en = ((Hashtable)hash["Material"]).GetEnumerator();
        while (en.MoveNext()) {
          materials.Add((string)en.Key,H3d.addResource((int)H3d.H3DResTypes.Material,(string)en.Value,0));
        }
      }
      
      if(hash.ContainsKey("SceneGraph")) {
        IDictionaryEnumerator en = ((Hashtable)hash["SceneGraph"]).GetEnumerator();
        while (en.MoveNext()) {
          scene_graph.Add((string)en.Key,H3d.addResource((int)H3d.H3DResTypes.SceneGraph,(string)en.Value,0));
        }
      }
      
      if(hash.ContainsKey("Camera")) {
        IDictionaryEnumerator en = ((Hashtable)hash["Camera"]).GetEnumerator();
        while (en.MoveNext()) {
          camera_init.Add(
            (string)en.Key,
            float.Parse((string)en.Value, NumberStyles.Float, CultureInfo.InvariantCulture)
          );
        }
      }
      
      if(hash.ContainsKey("Pipeline")) {
        Array kk = (Array)hash["Pipeline"];
        foreach(string str in kk) {
          pipelines.Add(H3d.addResource((int)H3d.H3DResTypes.Pipeline, str, 0));
        }
      }
    }
    
    public void loadFull(string path) {
      Horde3DUtils.loadResourcesFromDisk( path );

      // TODO: hardcode camera
      // Add camera
      camera = Object3D.fromNode(H3d.addCameraNode( H3d.H3DRootNode, "Camera", pipelines[0] ));
      camera.tx = camera_init["tx"];
      camera.ty = camera_init["ty"];
      camera.tz = camera_init["tz"];
      camera.rx = camera_init["rx"];
      camera.ry = camera_init["ry"];
      camera.rz = camera_init["rz"];
      camera.sx = camera_init["sx"];
      camera.sy = camera_init["sy"];
      camera.sz = camera_init["sz"];
      camera.update();

      H3d.setNodeParamI(camera.node, (int)H3d.H3DCamera.ViewportXI, 0);
      H3d.setNodeParamI(camera.node, (int)H3d.H3DCamera.ViewportYI, 0);
      H3d.setNodeParamI(camera.node, (int)H3d.H3DCamera.ViewportWidthI, 640);
      H3d.setNodeParamI(camera.node, (int)H3d.H3DCamera.ViewportHeightI, 480);
      
      foreach(int pipe in pipelines) {
        H3d.resizePipelineBuffers( pipe, 640, 480 );
      }
      H3d.setupCameraView( camera.node, 45.0f,640f / 480f, 1, 1000.0f );
      Console.WriteLine("loaded from "+path);
    }
    
    public Object3D addNode(string name) {
      return Object3D.fromResource(scene_graph[name]);
    }
    
    public void render() {
      // Render scene
      H3d.render( camera.node );
      // Finish rendering of frame
      H3d.finalizeFrame();
      // Remove all overlays
      // H3d.clearOverlays();
    }
  }
}
