using UnityEngine.UI;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    private Image IconImg;
    private Text DistanceText;

    public Transform player;
    public Transform Target;
    public Camera cam;

    public float CloseEnoughtDist;

    #region Singleton
    public static Waypoint Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            print("Several instances of waypoints found");
            return;
        }
        Instance = this;
    }
    #endregion

    void Start()
    {
        IconImg = GetComponent<Image>();
        DistanceText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (Target != null)
        {
            GetDistance();
            CheckOnScreen();
            UpdDistance();
        }
    }

    private void UpdDistance()
    {
        if (Vector3.Distance(player.position, Target.position) < CloseEnoughtDist)
        {
            IconImg.enabled = false;
            DistanceText.enabled = false;
        }
    }

    public void UpdateWaypoint(Transform target)
    {
        Target = target;
    }

    private void GetDistance()
    {
        float dist = Vector3.Distance(player.position, Target.position);
        DistanceText.text = dist.ToString("f1") + "m";

        if (dist < CloseEnoughtDist)
        {
            IconImg.enabled = false;
        }
    }

    private void CheckOnScreen()
    {
        float thing = Vector3.Dot((Target.position - cam.transform.position).normalized, cam.transform.forward);
        if (thing <= 0)
        {
            ToggleUI(false);
        }
        else
        {
            ToggleUI(true);
            transform.position = cam.WorldToScreenPoint(Target.position);
        }
    }


    private void ToggleUI(bool _value)
    {
        IconImg.enabled = _value;
        DistanceText.enabled = _value;
    }
}
