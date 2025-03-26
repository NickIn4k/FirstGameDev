using UnityEngine;

namespace Global_Variables
{
    [CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Objects/MapData")]
    public class MapData : ScriptableObject
    {
        public Vector3 ground;
    }
}