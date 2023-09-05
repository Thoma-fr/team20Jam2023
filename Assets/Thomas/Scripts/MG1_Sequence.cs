using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MG1_BaloonType
{
    Red,
    Green, 
    Blue,
    Yellow
};
public class MG1_Sequence : MonoBehaviour
{

    Color[] colorAvalable = new Color[] {Color.red,Color.green,Color.blue,Color.yellow };
    [SerializeField] private GameObject baloonParentPrefab= null;
    public List<GameObject> spawnedObject = new List<GameObject>();
    [SerializeField] private List<Color> ColorSequence = new List<Color>();

    public int NumberToSpawn{get; set;}
    public int speed { get; set;}
    // 1/3 deux balons spawn en même temps
    // 1/3 trois balons spawn en même temps
    void Start()
    {
        StartCoroutine(spawndelay());

    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
            spawnedObject[0].GetComponent<MG1_BaloonParent>().Checkcolor(MG1_BaloonType.Red);
        else if(Input.GetKeyUp(KeyCode.DownArrow))
            spawnedObject[0].GetComponent<MG1_BaloonParent>().Checkcolor(MG1_BaloonType.Blue);
        else if( Input.GetKeyUp(KeyCode.LeftArrow))
            spawnedObject[0].GetComponent<MG1_BaloonParent>().Checkcolor(MG1_BaloonType.Yellow);
        else if (Input.GetKeyUp(KeyCode.RightArrow))
            spawnedObject[0].GetComponent<MG1_BaloonParent>().Checkcolor(MG1_BaloonType.Green);
    }
    public void SpawnBaloonParent()
    {
        NumberToSpawn=Random.Range(1,4);

            var newballonParent = Instantiate(baloonParentPrefab, transform);
            spawnedObject.Add(newballonParent);
            newballonParent.GetComponent<MG1_BaloonParent>().spawnBalon(NumberToSpawn);

    }
    private IEnumerator spawndelay()
    {
        SpawnBaloonParent();
        yield return new WaitForSeconds(3);
        
        StartCoroutine(spawndelay());
    }
}
