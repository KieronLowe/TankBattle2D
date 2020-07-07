using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioClip TankShootSound;
    public static AudioClip MineExplosionSound;
    public static AudioClip CratePickupSound;
    public static AudioClip MineDropSound;
    public static AudioClip ButtonPushSound;
    public static AudioSource AudioSrc;
    public Slider VolumeSlider;

    void Start()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("VolumeLevel", VolumeSlider.value); 
        TankShootSound = Resources.Load<AudioClip>("TankShootSound");
        MineExplosionSound = Resources.Load<AudioClip>("MineExplosion");
        MineDropSound = Resources.Load<AudioClip>("DropMine");
        CratePickupSound = Resources.Load<AudioClip>("CratePickup");
        ButtonPushSound = Resources.Load<AudioClip>("ButtonPush");
        AudioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        AudioSrc.volume = VolumeSlider.value;
        PlayerPrefs.SetFloat("VolumeLevel", VolumeSlider.value);
    }

    public static void PlaySoundEffect (string clip)
    {
        switch (clip)
        {
            case "Shoot":
                AudioSrc.PlayOneShot(TankShootSound);
                break;
            case "MineExplosion":
                AudioSrc.PlayOneShot(MineExplosionSound);
                break;
            case "CratePickup":
                AudioSrc.PlayOneShot(CratePickupSound);
                break;
            case "DropMine":
                AudioSrc.PlayOneShot(MineDropSound);
                break;
            case "ButtonPush":
                AudioSrc.PlayOneShot(ButtonPushSound);
                break;
            default:
                break;
        }
    }
}
