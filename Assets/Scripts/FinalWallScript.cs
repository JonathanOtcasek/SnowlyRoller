using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWallScript : MonoBehaviour
{
    public MeshRenderer myRenderer;
    public BoxCollider myCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snowball"))
        {
            if (SnowballScript.instance.targetBallSize >= 10f)
            {
                myRenderer.enabled = false;
                myCollider.enabled = false;
                SnowballScript.instance.DisplayWin();
            }
            else
            {
                SnowballScript.instance.targetBallSize = 0f;
            }
            Destroy(this);
        }
    }
}
