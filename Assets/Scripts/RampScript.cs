using UnityEngine;

public class RampScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snowball"))
        {
            SnowballScript.instance.GetComponent<Rigidbody>().velocity = SnowballScript.instance.GetComponent<Rigidbody>().velocity / 2;
            Destroy(this);
        }
    }
}
