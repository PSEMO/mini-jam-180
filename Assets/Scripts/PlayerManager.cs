using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverDiedObj;
    [SerializeField] GameObject GameWonObj;
    [SerializeField] GameObject GameOverNotEnoughEnergyObj;

    [SerializeField] float EnergyNeededToWin = 70;

    [SerializeField] Image EnergyValueImg;

    [SerializeField] float MaxEnergy = 100;
    public float Energy = 100;
    [SerializeField] float EnergyLoseMult = 1;
    [SerializeField] float EnergyGainRefresher = 10;
    [SerializeField] float EnergyLoseEnemy = 10;

    bool isImmune = false;
    [SerializeField] float ImmunityTimeAfterDmg = 5;
    float ImmunityStopwatch = 0;

    SpriteRenderer SpriteRenderer;
    Color DefaultColor;

    void Start()
    {
        Time.timeScale = 1.0f;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        DefaultColor = SpriteRenderer.color;
    }

    void Update()
    {
        ChangeEnergy(-1 * Time.deltaTime * EnergyLoseMult);
        EnergyValueImg.fillAmount = Energy / MaxEnergy;

        if(isImmune)
        {
            ImmunityStopwatch += Time.deltaTime;

            if(ImmunityStopwatch > ImmunityTimeAfterDmg)
            {
                SpriteRenderer.color = DefaultColor;

                ImmunityStopwatch = 0;
                isImmune = false;
            }
            else
            {
                float percentage = ImmunityStopwatch / ImmunityTimeAfterDmg;
                SpriteRenderer.color = new Color(
                    DefaultColor.r * percentage,
                    DefaultColor.g * percentage,
                    DefaultColor.b * percentage,
                    DefaultColor.a);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Refresher"))
        {
            ChangeEnergy(EnergyGainRefresher);

            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy") && !isImmune)
        {
            ChangeEnergy(-EnergyLoseEnemy);

            isImmune = true;
        }
        else if (collision.CompareTag("EndGame"))
        {
            if(Energy > EnergyNeededToWin)
            {
                GameWonObj.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                GameOverNotEnoughEnergyObj.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    void ChangeEnergy(float EnergyChange)
    {
        Energy += EnergyChange;

        if (Energy < 0)
        {
            GameOverDiedObj.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Energy > MaxEnergy)
        {
            Energy = MaxEnergy;
        }
    }
}
