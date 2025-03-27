using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool spawn = true;
    public GameObject prefab;
    public float spawnRate = 1f;
    LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        StartSpawn();
    }

    IEnumerator Spawn(){
        while(spawn){
            yield return new WaitForSeconds(spawnRate);
            if(spawn)Instantiate(prefab, transform.position, transform.rotation);
            
        }
    }
    public void StartSpawn()
    {
        StartCoroutine(Spawn());
    }
}
