using Settings;
using UnityEngine;

public static class GeneralMethods
{
    public static void CheckCursorLockMode()
    {
        if (GeneralVariables.guiActive)
            CursorSettings.Lock();
        else
            CursorSettings.Unlock();
    }
}
