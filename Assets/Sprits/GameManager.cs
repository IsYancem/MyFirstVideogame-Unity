using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Añade una variable pública para la puntuación
    public TextMeshProUGUI puntuacionText; // Declarar la puntuación como TextMeshProUGUI, no como Text


    public float velocidad = 2;
    public Renderer fondo;
    public GameObject col1;
    public GameObject piedra1;
    public GameObject piedra2;
    public bool start = false;
    public bool gameOver = false;

    public GameObject menuInicio;
    public GameObject menuGameOver;

    public List<GameObject> suelo;
    public List<GameObject> obstaculos;

    public Jugador jugador;

    private void Start()
    {
        // Crear Mapa
        for (int i = 0; i < 21; i++)
        {
            suelo.Add(Instantiate(col1, new Vector2(-10 + i, -3), Quaternion.identity));
        }

        //Crear Obstaculos
        obstaculos.Add(Instantiate(piedra1, new Vector2(15, -2), Quaternion.identity));
        obstaculos.Add(Instantiate(piedra2, new Vector2(20, -2), Quaternion.identity));

        puntuacionText.text = "500";
    }

    private void Update()
    {
        if (!start && !gameOver)
        {
            menuInicio.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                start = true;
            }
        }
        else if(gameOver)
        {
            menuGameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            menuInicio.SetActive(false);
            menuInicio.SetActive(false);
            //Mover BG
            fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.015f, 0) * velocidad * Time.deltaTime;

            // Mover Mapa
            for (int i = 0; i < suelo.Count; i++)
            {
                if (suelo[i].transform.position.x <= -10)
                {
                    suelo[i].transform.position = new Vector3(10f, -3, 0);
                }
                suelo[i].transform.position = suelo[i].transform.position + new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            }

            //Mover Obstaculos
            for (int i = 0; i < obstaculos.Count; i++)
            {
                if (obstaculos[i].transform.position.x <= -10)
                {
                    float randomObs = Random.Range(10, 18);
                    obstaculos[i].transform.position = new Vector3(randomObs, -2, 0);
                }
                obstaculos[i].transform.position = obstaculos[i].transform.position + new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            }
        }

        if (!gameOver)
        {
            if (puntuacionText == null)
            {
                Debug.LogError("puntuacionText no se ha asignado en el inspector");
            }
            else if (jugador == null)
            {
                Debug.LogError("jugador no se ha asignado en el inspector");
            }
            else if (jugador.vida == null)
            {
                Debug.LogError("La vida del jugador no se ha inicializado correctamente");
            }
            else
            {
                // Aquí es donde actualizarías el texto de la puntuación
                puntuacionText.text = jugador.vida.ToString();
            }
        }
    }
}
