using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    
    public Transform Target { get; set; }

    private void Update()
    {
        if (Target)
        {
            transform.position = Vector3.Lerp(transform.position, Target.position, m_Speed * Time.deltaTime);
        }
    }
}