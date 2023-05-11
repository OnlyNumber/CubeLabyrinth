using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FPSshow : MonoBehaviour
{
    public TMP_Text fpsShow;

    public int fps;


    private void Update()
    {
        fps = (int) (1f / Time.unscaledDeltaTime);

        fpsShow.text = fps.ToString();

    }
}
