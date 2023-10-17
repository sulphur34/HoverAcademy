using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private Vehicle[] _vehicles;
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private Waypoint[] _waypoints;

    private Hover[] _enemies;
    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        Initialize(_vehicles);
    }

    private void Update()
    {
        SpawnEnemy(out GameObject result);
    }

    private void SpawnEnemy(out GameObject result)
    {
        int index = _random.Next(_vehicles.Length);

        if (TryGetObject(out GameObject enemy))
        {
            enemy.SetActive(true);
            enemy.transform.position = _spawnPositions[index].position;
        }

        result = enemy;
    }
    protected override GameObject GetRandomContainer(Vehicle[] prefabs)
    {
        int index = _random.Next(prefabs.Length);
        GameObject gameObject = prefabs[index].BuildHover(_container.transform);
        gameObject.GetComponent<Enemy>().Initialize(_waypoints, _player);
        return gameObject;
    }
}
