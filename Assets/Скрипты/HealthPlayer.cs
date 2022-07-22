using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HealthPlayer : MonoBehaviour
{
    public RectTransform healthTransform;
    private float cachedY;
    private float minXValue;
    private float maxXValue;

    public int currentHealth;

    private float HP_regen;
    private float TimeDelayHP;
    public float TimeDelay = 1;

    public int quantityFlask = 5;
    public int quantityHP = 25;

    private int СurrentHealth
    {
        get { return currentHealth; }

        set {
            currentHealth = value;
            HandleHealth();
            }
    }

    public int maxHealth;
    public Text healthText;
    public Image visualHealth;

    public float collDown;
    private bool onCD;

    // Start is called before the first frame update
    void Start()
    {
        cachedY = healthTransform.position.y;
        maxXValue = healthTransform.position.x;
        minXValue = healthTransform.position.x - healthTransform.rect.width;
        currentHealth = maxHealth;
        onCD = false;
    }

    // Update is called once per frame
    void Update()
    {
      
       if(currentHealth<=0)
        {
            Application.LoadLevel(Application.loadedLevel);
            Destroy(gameObject);
        }

       if (currentHealth<maxHealth)
        {
            TimeDelayHP += Time.deltaTime;

            if(TimeDelayHP>=TimeDelay)
            {
                currentHealth ++;
                TimeDelayHP = 0;

                if(currentHealth>=maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
            HandleHealth();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(currentHealth+quantityHP>=maxHealth)
            {
                currentHealth = maxHealth;
            }
            if (currentHealth < maxHealth)
            {             
                currentHealth += quantityHP;
                quantityFlask--;
            }
            HandleHealth();

            if(quantityHP+currentHealth>=maxHealth)
            {
                quantityFlask--;
            }         
            
            if(quantityFlask<=0)
            {
                quantityHP = 0;
                quantityFlask = 0;               
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Damage")
        {
            if (!onCD && currentHealth > 0)
            {
                Debug.Log("-HP");
                StartCoroutine(CollDownDmg());
                СurrentHealth -= 1;
            }
        }
            if(other.name=="Health")
            {
                Debug.Log("+HP");

                if(!onCD && currentHealth<maxHealth)
                {
                    StartCoroutine(CollDownDmg());
                    СurrentHealth += 1;
                }

            }
        
    }

    IEnumerator CollDownDmg()
    {
        onCD = true;
        yield return new WaitForSeconds(collDown);
        onCD = false;
    }

    private void HandleHealth()
    {
        healthText.text = "Health: " + currentHealth;
        float currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);
        healthTransform.position = new Vector3(currentXValue, cachedY);

        if(currentHealth>maxHealth/2)
        {
            visualHealth.color = new Color32((byte)MapValues(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
        }

        else
        {
            visualHealth.color = new Color32(255,(byte) MapValues(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
        }
    }

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    
}
