using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject asteroidPrefab;
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    public float xLimit = 8f;
    public float maxTimeLife = 6f;
    public float asteroidSpeed = 2f;

    private float spawnNext = 0;
    // Update is called once per frame
    void Update()
    {

        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60f / spawnRatePerMinute;

            spawnRatePerMinute += spawnRateIncrement;

            float rand = Random.Range(-xLimit, xLimit);

            Vector2 spawnPosition = new Vector3(rand, 13f, 0f);

            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            Rigidbody meteorRigid = meteor.GetComponent<Rigidbody>();
            
            //velocidad dek metorito
            if (meteorRigid != null)
            {

                float randomSpeed = Random.Range(asteroidSpeed * 0.8f, asteroidSpeed * 1.2f);

                meteorRigid.linearVelocity = Vector3.down * randomSpeed;

                float randomTorque = Random.Range(-15f, 15f);
                meteorRigid.AddTorque(Vector3.forward * randomTorque, ForceMode.Impulse);
            }

            Destroy(meteor, maxTimeLife);
        }

    }
}
