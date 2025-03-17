using Unity.VisualScripting;
using UnityEngine;

public static class GeneralVariables
{
    public static bool guiActive = false;
    public static int ISWALKINGFORWARD = Animator.StringToHash("isWalkingForward");
    public static int ISWALKINGBACK = Animator.StringToHash("isWalkingBack");
    public static int ISWALKINGLEFT = Animator.StringToHash("isWalkingLeft");
    public static int ISWALKINGRIGHT = Animator.StringToHash("isWalkingRight");
    public static int DODITHER = Shader.PropertyToID("_doDither");
    public static int CHARACTER =~ LayerMask.GetMask("Character");
    public static int GROUND = LayerMask.GetMask("Ground");
}
