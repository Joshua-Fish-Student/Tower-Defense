using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense{
    public class Player : MonoBehaviour
    {
        public UnityEvent gameOver = new UnityEvent();
        public GameObject towerPrefab;
        public int gold;
        LevelManager levelManager;
        Grid grid;
        Cursor cursor;
        UICursorCapture cursorCapture;
        public ParticleSystem ps;
        public UnityEvent victory = new UnityEvent();

        private void Awake(){
            grid = FindObjectOfType<Grid>();
            cursorCapture = FindObjectOfType<UICursorCapture>();
            cursor = GetComponentInChildren<Cursor>();
            levelManager = FindObjectOfType<LevelManager>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !cursorCapture.cursorOverUI && !GetIsTitleScreen())
            {
                TryPlaceTower(grid, Grid.WorldToGrid(cursor.transform.position));
            }
            if (Input.GetMouseButtonDown(1) && !cursorCapture.cursorOverUI && !levelManager.isTitleScreen)
            {
                TryRemoveTower(grid, Grid.WorldToGrid(cursor.transform.position));
            }
        }

        private bool GetIsTitleScreen()
        {
            return levelManager.isTitleScreen;
        }

        public bool TryPlaceTower(Grid grid, Vector3Int tileCoordinates){
            if(!towerPrefab) return false;
            if (gold < Tower_SO.GetCost(towerPrefab)) return false;
            if(grid.Occupied(tileCoordinates)) return false;
            if(Cursor.tile.CompareTag("Path")) return false;
            if (towerPrefab.GetComponent<Tower>() != null && towerPrefab.GetComponent<Tower>().isGoldTower && levelManager.goldTowersPlaced == 5) return false;
            GameObject newTower = Instantiate(towerPrefab, tileCoordinates, Quaternion.identity);
            grid.Add(tileCoordinates, newTower);
            if (towerPrefab.GetComponent<Tower>() != null && towerPrefab.GetComponent<Tower>().isGoldTower) levelManager.goldTowersPlaced++;
            gold -= Tower_SO.GetCost(towerPrefab);
            ValueDisplay.OnValueChanged.Invoke("PlayerGold", gold);
            return true;
        }
        bool TryRemoveTower(Grid grid, Vector3Int tileCoordinates)
        {
            if (!grid.Occupied(tileCoordinates)) return false;
            float towerGold = Tower_SO.GetCost(grid.Remove(tileCoordinates));
            gold += (int) towerGold / 2;
            ValueDisplay.OnValueChanged.Invoke("PlayerGold", gold);
            Destroy(grid.gameObjects[tileCoordinates]);
            grid.gameObjects.Remove(tileCoordinates);
            return true;
        }
    }

}
