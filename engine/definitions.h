#ifndef _definitions_h_
#define _definitions_h_

#if defined(__WIN32__) || defined(WIN32) || defined(__CYGWIN__)
	#define CALL_API __declspec(dllexport)
#else
	#define CALL_API
#endif

#endif

#ifdef _MONO_PATH
#define _MONO_PATH_ETC "..\\..\\Mono-2.10.2\\etc"
#define _MONO_PATH_LIB "..\\..\\Mono-2.10.2\\lib"
#endif