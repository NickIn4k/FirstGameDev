using UnityEngine;

namespace Settings
{
    public static class CursorSettings
    {
        public static void Lock()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }    

        public static void Unlock()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

