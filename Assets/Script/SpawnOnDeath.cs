using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TowerDefense
{
    public class SpawnOnDeath : MonoBehaviour
    {
        public GameObject prefab;
        public int numToSpawn;
        public void Spawn()
        {
            if (!GetComponent<Enemy>().end)
            {
                StartCoroutine(SpawnThing());
            }
            
        }
        IEnumerator SpawnThing()
        {
            while (numToSpawn > 0)
            {
                GameObject newThing = Instantiate(prefab, transform.position, transform.rotation);
                newThing.GetComponent<Enemy>().index = GetComponent<Enemy>().index;
                numToSpawn--;
                yield return new WaitForSeconds(0.1f);
            }
        }


    }

}
