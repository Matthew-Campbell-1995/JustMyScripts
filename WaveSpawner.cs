using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public GameObject[] EnemyTemplates;
    public float timeBetweenWaves;
    private float timeSinceLastWave;
    public int budget;
    public int budgetIncrease;
    public List<int> costs;
    public Vector2 spawnBoxUpperBounds;
    public Vector2 spawnBoxLowerBounds;

    // Start is called before the first frame update
    void Start()
    {
        
        //Debug.Log("Start called");
        foreach (GameObject go in EnemyTemplates)
        {
            costs.Add (go.GetComponent<DamageHandler>().salvage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastWave += Time.deltaTime;
        if (timeSinceLastWave >= timeBetweenWaves)
        { 
            SpawnWave(budget, "random");
            //Spawns wave with current budget, of the random wave type
            timeSinceLastWave = 0;
            budget += budgetIncrease;
        }
    }

    //default wave type is random
    private void SpawnWave (int waveBudget)
    {
        SpawnWave(waveBudget, "random");
    }

    private void SpawnWave (int waveBudget, string waveType)
    {
        int randomIndex = 0;
        int workingBudget = waveBudget;
        Vector2 newEnemyPosition = new Vector2();
        newEnemyPosition.x = Random.Range(spawnBoxLowerBounds.x, spawnBoxUpperBounds.x);
        newEnemyPosition.y = Random.Range(spawnBoxLowerBounds.y, spawnBoxUpperBounds.y);

        if (waveType == null || waveType == "random") 
        {
            //choose an enemy type, see if it's affordable, and spawn it
            while (workingBudget > 0)  //there is an enemy costing 1, ensuring this always ends
            {
                randomIndex = Random.Range(0, EnemyTemplates.Length);
                //Debug.Log("randomIndex: " + randomIndex);
                if (costs[randomIndex] > workingBudget)
                {
                    continue;
                }
                else
                {
                    SpawnEnemy(randomIndex, newEnemyPosition);
                    workingBudget -= costs[randomIndex];
                }
            }
        }
    }

    

    private void SpawnEnemy (int enemyIndex, Vector2 enemyPosition)
        //spawns a single enemy at specified position
    {
        GameObject newEnemy = Instantiate(EnemyTemplates[enemyIndex]);
        newEnemy.transform.position = enemyPosition;
    }

}
