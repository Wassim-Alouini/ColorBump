using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    Vector2 lastMousePos = Vector2.zero;
    public float thrust = 100f;
    [SerializeField] float wallDistance = 5f;
    [SerializeField] float minCamDistance = 4f;
    public float speed = 5f;
    public GameObject winPanel;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 deltaPos = Vector2.zero;
        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePos = Input.mousePosition;
            if(lastMousePos == Vector2.zero)
            {
                lastMousePos = currentMousePos;
            }
            deltaPos = currentMousePos - lastMousePos;
            lastMousePos = currentMousePos;
            Vector3 force = new Vector3(deltaPos.x,0, deltaPos.y) * thrust;
            rb.AddForce(force);
        }
        else
        {
            lastMousePos = Vector2.zero;
        }
    }
    private void LateUpdate()
    {
        Vector3 position = transform.position;
        if (position.x < -wallDistance)
        {
            position.x = -wallDistance;
            Debug.Log("left");
            ;
        }
        else if (position.x > wallDistance)
        {
            position.x = wallDistance;
            Debug.Log("right");
        }
        if (position.z < Camera.main.transform.position.z + minCamDistance)
        {
            position.z = Camera.main.transform.position.z + minCamDistance;
        }

        

        transform.position = position;
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector3.forward * speed * Time.fixedDeltaTime);
        Camera.main.transform.position += Vector3.forward * speed * Time.fixedDeltaTime;
    }
    IEnumerator Die(float delayTime)
    {
        Debug.Log("You're dead");

        speed = 0;
        thrust = 0;

        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Evil")
        {
            StartCoroutine(Die(2));
        }
    }
    IEnumerator Win(float delayTime)
    {
        thrust = 0;
        speed = 0;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(delayTime);

        winPanel.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Goal")
        {
            StartCoroutine(Win(0.5f));
        }
    }
}
