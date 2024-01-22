using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

using PDollarGestureRecognizer;

public class Demo : MonoBehaviour
{

    public Transform gestureOnScreenPrefab;
    public RectTransform canvasRectTransform; // Reference to the canvas' RectTransform

    private List<Gesture> trainingSet = new List<Gesture>();

    private List<Point> points = new List<Point>();
    private int strokeId = -1;

    private Vector3 virtualKeyPosition = Vector2.zero;
    private Rect drawArea;

    private RuntimePlatform platform;
    private int vertexCount = 0;

    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private LineRenderer currentGestureLineRenderer;

    //GUI
    public string message;
    public bool recognized;
    private string newGestureName = "";

    void Start()
    {

        platform = Application.platform;
        drawArea = new Rect(0, 0, Screen.width, Screen.height);

        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        //Load user custom gestures
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
    }

    void Update()
    {

        // checking if you need to change input system for different platforms; not important for this project
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }
        }
        else
        {   //on mouse click, get mouse position
            if (Input.GetMouseButton(1))
            {
                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            }
        }

        // Convert the mouse/touch position to screen space and adjust it to be relative to the canvas position
        Vector2 screenPos = virtualKeyPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPos, Camera.main, out Vector2 canvasPos);
        canvasPos += canvasRectTransform.rect.size / 2;

        // if cursor is in drawArea
        if (drawArea.Contains(virtualKeyPosition))
        {
            //if right click is clicked down
            if (Input.GetMouseButtonDown(1))
            {

                if (recognized)
                {

                    recognized = false;
                    strokeId = -1;
                    //clear all rendered lines and points
                    points.Clear();

                    foreach (LineRenderer lineRenderer in gestureLinesRenderer)
                    {

                        lineRenderer.SetVertexCount(0);
                        Destroy(lineRenderer.gameObject);
                    }

                    gestureLinesRenderer.Clear();
                }

                ++strokeId;
                //prepare gesture recognition and line renderer
                Transform tmpGesture = Instantiate(gestureOnScreenPrefab, canvasRectTransform);
                currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

                // Set the position of the line renderer to the center of the canvas
                currentGestureLineRenderer.transform.position = canvasRectTransform.rect.center;

                gestureLinesRenderer.Add(currentGestureLineRenderer);

                vertexCount = 0;
            }
            //while right click is held down
            if (Input.GetMouseButton(1))
            {
                points.Add(new Point(canvasPos.x, -canvasPos.y, strokeId));

                currentGestureLineRenderer.SetVertexCount(++vertexCount);
                currentGestureLineRenderer.SetPosition(vertexCount - 1, canvasPos);
            }

            if (Input.GetMouseButtonUp(1))
            {
                recognized = true;

                Gesture candidate = new Gesture(points.ToArray());
                Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

                message = gestureResult.GestureClass + " " + gestureResult.Score;
                points.Clear();
            }
        }
    }

    /*void OnGUI() {

        //GUI.Box(drawArea, "Draw Area");

        GUI.Label(new Rect(10, Screen.height - 40, 500, 50), message);

        if (GUI.Button(new Rect(Screen.width - 100, 10, 100, 30), "Recognize"))
        {

            recognized = true;

            Gesture candidate = new Gesture(points.ToArray());
            Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

            message = gestureResult.GestureClass + " " + gestureResult.Score;
        }

        GUI.Label(new Rect(Screen.width - 200, 150, 70, 30), "Add as: ");
		newGestureName = GUI.TextField(new Rect(Screen.width - 150, 150, 100, 30), newGestureName);

		if (GUI.Button(new Rect(Screen.width - 50, 150, 50, 30), "Add") && points.Count > 0 && newGestureName != "") {

			string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName, DateTime.Now.ToFileTime());

			#if !UNITY_WEBPLAYER
				GestureIO.WriteGesture(points.ToArray(), newGestureName, fileName);
			#endif

			trainingSet.Add(new Gesture(points.ToArray(), newGestureName));

			newGestureName = "";
		}
	}*/
}
