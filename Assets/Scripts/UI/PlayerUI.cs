using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider HPBar;
    [SerializeField] Slider InkBar;

    [SerializeField] Image HPBorder;
    [SerializeField] Image PaintBorder;

    Color HPBorderColor;
    Color PaintBorderColor;

    Coroutine Flashing;

    GameObject player;
    HealthComponent playerHealth;
    Paint paintComp;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        playerHealth = player.GetComponent<HealthComponent>();
        paintComp = player.GetComponent<Paint>();

        if(HPBorder != null)
        {
            HPBorderColor = HPBorder.color;
        }
         else
        {
            Debug.Log("Paint Border is null, please check.");
        }
       
        if(PaintBorder != null)
        {
            PaintBorderColor = PaintBorder.color;
        }
        else
        {
            Debug.Log("Paint Border is null, please check.");
        }
      

        InkBar.maxValue = paintComp.paintMax;
        InkBar.value = paintComp.PaintMeter;
        paintComp.OnPaintChanged += (paint) =>
        {
            InkBar.value = paint;

            if (paint == 0)
            {
                if (Flashing != null)
                {
                    StopCoroutine(Flashing);
                }
                if (PaintBorder != null)
                    Flashing = StartCoroutine(FlashBar(PaintBorder, PaintBorderColor));
            }
        };

        HPBar.maxValue = playerHealth.MaxHP;
        HPBar.value = HPBar.maxValue;

        playerHealth.ReduceHealthUI += (hp) => 
        {
            HPBar.value -= hp;

            if (Flashing != null)
            {
                StopCoroutine(Flashing);
            }


            if(HPBorder != null)
            Flashing = StartCoroutine(FlashBar(HPBorder, HPBorderColor));
        };


        playerHealth.OnRegen += (hp) => HPBar.value += hp;
        playerHealth.OnSetHP += (hp) => HPBar.value = hp;
    }

    IEnumerator FlashBar(Image bar, Color originalColor)
    {
        for (int i = 0; i < 4; i++)
        {
            bar.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            bar.color = originalColor;
            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnBarChanged (Slider bar, float value)
    {
        bar.value = value / 100;
    }
}
