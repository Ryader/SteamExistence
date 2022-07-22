using UnityEngine;

public class DayManager : MonoBehaviour
{
    [Range(0, 1)]
    public float TimeOfDay;
    public float DayDuration;
    public AnimationCurve SunCurve;
    public Light Sun;
    private float sunIntesity;
    void Start()
    {
        sunIntesity = Sun.intensity;
    }

    void Update()
    {
        TimeOfDay += Time.deltaTime / DayDuration;
        if (TimeOfDay >= 1) TimeOfDay -= 1;

        Sun.transform.localRotation = Quaternion.Euler(TimeOfDay * 360f, 180, 0);
        Sun.intensity = sunIntesity * SunCurve.Evaluate(TimeOfDay);
    }
}
