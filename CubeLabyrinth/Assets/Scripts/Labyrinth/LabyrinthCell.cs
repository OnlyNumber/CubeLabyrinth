using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthCell : MonoBehaviour
{
    public int X;

    public int Y;

    [SerializeField]
    public GameObject leftWall;

    [SerializeField]
    public GameObject downWall;

    public bool IsVisited;


}
