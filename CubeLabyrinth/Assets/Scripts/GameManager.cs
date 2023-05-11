using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string RELOAD_SCENE = "GameScene";

    [SerializeField]
    private MoveController _moveController;

    private ParticleController _particleController;

    private SoundController _soundController;

    [SerializeField]
    private ShowingPanel _showingPanel;

    [SerializeField]
    private LabyrinthGenerator _labyrinthGenerator;

    [SerializeField]
    private Zone _finishZone;

    private void Start()
    {
        _particleController = _moveController.GetComponent<ParticleController>();

        _soundController = _moveController.GetComponent<SoundController>();

        _finishZone.OnTriggerAction += _showingPanel.Dark;

        _finishZone.OnTriggerAction += NextLevel;

        

        StartCoroutine(CreateLabyrinth());
    }


    private IEnumerator CreateLabyrinth()
    {
        List<Vector3> playerPath;

        yield return new WaitForSeconds(0.1f);

        _labyrinthGenerator.Scan();

        yield return StartCoroutine(_moveController.SearchPath());

        playerPath = _moveController.CurrentPathCreate();

        _labyrinthGenerator.CreateDeathZones(playerPath, RestartLevel);

        _labyrinthGenerator.SpawnPlayerPath(playerPath);


    }

    public void RestartLevel()
    {
        StartCoroutine(RestartLevelawfesawgawegawgawge());
    }

    private IEnumerator RestartLevelawfesawgawegawgawge()
    {
        _particleController.DeathPlay();

        _soundController.ActivateExplosion();

        _moveController.ChangeState();

        yield return new WaitForSeconds(1);

        _moveController.ChangeState();

        _moveController.transform.position = new Vector3(-10.5f, 0, -10.5f);

        _moveController.RestartPathTarget();

    }


    [ContextMenu("NextLevel")]
    private void NextLevel()
    {
        StartCoroutine(StartBoom());
        
    }

    private IEnumerator StartBoom()
    {
        _particleController.BoomPlay();

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(RELOAD_SCENE);

    }


}
