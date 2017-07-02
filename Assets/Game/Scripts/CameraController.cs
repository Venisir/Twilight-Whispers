using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> players;

    // [SerializeField]
    // private List<GameObject> objectsList = new List<GameObject>();

    [SerializeField]
    private float cameraSpeed = 0.02f;

    [SerializeField]
    private float cameraAngle = 35f;

    [SerializeField]
    private float minZoom = 8.0f;

    [SerializeField]
    private float maxZoom = 40.0f;

    [SerializeField]
    private float elevationY = 1.0f;
    
    //Limites de los bordes de la pantalla
    private float leftBound = 0;
    private float rightBound = 0;
    private float topBound = 0;
    private float bottomBound = 0;
    
    private Vector2 cameraCenter;
    private Vector3 cameraPosition;

    private float zoom = 1.0f;
    
    void Start()
    {
        this.transform.Rotate(new Vector3(cameraAngle, 0, 0));
    }
    
    void Update()
    {

    }

    void FixedUpdate()
    {
        //Cada frame se inicializan al valor del jugador 1
        leftBound = players[0].transform.position.x;
        rightBound = players[0].transform.position.x;
        topBound = players[0].transform.position.z;
        bottomBound = players[0].transform.position.z;

        // leftBound = objectsList[0].transform.position.x;
        // rightBound = objectsList[0].transform.position.x;
        // topBound = objectsList[0].transform.position.z;
        // bottomBound = objectsList[0].transform.position.z;

        //Y se compara con el resto de jugadores
        //Si alguno se aleja del centro, expande el borde

        // foreach (GameObject player in players)
        // {
        //     if (player.transform.position.x < leftBound)
        //     {
        //         leftBound = player.transform.position.x;
        //     }
        //     if (player.transform.position.x > rightBound)
        //     {
        //         rightBound = player.transform.position.x;
        //     }
        //     if (player.transform.position.z > topBound)
        //     {
        //         topBound = player.transform.position.z;
        //     }
        //     if (player.transform.position.z < bottomBound)
        //     {
        //         bottomBound = player.transform.position.z;
        //     }
        // }

        //Calculando el centro de la camara
        cameraCenter = new Vector2(((rightBound + leftBound) / 2), ((topBound + bottomBound) / 2));

        //Calculando el zoom de la camara
        //Valores mas altos implican una camara mas lejana
        zoom = Mathf.Sqrt(Mathf.Pow(((rightBound - leftBound) / 1.6f), 2) + Mathf.Pow(((topBound - bottomBound) / 0.9f), 2));

        //Limitadores de distancia
        if (zoom < minZoom)
        {
            zoom = minZoom;
        }

        if (zoom > maxZoom)
        {
            zoom = maxZoom;
        }

        //La posicion que debe tomar la camara
        cameraPosition = new Vector3((cameraCenter.x)/* - (zoom * Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.y)))*/,
                                     zoom * Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.x)) + elevationY,
                                     (cameraCenter.y) - (zoom * Mathf.Cos(Mathf.Deg2Rad * (transform.rotation.eulerAngles.x)))
                                    );

        //Movimiento suavizado con interpolado lineal
        transform.position = Vector3.Lerp(transform.position, cameraPosition, cameraSpeed);
    }
}
