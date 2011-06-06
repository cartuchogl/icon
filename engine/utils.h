#ifndef _utils_h_
#define _utils_h_

extern "C" {
int PickNodes( int cameraNode, float nwx, float nwy );
int GetPickNodes(int index);
float GetPickNodesDistance(int index);
float GetPickNodesVector(int index,int elem);
float AABBminx(int node);
float AABBminy(int node);
float AABBminz(int node);
float AABBmaxx(int node);
float AABBmaxy(int node);
float AABBmaxz(int node);
}

#endif