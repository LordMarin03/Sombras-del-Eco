using UnityEngine;
using UnityEngine.UI;

public class HideOnClick : MonoBehaviour
{
    [Header("Imagen que se ocultará")]
    [SerializeField] private GameObject imagen;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo
        {
            if (imagen != null)
                imagen.SetActive(false);
        }
    }
}
