using System.Collections.Generic;
using Datas;
using ScriptableObjects.Variable;
using UnityEngine;
using InGame.Bomb;

namespace InGame.Bomb
{
    public class ExplodeHandler : MonoBehaviour
    {
        [SerializeField] private GameObject bodyPrefab;
        [SerializeField] private GameObject cornerLeft;
        [SerializeField] private GameObject cornerRight;
        [SerializeField] private GameObject cornerUp;
        [SerializeField] private GameObject cornerDown;

        private int powerupCount;

        private void OnEnable()
        {
            powerupCount = PlayerData.Instance.GetAmountOfPowerUp(PowerUpTypes.FullFire);
            ExplodeBomb.OnExplode += HandleExplode;
            
        }
        
        private void OnDisable()
        {
            ExplodeBomb.OnExplode -= HandleExplode; 
        }

        private void HandleExplode(List<Vector2> activeWays)
        {
            AddBodyToCorners(activeWays);
        }

        private void AddBodyToCorners(List<Vector2> activeWays)
        {
            if (!activeWays.Contains(Vector2.left))
            {
                MoveCornerAndInstantiateArms(cornerLeft, Vector3.left, Quaternion.Euler(0, 0, 0));
            }
            if (!activeWays.Contains(Vector2.right))
            {
                MoveCornerAndInstantiateArms(cornerRight, Vector3.right, Quaternion.Euler(0, 0, 0));
            }
            if (!activeWays.Contains(Vector2.up))
            {
                MoveCornerAndInstantiateArms(cornerUp, Vector3.up, Quaternion.Euler(0, 0, 90));
            }
            if (!activeWays.Contains(Vector2.down))
            {
                MoveCornerAndInstantiateArms(cornerDown, Vector3.down, Quaternion.Euler(0, 0, 90));
            }
        }


        private void MoveCornerAndInstantiateArms(GameObject corner, Vector3 direction, Quaternion rotation)
        {
            Vector3 originalCornerPosition = corner.transform.position;
            
            corner.transform.position += direction * powerupCount;
            
            for (int i = 0; i < powerupCount; i++)
            {
                Instantiate(bodyPrefab, originalCornerPosition + direction * i, rotation, transform);
            }
        }
    }
}