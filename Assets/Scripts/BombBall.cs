using UnityEngine;

public class BombBall : MonoBehaviour
{
    public float explosionForce = 500f;
    public float explosionRadius = 3f;
    public ParticleSystem explosionParticles;

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("WhiteBall")) return;
        Explode();
    }

    void Explode()
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in nearby)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        if (explosionParticles != null)
        {
            explosionParticles.transform.SetParent(null);
            explosionParticles.Play();
            Destroy(explosionParticles.gameObject, explosionParticles.main.duration);
        }

        ScoreManager.Instance.BallPocketed();
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
