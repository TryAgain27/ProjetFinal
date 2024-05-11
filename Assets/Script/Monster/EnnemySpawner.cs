using System.Collections;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    protected EnnemyFactory ennemyFactory;
    protected PlayerController player;

    private static EnnemySpawner instance;

    public static EnnemySpawner GetInstance() => instance;

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            player = PlayerController.GetInstance();
            ennemyFactory = EnnemyFactory.GetInstance();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //Appeler seulement si il existe deja un autre EnnemySpawner
        }
    }


    //--------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        StartCoroutine(SpawnLotsEnemyCoroutine());
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private IEnumerator SpawnLotsEnemyCoroutine()
    {
        do
        {
            SpawnEnemies(ennemyFactory.CreateWeakEnnemy(), 25 + (player.Level * 2));
            SpawnEnemies(ennemyFactory.CreateWeakEnnemy(), 40 + (player.Level * 2), false);
            SpawnEnemies(ennemyFactory.CreateWeakEnnemy(), 30 + (player.Level * 2));
            SpawnEnemiesElCombator(ennemyFactory.CreateDistanceEnnemy(), 5 + (player.Level * 2));
            yield return new WaitForSeconds(10);
            SpawnEnemies(ennemyFactory.CreateWeakEnnemy(), 35 + (player.Level * 2));
            SpawnEnemiesElCombator(ennemyFactory.CreateDistanceEnnemy(), 7 + (player.Level * 2));
            SpawnEnemies(ennemyFactory.CreateWeakEnnemy(), 20 + (player.Level * 2));
            SpawnEnemies(ennemyFactory.CreateWeakEnnemy(), 45 + (player.Level * 2), false);
            yield return new WaitForSeconds(10);
            SpawnEnemies(ennemyFactory.CreateWeakEnnemy(), 30 + (player.Level * 2), false);
            SpawnEnemies(ennemyFactory.CreateWeakEnnemy(), 25 + (player.Level * 2));
            SpawnEnemiesElCombator(ennemyFactory.CreateDistanceEnnemy(), 10 + (player.Level * 2));
            SpawnEnemies(ennemyFactory.CreateBossEnnemy(), 5);
            SpawnEnemies(ennemyFactory.CreateWeakEnnemy(), 15 + (player.Level * 2));
            yield return new WaitForSeconds(10);
            SpawnEnemies(ennemyFactory.CreateWeakEnnemy(), 40);
            SpawnEnemies(ennemyFactory.CreateBossEnnemy(), 4);
            SpawnEnemiesElCombator(ennemyFactory.CreateWeakEnnemy(), 10 + (player.Level * 2));
            SpawnEnemies(ennemyFactory.CreateWeakEnnemy(), 50 + (player.Level * 2));
        } while (true) ;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void SpawnEnemies(GameObject enemyPrefab, int numberofEnemies, bool isWaveTracking = true)                    //Spawn concept
    {
        for (int i = 0; i < numberofEnemies; i++)
        {
            Vector3 spawnPosition = player.transform.position;                                      //Give position for the monsters spawning position
            Vector2 randompoint = Random.insideUnitCircle.normalized * 17;

            spawnPosition.x += randompoint.x;
            spawnPosition.y += randompoint.y;
            if (!isWaveTracking)
            {
                spawnPosition.x = player.transform.position.x - 20;
            }


            GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Monster enemy = enemyObject.GetComponent<Monster>();
            enemy.isTrackingPlayer = isWaveTracking;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    void SpawnEnemiesElCombator(GameObject enemyPrefab, int numberofEnemies)                    //Spawn concept
    {
        for (int i = 0; i < numberofEnemies; i++)
        {
            Vector3 spawnPosition = player.transform.position;                                      //Give position for the monsters spawning position
            Vector2 randompoint = Random.insideUnitCircle.normalized * 17;

            spawnPosition.x += randompoint.x;
            spawnPosition.y += randompoint.y;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

}

