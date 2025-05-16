using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TowerDefense{
    public class Grid : MonoBehaviour
    {
        public Dictionary<Vector3Int, GameObject> gameObjects = new Dictionary<Vector3Int, GameObject>();

        public bool Occupied(Vector3Int tileCoordinates){
            if (Cursor.tile.transform.childCount > 0) return true;
            return gameObjects.ContainsKey(tileCoordinates);
        }

        public bool Add(Vector3Int tileCoordinates, GameObject gameObject){
            if (gameObjects.ContainsKey(tileCoordinates)) return false;

            gameObjects.Add(tileCoordinates, gameObject);
            return true;
        }

        public GameObject Remove(Vector3Int tileCoordinates){
            if (!gameObjects.ContainsKey(tileCoordinates)) return null;
            return gameObjects[tileCoordinates];
        }

        public static Vector3Int WorldToGrid(Vector3 worldPosition){
            return Vector3Int.RoundToInt(worldPosition);
        }

        public static Vector3 GridToWorld(Vector3Int gridPosition){
            return gridPosition;
        }
    }
}

