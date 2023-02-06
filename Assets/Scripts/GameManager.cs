using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject menuGameOver;

    public GameObject gPlayer1;
    public GameObject gPlayer2;
    // public CinemachineVirtualCamera vcam;
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioInicio;

    private GameObject player1;
    private GameObject player2;

    public bool gameOver = false;
    public bool start = false;

    // Camara
    public GameObject CMvcam1;
    public List<Transform> targets;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.R))
            {
                start = true;
                // player1 = Instantiate(gPlayer1, new Vector2(0, 0), transform.rotation);
                player2 = Instantiate(gPlayer2, new Vector2(0, 0), transform.rotation);
                audioSource.PlayOneShot(audioInicio);
                // vcam.Follow = player1.transform;
                // CMvcam1.Follow = targets;
                
            }
        }
        else if (gameOver)
        {
            menuGameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (start && !gameOver)
        {
            menuPrincipal.SetActive(false);
        }
    }
}
