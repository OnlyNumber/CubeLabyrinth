using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    [SerializeField]
    private GameObject opitonsPanel;

    public void ShowOptions()
    {


        if(opitonsPanel.activeInHierarchy)
        {
            opitonsPanel.SetActive(false);
        }
        else
        {
            opitonsPanel.SetActive(true);
        }
    }

    public void ChangeTime()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }


}
