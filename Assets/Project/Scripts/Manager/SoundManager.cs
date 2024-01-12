using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    private const string SOUND_SFX = "vfxSound";
    private const string INSERT_KEY = "_number_";

    [SerializeField] private sfx sfxPrefab;
    [SerializeField] private List<AudioClip> bgmList;
    [SerializeField] private List<AudioClip> sfxList;
    [SerializeField] private AudioSource themeSource;
    [SerializeField] private AudioSource vfxSource;
    private static SoundManager instance;
    public static SoundManager Intance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        PlayBgm("BGM Forest");
    }

    public void playsfx(string clip, bool isLoop = false, float delay = 0f, Action onComplete = null)
        {
    
             var simple = GetSfxInstance();
           foreach (AudioClip sfx in sfxList)
            {
                if (sfx.name == clip)
                {
                    simple.Play(sfx, isLoop, delay, onComplete);
                    return;
                }
            }

        }

      

        public void PlayBgm(string clip)
        {
            foreach (AudioClip bgm in bgmList)
            {
                if (bgm.name == clip)
                {
                    BmgMusic.Instance.Play(bgm);
                    return;
                }
            }
            Debug.Log("Khong Tim Thay Nhac");
        }

        #region Player
        public void PlayVfxJump()
        {
            playsfx(sfxList[0].name);
        }
        public void PlayVfxShot()
        {
            playsfx(sfxList[1].name);
        }
        public void PlayVfxTouchGround()
        {
            playsfx(sfxList[3].name);
        }
        public void PlayVfxDeducthp()
        {
            playsfx(sfxList[4].name);
        }
        public void PlayVfxDead()
        {
            playsfx(sfxList[2].name);
        }
        #endregion

        #region GameObject

        public void PlayVfxStar()
        {
            playsfx(sfxList[5].name);

        }

        public void PlayVfxBrick()
        {
            playsfx(sfxList[6].name);
        }

        public void PlayBreakBox()
        {
            playsfx(sfxList[7].name);
        }
        public void PlayTouchEnemy()
        {
            playsfx(sfxList[8].name);
        }

        #endregion
        
 

        private sfx GetSfxInstance()
        {
            if (sfxPrefab == null)
                return null;
 
            var count = sfxList.Count;
            var go = Instantiate(sfxPrefab, transform);
            go.transform.SetParent(transform);
            go.gameObject.name = SOUND_SFX + INSERT_KEY + count;
      //      sfxList.Add(go);
            return go;
        }
     


    
    
}
