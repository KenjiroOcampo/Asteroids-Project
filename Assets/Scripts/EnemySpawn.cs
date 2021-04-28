using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public EnemyObjectScript enemyPrefab; //Clone of the enemy.
    public float spawnRadius = 12.0f; //Radius of which the enemy is spawned at.
    public float spawnRate = 2.0f; //The rate of how often the enemy spawns.
    public float enemyMovementAngle = 15.0f; //The variation angle for the enemy trajectory.
    public int spawnMax = 1; //How many spawn per call.
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate); //Repeats a given method per second.
    }

    private void Spawn()
    {
        for (int i = 0; i < this.spawnMax; i++)
        {
            //Chooses a point on the circunmfrance of a circle some distance (radius) away with a random direction from the center.
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnRadius;
            Vector3 spawnPointObj = this.transform.position + spawnDirection;

            //Change the rotation of the sprite so that the object's trajectory is near the spawn point.
            float variance = Random.Range(-this.enemyMovementAngle, this.enemyMovementAngle);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            //When the enemy is created the object is given the direction of where it's supossed to go.
            EnemyObjectScript enemy = Instantiate(this.enemyPrefab, spawnPointObj, rotation);
            enemy.Size = Random.Range(enemy.minSize, enemy.maxSize);
            Vector2 trajectory = rotation * -spawnDirection;
            enemy.trajectory(trajectory);
        }
    }
}
