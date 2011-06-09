#ifndef _definitions_h_
#define _definitions_h_

#if defined(__WIN32__) || defined(WIN32) || defined(__CYGWIN__)
	#define CALL_API __declspec(dllexport)
#else
	#define CALL_API
#endif

#endif