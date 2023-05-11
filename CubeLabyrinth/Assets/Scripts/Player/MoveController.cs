using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using AOT;


public class MoveController : MonoBehaviour
{
    private float _speed;

    [SerializeField]
    private float _currentSpeed;

    [SerializeField]
    private Seeker _seeker;

    [SerializeField]
    private AIPath _aIPath;

    private List<Vector3> _currentPath = new List<Vector3>();

    private int targetPoint = 0;

    [SerializeField]
    private float delay;

    List<GraphNode> graphNodes;

    [SerializeField]
    private GameObject playerAvatar;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        StartCoroutine(DelayToMove());

    }

    


    [ContextMenu("CurrentPathCreate")]
    public List<Vector3> CurrentPathCreate()
    {

        //_currentPath.Clear();

        foreach (var item in graphNodes)
        {
            _currentPath.Add(new Vector3(item.position.x, item.position.y, item.position.z) / 1000);
        }

        return _currentPath;
    }

    void Update()
    {
        if (_currentPath.Count > 0 && targetPoint < _currentPath.Count)
            MoveForward();
    }


    public void RestartPathTarget()
    {
        targetPoint = 0;
    }

    private void MoveForward()
    {
        if (Vector3.Distance(transform.position, _currentPath[targetPoint]) > 0.1)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentPath[targetPoint], _speed * Time.deltaTime);
        }
        else
        {
            targetPoint++;
        }
    }

    [ContextMenu("SearchPath")]
    public void Search()
    {
        StartCoroutine(SearchPath());
    }

    public IEnumerator SearchPath()
    {
        _aIPath.SearchPath();

        graphNodes = _seeker.GetCurrentPath().path;
        yield return new WaitForSecondsRealtime(0.1f);

    }

    private IEnumerator DelayToMove()
    {
        yield return new WaitForSecondsRealtime(delay);

        _speed = _currentSpeed;

    }

    public void ChangeState()
    {
        if (_speed == 0)
        {
            playerAvatar.SetActive(true);

            _speed = _currentSpeed;
        }
        else
        {
            playerAvatar.SetActive(false);

            _speed = 0;
        }

    }


}
