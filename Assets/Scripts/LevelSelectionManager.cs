using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    public GameObject[] lvlButtons; //Array di sfere con cui si selezionano i livelli
    public Material lockedMaterial; //Materiale per i livelli bloccati
    public Material unlockedMaterial; //Materiale per i livelli sbloccati
    
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 3); //Il livello 1 (scena 3) è sbloccato da subito

        for(int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 3 > levelAt) //I livelli iniziano dalla scena 3 (Livello 1)
            {
                //Livelli non sbloccati (Rimuove il collider e modifica il materiale)
                lvlButtons[i].GetComponent<Collider>().enabled = false;
                lvlButtons[i].GetComponent<Renderer>().material = lockedMaterial;
            }
            else
            {
                //Livelli sbloccati
                lvlButtons[i].GetComponent<Collider>().enabled = true;
                lvlButtons[i].GetComponent<Renderer>().material = unlockedMaterial;
            }
        }
    }
}
