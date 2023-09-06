using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG3_BellAnim : MonoBehaviour
{
    public float waitBetweenRot;
    public float angle;
    public float speed;
    public Ease ease;
    // Start is called before the first frame update
    void Start()
    {
        Sequence rotAnime = DOTween.Sequence();
        rotAnime.AppendInterval(waitBetweenRot);
        rotAnime.Append(transform.DORotate(new Vector3(0, 0, transform.rotation.z + angle), speed).SetEase(ease));
        rotAnime.Append(transform.DORotate(new Vector3(0, 0, transform.rotation.z - angle), speed).SetEase(ease));
        rotAnime.AppendInterval(waitBetweenRot).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
