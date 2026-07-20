#include <Windows.h>
#include <string>
#include "pch.h"

extern "C" __declspec(dllexport) void Initialize(wchar_t* arg) {

    wchar_t* delimeter = wcschr(arg, ';');

    if (delimeter == nullptr)
        return;

    *delimeter = L'\0';

    MessageBoxW(NULL, (delimeter+1), arg, 0x0);
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

