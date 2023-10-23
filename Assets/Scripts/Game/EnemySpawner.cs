using System.Collections;
using UnityEngine;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private Vehicle[] _vehicles;
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private Waypoint[] _waypoints;

    private Player _player;
    
    public IEnumerator SpawnEnemies(float enemiesAmount)
    {
        _player = FindObjectOfType<Player>();
        Initialize(_vehicles, enemiesAmount);

        for (int i = 0; i < enemiesAmount; i++)
        {
            TrySpawnEnemy(out GameObject result);
            yield return new WaitForSeconds(2);
        }
    }

    private bool TrySpawnEnemy(out GameObject result)
    {
        int index = _random.Next(_vehicles.Length);

        if (TryGetObject(out GameObject enemy))
        {
            enemy.SetActive(true);
            enemy.transform.position = _spawnPositions[index].position;
            result = enemy;
            return true;
        }

        result = null;
        return false;
    }

    protected override GameObject GetRandomContainer(Vehicle[] prefabs)
    {
        int index = _random.Next(prefabs.Length);
        Hover hover = prefabs[index].BuildHover(transform, typeof(Player));
        hover.gameObject.GetComponent<Enemy>().Initialize(_waypoints, _player);
        return hover.gameObject;
    }
}
