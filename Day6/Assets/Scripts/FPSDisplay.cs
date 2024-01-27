using UnityEngine;
/// <summary>
/// FPS显示工具
/// </summary>
public class FPSDisplay : MonoBehaviour
{

[Header("是否显示")]
public bool isShow = true;
[Header("更新间隔")]
public float updateInterval = 1f;
[Header("字体大小")]
public int fontSize = 42;
[Header("字体颜色")]
public Color fontColor = Color.white;
[Header("距离边缘")]
public int margin = 50;
[Header("显示位置")]
public TextAnchor alignment = TextAnchor.UpperLeft;
private GUIStyle guiStyle;
private Rect rect;
private int frames;
private float fps;
private float lastInterval;
void Start()
{

guiStyle = new GUIStyle();
guiStyle.fontStyle = FontStyle.Bold; //字体加粗
guiStyle.fontSize = fontSize; //字体大小
guiStyle.normal.textColor = fontColor; //字体颜色
guiStyle.alignment = alignment; //对其方式
rect = new Rect(margin, margin, Screen.width - (margin * 2), Screen.height - (margin * 2));
lastInterval = Time.realtimeSinceStartup;
frames = 0;
fps = 0.0f;
}
void Update()
{

++frames;
float timeNow = Time.realtimeSinceStartup;
if (timeNow > lastInterval + updateInterval)
{

fps = frames / (timeNow - lastInterval);
frames = 0;
lastInterval = timeNow;
}
}
void OnGUI()
{

if (!isShow) return;
GUI.Label(rect, "FPS: " + fps.ToString("F2"), guiStyle);
}
}