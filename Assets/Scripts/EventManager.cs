using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
   #region Actions

   public static Action initialDive;
   public static Action onDive;
   public static Action<eDirection> turn;
   public static Action shoot;
   public static Action onGameOver;
   public static Action <OceanColor> onOceanColorSelected;
   public static Action <Character> onCharacterSelected;
   public static Action <Boat> onBoatSelected;
   public static Action <GameObject> onCoinCollected;
   public static Action <int> onCoinsUpdated;
   public static Action <int> onMaxDistanceUpdated;
   public static Action <int, int> onDistancesUpdated;
   public static Action <float> SetShakeSpeed;
   public static Action<float> SetHeartbeatVolume;
   public static Action <ePowerUp> onPoweredUp;
   public static Action onDataSaved;
   public static Action <DataSaveController> onDataLoaded;
   public static Action onPowerUpsDataLoaded;
   public static Action onMoneyDataLoaded;
   public static Action saveData;
   public static Action onGamePause;
   public static Action onGameResume;
   public static Action <float> onStaminaUpdated;
   public static Action onMilestoneCollided;
   public static Action onCoinMissed;
   public static Action onPlayerStop;
   
   


   #endregion

   #region Functions

   public static void OnDive()
   {
      onDive?.Invoke();
   }
   
   public static void Turn(eDirection direction)
   {
      turn?.Invoke(direction);
   }

   public static void Shoot()
   {
      shoot?.Invoke();
   }
   
   public static void OnGameOver()
   {
      onGameOver?.Invoke();
   }

   #endregion
   
}
