using GLTF.Schema;
using Settings;
using System.Linq;
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
    public static GameObject GetPlayer()
    {
        return GameObject.FindObjectsByType<GameObject>(findObjectsInactive: FindObjectsInactive.Include, sortMode: FindObjectsSortMode.None).Where((gameObject, b) => gameObject.name == "Player").ToArray()[0];
    }
    public static GameObject GetRotator()
    {
        return GameObject.FindObjectsByType<GameObject>(findObjectsInactive: FindObjectsInactive.Include, sortMode: FindObjectsSortMode.None).Where((gameObject, b) => gameObject.name == "Rotator").ToArray()[0];
    }

    public static bool TryGetRotator(out GameObject rotator)
    {
        GameObject[] arr = GameObject.FindObjectsByType<GameObject>(findObjectsInactive: FindObjectsInactive.Include, sortMode: FindObjectsSortMode.None).Where((gameObject, b) => gameObject.name == "Rotator").ToArray();
        if (arr.Length != 0){
            rotator = arr[0];
            return true;
        }
        rotator = null;
        return false;
    }

    public static GameObject GetCamera()
    {
        return GameObject.FindObjectsByType<GameObject>(findObjectsInactive: FindObjectsInactive.Include, sortMode: FindObjectsSortMode.None).Where((gameObject, b) => gameObject.name == "Camera").ToArray()[0];
    }
    public static GameObject GetInteractCanvas()
    {
        return GameObject.FindObjectsByType<GameObject>(findObjectsInactive: FindObjectsInactive.Include, sortMode: FindObjectsSortMode.None).Where((gameObject, b) => gameObject.name == "Interact").ToArray()[0];
    }

    #nullable enable
    public static void FreezeGame(GameObject? UI = null, GameObject? QuestUI = null, Collider? collider = null)
    {
        //Disattiva o attiva componenti UI
        GeneralVariables.guiActive = true;

        if (UI != null)
            UI.SetActive(true);
        if (QuestUI != null) 
            QuestUI.SetActive(false);
        if (collider != null)
            collider.enabled = false;
        if (UI != null && collider != null && UI.TryGetComponent<ItemClickerOld>(out var cd))
            cd.cld = collider;

        GetRotator().SetActive(false);

        //Time.timeScale = 0f;    //blocco il gioco
        CursorSettings.Unlock();
    }
    public static void ResumeGame(GameObject? UI = null, GameObject? QuestUI = null, Collider? collider = null)
    {
        //Riattiva la grafica di base
        GeneralVariables.guiActive = false;
        
        if (UI != null)
            UI.SetActive(false);
        if (QuestUI != null) 
            QuestUI.SetActive(true);
        if (collider != null)
            collider.enabled = true;

        GetRotator().SetActive(true);

        //Time.timeScale = 1f;
        CursorSettings.Lock();
    }

    #nullable disable
}
