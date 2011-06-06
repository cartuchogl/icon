#include "Horde3D.h"
#include "Horde3DUtils.h"

#include <stdio.h>

extern "C" {
	
int PickNodes( int cameraNode, float nwx, float nwy )
{	
	float ox, oy, oz, dx, dy, dz;
	h3dutPickRay( cameraNode, nwx, nwy, &ox, &oy, &oz, &dx, &dy, &dz );
	int num = h3dCastRay( H3DRootNode, ox, oy, oz, dx, dy, dz, 0 );
	return num;
}

int GetPickNodes(int index) {
		H3DNode intersectionNode = 0;
		h3dGetCastRayResult( index, &intersectionNode, 0, 0 );
		return intersectionNode;
}

float GetPickNodesDistance(int index) {
		H3DNode intersectionNode = 0;
		float distance;
		h3dGetCastRayResult( index, &intersectionNode, &distance, 0 );
		return distance;
}

float GetPickNodesVector(int index,int elem) {
		H3DNode intersectionNode = 0;
		float ary[3];
		h3dGetCastRayResult( index, &intersectionNode, 0, ary );
		return ary[elem];
}

float AABBminx(int node) {
	float minX,minY,minZ,maxX,maxY,maxZ;
	h3dGetNodeAABB( node, &minX, &minY, &minZ, &maxX, &maxY, &maxZ );
	return minX;
}

float AABBminy(int node) {
	float minX,minY,minZ,maxX,maxY,maxZ;
	h3dGetNodeAABB( node, &minX, &minY, &minZ, &maxX, &maxY, &maxZ );
	return minY;
}

float AABBminz(int node) {
	float minX,minY,minZ,maxX,maxY,maxZ;
	h3dGetNodeAABB( node, &minX, &minY, &minZ, &maxX, &maxY, &maxZ );
	return minZ;
}

float AABBmaxx(int node) {
	float minX,minY,minZ,maxX,maxY,maxZ;
	h3dGetNodeAABB( node, &minX, &minY, &minZ, &maxX, &maxY, &maxZ );
	return maxX;
}

float AABBmaxy(int node) {
	float minX,minY,minZ,maxX,maxY,maxZ;
	h3dGetNodeAABB( node, &minX, &minY, &minZ, &maxX, &maxY, &maxZ );
	return maxY;
}

float AABBmaxz(int node) {
	float minX,minY,minZ,maxX,maxY,maxZ;
	h3dGetNodeAABB( node, &minX, &minY, &minZ, &maxX, &maxY, &maxZ );
	return maxZ;
}

}