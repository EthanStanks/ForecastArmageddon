using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootParticleScript : MonoBehaviour
{
    [SerializeField] GameObject particles;

    public void DoParticle()
    {
        GameObject effect = Instantiate(particles, transform.position, transform.rotation);

        Destroy(effect, 1);
    }
}
