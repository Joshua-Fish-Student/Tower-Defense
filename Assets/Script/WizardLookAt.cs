using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense{
    public class WizardLookAt : MonoBehaviour
    {
        public WizardTower tower;
        // Update is called once per frame
        void Update()
        {
            if(tower.enemyTarget) transform.LookAt(tower.enemyTarget.transform);
        }
    }

}
