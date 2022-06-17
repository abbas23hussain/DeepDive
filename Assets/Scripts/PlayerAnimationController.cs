using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem gameOverExplosionPS;
    [SerializeField] private PlayerController playerController;
    private static readonly int Dive = Animator.StringToHash("Dive");
    private static readonly int Return1 = Animator.StringToHash("Return");
    private Vector3 rotateDownVector;
    private Vector3 rotateUpVector;
    private float initialDiveDelay = 1f;
    private float turnDelay = 0.5f;
    private bool isInitialDive = true;
    private eDirection playerDirection = eDirection.down;

    private void Awake()
    {
        BindEvents();
    }

    private void OnDestroy()
    {
        UnbindEvents();
    }

    private void BindEvents()
    {
        EventManager.shoot += ReturnAnim;
        EventManager.initialDive += OnInitialDive;
        EventManager.onCharacterSelected += OnCharacterSelected;
        EventManager.onGameOver += OnGameOver;

    }

    private void UnbindEvents()
    {
        EventManager.shoot -= ReturnAnim;
        EventManager.initialDive -= OnInitialDive;
        EventManager.onCharacterSelected -= OnCharacterSelected;
        EventManager.onGameOver -= OnGameOver;
    }
    private void Start()
    {
        rotateDownVector = new Vector3(90f, 180f, 0f);
        rotateUpVector = new Vector3(-90f, 180f, 0f);
    }

    private void Update()
    {
        if (isInitialDive)
        {
            return;
        }

        if (playerController.playerRigidBody.velocity.y == 0 && playerDirection == eDirection.down)
        {
            RotatePlayerUpwards();
        }

        if (playerController.playerRigidBody.velocity.y < 0 && playerDirection == eDirection.up)
        {
            RotatePlayerDownwards();
        }
    }

    private void RotatePlayerDownwards()
    {
        transform.DOLocalRotate(rotateDownVector, 1.75f, RotateMode.Fast);
        playerDirection = eDirection.down;
    }

    private void RotatePlayerUpwards()
    {
        
        transform.DOLocalRotate(rotateUpVector, 1.75f, RotateMode.Fast);
        playerDirection = eDirection.up;
        
    }


    private void ReturnAnim()
    {
        animator.SetTrigger(Return1);
        StartCoroutine(ReturnCoroutine());
    }

    private void OnInitialDive()
    {
        animator.SetTrigger(Dive);
        StartCoroutine(DiveCoroutine());
        // isInitialDive = false;
    }

    private void OnCharacterSelected(Character character)
    {
        skinnedMeshRenderer.sharedMesh = character.characterMesh;
    }

    private void OnCharacterHitsSurface()
    {
        //transform.DOLocalRotate(stopRotationVector, 1f, RotateMode.Fast);
    }

    private void OnGameOver()
    {
        skinnedMeshRenderer.gameObject.SetActive(false);
        Explode();
    }

    private void Explode()
    {
        gameOverExplosionPS.gameObject.SetActive(true);
        gameOverExplosionPS.Play(true);
    }
    
    private IEnumerator ReturnCoroutine()
    {
        yield return new WaitForSeconds(turnDelay);
        RotatePlayerDownwards();
    }
    private IEnumerator DiveCoroutine()
    {
        yield return new WaitForSeconds(initialDiveDelay);
        RotatePlayerDownwards();
        EventManager.OnDive();
        isInitialDive = false;
    }

}
