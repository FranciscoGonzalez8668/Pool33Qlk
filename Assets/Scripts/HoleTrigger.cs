using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WhiteBall"))
        {
            ScoreManager.Instance.WhiteBallPocketed();
            BallShooter.Instance.ResetWhiteBall();
        }
        else
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb == null) return;

            ScoreManager.Instance.BallPocketed();
            other.gameObject.SetActive(false);
        }
    }
}
