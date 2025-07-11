using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    public AudioSource bulletAudioSource;
    public AudioClip shootBulletAudio;
    public AudioClip bulletCollideAudio;

    public GameObject hitEffectPrefab;
    public GameObject destroyEffectPrefab;

    void Start()
    {
        bulletAudioSource = GetComponent<AudioSource>();
    }

    public void ShootBulletEffect()
    {
        bulletAudioSource.PlayOneShot(shootBulletAudio);
    }

    public void BulletHitEffect(Transform _hitTransform)
    {
        bulletAudioSource.PlayOneShot(bulletCollideAudio);
        GameObject hitEffect = Instantiate(hitEffectPrefab, _hitTransform.position, Quaternion.identity);
        Destroy(hitEffect, 1f);
    }

    public void DestroyEffect(Transform _hitTranform)
    {
        GameObject destroyEffect = Instantiate(destroyEffectPrefab, _hitTranform.position, Quaternion.identity);
        Destroy(destroyEffect, 2f);
    }
}
