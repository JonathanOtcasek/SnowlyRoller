using UnityEngine;

public class WallScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Snowball"))
        {
            SnowballScript.instance.targetBallSize--;
        }
    }
}
