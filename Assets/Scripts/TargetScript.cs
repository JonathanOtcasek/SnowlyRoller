using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public MeshRenderer myRenderer;
    public SkinnedMeshRenderer mySkinnedRenderer;
    public Material[] possibleMats;

    public BoxCollider myCollider;

    public int type = 0;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        if(type == 1)
        {
            int which = Random.Range(0, 2);
            direction = which == 0 ? new Vector3(transform.right.x, transform.right.y, transform.right.z).normalized/100 : new Vector3(-transform.right.x, -transform.right.y, - transform.right.z).normalized/100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myRenderer != null)
        {
            if (SnowballScript.instance.targetBallSize > transform.localScale.x)
            {
                myRenderer.material = possibleMats[1];
            }
            else
            {
                myRenderer.material = possibleMats[0];
            }
        } else
        {
            if (SnowballScript.instance.targetBallSize > transform.localScale.x)
            {
                mySkinnedRenderer.material = possibleMats[1];
            }
            else
            {
                mySkinnedRenderer.material = possibleMats[0];
            }
        }

        if(type == 1 && myCollider.enabled)
        {
            transform.Translate(direction * (Random.value));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snowball"))
        {
            if (myRenderer != null)
            {
                myRenderer.material = possibleMats[2];
            }
            else
            {
                mySkinnedRenderer.material = possibleMats[2];
            }
            myCollider.enabled = false;

            if (SnowballScript.instance.targetBallSize > transform.localScale.x)
            {
                transform.parent = SnowballScript.instance.transform;
                SnowballScript.instance.targetBallSize++;
            }
            else
            {
                SnowballScript.instance.targetBallSize--;
            }
            Destroy(this);

        }
    }
}
