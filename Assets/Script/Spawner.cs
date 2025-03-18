using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool spawn = true;
    public GameObject prefab;
    public float spawnRate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){
        while(spawn){
            yield return new WaitForSeconds(spawnRate);
            Instantiate(prefab, transform.position, transform.rotation);
            
        }
    }
}
