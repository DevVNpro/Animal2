using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    #region Dataconfig    
      public const string SoundVfx = "SoundVfx";
      public const string SoundTheme = "SoundTheme";
      public const string Vibration = "Vibration";

      public static int GetKeySoundVFX()
      {
          return PlayerPrefs.GetInt(SoundVfx, 1);
      }
      public static int GetKeySoundTheme()
      {
          return PlayerPrefs.GetInt(SoundTheme, 1);
      }
      public static int GetKeyVibration()
      {
          return PlayerPrefs.GetInt(Vibration, 1);
      }

      public static void SetKeySoundVFX(int val)
      {
          PlayerPrefs.SetInt(SoundVfx, val);
          PlayerPrefs.Save();
      }
      public static void SetKeySoundTheme(int val)
      {
          PlayerPrefs.SetInt(SoundTheme, val);
          PlayerPrefs.Save();
      }
      
      #endregion
    #region GamePlay
        public const string LevelCanPlay = "LevelCanPlay";
        public const string LevelUnlock = "LevelUnlock"; 
        public const string  IdSkinUse = "IdSkinUse";
        public const string ItemBom = "ItemBom";
        public const string ItemShield = "ItemShield";
        public const string ItemHeal = "ItemHeal";
        public const string ItemBullet = "ItemBullet";
        
        #endregion


}
