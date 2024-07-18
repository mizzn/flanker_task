using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Capture2DImage : MonoBehaviour
{
    public Camera _camera;
    private string dirPath = "Assets/Prehabs/2D";
    public string fileName;

    // Start is called before the first frame update
    void Start()
    {
        // _camera = GetComponent<Camera>(); // もしくは適切な方法で Camera を取得する
        if (_camera == null)
        {
            Debug.LogError("Camera component not found.");
            return;
        }else{
            Debug.Log("Camera component found.");
        }
        
        if (_camera.targetTexture == null)
        {
            _camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        }

        var tex = Get2DImage();
        SaveImage(dirPath, fileName, tex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Texture2D Get2DImage(){
        _camera.Render();
        var targetTexture = _camera.targetTexture; //カメラの画素の集まりを取得
        if (targetTexture == null){
            Debug.LogError("targetTexture is null");
        }
        var width = targetTexture.width; //それぞれ横と縦の画素数
        var height = targetTexture.height;

        var current = RenderTexture.active; //元の描画データを取っておく
        var backgroundColor = _camera.backgroundColor; //背景も取っておく

        var temp = RenderTexture.GetTemporary(width, height, 0); //同じ大きさでRenderTextureを取得
        RenderTexture.active = temp; //これからはtempについて操作を行う

        {
            GL.Clear(true, true, Color.clear); //背景を透明にする
            Graphics.Blit(targetTexture, temp); //カメラが見ているものをtempにコピーする
        }

        var format = TextureFormat.ARGB32; //ピクセルフォーマットを指定，透明度情報がいるのでARGB
        var resultTexture = new Texture2D(width, height, format, false); //指定した大きさ，フォーマットの2Dテクスチャを作る,falseでミニマップを作成しない
        resultTexture.hideFlags = HideFlags.DontSave; // 保存されないようにする
        resultTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0, false); //tempのテクスチャをコピーする
        resultTexture.Apply(); //変更したピクセルデータを反映させる

        RenderTexture.active = current; //描画データをもとに戻す
        RenderTexture.ReleaseTemporary(temp); //使ったものを削除

        _camera.backgroundColor = backgroundColor;

        return resultTexture;
    }

    private void SaveImage(string dirPath, string fileName, Texture2D tex)
    {
        Debug.Log("Save Image");
        if (Directory.Exists(dirPath))
        {
             var path = dirPath + "/" + fileName;
            File.WriteAllBytes($"{path}.png", tex.EncodeToPNG());
            Debug.Log("save png");
        }else{
            Debug.Log("dont exist");
        }
    }
}
