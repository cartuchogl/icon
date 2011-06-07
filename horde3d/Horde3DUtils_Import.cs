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

namespace Horde3DNET.Utils
{
    /// <summary>
    /// Separates native methods from managed code.
    /// </summary>
    internal static class NativeMethodsUtils    
    {

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dutFreeMem(IntPtr ptr);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dutDumpMessages();

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dutInitOpenGL(int hDC);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dutReleaseOpenGL();

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dutSwapBuffers();

        // Utilities
        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr h3dutGetResourcePath(H3d.H3DResTypes type);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dutSetResourcePath(H3d.H3DResTypes type, string path);
             

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dutLoadResourcesFromDisk(string contentDir);


        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.U1)]   // represents C++ bool type 
        internal static extern bool h3dutCreateTGAImage(IntPtr pixels, uint width, uint height, uint bpp, out IntPtr outData, out uint outSize);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dutPickRay(int cameraNode, float nwx, float nwy,
            out float ox, out float oy, out float oz,
            out float dx, out float dy, out float dz);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern int h3dutPickNode(int node, float nwx, float nwy);

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dutShowText(string text, float x, float y, float size,
                                             float colR, float colG, float colB,
                                             int fontMatRes );

        [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
        internal static extern void h3dutShowFrameStats(int fontMaterialRes, int panelMaterialRes, int mode);
    }
}