#ifndef _utils_h_
#define _utils_h_

#include "definitions.h"

extern "C" {
CALL_API int PickNodes( int cameraNode, float nwx, float nwy );
CALL_API int GetPickNodes(int index);
CALL_API float GetPickNodesDistance(int index);
CALL_API float GetPickNodesVector(int index,int elem);
CALL_API float AABBminx(int node);
CALL_API float AABBminy(int node);
CALL_API float AABBminz(int node);
CALL_API float AABBmaxx(int node);
CALL_API float AABBmaxy(int node);
CALL_API float AABBmaxz(int node);
}

#endif