using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Gallery : MonoBehaviour
{
    public int SizeGallery = 66;

    int loadImage = 1;

    [SerializeField] GameObject Content;

    [SerializeField] GameObject Viewer;

    [SerializeField] GameObject PrefabImage;

    public List<GameObject> Images;

    private void Start()
    {
        CreateGallery();
    }

    private void CreateGallery()
    {
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 525 * (SizeGallery / 2));

        int PosX = 0;

        for (int i= 0; i < SizeGallery; i++)
        {
            GameObject newImg = Instantiate(PrefabImage);

            newImg.transform.SetParent(Content.transform);

            if (i % 2 == 0)
                PosX = 1;
            else
                PosX = -1;

            newImg.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/2*0.93f, Screen.width / 2 * 0.93f);

            newImg.GetComponent<RectTransform>().anchoredPosition = new Vector2(-265 * PosX, -(265 + 525 * (i / 2)));

            Images.Add(newImg);

        }

        StartCoroutine(DownloadImage());
    }


    IEnumerator DownloadImage()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture("http://data.ikppbb.com/test-task-unity-data/pics/" + loadImage.ToString() + ".jpg");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;

            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));

            Images[loadImage - 1].GetComponent<Image>().overrideSprite = sprite;

            if (loadImage < SizeGallery && !Viewer.activeSelf)
            {
                Images[loadImage - 1].GetComponent<ButtonGallery>().number = loadImage - 1;
                Images[loadImage - 1].GetComponent<ButtonGallery>().gal = gameObject.GetComponent<Gallery>();

                loadImage++;

                StartCoroutine(DownloadImage());
            }

        }
    }

    public void OpenViewer(int img)
    {
        Viewer.SetActive(true);

        Debug.Log("Input " + img);

        Viewer.transform.GetChild(0).GetComponent<Image>().sprite = Images[img].GetComponent<Image>().overrideSprite;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Viewer.activeSelf)
            {
                Viewer.SetActive(false);

                StartCoroutine(DownloadImage());
            }    
            else
                SceneManager.LoadScene(0);
        }

        if (Input.acceleration.x > 0.7f)
            Viewer.transform.GetChild(0).eulerAngles = new Vector3(0, 0, 90);

        if (Input.acceleration.x < 0.7f && Input.acceleration.x > -0.7f)
            Viewer.transform.GetChild(0).eulerAngles = new Vector3(0, 0, 0);

        if (Input.acceleration.x < -0.7f)
            Viewer.transform.GetChild(0).eulerAngles = new Vector3(0, 0, -90);




    }
            
}
