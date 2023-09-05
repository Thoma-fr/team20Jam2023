using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MG1_BaloonParent : MonoBehaviour
{
    [SerializeField] private GameObject baloonPrefab;
    [SerializeField] private List<Vector2> spawnPoint = new List<Vector2>();
    private void Start()
    {
        //spawnBalon(3);newBaloon.GetComponent<MG1_Baloon>().color = (MG1_BaloonType)Random.Range(0, 3);
    }
    
    public void Checkcolor(MG1_BaloonType colorchossen)
    {
        bool hasFoundColor=false;
        foreach (Transform t in transform)
        {
            if(t.GetComponent<MG1_Baloon>().color == colorchossen)
            {
                t.GetComponent<MG1_Baloon>().Explode();
                Debug.Log("baloon destroyed");
                hasFoundColor = true;
                break;
            }
        }
        if (!hasFoundColor)
            Debug.Log("Stunt");


    }
    private void Update()
    {
        if (transform.childCount == 0)
        {
            transform.parent.GetComponent<MG1_Sequence>().spawnedObject.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    public void spawnBalon(int number)
    {
        ProcSpawnPoint(number);
        for (int i = 0; i < number; i++)
        {
            var newBaloon = Instantiate(baloonPrefab, transform);
            newBaloon.transform.position = spawnPoint[0];
            newBaloon.GetComponent<MG1_Baloon>().color = (MG1_BaloonType)Random.Range(0,3);
            spawnPoint.RemoveAt(0);
            if (number == 1)
            {
                int random= Random.Range(0,3);
                if(random == 2)
                newBaloon.GetComponent<MG1_Baloon>().lives= Random.Range(2, 4);
            }
        }
    }
    public void ProcSpawnPoint(int number)
    {
        if (number == 1)
            spawnPoint.Add(transform.position);
        else if (number == 2)
        {
            spawnPoint.Add(new Vector2(transform.position.x - 1.5f, transform.position.y));
            spawnPoint.Add(new Vector2(transform.position.x + 1.5f, transform.position.y));
        }
        else if (number == 3)
        {
            spawnPoint.Add(transform.position);
            spawnPoint.Add(new Vector2(transform.position.x - 1.5f, transform.position.y));
            spawnPoint.Add(new Vector2(transform.position.x + 1.5f, transform.position.y));
        }
    }
}
