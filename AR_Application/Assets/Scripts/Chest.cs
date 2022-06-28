using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private GameObject chestModelGO;
    [SerializeField]
    private GameObject keyModelGO;
    [SerializeField]
    private Animator chestAnimator;
    [SerializeField]
    private ParticleSystem particleSystem_Explosion;
    [SerializeField]
    private ParticleSystem particleSystem_Explosion_Smoke;


    private void Awake()
    {
        chestAnimator = GetComponentInChildren<Animator>();
        chestAnimator.SetBool("PlayUnlockChestAnimation", false);
    }

    private IEnumerator UnlockAnimationCoroutine()
    {
        UIDebugText.AddLog("Unlocking Chest!");


        chestAnimator.SetBool("PlayUnlockChestAnimation", true);

        yield return new WaitForSeconds(chestAnimator.GetCurrentAnimatorStateInfo(0).length + chestAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.5f);

        PlayExplosionAnimation();

        chestModelGO.SetActive(false);
        keyModelGO.SetActive(false);

        Reward reward = FindObjectOfType<Reward>();
        reward.ShowReward(chestModelGO.transform);

        FindObjectOfType<ImageTrackingPrefabs>().StopTracking();
    }

    public void PlayExplosionAnimation()
    {
        particleSystem_Explosion.Emit(100);
        particleSystem_Explosion_Smoke.Emit(100);
    }

    public void PlayUnlockAnimation()
    {
        if (!UnlockChest.chestIsUnlocked)
        {
            StartCoroutine(UnlockAnimationCoroutine());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayUnlockAnimation();
        }
    }
}
