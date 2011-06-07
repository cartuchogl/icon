using System;
using System.Collections;
using System.Collections.Generic;

using Horde3DNET;
using Horde3DNET.Utils;

namespace GameEngine {
  public class Scene {
    private List<int> pipelines;
    private Dictionary<string,int> materials;
    private Dictionary<string,int> scene_graph;
    
    public Object3D camera;
    
    public Scene() {}
    
    public void load(Hashtable hash) {
      pipelines = new List<int>();
      materials = new Dictionary<string,int>();
      scene_graph = new Dictionary<string,int>();
      
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
      
      if(hash.ContainsKey("Pipeline")) {
        Array kk = (Array)hash["Pipeline"];
        foreach(string str in kk) {
          pipelines.Add(H3d.addResource((int)H3d.H3DResTypes.Pipeline,str,0));
        }
      }
    }
    
    public void loadFull(string path) {
      Horde3DUtils.loadResourcesFromDisk( path );

      // TODO: hardcode camera
      // Add camera
      camera = Object3D.fromNode(H3d.addCameraNode( H3d.H3DRootNode, "Camera", pipelines[0] ));
      camera.tx = 59;
      camera.ty = 11;
      camera.tz = -51;
      camera.rx = -20;
      camera.ry = 135;
      camera.rz = 0;
      camera.sx = 1;
      camera.sy = 1;
      camera.sz = 1;
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
