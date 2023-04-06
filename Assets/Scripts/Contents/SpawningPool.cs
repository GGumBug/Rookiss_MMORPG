using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    private int     _monsterCount = 0;
    [SerializeField]
    private int     _keepMonsterCount = 0;
    private int     _reserveCount = 0;
    [SerializeField]
    float           _spawnRadius = 15.0f;
    [SerializeField]
    float           _spawnTime = 5.0f;

    [SerializeField]
    Vector3         _spawnPos;

    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }


    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        while (_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    private IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, _spawnTime));
        GameObject go = Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        NavMeshAgent nma = go.GetOrAddComponent<NavMeshAgent>();

        Vector3 randPos;

        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
            randDir.y = 0;
            randPos = _spawnPos + randDir;

            NavMeshPath path = new NavMeshPath();
            // CalculatePath 반환값이 true이면 갈 수 있는 길이라는 뜻
            if (nma.CalculatePath(randPos, path))
                break;
        }

        go.transform.position = randPos;
        _reserveCount--;
    }
}
