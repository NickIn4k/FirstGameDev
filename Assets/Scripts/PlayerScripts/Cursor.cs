using UnityEngine;

namespace Settings
{
    public static class CursorSettings
    {
        public static void Lock()
        {
            GeneralVariables.guiActive--;
            
            
            if (GeneralVariables.guiActive < 0)
                GeneralVariables.guiActive = 0;
            
            Debug.Log(GeneralVariables.guiActive + " Cursor Lock");
            
            if (GeneralVariables.guiActive == 0)
                Cursor.lockState = CursorLockMode.Locked;
        }    

        public static void Unlock()
        {
            GeneralVariables.guiActive++;
            
            Debug.Log(GeneralVariables.guiActive + " Cursor Unlock");
            
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

