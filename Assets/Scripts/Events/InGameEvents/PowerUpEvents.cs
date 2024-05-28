using ScriptableObjects.Variable;
using UnityEngine.Events;

namespace Events.InGameEvents
{
   public static class PowerUpEvents
   {
      public static UnityAction<PowerUpTypes,int> CollectPowerUp;
   }
}