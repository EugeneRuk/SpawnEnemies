using System.Collections;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    [SerializeField] private float _spawnWaitingTimeInSeconds;
    [SerializeField] private Transform _spawnPointParent;
    [SerializeField] private Enemy[] _templates;

    private Transform[] _spawnPoints;

    private void OnValidate()
    {
        if (_spawnWaitingTimeInSeconds < 0)
        {
            _spawnWaitingTimeInSeconds = 0;
        }
    }

    private void Awake()
    {
        _spawnPoints = new Transform[_spawnPointParent.childCount];
        for (int i = 0; i < _spawnPointParent.childCount; i++)
        {
            _spawnPoints[i] = _spawnPointParent.GetChild(i);
        }
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var waitForSeconds = new WaitForSeconds(_spawnWaitingTimeInSeconds);
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            var createdEnemy = Instantiate(_templates[Random.Range(0, _templates.Length)], _spawnPoints[i].position, Quaternion.identity);
            yield return waitForSeconds;
        }
    }
}