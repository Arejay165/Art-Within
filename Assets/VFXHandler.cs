using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class VFXHandler : MonoBehaviour
{
    public List<VisualEffectAsset> VFXAssets;

    //Make sure this lines up with the list ^
    public enum VFX
    {
        HealthRegen,
        KeyPickup,
        OnHit,
        PaintPickup
    }

    VisualEffect visualEffect;
    // Start is called before the first frame update
    void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
    }

    public void PlayVFX(VFX name)
    {
        if ((int)name >= VFXAssets.Count)
        {
            Debug.LogError("VFX IS NOT IN THE LIST");
            return;
        }
        visualEffect.visualEffectAsset = VFXAssets[(int)name];
        visualEffect.Play();
    }
}
