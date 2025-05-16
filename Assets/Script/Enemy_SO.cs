using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class Enemy_SO : ScriptableObject
    {
        public float speed = 1f;
        public int damage = 1;
        public int goldGive = 1;
    }
}