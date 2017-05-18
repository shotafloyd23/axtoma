using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    public float playerRadius;
    public float destroyTime;
    public GameObject particlesPrefab;

    public void SpawnParticles()
    {
        GameObject clone;

        Vector3 spawnPoint = transform.position + Vector3.down * playerRadius;

        clone = Instantiate( particlesPrefab );
        clone.transform.transform.position = spawnPoint;
        
        Destroy(clone, destroyTime);
    }
}
