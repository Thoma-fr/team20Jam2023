using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG1_Sequence : MonoBehaviour
{

    Color[] colorAvalable = new Color[] {Color.red,Color.green,Color.blue,Color.yellow };
    [SerializeField] private List<GameObject> spawnedObject = new List<GameObject>();
    //[SerializeField] private List<Color> ColorSequence = { Color.red }
    public int NumberToSpawn{get; set;}
    public int speed { get; set;}

    void Start()
    {
        for (int i = 0; i < NumberToSpawn; i++) 
        { 
            
        }

    }
    void Update()
    {
        
    }
}
