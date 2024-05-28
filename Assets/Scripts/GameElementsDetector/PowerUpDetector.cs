using System;
using InGame.Bomb;
using InGame.DestroyableWall;
using ScriptableObjects.Variable;
using UnityEngine;

namespace InGame.PowerUp
{
    public class PowerUpDetector : MonoBehaviour
    {
        public bool canInteract = false;
        public PowerUpTypes _PowerUpType;
        public int Amount;


        public void Init(PowerUps_SO powerUpsSo)
        {
            this._PowerUpType = powerUpsSo.PowerUpType;
            this.Amount = (int)(powerUpsSo.AmountToIncreaseToIncrease);

        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.TryGetComponent(out ExplodeDetector explodeDetector))
            {
                canInteract = true;
            }
        
        }

        
    }
}