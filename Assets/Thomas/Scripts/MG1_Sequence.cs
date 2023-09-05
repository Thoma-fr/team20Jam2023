using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BaloonType{
    Red,
    Green, 
    Blue,
    Yellow
};
public class MG1_Sequence : MonoBehaviour
{

    Color[] colorAvalable = new Color[] {Color.red,Color.green,Color.blue,Color.yellow };
    [SerializeField] private GameObject baloonPrefab= null;
    [SerializeField] private List<GameObject> spawnedObject = new List<GameObject>();
    [SerializeField] private List<Color> ColorSequence = new List<Color>();

    //la ou les couleurs voulus
    /*
     ça fais spawn des objet qui contiennent les balons, ces objet se focus seulement si le précédant a été validé,
    
    une liste d'object baloon parent, balon parent contient les balon associé, si la couleur correspond a l'input alors le parent éclate
    l'enfant 



     */


    public int NumberToSpawn{get; set;}
    public int speed { get; set;}
    // 1/3 deux balons spawn en même temps
    // 1/3 trois balons spawn en même temps
    void Start()
    {
        for (int i = 0; i < NumberToSpawn; i++) 
        {
            var newballon = Instantiate(baloonPrefab, transform.position, transform.rotation);
            spawnedObject.Add(newballon);
        }

    }
    void Update()
    {
        
    }
}
