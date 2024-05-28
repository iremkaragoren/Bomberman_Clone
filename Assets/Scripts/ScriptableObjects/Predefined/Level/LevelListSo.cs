using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Predefined.Level
{
   [CreateAssetMenu(fileName = "LevelList", menuName = "ThisGame/Predefined/LevelList", order = 0)]
   public class LevelListSo : ScriptableObject
   {
      [SerializeField] private List<LevelSo> m_allLevels;
      public List<LevelSo> Levels => m_allLevels;

   }
}
