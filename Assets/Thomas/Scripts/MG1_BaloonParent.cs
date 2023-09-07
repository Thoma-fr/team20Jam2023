using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MG1_BaloonParent : MonoBehaviour
{
    [SerializeField] private GameObject baloonPrefab;
    [SerializeField] private List<Vector2> spawnPoint = new List<Vector2>();
    public MG1_Sequence spawner {  get; set; }
    public AudioClip missShot, explode;
    private void Start()
    {
        //spawnBalon(3);newBaloon.GetComponent<MG1_Baloon>().color = (MG1_BaloonType)Random.Range(0, 3);
        GetComponent<Rigidbody2D>().gravityScale = -spawner.speed;
    }
    
    public void Checkcolor(MG1_BaloonType colorchossen)
    {
        bool hasFoundColor=false;
        spawner.GunAnim();
        foreach (Transform t in transform)
        {
            if(t.GetComponent<MG1_Baloon>().color == colorchossen)
            {
                t.GetComponent<MG1_Baloon>().Explode();
                Debug.Log("baloon destroyed");
                hasFoundColor = true;
                spawner.numberPoped++;
                GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1f);

                GetComponent<AudioSource>().PlayOneShot(explode);

                break;
            }
        }
        if (!hasFoundColor)
        {
            Debug.Log("Stunt");

                GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1f);

            GetComponent<AudioSource>().PlayOneShot(missShot);
            spawner.StuntTrigger();
            ChangeOpacity();
        }
    }
    private void ChangeOpacity()
    {
        foreach (Transform t in transform)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append((t.GetComponent<SpriteRenderer>().DOFade(0.5f,spawner.stuntTime / 3)));
            mySequence.AppendInterval(spawner.stuntTime / 3);
            mySequence.Append((t.GetComponent<SpriteRenderer>().DOFade(1, spawner.stuntTime / 3)));
        }
    }
    private void Update()
    {
        if (transform.childCount == 0)
        {
            spawner.spawnedObject.Remove(gameObject);
            Destroy(gameObject,0.2f);
        }
    }
    public void spawnBalon(int number)
    {
        ProcSpawnPoint(number);
        for (int i = 0; i < number; i++)
        {
            var newBaloon = Instantiate(baloonPrefab, transform);
            newBaloon.transform.position = spawnPoint[0];
            newBaloon.GetComponent<MG1_Baloon>().color = (MG1_BaloonType)Random.Range(0,4);
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
            spawnPoint.Add(new Vector2(transform.position.x - 2f, transform.position.y));
            spawnPoint.Add(new Vector2(transform.position.x + 2f, transform.position.y));
        }
        else if (number == 3)
        {
            spawnPoint.Add(transform.position);
            spawnPoint.Add(new Vector2(transform.position.x - 2f, transform.position.y));
            spawnPoint.Add(new Vector2(transform.position.x + 2f, transform.position.y));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("spike"))
        {
            spawner.spawnedObject.Remove(gameObject);
            //foreach(GameObject go in spawner.spawnedObject)
            //{
            //    Destroy(go);
            //}
            //spawner.spawnedObject.Clear();
            Destroy(gameObject);
        }
    }
}
