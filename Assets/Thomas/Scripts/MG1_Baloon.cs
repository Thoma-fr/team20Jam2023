using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MG1_Baloon : MonoBehaviour
{
    public MG1_BaloonType color { get;  set; }
    public int lives = 1;
    public TextMeshPro livestex;
    private void Start()
    {   if (lives > 1)
            livestex.text = lives.ToString();
        else
            livestex.text = "";
        var sprite=GetComponent<SpriteRenderer>();
        switch(color)
        {
            case MG1_BaloonType.Blue:
                sprite.color = Color.blue;
                break;
                case MG1_BaloonType.Green:
                sprite.color = Color.green; break;
                case MG1_BaloonType.Red:
                sprite.color = Color.red; break;
                case MG1_BaloonType.Yellow:
                sprite.color = Color.yellow; break;
        }
    }
    public void Explode()
    {
        lives--;
        if(lives <= 0)
            Destroy(gameObject);
    }
}
