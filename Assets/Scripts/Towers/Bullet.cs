using UnityEngine;


public class Bullet : MonoBehaviour
{

    public float speed = 15f;
    public Transform target;
    public float damage = 1f;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - this.transform.localPosition;
        float distThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)
        {
            DoBulletHit();
        }
        else
        {
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
        }

    }

    void DoBulletHit()
    {
        target.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
