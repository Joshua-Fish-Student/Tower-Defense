using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense{
    public class LookAt : MonoBehaviour
    {
        public Tower tower;
        // Update is called once per frame
        void Update()
        {
            if(tower.enemyTarget) transform.LookAt(tower.enemyTarget.transform);
        }
    }

}
