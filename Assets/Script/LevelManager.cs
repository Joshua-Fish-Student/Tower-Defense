using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense{
    [System.Serializable]
    public class SpawnerHandle
    {
        public List<GameObject> list;
    }

    [System.Serializable]
    public class SpawnerList
    {
        public List<SpawnerHandle> list;
    }
    public class LevelManager : MonoBehaviour
    {
        public bool isTitleScreen;
        public float waveLength;
        public SpawnerList listOfSpawnerLists = new SpawnerList();
        public int numWavesLeft;
        public bool isSpawning = true;
        public int goldTowersPlaced = 0;
        Player player;
        Grid grid;
        [SerializeField] private int waveAt = 0;
        public void LoadIt(string loadNum) {
            
            SceneManager.LoadScene(int.Parse(loadNum));
        }
        private void Start()
        {
            if (!isTitleScreen) StartCoroutine(Wave());
            grid = FindFirstObjectByType<Grid>();
            player = FindFirstObjectByType<Player>();
        }
        IEnumerator Wave()
        {
            
            yield return new WaitForSeconds(waveLength);
            int i = 0;
            while (i < GetList().Count)
            {
                GetList()[i].GetComponent<Spawner>().spawn = false; i++;
            }
            isSpawning = false;
        }
        private void Update()
        {
            if (!(isTitleScreen || isSpawning || FindAnyObjectByType<Enemy>())) {
                numWavesLeft--;
                waveAt++;
                ValueDisplay.OnValueChanged.Invoke("WaveNum", "Wave: " + (waveAt+1));
                if (numWavesLeft > 0) {
                    isSpawning = true;
                    int i = 0;
                    while (i < GetList().Count)
                    {
                        GetList()[i].GetComponent<Spawner>().spawn = true;
                        GetList()[i].GetComponent<Spawner>().StartSpawn();
                        i++;
                    }
                    StartCoroutine(Wave());
                } else player.victory.Invoke();
            }
        }
        private List<GameObject> GetList()
        {
            return listOfSpawnerLists.list[waveAt].list;
        }
        public void Quit()
        {
            Application.Quit();
        }
        public void GameOver()
        {
            foreach (KeyValuePair<Vector3Int, GameObject> entry in grid.gameObjects)
            {
                Destroy(entry.Value);
            }
        }
    }
}
