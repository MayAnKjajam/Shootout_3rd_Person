using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CanvasController : MonoBehaviour
{

    public Slider PlayerHealthSlider;
    public GameObject Playerobj,RestartPanel;
    public Button RestartButton, RestartButton1;
    public delegate void Fire();
    public static event Fire FireShot;

    public Button FireBtn, ReloadBtn;
    // Start is called before the first frame update
    void Start()
    {
        Playerobj = GameObject.FindGameObjectWithTag("Player");
        FireBtn.onClick.AddListener(FireGun);
        RestartButton.onClick.AddListener(delegate { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
        RestartButton1.onClick.AddListener(delegate { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
    }




    // Update is called once per frame
    void Update()
    {
        PlayerHealthSlider.value = Playerobj.GetComponent<PlayerController>().Health;
        if (PlayerHealthSlider.value <= 0)
        {
            RestartPanel.SetActive(true);
        }
    }

    private void FireGun()
    {
        FireShot();
    }

}
