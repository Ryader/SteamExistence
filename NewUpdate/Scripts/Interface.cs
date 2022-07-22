using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{

    public float health = 100;

    public float colRed = 0;
    public float colGre = 0;

    public Slider sliderHealth;
    public Image imageHealth;

	public Sprite[] iconWeapon;
	public Image imageWeapon;

    void Update()
    {
        colGre = 255f / 100f * health;
        colRed = 255f / 100f * (100f - health);

        imageHealth.color = new Color((colRed / 255f), (colGre / 255f), 0f, 255f);
        sliderHealth.value = health;

        health = Mathf.Clamp(health, 0, 100);
    }
}
