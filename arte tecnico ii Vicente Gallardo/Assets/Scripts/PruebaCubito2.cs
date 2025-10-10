using UnityEngine;

public class PruebaCubito2 : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject cubo;
    public int largoDeSpectro = 512;
    public float umbral = 0.1f;
    public float factorEscala = 5.0f;

    [Header("Parámetros de Espiral")]
    public float radioBase = 0.2f;
    public float factorRadio = 0.1f;
    public float alturaPorCubo = 0.05f;

    [Header("Colores del espectro")]
    public Color colorBajo = Color.yellow;   // Color para las frecuencias bajas
    public Color colorAlto = Color.green;    // Color para las frecuencias altas
    public float intensidadColor = 4f;     // Cuánto se ilumina el color con la frecuencia

    private float[] spectrumData;
    private GameObject[] cubos;
    private Renderer[] renderers;

    private const float ANGULO_AUREO = 137.5f * Mathf.Deg2Rad;

    private void Awake()
    {
        InstanciarCubosFibonacci3D();
    }

    void Update()
    {
        MoverCubosColores();
    }

    void InstanciarCubosFibonacci3D()
    {
        spectrumData = new float[largoDeSpectro];
        cubos = new GameObject[largoDeSpectro];
        renderers = new Renderer[largoDeSpectro];

        for (int i = 0; i < largoDeSpectro; i++)
        {
            float angulo = i * ANGULO_AUREO;
            float radio = radioBase + factorRadio * Mathf.Sqrt(i);

            float x = Mathf.Cos(angulo) * radio;
            float z = Mathf.Sin(angulo) * radio;
            float y = 0;

            cubos[i] = Instantiate(cubo, new Vector3(x, y, z), Quaternion.identity);
            renderers[i] = cubos[i].GetComponent<Renderer>();

            // Asigna un color base según el índice (gradiente de colorBajo → colorAlto)
            float t = (float)i / (largoDeSpectro - 1);
            renderers[i].material.color = Color.Lerp(colorBajo, colorAlto, t);
        }
    }

    void MoverCubosColores()
    {
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        for (int i = 0; i < largoDeSpectro; i++)
        {
            float valorFrecuencia = spectrumData[i];
            float intensidad = Mathf.Clamp01(valorFrecuencia * intensidadColor);

            // Escalado vertical
            float nuevoTamanoY = valorFrecuencia > umbral
                ? Mathf.Lerp(cubos[i].transform.localScale.y, factorEscala, Time.deltaTime * 10)
                : Mathf.Lerp(cubos[i].transform.localScale.y, 1.0f, Time.deltaTime * 10);

            cubos[i].transform.localScale = new Vector3(1, nuevoTamanoY, 1);

            //// Cambio dinámico de color según la intensidad de la frecuencia
            //Color baseColor = renderers[i].material.color;
            //Color colorFinal = baseColor * (1 + intensidad); // hace que brille más con la intensidad
            //renderers[i].material.color = colorFinal;
        }
    }
}
