using UnityEngine;

public class PruebaCubito : MonoBehaviour
{
    // Referencia al componente AudioSource, que se utilizará para obtener los datos del espectro de audio.
    public AudioSource audioSource;

    // Referencia al GameObject cubo, que es el objeto que vamos a modificar (agrandar o reducir) en función del sonido.
    public GameObject cubo;

    // Definimos el índice de la frecuencia dentro del array del espectro de audio. En este caso, estamos interesados en el valor de la frecuencia número 10.
    public int indiceFrecuencia = 10; // Índice de la frecuencia en el array del espectro.

    // Factor de escala que se aplicará al cubo cuando la frecuencia supere el umbral, es decir, cuánto se agrandará.
    public float factorEscala = 0.5f; // Factor de escala para agrandar el cubo.

    // Creamos un array de flotantes que almacenará los datos del espectro de audio. El tamaño de este array es 512, que es el número de muestras en el espectro de audio.
    private float[] spectrumData = new float[512]; // Array para almacenar los datos del espectro.

    public AnimationCurve curvaCrecimiento; // Curva para el crecimiento del cubo.
    public AnimationCurve curvaDecrecimiento; // Curva para el decrecimiento del cubo.

    private float tiempoTranscurrido = 0f;



    // Método Update, que se llama en cada frame. Aquí es donde se ejecutará la lógica de agrandar o reducir el cubo basado en el sonido.
    void Update()
    {
        // Usamos el método GetSpectrumData del AudioSource para obtener los datos del espectro de audio.
        // Esta función rellena el array spectrumData con las frecuencias del sonido en tiempo real.
        // El segundo parámetro es el canal de audio (0 para el canal principal), y el tercero define el tipo de ventana FFT (Rectangular).
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        // Obtenemos el valor específico de la frecuencia en el índice seleccionado (en este caso, el índice es 10).
        // Este valor indica la intensidad de la frecuencia en ese punto del espectro.
        float valorFrecuencia = spectrumData[indiceFrecuencia];

        // Comprobamos si el valor de la frecuencia obtenida supera un umbral mínimo para determinar si el sonido es lo suficientemente fuerte.
        if (valorFrecuencia > 0.1f)
        {
            tiempoTranscurrido += Time.deltaTime; // Aumentamos el tiempo transcurrido.

            float nuevoTamaño = curvaCrecimiento.Evaluate(tiempoTranscurrido);

            // Si el valor de la frecuencia supera el umbral, se considera que hay suficiente sonido y procedemos a agrandar el cubo.
            // Usamos Mathf.Lerp para hacer una interpolación suave entre el tamaño actual del cubo y el tamaño deseado (factorEscala).
            // Esto hace que el cambio en el tamaño sea gradual y no abrupto.

            //float nuevoTamaño = Mathf.Lerp(cubo.transform.localScale.y, factorEscala, Time.deltaTime * 10);  // ESTE ES CON EL MATHF.LERP

            // Actualizamos la escala del cubo, pero solo cambiamos la componente Y (altura), mientras mantenemos la X y Z constantes.
            cubo.transform.localScale = new Vector3(cubo.transform.localScale.x, nuevoTamaño, cubo.transform.localScale.z);
        }
        else
        {
            // Si la frecuencia no supera el umbral, comenzamos a decrecer el tamaño del cubo.
            tiempoTranscurrido += Time.deltaTime; // Aumentamos el tiempo transcurrido.


            float nuevoTamaño = curvaDecrecimiento.Evaluate(tiempoTranscurrido);

            // Si el valor de la frecuencia no supera el umbral, significa que no hay suficiente sonido y el cubo debe reducirse.
            // Nuevamente, usamos Mathf.Lerp para hacer que el cambio en el tamaño sea gradual.
            // La escala del cubo vuelve a ser 1 (tamaño original).


            //float nuevoTamaño = Mathf.Lerp  (cubo.transform.localScale.y, 1.0f, Time.deltaTime * 10); // ESTE ES CON EL MATHF.LERP

            // Actualizamos la escala del cubo para devolverlo a su tamaño original, manteniendo las componentes X y Z iguales.
            cubo.transform.localScale = new Vector3(cubo.transform.localScale.x, nuevoTamaño, cubo.transform.localScale.z);
        }
    }


}
