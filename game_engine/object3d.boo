namespace GameEngine

import Horde3DNET

class Object3D:  
  [Property(tx)]
  _tx as double = 0
  [Property(ty)]
  _ty as double = 0
  [Property(tz)]
  _tz as double = 0
  [Property(rx)]
  _rx as double = 0
  [Property(ry)]
  _ry as double = 0
  [Property(rz)]
  _rz as double = 0
  [Property(sx)]
  _sx as double = 1
  [Property(sy)]
  _sy as double = 1
  [Property(sz)]
  _sz as double = 1
  
  [Property(node)]
  _node as int
  
  def constructor(resource as int):
    node = h3d.addNodes( h3d.H3DRootNode, resource )
    
  def update():
    h3d.setNodeTransform( node,
      tx, ty, tz,
      rx, ry, rz,
      sx, sy, sz
    )