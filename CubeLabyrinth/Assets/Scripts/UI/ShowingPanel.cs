using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShowingPanel : MonoBehaviour
{
    private void Start()
    {
        Fading();
    }

    public void Fading()
    {
        GetComponent<Image>().DOFade(0, 2);

        StartCoroutine(WaitingDestroy());
    }

    private IEnumerator WaitingDestroy()
    {
        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }

    public void Dark()
    {
        gameObject.SetActive(true);

        GetComponent<Image>().DOFade(1, 2);
    }


}
