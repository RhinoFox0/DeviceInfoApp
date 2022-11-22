using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Antilatency.SDK;
using Antilatency.DeviceNetwork;
using UnityEngine.UI;


public class DeviceInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text[] textInfo;
    DeviceNetwork deviceNetwork;
    private NodeHandle[] _nodes;
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private GameObject canvas;

    private GameObject panelDeviceTemplate;
    private GameObject panelTrackingTemplate;
    

    void Awake()
    {
        deviceNetwork = GetComponent<DeviceNetwork>();
    }
    void Start()
    {
        var panelWidth = Screen.width / 10;
        var panelHeight = Screen.height / 2;

        var panelTrackingWidth = Screen.width / 5;

        panelDeviceTemplate = createPanel((panelWidth / 2), (panelHeight / 2) * -1, panelWidth, panelHeight);
        panelTrackingTemplate = createPanel(panelTrackingWidth / 2, (panelHeight / 2) * -1, panelTrackingWidth, panelHeight);
        panelDeviceTemplate.SetActive(false);
        panelTrackingTemplate.SetActive(false);

        _nodes = deviceNetwork.NativeNetwork.getNodes();

        createPanels(_nodes.Length);

        Debug.Log("Nodes count: " + _nodes.Length.ToString());
    }

    

    void Update()
    {


        //for (int i = 0; i < _nodes.Length; i++)
        //{
        //    textInfo[i].text = $"Hardware Name: {deviceNetwork.NativeNetwork.nodeGetStringProperty(_nodes[i], "sys/HardwareName")} \n" +
        //    $"Serial number: \n" +
        //    $"ConnLimit:";
        //}
        

    }

    GameObject createPanel(float x, float y, float width, float height)
    {
        GameObject panel = new GameObject("Panel");
        panel.AddComponent<CanvasRenderer>();
        Image i = panel.AddComponent<Image>();
        i.color = Color.red;
        panel.transform.SetParent(canvas.transform, false);
        var rectTransform = panel.GetComponent<RectTransform>();
        
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);   
        rectTransform.localPosition = new Vector3(x,y,0);
        rectTransform.sizeDelta = new Vector2(width, height);
        //rectTransform.rect.Set(x, y, width, height);

        return panel;
    }

    void createPanels(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var rectTransform = panelDeviceTemplate.GetComponent<RectTransform>();
            panels.Add(createPanel(rectTransform.rect.x * (i + 1), rectTransform.rect.y * -1, rectTransform.rect.width, rectTransform.rect.height));
        }
    }
}
