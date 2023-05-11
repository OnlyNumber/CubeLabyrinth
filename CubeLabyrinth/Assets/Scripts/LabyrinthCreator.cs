using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using AOT;

public class LabyrinthCreator : MonoBehaviour
{
    [SerializeField]
    private List<Obstacle> _obstacles;

    [SerializeField]
    private int _numberOfObstacles;

    [SerializeField]
    private float _widthOfPlane;

    [SerializeField]
    private float _highOfPlane;

    private List<Obstacle> _obstaclesPool = new List<Obstacle>();

    [SerializeField]
    private AstarPath _astarPath;

    [ContextMenu("Scan")]
    public void Scan()
    {
        _astarPath.Scan();
    }



    [ContextMenu("SpawnObstacles")]
    public void SpawnObstacles()
    {
        int randomNumber;


        for (int i = 0; i < _numberOfObstacles; i++)
        {
            randomNumber = Random.Range(0, _obstacles.Count);

            _obstaclesPool.Add(Instantiate(_obstacles[randomNumber], new Vector3(Random.Range(-_widthOfPlane, _widthOfPlane),0.5f, Random.Range(-_highOfPlane, _highOfPlane)), Quaternion.identity, transform ));

            //Debug.Log(_obstaclesPool[i].gameObject.transform.rotation);

            //_obstaclesPool[i].transform.Rotate(new Vector3(0, Random.Range(0,180), 0));


        }

        //_astarPath.Scan();

        //Scan();


    }

    public void DestroyAllObstacles()
    {

        foreach (var item in _obstaclesPool)
        {
            if(item != null)
            Destroy(item.gameObject);

        }

    }


}
