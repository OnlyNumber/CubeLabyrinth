using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _deathParticle;

    [SerializeField]
    private ParticleSystem _boomParticle;

    [SerializeField]
    private Sprite _playerAvatar;

    [ContextMenu("Death")]
    public void DeathPlay()
    {
        Debug.Log("DeathPlay");
        _deathParticle.Play();
    }

    public void BoomPlay()
    {

        Debug.Log("BoomPlay");
        _boomParticle.Play();
    }


}
