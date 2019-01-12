using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    GameObject pathGo;

    Transform targetPathNode;
    int pathNodeIndex = 0;

    float speed = 2;

    public int health = 1;

    // Use this for initialization
    void Start()
    {
        pathGo = GameObject.Find("Path");
    }

    void GetNextPathNode() {
        if (pathNodeIndex < pathGo.transform.childCount)
        {
            targetPathNode = pathGo.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;
        }
        else
        {
            Finish();
        }    
    }

	// Update is called once per frame
	void Update () {
        
        if (targetPathNode == null)
        {
            GetNextPathNode();
        }

        Vector3 dir = targetPathNode.position - this.transform.localPosition;
        
        float distThisFrame = speed * Time.deltaTime;
        if(dir.magnitude <= distThisFrame) {
            targetPathNode = null;
        } else {
            transform.Translate( dir.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime*5);
        }
	}

    void Finish() {
        Destroy(gameObject);
    }
}
