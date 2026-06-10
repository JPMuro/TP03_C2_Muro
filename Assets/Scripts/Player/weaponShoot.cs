using System.Collections;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [Header("References")]
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;
    public LineRenderer bulletLine;

    [Header("Weapon Stats")]
    public float range = 100f;
    public int damage = 25;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        RaycastHit hit;

        if (Physics.Raycast(
            fpsCam.transform.position,
            fpsCam.transform.forward,
            out hit,
            range))
        {
            // Ignorar al jugador
            if (hit.collider.transform.root.CompareTag("Player"))
                return;

            Debug.Log("Impactó: " + hit.collider.name);

            // Mostrar línea del disparo
            if (bulletLine != null)
            {
                StartCoroutine(
                    ShowBulletLine(
                        fpsCam.transform.position,
                        hit.point
                    )
                );
            }

            // Aplicar daño
            IDamageable target =
                hit.collider.GetComponentInParent<IDamageable>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // Efecto de impacto
            if (hitEffect != null)
            {
                GameObject effect = Instantiate(
                    hitEffect,
                    hit.point,
                    Quaternion.LookRotation(hit.normal)
                );

                Destroy(effect, 1f);
            }
        }
        else
        {
            // Mostrar línea aunque no impacte nada
            if (bulletLine != null)
            {
                StartCoroutine(
                    ShowBulletLine(
                        fpsCam.transform.position,
                        fpsCam.transform.position +
                        fpsCam.transform.forward * range
                    )
                );
            }
        }
    }

    IEnumerator ShowBulletLine(Vector3 start, Vector3 end)
    {
        bulletLine.enabled = true;

        bulletLine.SetPosition(0, start);
        bulletLine.SetPosition(1, end);

        yield return new WaitForSeconds(0.05f);

        bulletLine.enabled = false;
    }
}