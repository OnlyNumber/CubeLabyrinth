using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldActivation : MonoBehaviour
{
    private const int DEFAULT_LAYER = 0;

    private const int PLAYER_LAYER = 10;


    [SerializeField]
    private BoxCollider _boxCollider;

    [SerializeField]
    private float _maxTime;

    private float _currentTime;

    private bool isShieldActive;

    [SerializeField]
    private GameObject _playerAvatar;

    [SerializeField]
    private Material _playerMaterial;

    [SerializeField]
    private Color _casualColor;

    [SerializeField]
    private Color _shieldColor;

    private void Start()
    {
        _currentTime = _maxTime;
    }

    private void Update()
    {
        if (isShieldActive)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                OnUp();
            }
        }
    }


    public void OnDown()
    {
        if (_playerAvatar.activeInHierarchy)
        {
            gameObject.layer = DEFAULT_LAYER;


            _playerMaterial.color = _shieldColor;

            isShieldActive = true;
        }
    }

    public void OnUp()
    {
        if (_playerAvatar.activeInHierarchy)
        {

            gameObject.layer = PLAYER_LAYER;

            isShieldActive = false;

            _playerMaterial.color = _casualColor;

            _currentTime = _maxTime;
        }
    }


}
