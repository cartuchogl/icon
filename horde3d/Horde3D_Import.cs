// *************************************************************************************************
//
// h3d .NET wrapper
// ----------------------------------
// Copyright (C) 2007 Martin Burkhard
// Copyright (C) 2009 Volker Wiendl
//
//
// This software is distributed under the terms of the Eclipse Public License v1.0.
// A copy of the license may be obtained at: http://www.eclipse.org/legal/epl-v10.html
//
// *************************************************************************************************

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Horde3DNET
{
    /// <summary>
    /// Separates native methods from managed code.
    /// </summary>
    internal static class NativeMethodsEngine
    {
        // added (h3d 1.0)        

        // --- Basic funtions ---
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr h3dGetVersionString();        

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dCheckExtension(string extensionName);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dGetError();
        
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dInit();

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dRelease();

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dResizePipelineBuffers( int pipeRes, int width, int height );

        //horde3d 1.0
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dRender(int node);
        /////

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dFinalizeFrame();

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dClear();


        // --- General functions ---
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr h3dGetMessage(out int level, out float time);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern float h3dGetOption(H3d.H3DOptions param);


        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dSetOption(int param, float value);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern float h3dGetStat(int param, [MarshalAs(UnmanagedType.U1)]bool reset);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dShowOverlays(float[] verts, int vertCount, float colR, float colG, float colB, float colA, int material, int flags );

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dClearOverlays();

        // --- Resource functions ---
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dGetResType(int res);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr h3dGetResName(int res);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dGetNextResource(int type, int start);
        
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dFindResource(int type, string name);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dAddResource(int type, string name, int flags);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dCloneResource(int sourceRes, string name);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dRemoveResource(int res);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool h3dIsResLoaded(int res);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dLoadResource(int name, IntPtr data, int size);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dUnloadResource(int res);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dGetResElemCount(int res, int elem);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dFindResElem(int res, int elem, int param, string value );

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dGetResParamI(int res, int elem, int elemIdx, int param);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dSetResParamI(int res, int elem, int elemIdx, int param, int value);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern float h3dGetResParamF(int res, int elem, int elemIdx, int param, int compIdx);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dSetResParamF(int res, int elem, int elemIdx, int param, int compIdx, float value);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr h3dGetResParamStr(int res, int elem, int elemIdx, int param);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dSetResParamStr(int res, int elem, int elemIdx, int param, string value);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr h3dMapResStream(int res, int elem, int elemIdx, int stream, [MarshalAs(UnmanagedType.U1)]bool read, [MarshalAs(UnmanagedType.U1)]bool write);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dUnmapResStream(int res);
        
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dQueryUnloadedResource(int index);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dReleaseUnusedResources();

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dCreateTexture(string name, int width, int height, int fmt, int flags);

        // --- Shader specific ---
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dSetShaderPreambles( string vertPreamble, string fragPreamble );

        // --- Material specific ---
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dSetMaterialUniform(int materialRes, string name, float a, float b, float c, float d);


        //DLL bool h3dGetPipelineRenderTargetData( ResHandle pipelineRes, const char *targetName,
        //                              int bufIndex, int *width, int *height, int *compCount,
        //                              float *dataBuffer, int bufferSize );
        
        // --- Scene graph functions ---
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern H3d.H3DNodeTypes h3dGetNodeType(int node);        

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dGetNodeParent(int node);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dSetNodeParent(int node, int parent);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dGetNodeChild(int parent, int index);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dAddNodes(int parent, int res);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dRemoveNode(int node);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dGetNodeFlags(int node);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dSetNodeFlags(int node, int flags, [MarshalAs(UnmanagedType.U1)]bool recursive);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type
        internal static extern bool h3dCheckNodeTransFlag(int node, [MarshalAs(UnmanagedType.U1)]bool reset);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dGetNodeTransform(int node, out float px, out float py, out float pz,
                                out float rx, out float ry, out float rz, out float sx, out float sy, out float sz);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dSetNodeTransform(int node, float px, float py, float pz,
                                float rx, float ry, float rz, float sx, float sy, float sz);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]                
        internal static extern void h3dGetNodeTransMats(int node, out IntPtr relMat, out IntPtr absMat);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dSetNodeTransMat(int node, float[] mat4x4);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dGetNodeParamI(int node, int param);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dSetNodeParamI(int node, int param, int value);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern float h3dGetNodeParamF(int node, int param, int compIdx);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dSetNodeParamF(int node, int param, int compIdx, float value);
        
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr h3dGetNodeParamStr(int node, int param);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dSetNodeParamStr(int node, int param, string value);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dGetNodeAABB(int node, out float minX, out float minY, out float minZ, out float maxX, out float maxY, out float maxZ);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dFindNodes(int node, string name, int type);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dGetNodeFindResult(int index);
        
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dCastRay(int node, float ox, float oy, float oz, float dx, float dy, float dz, int numNearest);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dGetCastRayResult(int index, out int node, out float distance, float[] intersection);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dCheckNodeVisibility(int node, int cameraNode, [MarshalAs(UnmanagedType.U1)]bool checkOcclusion, [MarshalAs(UnmanagedType.U1)]bool calcLod);

        // Group specific
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dAddGroupNode(int parent, string name);

        // Model specific
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dAddModelNode(int parent, string name, int geoRes);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dSetupModelAnimStage(int node, int stage, int animationRes,
                                      int layer, string startNode, [MarshalAs(UnmanagedType.U1)]bool additive);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dSetModelAnimParams(int node, int stage, float time, float weight);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dSetModelMorpher(int node, string target, float weight);

        // Mesh specific
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dAddMeshNode(int parent, string name, int matRes, 
								    int batchStart, int batchCount,
							    int vertRStart, int vertREnd );
      
        // Joint specific
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dAddJointNode(int parent, string name, int jointIndex);

        // Light specific
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dAddLightNode(int parent, string name, int materialRes,
                                     string lightingContext, string shadowContext);

          // Camera specific
        //horde3d 1.0
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dAddCameraNode(int parent, string name, int pipelineRes);
        /////

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dSetupCameraView(int node, float fov, float aspect, float nearDist, float farDist);
            
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dGetCameraProjMat(int node, float[] projMat);


	    // Emitter specific
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dAddEmitterNode(int parent, string name,
								       int matRes, int effectRes,
								       int maxParticleCount, int respawnCount );

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]        
        internal static extern void h3dAdvanceEmitterTime(int node, float timeDelta);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dHasEmitterFinished(int emitterNode);

    }
}