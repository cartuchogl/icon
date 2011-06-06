namespace GameEngine

import Horde3DNET
import Horde3DNET.Utils

class Scene:
  [Property(Loaded)]
  _loaded as Hash
  
  [Property(Pipelines)]
  _pipelines as List
  
  [Property(Cam)]
  _cam as int
  
  [Property(Camera)]
  _camera as int
  
  def load(hash as Hash):
    _loaded = {'Material':{},'SceneGraph':{}}
    _pipelines = []
    if hash.ContainsKey('Material'):
      for item as System.Collections.DictionaryEntry in hash['Material']:
        print item.Key, ":", item.Value
        (_loaded['Material'] as Hash)[item.Key] = h3d.addResource(cast(int,h3d.H3DResTypes.Material),item.Value as string,0)
    if hash.ContainsKey('SceneGraph'):
      for item as System.Collections.DictionaryEntry in hash['SceneGraph']:
        print item.Key, ":", item.Value
        (_loaded['SceneGraph'] as Hash)[item.Key] = h3d.addResource(cast(int,h3d.H3DResTypes.SceneGraph),item.Value as string,0)
    if hash.ContainsKey('Pipeline'):
      for i in hash['Pipeline']:
        print i
        _pipelines.Add(h3d.addResource( cast(int,h3d.H3DResTypes.Pipeline), i as string, 0 ))
  
  def loadFull(path as string):
    Horde3DUtils.loadResourcesFromDisk( path )
    
    // Add camera
    _cam = h3d.addCameraNode( h3d.H3DRootNode, "Camera", _pipelines[0] )
    
    h3d.setNodeParamI(_cam, cast(int,h3d.H3DCamera.ViewportXI), 0)
    h3d.setNodeParamI(_cam, cast(int,h3d.H3DCamera.ViewportYI), 0)
    h3d.setNodeParamI(_cam, cast(int,h3d.H3DCamera.ViewportWidthI), 640)
    h3d.setNodeParamI(_cam, cast(int,h3d.H3DCamera.ViewportHeightI), 480)
    
    for pipe in _pipelines:
      h3d.resizePipelineBuffers( pipe, 640, 480 )
    
    h3d.setupCameraView( _cam, 45.0, 640 / 480, 1, 1000.0 );
    print "loaded from ${path}"
    
  def addNode(name):
    return Object3D(h3d.addNodes( h3d.H3DRootNode, (_loaded['SceneGraph'] as Hash)[name] ))
    
  def render():
    // Render scene
    h3d.render( _cam )
    // Finish rendering of frame
    h3d.finalizeFrame()
    // Remove all overlays
    // h3d.ClearOverlays();

 