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

    // Start is called before the first frame update
    void Start()
    {
        clockTime = 20;
        Transform[] bounds = gameObject.GetComponentsInChildren<Transform>();
        bounds1 = bounds[0].position;
        bounds2 = bounds[1].position;
        numSpawn = 2;
        StartCoroutine(Spawn());
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
