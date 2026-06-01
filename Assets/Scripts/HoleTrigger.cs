using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    public GameObject pocketParticlesPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WhiteBall"))
        {
            ScoreManager.Instance.WhiteBallPocketed();
            BallShooter.Instance.ResetWhiteBall();
        }
        else if (other.CompareTag("BlackBall"))
        {
            SpawnParticles(other.transform.position);
            ScoreManager.Instance.ResetScore();
            other.gameObject.SetActive(false);
        }
        else
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb == null) return;

            SpawnParticles(other.transform.position);
            ScoreManager.Instance.BallPocketed();
            other.gameObject.SetActive(false);
        }
    }

    void SpawnParticles(Vector3 position)
    {
        if (pocketParticlesPrefab == null) return;
        GameObject vfx = Instantiate(pocketParticlesPrefab, position, Quaternion.identity);
        Destroy(vfx, 1f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        CapsuleCollider cc = GetComponent<CapsuleCollider>();
        if (cc != null)
            Gizmos.DrawWireSphere(transform.position + cc.center, cc.radius * transform.lossyScale.x);
    }
}
