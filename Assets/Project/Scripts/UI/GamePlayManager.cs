using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Spine.Unity;
using UnityEngine;
using UniRx;
public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [ReadOnly(true)] public bool PlayerWin;
    [ReadOnly(true)] public bool Playerlose;

    private void Awake()
    {
        Rxmanager.LockCharater.Subscribe((b) =>
        {
            AnimationReferenceAsset animationReferenceAsset = _characterController.AnimationReferenceAsset[0];
            _characterController._characterAnimation.PlayAnimation(animationReferenceAsset,true,1);
            _characterController.enabled = false;
        }).AddTo(this);
        Rxmanager.FreeCharater.Subscribe((b) =>
        {
            _characterController.enabled = true;
        }).AddTo(this);
    }


    void Start()
    {
        Application.targetFrameRate = 60;
    }


}
