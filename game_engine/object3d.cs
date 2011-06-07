using System;

using Horde3DNET;

namespace GameEngine {
  public class Object3D {
    public float tx { get; set; }
    public float ty { get; set; }
    public float tz { get; set; }
    public float rx { get; set; }
    public float ry { get; set; }
    public float rz { get; set; }
    public float sx { get; set; }
    public float sy { get; set; }
    public float sz { get; set; }
    
    public int node { get; set; }
    
    private Object3D() {
      tx = ty = tz = 0;
      rx = ry = rz = 0;
      sx = sy = sz = 1;
    }
    
    public static Object3D fromResource(int res) {
      return fromNode( H3d.addNodes( H3d.H3DRootNode, res ) );
    }
    
    public static Object3D fromNode(int node) {
      Object3D ret = new Object3D();
      ret.node = node;
      return ret;
    }
    
    public void debug() {
      Console.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",tx,ty,tz,rx,ry,rz,sx,sy,sz));
    }
    
    public void update() {
      H3d.setNodeTransform( node,
        tx, ty, tz,
        rx, ry, rz,
        sx, sy, sz
      );
    }
    
    public static double degToRad(float angle)
    {
      return angle * (System.Math.PI / 180.0f);
    }
    
    public void moveForward(float distance) {
      tx -= (float)(Math.Sin( degToRad( ry ) ) * Math.Cos( -degToRad( rx ) ) * distance);
      ty -= (float)Math.Sin( -degToRad( rx ) ) * distance;
      tz -= (float)(Math.Cos( degToRad( ry ) ) * Math.Cos( -degToRad( rx ) ) * distance);
    }

    public void moveBackward(float distance) {
      tx += (float)(Math.Sin( degToRad(  ry ) ) * Math.Cos( -degToRad( rx ) ) * distance);
      ty += (float)Math.Sin( -degToRad( rx ) ) * distance;
      tz += (float)(Math.Cos( degToRad(  ry ) ) * Math.Cos( -degToRad( rx ) ) * distance);
    }

    public void strafeLeft(float distance) {
      tx += (float)Math.Sin( degToRad( ry - 90) ) * distance;
      tz += (float)Math.Cos( degToRad( ry - 90 ) ) * distance;
    }

    public void strafeRight(float distance) {
      tx += (float)Math.Sin( degToRad( ry + 90 ) ) * distance;
      tz += (float)Math.Cos( degToRad( ry + 90 ) ) * distance;
    }

    public void strafeBackward(float distance) {
      tx += (float)Math.Sin( degToRad( ry ) ) * distance;
      tz += (float)Math.Cos( degToRad( ry ) ) * distance;
    }

    public void strafeForward(float distance) {
      tx += (float)Math.Sin( degToRad( ry +180) ) * distance;
      tz += (float)Math.Cos( degToRad( ry +180) ) * distance;
    }
  }
}
