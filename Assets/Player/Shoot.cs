using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    public float range = 100f;
    public float damage = 50f;
    bool fire;
    bool action1;
    public GameObject prefab;


    private void Update()
    {
        if (fire)
        {
            OnShoot();
        }
        fire = false;
        if (action1)
        {
            OnAction1();
        }
        action1 = false;
    }

    private void OnShoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    private void OnAction1()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, 
            fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
                Instantiate(prefab, target.transform.position, target.transform.rotation);
            }
        }
    }

    public void OnFirePressed()
    {
        fire = true;
    }

    public void On1Pressed()
    {
        action1 = true;
    }
}
