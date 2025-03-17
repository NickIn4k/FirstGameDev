using UnityEngine;

public class PlayerDitherSettings : MonoBehaviour
{
    public void SetDither(bool dither)
    {
        foreach (var skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            skinnedMeshRenderer.materials[0].SetInt(GeneralVariables.DODITHER, dither ? 1 : 0);
        }
    }
}
