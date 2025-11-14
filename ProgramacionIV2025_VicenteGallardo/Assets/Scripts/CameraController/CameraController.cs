using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform pj;

    private float tamañoCamera;
    private float alturaPantalla;

    private void Start()
    {
        tamañoCamera = Camera.main.orthographicSize;
    }

    void CalcularPosicionCamera()
    {
        int pantallaPersonaje = (int) (pj.position.y / alturaPantalla);
        float alturaCamara = (pantallaPersonaje * alturaPantalla) + tamañoCamera;
        
        transform.position = new Vector3(transform.position.x, alturaCamara, transform.position.z);
    }
}
