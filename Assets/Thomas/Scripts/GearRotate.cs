using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GearRotate : MonoBehaviour
{
    public float waitBetweenRot;
    public float angle;
    public float speed;
    public Ease ease;
    void Start()
    {
        Sequence rotAnime = DOTween.Sequence();
        rotAnime.AppendInterval(waitBetweenRot);
        rotAnime.Append(transform.DORotate(new Vector3(0,0,transform.rotation.z+angle),speed).SetEase(ease));
        rotAnime.AppendInterval(waitBetweenRot).SetLoops(-1,LoopType.Incremental);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
