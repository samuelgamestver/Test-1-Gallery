using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class StartScene : MonoBehaviour
{
    private bool loading = false;

    private int percent;

    [SerializeField] GameObject Button;

    [SerializeField] GameObject Bar;

    [SerializeField] TextMeshProUGUI PercentTxt;
    public void Click()
    {
        Button.SetActive(false);

        Bar.SetActive(true);

        loading = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    private void FixedUpdate()
    {
        if(loading && percent <100)
        {
            percent++;

            PercentTxt.text = percent.ToString() + " %";

            Bar.GetComponent<Slider>().value = percent / 100f;
            
        }

        if (percent >= 100)
            Next();
    }

    private void Next()
    {
        SceneManager.LoadScene(1);
    }
}
