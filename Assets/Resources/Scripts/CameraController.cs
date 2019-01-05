using UnityEngine;
using System.Collections;
using System.IO;

public class CameraController : MonoBehaviour
{

    public Camera mCamera;
    private RenderTexture mRenderTexture;

    private int x = 128, y = 80;
    private Texture2D png;
    private string path;

    private bool flag = false;

    // Use this for initialization
    void Start()
    {
        mRenderTexture = mCamera.targetTexture;
        png = new Texture2D(128, 128);
        path = Application.persistentDataPath + "/ImageCache/";

        Invoke("setFlag", 5f);

        if (!Directory.Exists(Application.persistentDataPath + "/ImageCache/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/ImageCache/");
        }
    }

    private int i = 0, j = 0;
	
    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            RenderTexture.active = mRenderTexture;
            png.ReadPixels(new Rect(0, 0, mRenderTexture.width, mRenderTexture.height), 0, 0);
            png.Apply();
            byte[] bytes = png.EncodeToPNG();
            string name = j.ToString("000") + "_" + i.ToString("000") + ".png";
            File.WriteAllBytes(path + name, bytes);
            RenderTexture.active = null;
            Debug.Log(name);
            i += 1;
            if (i >= x)
            {
                i = 0;
                j += 1;
                if (j >= y)
                {
                    flag = false;
                    return;
                }
            }
            float posX = -9f + 18f / 127f * i;
            float posY = 5.625f - 11.25f / 79f * j;
            mCamera.transform.localPosition = new Vector3(posX, posY, 0f);
        }
    }

    private void setFlag()
    {
        flag = true;
    }
}
