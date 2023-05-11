using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LabyrinthGenerator : MonoBehaviour
{
    private int width = 22;

    private int height = 22;

    [SerializeField]
    private int deathZonesCount;

    [SerializeField]
    private AstarPath _astarPath;

    [SerializeField]
    private GameObject _cell;

    [SerializeField]
    private Zone deathZone;

    private LabyrinthCell[,] _labyrinthMas;



    private void Start()
    {
        _labyrinthMas = new LabyrinthCell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _labyrinthMas[x, y] = Instantiate(_cell, new Vector3((float)x - 10.5f, 0, (float)y - 10.5f), Quaternion.identity, transform).GetComponent<LabyrinthCell>();

                _labyrinthMas[x, y].X = x;
                _labyrinthMas[x, y].Y = y;
                if (x == 0)
                    _labyrinthMas[x, y].leftWall.SetActive(false);
                if (y == 0)
                    _labyrinthMas[x, y].downWall.SetActive(false);

            }
        }

        Generation();

    }

    public void Generation()
    {
        LabyrinthCell currentCell = _labyrinthMas[0, 0];

        currentCell.IsVisited = true;

        Stack<LabyrinthCell> previousCells = new Stack<LabyrinthCell>();

        List<LabyrinthCell> unvisitedCells = new List<LabyrinthCell>();

        do
        {
            unvisitedCells.Clear();

            if (currentCell.X + 1 < width && !_labyrinthMas[currentCell.X + 1, currentCell.Y].IsVisited)
            {
                unvisitedCells.Add(_labyrinthMas[currentCell.X + 1, currentCell.Y]);
            }
            if (currentCell.X - 1 >= 0 && !_labyrinthMas[currentCell.X - 1, currentCell.Y].IsVisited)
            {
                unvisitedCells.Add(_labyrinthMas[currentCell.X - 1, currentCell.Y]);
            }

            if (currentCell.Y + 1 < width && !_labyrinthMas[currentCell.X, currentCell.Y + 1].IsVisited)
            {
                unvisitedCells.Add(_labyrinthMas[currentCell.X, currentCell.Y + 1]);
            }
            if (currentCell.Y - 1 >= 0 && !_labyrinthMas[currentCell.X, currentCell.Y - 1].IsVisited)
            {
                unvisitedCells.Add(_labyrinthMas[currentCell.X, currentCell.Y - 1]);
            }

            if (unvisitedCells.Count > 0)
            {


                LabyrinthCell neighbourCell = unvisitedCells[UnityEngine.Random.Range(0, unvisitedCells.Count)];

                currentCell.IsVisited = true;

                RemoveWallBetweenCells(currentCell, neighbourCell);

                previousCells.Push(neighbourCell);

                currentCell = neighbourCell;
            }
            else
            {
                currentCell = previousCells.Pop();
            }


        } while (previousCells.Count != 0);

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

    public LabyrinthCell[,] GenerateLabyrinth()
    {
        LabyrinthCell[,] labyrinth = new LabyrinthCell[width, height];

        for (int x = 0; x < labyrinth.GetLength(0); x++)
        {
            for (int y = 0; y < labyrinth.GetLength(1); y++)
            {
                //labyrinth[x,y] = new 
            }
        }

        return labyrinth;

    }

    public void Scan()
    {
        _astarPath.Scan();
    }

    public void CreateDeathZones(List<Vector3> playerPath,   Action action )
    {
        Zone zone;

        for (int i = 0; i < deathZonesCount; i++)
        {
            zone = Instantiate(deathZone, playerPath[UnityEngine.Random.Range(20, playerPath.Count - 20)], Quaternion.identity, transform);

            zone.OnTriggerAction += action;
            zone.transform.position = new Vector3(zone.transform.position.x, 0.1f, zone.transform.position.z);



        }
    }


}

