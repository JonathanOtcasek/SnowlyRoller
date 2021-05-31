using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnowballScript : MonoBehaviour
{
    public static SnowballScript instance;

    public Transform myTransform;
    public Transform camTransform;

    Vector3 lastFramePos = Vector3.zero;
    Vector3 mouseLastFramePos = Vector3.zero;

    Vector3 camOrigPos;

    public float targetBallSize = 1;
    public Text floatingBallTextReadout;

    public GameObject winText;
    public GameObject loseText;

    public GameObject shadow;
    GameObject[] shadowPool = new GameObject[30];
    int frameCounter = 0;
    int shadowCounter = 0;

    LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }
        for (int i = 0; i < shadowPool.Length; i++)
        {
            shadowPool[i] = GameObject.Instantiate(shadow, new Vector3(-2000, -2000, -2000), Quaternion.identity);
        }
        mask = LayerMask.GetMask("Floor");
        camOrigPos = camTransform.position;
    }

    void FixedUpdate()
    {
        camTransform.position += myTransform.position - lastFramePos;
        lastFramePos = myTransform.position;

        if (Input.GetMouseButton(0))
        {
            float xDiff = Camera.main.ScreenToViewportPoint(Input.mousePosition).x - mouseLastFramePos.x;
            //print(xDiff);
            float yDiff = Camera.main.ScreenToViewportPoint(Input.mousePosition).y - mouseLastFramePos.y;
            //print(yDiff);

            if (Mathf.Abs(xDiff) > Mathf.Abs(yDiff))
            {
                if (xDiff < 0)
                {
                    myTransform.GetComponent<Rigidbody>().AddForce(camTransform.right * (-22 * (targetBallSize / 5 > 1 ? targetBallSize / 5 : 1)));
                }
                else if (xDiff > 0)
                {
                    myTransform.GetComponent<Rigidbody>().AddForce(camTransform.right * (22 * (targetBallSize/5 > 1 ? targetBallSize/5 : 1)));
                }
            }
            else
            {
                if (yDiff > 0f)
                {
                    myTransform.GetComponent<Rigidbody>().AddForce(camTransform.forward * 50);
                }
            }
        }

        mouseLastFramePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Vector3.Distance(myTransform.position, lastFramePos) > .05f && targetBallSize > 0)
        {
            targetBallSize += 0.0002f;
        }

        floatingBallTextReadout.text = targetBallSize.ToString("F1") + " m"; 

        if(targetBallSize <= 0) {
            loseText.SetActive(true);
        }

        if (Mathf.Abs(myTransform.localScale.x - targetBallSize) > .01f)
        {
            myTransform.localScale = Vector3.Lerp(myTransform.localScale, new Vector3(targetBallSize, targetBallSize, targetBallSize), Time.deltaTime);
            camTransform.position = Vector3.LerpUnclamped(myTransform.position, myTransform.position + camOrigPos, targetBallSize * .5f);
        }

        if(frameCounter < 30)
        {
            frameCounter++;
        } else
        {
            frameCounter = 0;
            shadowCounter = shadowCounter >= shadowPool.Length-1 ? 0 : shadowCounter + 1;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.position - new Vector3(0, 100f, 0f), out hit , 10f * targetBallSize, mask.value))
            {
                shadowPool[shadowCounter].transform.position = transform.position - new Vector3(0, targetBallSize, 0f);//hit.point + new Vector3(0f, 0f, .0000000001f);
                shadowPool[shadowCounter].transform.rotation = hit.transform.rotation;
                shadowPool[shadowCounter].transform.localScale = (transform.localScale/5) * 4;
            }
        }
    }

    public void DisplayWin()
    {
        winText.SetActive(true);
    }
}
