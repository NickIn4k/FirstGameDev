using UnityEngine;
using UnityEditor;
using System.IO;

public class EnableReadWriteMesh : EditorWindow
{
    [MenuItem("Tools/Enable Read-Write on All Meshes")]
    public static void EnableReadWrite()
    {
        string[] guids = AssetDatabase.FindAssets("t:Mesh"); // Trova tutti i mesh nel progetto
        int count = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;

            if (importer != null && !importer.isReadable) // Controlla se Read/Write è già abilitato
            {
                importer.isReadable = true; // Abilita Read/Write
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                count++;
            }
        }

        Debug.Log($"✅ Read/Write abilitato su {count} mesh.");
    }
}