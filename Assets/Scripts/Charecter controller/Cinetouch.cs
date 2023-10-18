using UnityEngine;
using Cinemachine;

public class Cinetouch : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cineCam;
    [SerializeField] Transform forwardOrientation;
    [SerializeField] TouchField touchField;
    [SerializeField] float SenstivityX = 0.2f;
    [SerializeField] float SenstivityY = -0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cineCam.m_XAxis.Value += touchField.TouchDist.x * 200 * SenstivityX * Time.deltaTime;
        cineCam.m_YAxis.Value -= touchField.TouchDist.y * SenstivityY * Time.deltaTime;

        // Crea un Vector3 para almacenar la rotación deseada
        Vector3 nuevaRotacion = new Vector3(0, cineCam.m_XAxis.Value, 0); // Solo asignamos el valor al eje Y, puedes ajustar los ejes según tus necesidades
        // Asigna la nueva rotación al objetoDestino
        forwardOrientation.rotation = Quaternion.Euler(nuevaRotacion);
    }
}