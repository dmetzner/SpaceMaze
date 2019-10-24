using System.Collections;
using System.Threading;
using UnityEngine;

public class FramesPerSecond : MonoBehaviour
{
  float deltaTime = 0.0f;
  GUIStyle style;
  Rect rect;

  private void Awake()
  {
    QualitySettings.vSyncCount = 0;

    Application.targetFrameRate = 60;

    rect = new Rect(100, 10, 400, 100);

    style = new GUIStyle();
    style.alignment = TextAnchor.UpperLeft;
    style.fontSize = 24;
    style.normal.textColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

    DontDestroyOnLoad(gameObject);
  }

  void Update()
  {
    deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
  }

  void OnGUI()
  {
    float msec = deltaTime * 1000.0f;
    float fps = 1.0f / deltaTime;
    string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
    GUI.Label(rect, text, style);
  }
}