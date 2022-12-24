using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTimer;
    private float clockTime;

    Vector3 bounds1;
    Vector3 bounds2;

    public AudioScript aScript;

    private int numSpawn;


    private List<GameObject> darkEnemies;
    private List<GameObject> lightEnemies;

    // Start is called before the first frame update
    void Start()
    {
        clockTime = 20;
        Transform[] bounds = gameObject.GetComponentsInChildren<Transform>();
        bounds1 = bounds[0].position;
        bounds2 = bounds[1].position;
        numSpawn = 2;

        string ticks = System.DateTime.Now.Ticks.ToString();
        ticks = ticks.Substring(ticks.Length - 6);
        Random.InitState(int.Parse(ticks));

        darkEnemies = new List<GameObject>();
        lightEnemies = new List<GameObject>();

        StartCoroutine(WeightedSpawn());
        StartCoroutine(DifficultyCurve());
        StartCoroutine(DestroyAllEnemies());
    }

    public IEnumerator DifficultyCurve()
    {
        while (spawnTimer > 2)
        {
            yield return new WaitForSeconds(clockTime);
            spawnTimer -= 1;
            numSpawn += 1;
        }
    }

    public IEnumerator Spawn()
    {
        while (StateManager.PLAYERSTATE == Constants.PLAYERSTATE.ALIVE)
        {
            for (int i = 0; i < (int)Random.Range(1, numSpawn); i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(bounds1.x, bounds2.x), Random.Range(bounds2.y, bounds1.y), 0);

                GameObject obj = Instantiate(enemy, spawnPos, Quaternion.identity);
                obj.GetComponent<Shoot>().aScript = aScript;

                int num = Random.Range(0, 2);
                obj.GetComponent<EnemyController>().SetType(num == 0 ? "DarkEnemy" : "LightEnemy");
            }
            yield return new WaitForSeconds(spawnTimer);
        }
        
    }

    public IEnumerator WeightedSpawn()
    {
        while (StateManager.PLAYERSTATE == Constants.PLAYERSTATE.ALIVE)
        {
            for (int i = 0; i < (int)Random.Range(1, numSpawn); i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(bounds1.x, bounds2.x), Random.Range(bounds2.y, bounds1.y), 0);

                GameObject obj = Instantiate(enemy, spawnPos, Quaternion.identity);
                obj.GetComponent<Shoot>().aScript = aScript;


                float numLightEnemy = lightEnemies.Count;
                float numDarkEnemy = darkEnemies.Count;
                float totalEnemy = numLightEnemy + numDarkEnemy;
                float lightEnemyWeight = 0.5f + (numDarkEnemy - numLightEnemy) / totalEnemy;
                float darkEnemyWeight = 0.5f + (numLightEnemy - numDarkEnemy) / totalEnemy;
                string enemyToSpawn = WeightedRandomChoice(lightEnemyWeight, darkEnemyWeight);

                obj.GetComponent<EnemyController>().SetType(enemyToSpawn);
                List<GameObject> enemyList = enemyToSpawn == "DarkEnemy" ? darkEnemies : lightEnemies;
                enemyList.Add(obj);
            }
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private string WeightedRandomChoice(float lightEnemyWeight, float darkEnemyWeight)
    {
        // normalize the weights
        float totalWeight = lightEnemyWeight + darkEnemyWeight;
        lightEnemyWeight /= totalWeight;

        // choose an index based on the normalized weights
        float choice = Random.value;

        if (choice < lightEnemyWeight)
        {
            return "LightEnemy";
        }

        return "DarkEnemy";
    }


    public void ActivateEnemies()
    {
        string type = StateManager.WORLDSTATE == Constants.WORLDSTATE.WHITE ? "LightEnemy" : "DarkEnemy";
        GameObject[] list = GameObject.FindGameObjectsWithTag(type);
        
        if (list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                // Set enemy to normal movement and stuff
                try
                {
                    list[i].GetComponent<EnemyController>().Unfreeze();

                } catch { }
            }
        }
    }
    
    public void DeactivateEnemies()
    {
        string type = StateManager.WORLDSTATE == Constants.WORLDSTATE.BLACK ? "LightEnemy" : "DarkEnemy";
        GameObject[] list = GameObject.FindGameObjectsWithTag(type);

        if (list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                // Set enemy to not move and stuff
                try
                {
                    list[i].GetComponent<EnemyController>().Freeze();
                } catch { }
            }
        }
    }

    
    private IEnumerator DestroyAllEnemies()
    {
        yield return new WaitUntil(() => StateManager.PLAYERSTATE == Constants.PLAYERSTATE.DEAD);
        List<GameObject> list = GameObject.FindGameObjectsWithTag("DarkEnemy").ToList();
        list.AddRange(GameObject.FindGameObjectsWithTag("LightEnemy"));
        for (int i = 0; i < list.Count; i++)
        {
            // Stop all enemies
            try
            {
                Destroy(list[i]);
            }
            catch { }
        }
    }
}
