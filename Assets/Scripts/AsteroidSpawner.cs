using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
   public Asteroid asteroidPrefab;
   public float spawnAmount = 1.0f;
   public float spawnRate = 1.0f;
   public float spawnDistance = 15.0f;
   public float varianceForAngle = 10.0f; // 10deg
   public void Start()
   {
      InvokeRepeating(nameof(Spawn), spawnRate, spawnRate); // =spawn spawnAmount (here, 1) amount of asteroid each spawnRate (here 1) second
   }
   private void Spawn()
   {
      for (int i = 0; i < spawnAmount; i++)
      {
         var circleSpawn = Random.insideUnitCircle.normalized; //spawn at random position in circle's border
         var finalSpawner = circleSpawn * spawnDistance; //increase the gap between the center and the border (of the circle)

         float varianceForAngle = Random.Range(-this.varianceForAngle, this.varianceForAngle);
         Quaternion rotation = Quaternion.AngleAxis(varianceForAngle, Vector3.forward);

         Asteroid myAsteroid = Instantiate(asteroidPrefab, finalSpawner, rotation);
         myAsteroid.asteroidSize = Random.Range(myAsteroid.asteroidMinSize, myAsteroid.asteroidMaxSize);
         myAsteroid.setTrajectory(rotation * -circleSpawn);

      }
   }

   
}
