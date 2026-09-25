using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;

    public GameObject smallAsteroidPrefab;
    public int fragmentCount = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ShatterAsteroid(collision.transform.position);

            IncreaseScore();
            Destroy(collision.gameObject);
            Destroy(gameObject);

            if (CameraShake.Instance != null)
            {
                CameraShake.Instance.Shake(0.1f, 0.15f); 
            }
        }
    }

    private void ShatterAsteroid(Vector3 spawnPosition)
    {
        if (smallAsteroidPrefab == null) return;

        for(int i = 0; i < fragmentCount; i++)
        {
            GameObject fragment = Instantiate(smallAsteroidPrefab, spawnPosition, Quaternion.identity);
            
            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            if(rb != null)
            {
                Vector3 randomDir = UnityEngine.Random.onUnitSphere;
                randomDir.z = 0;
                randomDir.Normalize();

                float randomForce = UnityEngine.Random.Range(3f, 7f); 
                rb.AddForce(randomDir * randomForce, ForceMode.Impulse);
            }
            Destroy(fragment, 1.5f);
        }
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        Debug.Log("Score: " + Player.SCORE);
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        if(go != null)
        {
            Text scoreText = go.GetComponent<Text>();
            if(scoreText != null)
            {
                scoreText.text = "Puntos : " + Player.SCORE;
            }
        }
    }
}
