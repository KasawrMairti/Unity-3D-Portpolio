using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform objectTofollow;
    public float followSpeed = 10.0f;
    public float sensitivity = 100.0f;
    public float clampAngle = 70.0f;

    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 50.0f;

    public bool b_moveCamera = true;

    [SerializeField] private GameObject[] objectWall;

    private void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (b_moveCamera)
        {
            rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
            rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
            Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
            transform.rotation = rot;
        }
        else
        {
            Player player = CharacterManager.Instance.player;

            rotX = player.transform.localRotation.eulerAngles.x;
            rotY = player.transform.localRotation.eulerAngles.y;
        }
    }

    private void LateUpdate()
    {
        if (b_moveCamera)
        {
            transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);

            finalDir = transform.TransformPoint(dirNormalized * maxDistance);

            RaycastHit hit;
            if (Physics.Linecast(transform.position, finalDir, out hit) && !hit.collider.isTrigger)
            {
                finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            }
            else
                finalDistance = maxDistance;

            realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
        }
    }

    public RaycastHit RayCastHit(float maxDistance = 0.0f)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (maxDistance == 0.0f)
            Physics.Raycast(ray, out hit);
        else
            Physics.Raycast(ray, out hit, maxDistance);
        
        return hit;
    }
}
