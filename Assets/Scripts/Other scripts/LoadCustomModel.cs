using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SFB; // Add this if using StandaloneFileBrowser

public class LoadCustomModel : MonoBehaviour
{
    [SerializeField] RawImage previewImage; // UI Preview
    [SerializeField] Renderer pursuerRenderer; // Assign the pursuer's face mesh renderer

    public void OpenFilePicker()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Select an Image", "", "jpg", false);
        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            LoadImage(paths[0]);
        }
    }

    public void LoadImage(string path)
    {
        if (File.Exists(path))
        {
            byte[] imageData = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);
            texture.Apply();

            pursuerRenderer.material.mainTexture = texture;
            previewImage.texture = texture; // Show preview in UI
        }
    }
}
