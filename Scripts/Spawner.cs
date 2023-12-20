using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<AppleCoin> _appleCoins;
    [SerializeField] private List<Transform> _spawnPoints;

    private int _delay = 2;

    private void Start()
    {
        _spawnPoints = new List<Transform>(_spawnPoints);
        StartCoroutine(SpawnAppleCoins());
    }

    private IEnumerator SpawnAppleCoins()
    {
        var spawn = Random.Range(0, _spawnPoints.Count);

        if(_appleCoins.Count <= _spawnPoints.Count)
        { 
            for (int i = 0; i < _appleCoins.Count; i++)
            {
                var waitForSeconds = new WaitForSeconds(_delay);
                Instantiate(_appleCoins[i], _spawnPoints[spawn].transform.position, Quaternion.identity);
                _spawnPoints.RemoveAt(spawn);
                yield return waitForSeconds;
            }
        }
    }
}
