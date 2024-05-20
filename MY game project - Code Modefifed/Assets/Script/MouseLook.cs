using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    public float minimumVert = 35.0f;
    public float maximumVert = 45.0f;
    private PlayerCharacter playerCharacter;

    private float rotationX = 0;

    void Start()
    {
        playerCharacter = GetComponentInParent<PlayerCharacter>();
    }
    void Update()
    {
        if (Time.timeScale == 1 && playerCharacter.die == false)
        {
            if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            }
            else if (axes == RotationAxes.MouseY)
            {
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);
                transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, 0);
            }
            else
            {
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);

                float delta = Input.GetAxis("Mouse X") * sensitivityHor;
                transform.localEulerAngles += new Vector3(rotationX, delta, 0);
            }
        }
    }
}
