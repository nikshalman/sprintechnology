#pragma once

//this file is to put in all .hpp files only
//here will be common declarations from Common.hpp and type forward declarations



// Including SDKDDKVer.h defines the highest available Windows platform.

// If you wish to build your application for a previous Windows platform, include WinSDKVer.h and
// set the _WIN32_WINNT macro to the platform you wish to support before including SDKDDKVer.h.

#include <SDKDDKVer.h>

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
// Windows Header Files:
#include <windows.h>

#include <string>
#include <memory>
#include <filesystem>
#include "Shlwapi.h"
#include <iostream>
#include <chrono>
#include <sstream>
#include <thread>
#include <map>
#include <unordered_map>
#include <algorithm>
#include <set>


#include <Common.Util.hpp>



//typedef wstring String;
typedef unsigned __int64 ulong;


//---
#define _DLLAPI extern "C" __declspec(dllexport)
//+------------------------------------------------------------------+


typedef unsigned __int64 datetime; //time since 1970 with seconds only
typedef unsigned __int64 DateTime; //time since 1970 with millis



namespace NickyGark::CPP::Common
{
	
	class FileHandlerMdl;
	class FileHandlerCtr;

	class LoggerMdl;
	class LoggerCtr;
	class Logger;

	class BackupLogCtr;
	class BackupLogMdl;

	class TracerMdl;
	class TracerCtr;
	class Tracer;

}













