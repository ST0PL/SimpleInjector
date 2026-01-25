#include <Windows.h>
#include <string>
#include "pch.h"

extern "C" __declspec(dllexport) void Initialize(LPCWSTR args) {
    auto m = args[0] + L"";
    MessageBoxW(NULL, L"Hello from c++!", L"Boo!", 0x0);
}

const wchar_t** Split(const wchar_t* string,  wchar_t separator) {
    std::string a = "1";

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

