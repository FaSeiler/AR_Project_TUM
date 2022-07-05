using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public ParticleSystem particleSystem_Explosion;
    public ParticleSystem particleSystem_Explosion_Smoke;

    public void SetExplosionPosition(Vector3 position)
    {
        this.gameObject.transform.position = position;
    }

    public void PlayExplosion()
    {
        StartCoroutine(PlayAnimAndDestroy());
    }

    private IEnumerator PlayAnimAndDestroy()
    {
        particleSystem_Explosion.Emit(100);
        particleSystem_Explosion_Smoke.Emit(100);

        yield return new WaitForSeconds(2.0f);

        Destroy(this.gameObject);
    }
}
