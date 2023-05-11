using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LabyrinthGenerator : MonoBehaviour
{
    private int _width = 22;

    private int _height = 22;

    [SerializeField]
    private int _deathZonesCount;

    [SerializeField]
    private AstarPath _astarPath;

    [SerializeField]
    private GameObject _cell;

    [SerializeField]
    private Zone _deathZone;

    private LabyrinthCell[,] _labyrinthMatrix;

    [SerializeField]
    private GameObject _pathZone;

    [SerializeField]
    private int _rangeToStartOrFinish;


    private void Start()
    {
        _labyrinthMatrix = new LabyrinthCell[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _labyrinthMatrix[x, y] = Instantiate(_cell, new Vector3((float)x - 10.5f, 0, (float)y - 10.5f), Quaternion.identity, transform).GetComponent<LabyrinthCell>();

                _labyrinthMatrix[x, y].X = x;
                _labyrinthMatrix[x, y].Y = y;
                if (x == 0)
                    _labyrinthMatrix[x, y].leftWall.SetActive(false);
                if (y == 0)
                    _labyrinthMatrix[x, y].downWall.SetActive(false);

            }
        }

        Generation();

    }

    public void Generation()
    {
        LabyrinthCell currentCell = _labyrinthMatrix[0, 0];

        currentCell.IsVisited = true;

        Stack<LabyrinthCell> previousCells = new Stack<LabyrinthCell>();

        List<LabyrinthCell> unvisitedCells = new List<LabyrinthCell>();

        do
        {
            currentCell.IsVisited = true;

            unvisitedCells.Clear();

            if (currentCell.X + 1 < _width && !_labyrinthMatrix[currentCell.X + 1, currentCell.Y].IsVisited)
            {
                unvisitedCells.Add(_labyrinthMatrix[currentCell.X + 1, currentCell.Y]);
            }
            if (currentCell.X > 0 && !_labyrinthMatrix[currentCell.X - 1, currentCell.Y].IsVisited)
            {
                unvisitedCells.Add(_labyrinthMatrix[currentCell.X - 1, currentCell.Y]);
            }

            if (currentCell.Y + 1 < _width && !_labyrinthMatrix[currentCell.X, currentCell.Y + 1].IsVisited)
            {
                unvisitedCells.Add(_labyrinthMatrix[currentCell.X, currentCell.Y + 1]);
            }
            if (currentCell.Y > 0 && !_labyrinthMatrix[currentCell.X, currentCell.Y - 1].IsVisited)
            {
                unvisitedCells.Add(_labyrinthMatrix[currentCell.X, currentCell.Y - 1]);
            }

            if (unvisitedCells.Count > 0)
            {
                LabyrinthCell neighbourCell = unvisitedCells[UnityEngine.Random.Range(0, unvisitedCells.Count)];

                RemoveWallBetweenCells(currentCell, neighbourCell);

                previousCells.Push(neighbourCell);

                currentCell = neighbourCell;

                
            }
            else
            {
                currentCell = previousCells.Pop();
            }


        } while (previousCells.Count > 0);

    }

    private void RemoveWallBetweenCells(LabyrinthCell fromCell, LabyrinthCell toCell)
    {
        if (fromCell.X == toCell.X)
        {

            if (fromCell.Y < toCell.Y)
            {
                toCell.downWall.SetActive(false);
            }
            else
            {
                fromCell.downWall.SetActive(false);
            }

        }
        else
        {
            if (fromCell.X < toCell.X)
            {
                toCell.leftWall.SetActive(false);
            }
            else
            {
                fromCell.leftWall.SetActive(false);
            }
        }

    }

    public void Scan()
    {
        _astarPath.Scan();
    }

    public void SpawnPlayerPath(List<Vector3> playerPath)
    {
        GameObject zone;

        foreach(var node in playerPath)
        {
            zone = Instantiate(_pathZone, node, Quaternion.identity,transform);

            zone.transform.position = new Vector3(zone.transform.position.x, 0.01f, zone.transform.position.z);
        }

    }

    public void CreateDeathZones(List<Vector3> playerPath,   Action action )
    {
        Zone zone;

        int randomX;

        int randomY;

        


        for (int i = 0; i < _deathZonesCount; i++)
        {
            randomX = UnityEngine.Random.Range(_rangeToStartOrFinish, _width - _rangeToStartOrFinish);

            randomY = UnityEngine.Random.Range(_rangeToStartOrFinish, _height - _rangeToStartOrFinish);

            zone = Instantiate(_deathZone, _labyrinthMatrix[randomX,randomY].transform.position, Quaternion.identity, transform);

            zone.OnTriggerAction += action;

            zone.transform.position = new Vector3(zone.transform.position.x, 0.02f, zone.transform.position.z);
        }


        /*for (int i = 0; i < _deathZonesCount; i++)
        {
            zone = Instantiate(_deathZone, playerPath[UnityEngine.Random.Range(20, playerPath.Count - 20)], Quaternion.identity, transform);

            zone.OnTriggerAction += action;
            zone.transform.position = new Vector3(zone.transform.position.x, 0.02f, zone.transform.position.z);
        }*/
    }


}

