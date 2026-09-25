using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float thrustForce = 100f;
    public float rotationSpeed = 120f;

    public float xBorderLimit = 9f;
    public float yBorderLimit = 5f;

    public GameObject gun, bullerPrefab;

    private Rigidbody _rigid;

    public static int SCORE = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustForce * thrustDirection * thrust);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        Vector3 newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit + 0.5f;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit - 0.5f;

        if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit + 0.5f;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit - 0.5f;
        
        transform.position = newPos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bullerPrefab, gun.transform.position, Quaternion.identity);

            Bullet balaScript = bullet.GetComponent<Bullet>();
            
            if(balaScript != null)
            {
                balaScript.targetVector = transform.right;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa...");
        }
      
    }

}
