using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public int health;
    public float speed;

    private void Start() {
        // Logica movimiento puercos
        StartCoroutine(MovePig());
    }

    private IEnumerator MovePig()
    {
        while (true)
        {
            // Mover a la izquierda
            yield return Move(Vector3.left);
            // Esperar
            yield return new WaitForSeconds(1.0f);

            // Mover a la derecha
            yield return Move(Vector3.right);
            // Esperar
            yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator Move(Vector3 direction)
    {
        float elapsedTime = 0;
        float duration = 0.5f; // Duración del movimiento
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + direction * 0.5f; // Avanzar muy poco

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Asegurarse de que el movimiento llegue a la posición final
    }
}
