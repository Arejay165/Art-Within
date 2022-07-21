using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Paint : MonoBehaviour
{
    [SerializeField] float paintMeter;
    [HideInInspector] public float paintMax = 150;
    public Action<float> OnPaintChanged;
   
    public AudioSource sfxSource;


    float originalPaint;
    private void Start()
    {
        originalPaint = paintMeter;
    }
    public void PlayPaintSFX()
    {
        sfxSource.Play();
    }

    public void StopPaintSFX()
    {
        sfxSource.Stop();
    }
    public float PaintMeter
    {
        get
        {
            return paintMeter;
        }

        set
        {
            paintMeter = Mathf.Clamp(value, 0, 150);
            OnPaintChanged?.Invoke(paintMeter);
        }
    }

    public void ResetPaint()
    {
        paintMeter = originalPaint;
    }
}
