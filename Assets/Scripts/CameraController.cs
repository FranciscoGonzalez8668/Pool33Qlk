using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform whiteBall;

    [Range(10f,200f)]
    public float rotateSpeed = 80f;
    [Range(1f,10f)]
    public float zoomSpeed = 3f;
    public float minDistance = 2f;
    public float maxDistance = 15f;
    public float minPitch = 5f;
    public float maxPitch = 85f;

    private float yaw;
    private float pitch;
    private float distance;

    void Start()
    {
        // Inicializar desde la posicion actual de la camara
        Vector3 offset = transform.position - whiteBall.position;
        distance = offset.magnitude;
        yaw = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;
        pitch = Mathf.Asin(offset.y / distance) * Mathf.Rad2Deg;
    }

    void Update()
    {
        yaw += Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        pitch -= Input.GetAxis("Vertical") * rotateSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        float pitchRad = pitch * Mathf.Deg2Rad;
        float yawRad = yaw * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Sin(yawRad) * Mathf.Cos(pitchRad),
            Mathf.Sin(pitchRad),
            Mathf.Cos(yawRad) * Mathf.Cos(pitchRad)
        ) * distance;

        transform.position = whiteBall.position + offset;
        transform.LookAt(whiteBall);
    }


    void OnDrawGizmos()
    {
        if(whiteBall ==null)return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, whiteBall.position);
        Gizmos.DrawWireSphere(whiteBall.position, 0.5f);
    }

}
