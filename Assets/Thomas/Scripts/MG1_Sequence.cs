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
    [SerializeField,Tooltip("le numero du joueur(player 1 ou 2)"),Range(1,2)] private int playerID;
    [SerializeField] private GameObject baloonParentPrefab= null;
    //[SerializeField, Tooltip("nombre que tu veux fair spawn")] private int numberToPop;
    [SerializeField ,Range(0, 1)] private float acceleration;
    [Tooltip("temp pendant combien de temps tu peut plus faire d'input")] public float stuntTime;
    [Tooltip("vitesse des baloon"), Range(0, 3)] public float speed;
    [SerializeField, Tooltip("vitesse des baloon"), Range(0, 3)] public float SpawnSpeed;
    [HideInInspector]public List<GameObject> spawnedObject = new List<GameObject>();

    //public float speed;
    private bool canInput=true;
    public int NumberToSpawn{get; set;}
 

    
    public int numberPoped { get; set;}
    // 1/3 deux balons spawn en même temps
    // 1/3 trois balons spawn en même temps
    void Start()
    {
        StartCoroutine(spawndelay());

    }
    void Update()
    {
        SpawnSpeed -= GameManager.instance.currentTime * acceleration*Time.deltaTime;
        if (canInput&& spawnedObject.Count>0)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
                spawnedObject[0].GetComponent<MG1_BaloonParent>().Checkcolor(MG1_BaloonType.Red);
            else if (Input.GetKeyUp(KeyCode.DownArrow))
                spawnedObject[0].GetComponent<MG1_BaloonParent>().Checkcolor(MG1_BaloonType.Blue);
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
                spawnedObject[0].GetComponent<MG1_BaloonParent>().Checkcolor(MG1_BaloonType.Yellow);
            else if (Input.GetKeyUp(KeyCode.RightArrow))
                spawnedObject[0].GetComponent<MG1_BaloonParent>().Checkcolor(MG1_BaloonType.Green);
        }

    }
    public void StuntTrigger()
    {
        StartCoroutine(Stunt());
    }
    public void SpawnBaloonParent()
    {
        
        NumberToSpawn=Random.Range(1,4);

        var newballonParent = Instantiate(baloonParentPrefab, transform.position,Quaternion.identity);
        spawnedObject.Add(newballonParent);
        newballonParent.GetComponent<MG1_BaloonParent>().spawner= this;
        newballonParent.GetComponent<MG1_BaloonParent>().spawnBalon(NumberToSpawn);

    }
    private IEnumerator spawndelay()
    {
        SpawnBaloonParent();
        yield return new WaitForSeconds(SpawnSpeed);
        //if(numberPoped<= numberToPop)
            //fair la fin du minijeu

        StartCoroutine(spawndelay());
    }
    private IEnumerator Stunt()
    {   canInput=false;
        yield return new WaitForSeconds(stuntTime);
        canInput=true;
    }
}
