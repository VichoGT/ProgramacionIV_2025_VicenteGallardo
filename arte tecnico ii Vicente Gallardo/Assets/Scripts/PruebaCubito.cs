using UnityEngine;

public class PruebaCubito : MonoBehaviour
{
    // Referencia al componente AudioSource, que se utilizar� para obtener los datos del espectro de audio.
    public AudioSource audioSource;

    // Referencia al GameObject cubo, que es el objeto que vamos a modificar (agrandar o reducir) en funci�n del sonido.
    public GameObject cubo;

    // Definimos el �ndice de la frecuencia dentro del array del espectro de audio. En este caso, estamos interesados en el valor de la frecuencia n�mero 10.
    public int indiceFrecuencia = 10; // �ndice de la frecuencia en el array del espectro.

    // Factor de escala que se aplicar� al cubo cuando la frecuencia supere el umbral, es decir, cu�nto se agrandar�.
    public float factorEscala = 0.5f; // Factor de escala para agrandar el cubo.

    // Creamos un array de flotantes que almacenar� los datos del espectro de audio. El tama�o de este array es 512, que es el n�mero de muestras en el espectro de audio.
    private float[] spectrumData = new float[512]; // Array para almacenar los datos del espectro.

    public AnimationCurve curvaCrecimiento; // Curva para el crecimiento del cubo.
    public AnimationCurve curvaDecrecimiento; // Curva para el decrecimiento del cubo.

    private float tiempoTranscurrido = 0f;



    // M�todo Update, que se llama en cada frame. Aqu� es donde se ejecutar� la l�gica de agrandar o reducir el cubo basado en el sonido.
    void Update()
    {
        // Usamos el m�todo GetSpectrumData del AudioSource para obtener los datos del espectro de audio.
        // Esta funci�n rellena el array spectrumData con las frecuencias del sonido en tiempo real.
        // El segundo par�metro es el canal de audio (0 para el canal principal), y el tercero define el tipo de ventana FFT (Rectangular).
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        // Obtenemos el valor espec�fico de la frecuencia en el �ndice seleccionado (en este caso, el �ndice es 10).
        // Este valor indica la intensidad de la frecuencia en ese punto del espectro.
        float valorFrecuencia = spectrumData[indiceFrecuencia];

        // Comprobamos si el valor de la frecuencia obtenida supera un umbral m�nimo para determinar si el sonido es lo suficientemente fuerte.
        if (valorFrecuencia > 0.1f)
        {
            tiempoTranscurrido += Time.deltaTime; // Aumentamos el tiempo transcurrido.

            float nuevoTama�o = curvaCrecimiento.Evaluate(tiempoTranscurrido);

            // Si el valor de la frecuencia supera el umbral, se considera que hay suficiente sonido y procedemos a agrandar el cubo.
            // Usamos Mathf.Lerp para hacer una interpolaci�n suave entre el tama�o actual del cubo y el tama�o deseado (factorEscala).
            // Esto hace que el cambio en el tama�o sea gradual y no abrupto.

            //float nuevoTama�o = Mathf.Lerp(cubo.transform.localScale.y, factorEscala, Time.deltaTime * 10);  // ESTE ES CON EL MATHF.LERP

            // Actualizamos la escala del cubo, pero solo cambiamos la componente Y (altura), mientras mantenemos la X y Z constantes.
            cubo.transform.localScale = new Vector3(cubo.transform.localScale.x, nuevoTama�o, cubo.transform.localScale.z);
        }
        else
        {
            // Si la frecuencia no supera el umbral, comenzamos a decrecer el tama�o del cubo.
            tiempoTranscurrido += Time.deltaTime; // Aumentamos el tiempo transcurrido.


            float nuevoTama�o = curvaDecrecimiento.Evaluate(tiempoTranscurrido);

            // Si el valor de la frecuencia no supera el umbral, significa que no hay suficiente sonido y el cubo debe reducirse.
            // Nuevamente, usamos Mathf.Lerp para hacer que el cambio en el tama�o sea gradual.
            // La escala del cubo vuelve a ser 1 (tama�o original).


            //float nuevoTama�o = Mathf.Lerp  (cubo.transform.localScale.y, 1.0f, Time.deltaTime * 10); // ESTE ES CON EL MATHF.LERP

            // Actualizamos la escala del cubo para devolverlo a su tama�o original, manteniendo las componentes X y Z iguales.
            cubo.transform.localScale = new Vector3(cubo.transform.localScale.x, nuevoTama�o, cubo.transform.localScale.z);
        }
    }


}
