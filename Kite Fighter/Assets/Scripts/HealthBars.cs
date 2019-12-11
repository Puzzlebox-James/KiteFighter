using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    public Image currentHealthBar;
    public Text ratioText;

    public float minDamage = 10;


    private float hitpoint = 100;
    private float maxHitpoint = 100;

    private void Start()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float ratio = hitpoint / maxHitpoint;
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString("0") + "%";

    }

    public void TakeDamage(float IncBounsDmgMag)
    {
        hitpoint -= minDamage + IncBounsDmgMag;

        if(hitpoint < 0)
        {
            hitpoint = 0;
            Debug.Log("u deadboi");
        }

        UpdateHealthBar();
    }
}
