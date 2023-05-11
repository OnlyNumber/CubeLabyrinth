using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _explosionSound;

    [ContextMenu("ActivateExplosion")]
    public void ActivateExplosion()
    {
        _explosionSound.Play();
    }
}
