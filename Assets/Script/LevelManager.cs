using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense{
    public class LevelManager : MonoBehaviour
    {
        public int loadNum;
        public static bool isTitleScreen;
        public float waveLength;
        public List<GameObject> spawners;
        public int numWavesLeft;
        public bool isSpawning = true;
        private int waveAt = 0;
        public void LoadIt() {
            
            SceneManager.LoadScene(loadNum);
        }
        private void Start()
        {
            if (loadNum != 1) isTitleScreen = false;
            else isTitleScreen = true;
            if (!isTitleScreen) StartCoroutine(Wave());
        }
        IEnumerator Wave()
        {
            
            yield return new WaitForSeconds(waveLength);
            int i = 0;
            while (i < spawners.Count)
            {
                spawners[i].GetComponent<Spawner>().spawn = false; i++;
            }
            isSpawning = false;
        }
        private void Update()
        {
            if (!(isTitleScreen || isSpawning || FindAnyObjectByType<Enemy>())) {
                numWavesLeft--;
                if (numWavesLeft > 0) {
                    isSpawning = true;
                    int i = 0;
                    while (i < spawners.Count)
                    {
                        spawners[i].GetComponent<Spawner>().spawn = true;
                        spawners[i].GetComponent<Spawner>().StartSpawn();
                        i++;
                    }
                    StartCoroutine(Wave());
                } else LoadIt();
            }
        }
    }
}
