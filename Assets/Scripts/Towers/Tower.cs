using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Tower : MonoBehaviour {

    public float range = 5f;
    public GameObject bulletPrefab;

    public int cost;

    public float fireCooldown;
    float fireCooldownLeft = 0;

    public float damage = 0.5f;
    public float radius = 0;
    Enemy nearestEnemy = null;
    float dist = Mathf.Infinity;

    private Animation anim;
    public GameObject[] arrayOfMuzzles;

    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if ( nearestEnemy == null)
        {
            FindNewTarget();
        }
        if (nearestEnemy != null)
        {
            TryShoot(nearestEnemy);
        }
    }

    void FindNewTarget()
    {
        dist = Mathf.Infinity;
        Collider[] targets = Physics.OverlapSphere(this.transform.position, range);
        foreach (Collider coll in targets)
        {
            if (coll.gameObject.tag == "Enemy")
            {
                float d = Vector3.Distance(this.transform.position, coll.transform.position);
                if (d < dist)
                {
                    nearestEnemy = coll.gameObject.transform.parent.gameObject.GetComponent<Enemy>();
                    dist = d;
                }
            }
        }
    }

    void TryShoot(Enemy e)
    {
        Vector3 dir = e.transform.position - this.transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        this.transform.transform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);

        if (dir.magnitude <= range)
        {
            fireCooldownLeft -= Time.deltaTime;
            if (fireCooldownLeft <= 0)
            {
                fireCooldownLeft = fireCooldown;
                for (int i = 0; i < arrayOfMuzzles.Length; i++)
                {
                    GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, arrayOfMuzzles[i].transform.position, transform.rotation);
                    Bullet b = bulletGO.GetComponent<Bullet>();
                    b.target = e.transform;
                    b.damage = damage;
                    playShootAnimation();
                }
            }
        }
    }
    

    public void playShootAnimation()
    {
        anim = gameObject.GetComponentInChildren<Animation>();
        if (anim)
        {
            //anim.Play("Skel_PortableCannon|PortableCannon_Fire");
        }
    }

}
