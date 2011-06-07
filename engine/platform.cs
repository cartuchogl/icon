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
    internal static extern void quit();
  }
  
  public static class Methods {
    public static int setWindow(string caption,int width,int height,int fullscreen){
      return NativeMethods.setWindow(caption,width,height,fullscreen);
    }
    
    public static void quit(){
      NativeMethods.quit();
    }
  }
}

