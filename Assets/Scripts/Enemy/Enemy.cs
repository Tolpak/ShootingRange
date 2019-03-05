using UnityEngine;

public class Enemy : MonoBehaviour {

    GameObject pathGo;

    Transform targetPathNode;
    int pathNodeIndex = 0;

    public float speed = 4;
    public float health = 1;
    public int moneyValue = 1;

    void Start()
    {
        pathGo = GameObject.Find("Path");
        this.GetComponentInChildren<Animator>().speed = speed;
        Debug.Log("animation speed is " + this.GetComponentInChildren<Animator>().speed);
    }

    void GetNextPathNode() {
        if (pathNodeIndex < pathGo.transform.childCount-1)
        {
            targetPathNode = pathGo.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;
        }
        else
        {
            ReachedGoal();
        }    
    }

    void Update()
    {

        if (targetPathNode == null)
        {
            GetNextPathNode();
        }
        if (targetPathNode != null)
        {
            Vector3 dir = targetPathNode.position - this.transform.localPosition;

            float distThisFrame = speed * Time.deltaTime;
            if (dir.magnitude <= distThisFrame)
            {
                targetPathNode = null;
            }
            else
            {
                transform.Translate(dir.normalized * distThisFrame, Space.World);
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
            }
        }
	}

    void ReachedGoal()
    {
        ScoreManager.Instance.LoseLife();
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        ScoreManager.Instance.AddMoney(moneyValue);
        Destroy(gameObject);
    }
}
