using UnityEngine;

public class Gun : MonoBehaviour
{
    private Gun gun;

    public float damage = 5f;
    public float fireRate = 5f;
    private bool held = false;

    public GameObject barrelExit;
    public ParticleSystem muzFlash;
    public GameObject impactFlash;

    public float knockback = 30f;

    private float fireDelay = 0.5f;

    private void Awake()
    {
        gun = this;
    }

    void Update()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && Time.time >= fireDelay)
        {
            fireDelay = Time.time + 0.8f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("Shot fired");
        muzFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(barrelExit.transform.position, barrelExit.transform.forward, out hit))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * knockback);
            }

            GameObject impactGO = Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1.5f);
        }

        
    }
}
