using Settings;
using System.Collections;
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

    public static GameObject GetCC()
    {
        return Object.FindObjectsByType<GameObject>(findObjectsInactive: FindObjectsInactive.Include, sortMode: FindObjectsSortMode.None).Where((gameObject, b) => gameObject.name == "CC").ToArray()[0];
    }

    public static GameObject GetComponentInCC(string name)
    {
        return GetCC().GetComponentsInChildren<Transform>(includeInactive: true).Where((gameObject, b) => gameObject.name == name).ToArray()[0].gameObject;
    }

    public static GameObject GetPlayer()
    {
        return GetComponentInCC("Player");
    }

    public static GameObject GetRotator()
    {
        return GetComponentInCC("Rotator");
    }

    public static bool TryGetRotator(out GameObject rotator)
    {
        rotator = GetRotator();

        if (rotator)
            return true;
        return false;
    }
    
    public static GameObject GetCamera()
    {
        return GetComponentInCC("Camera");
    }
    public static GameObject GetInteractCanvas()
    {
        return Object.FindObjectsByType<GameObject>(findObjectsInactive: FindObjectsInactive.Include, sortMode: FindObjectsSortMode.None).Where((gameObject, b) => gameObject.name == "Interact").ToArray()[0];
    }

    public static GameObject GetGameObjectByName(string name)
    {
        return Object.FindObjectsByType<GameObject>(findObjectsInactive: FindObjectsInactive.Include, sortMode: FindObjectsSortMode.None).Where((gameObject, b) => gameObject.name == name).ToArray()[0];
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
