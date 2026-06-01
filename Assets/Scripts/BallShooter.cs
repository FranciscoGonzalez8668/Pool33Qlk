using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    public static BallShooter Instance;
    public Rigidbody whiteBall;
    public Camera mainCamera;

    public float MaxForce = 15f;
    public float chargeSpeed = 1.5f;

    public Image powerBarFill;
    public LineRenderer aimLine;
    public float aimLineLength = 2f;

    public Transform spawnPoint;

    private float currentPower = 0f;
    private float chargeStartTime = 0f;
    private bool isCharging = false;
    private Vector3 hitDirection;


    void Awake()
    {
        Instance = this;
        spawnPoint.position = whiteBall.position;
    }

    public void ResetWhiteBall()
    {
        whiteBall.velocity = Vector3.zero;
        whiteBall.angularVelocity = Vector3.zero;
        whiteBall.transform.position = spawnPoint.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryStartCharge();

        UpdateAimLine();

        if (isCharging)
        {
            currentPower = Mathf.PingPong((Time.time - chargeStartTime) * chargeSpeed, MaxForce);
            UpdatePowerBar();

            if (Input.GetMouseButtonUp(0))
                Shoot();
        }
    }

    void TryStartCharge()
    {
        
        if (whiteBall.velocity.magnitude > 0.05f) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;
        if (hit.rigidbody != whiteBall) return;

        hitDirection = (whiteBall.position - hit.point).normalized;
        hitDirection.y = 0f;
        chargeStartTime = Time.time;
        isCharging = true;

        powerBarFill?.gameObject.SetActive(true);
    }

    void Shoot()
    {
        whiteBall.AddForce(hitDirection * currentPower, ForceMode.Impulse);
        isCharging = false;
        currentPower = 0f;
        ScoreManager.Instance.OnShotFired();

        powerBarFill?.gameObject.SetActive(false);
        if (aimLine != null) aimLine.enabled = false;
    }

    void UpdateAimLine()
    {
        if (aimLine == null) return;
        if (whiteBall.velocity.magnitude > 0.05f) { aimLine.enabled = false; return; }

        if (isCharging)
        {
            aimLine.enabled = true;
            aimLine.SetPosition(0, whiteBall.position);
            aimLine.SetPosition(1, whiteBall.position + hitDirection * aimLineLength);
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit) || hit.rigidbody != whiteBall)
        {
            aimLine.enabled = false;
            return;
        }

        Vector3 dir = (whiteBall.position - hit.point).normalized;
        dir.y = 0f;

        aimLine.enabled = true;
        aimLine.SetPosition(0, whiteBall.position);
        aimLine.SetPosition(1, whiteBall.position + dir * aimLineLength);
    }

    void UpdatePowerBar()
    {
        if (powerBarFill == null) return;
        float t = currentPower / MaxForce;
        powerBarFill.fillAmount = t;
        powerBarFill.color = Color.Lerp(Color.green, Color.red, t);
    }

    void OnDrawGizmos()
    {
        if (whiteBall == null || hitDirection == Vector3.zero) return;

        Gizmos.color = Color.cyan;

        if (Physics.Raycast(whiteBall.position, hitDirection, out RaycastHit hit))
        {
            Gizmos.DrawLine(whiteBall.position, hit.point);
            Gizmos.DrawWireSphere(hit.point, 0.15f);
        }
        else
        {
            Gizmos.DrawLine(whiteBall.position, whiteBall.position + hitDirection * aimLineLength);
            Gizmos.DrawWireSphere(whiteBall.position + hitDirection * aimLineLength, 0.1f);
        }
    }

}
