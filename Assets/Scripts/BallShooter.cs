using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    public static BallShooter Instance;
    public Rigidbody whiteBalls;
    public Camera mainCamera;

    public float MaxForce = 15f;
    public float chargeSpeed = 1.5f;

    public Image powerBarFill;

    public Transform spawnPoint;

    private float currentPower = 0f;
    private float chargeStartTime = 0f;
    private bool isCharging = false;
    private Vector3 hitDirection;


    void Awake()
    {
        Instance = this;
        spawnPoint.position = whiteBalls.position;
    }

    public void ResetWhiteBall()
    {
        whiteBalls.velocity = Vector3.zero;
        whiteBalls.angularVelocity = Vector3.zero;
        whiteBalls.transform.position = spawnPoint.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryStartCharge();

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
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;
        if (hit.rigidbody != whiteBalls) return;

        hitDirection = (whiteBalls.position - hit.point).normalized;
        hitDirection.y = 0f;
        chargeStartTime = Time.time;
        isCharging = true;

        powerBarFill?.gameObject.SetActive(true);
    }

    void Shoot()
    {
        whiteBalls.AddForce(hitDirection * currentPower, ForceMode.Impulse);
        isCharging = false;
        currentPower = 0f;
        ScoreManager.Instance.OnShotFired();

        powerBarFill?.gameObject.SetActive(false);
    }

    void UpdatePowerBar()
    {
        if (powerBarFill == null) return;
        float t = currentPower / MaxForce;
        powerBarFill.fillAmount = t;
        powerBarFill.color = Color.Lerp(Color.green, Color.red, t);
    }
}
