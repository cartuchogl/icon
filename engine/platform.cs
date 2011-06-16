using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Platform
{
  internal static class NativeMethods    
  {
    [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
    internal static extern int setWindow(string caption,int width,int height,int fullscreen);
    [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
    internal static extern void setWindowTitle(string caption);
    [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
    internal static extern int getWidth();
    [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
    internal static extern int getHeight();
    [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
    internal static extern int getTime();
    [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
    internal static extern int swapBuffers();
    [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
    internal static extern string getPlatform();
    [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
    internal static extern string getCpuFlags();
    [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
    internal static extern string checkEvents();
    [DllImport("__Internal"), SuppressUnmanagedCodeSecurity]
    internal static extern void setRelaunchAssembly(string assembly);
  }
  
  public static class Methods {
    public static int setWindow(string caption,int width,int height,int fullscreen){
      return NativeMethods.setWindow(caption,width,height,fullscreen);
    }
    
    public static int getWidth() {
      return NativeMethods.getWidth();
    }
    
    public static int getHeight() {
      return NativeMethods.getHeight();
    }
    
    public static int getTime() {
      return NativeMethods.getTime();
    }
    
    public static void swapBuffers() {
      NativeMethods.swapBuffers();
    }
    
    public static string getPlatform() {
      return NativeMethods.getPlatform();
    }
    
    public static string getCpuFlags() {
      return NativeMethods.getCpuFlags();
    }
    
    public static string checkEvents() {
      return NativeMethods.checkEvents();
    }
    
    public static void setRelaunchAssembly(string assembly) {
      Console.WriteLine("cs:"+assembly);
      NativeMethods.setRelaunchAssembly(assembly);
    }
  }
}

