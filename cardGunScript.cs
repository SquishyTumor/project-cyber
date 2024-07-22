using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardGunScript : MonoBehaviour
{
    public List<Weapon> playerDeck = new List<Weapon>();
    public Weapon[] playerDeckPasserArray;
    public List<Weapon> discardPile = new List<Weapon>();
    public Dictionary<int, Weapon> activePlayerCardHand = new Dictionary<int, Weapon>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    public bool gameHasStarted;

    public Transform heldWeaponSlot;
    public Weapon activeWeapon;
    public PlayerWeaponScript PlayerWeaponScript;

    public Image[] weaponImageArray;
    public Sprite[] weaponAttackProjectileSpriteArray;
    /*
    Remember that shit starts with 0
    0: Bazooka
    */
    // Start is called before the first frame update
    void Start()
    {
        playerDeckPasserArray = FindObjectsOfType<Weapon>(true);
        for (int i = 0; i == playerDeckPasserArray.Length; i++) //might cause a bug because of count 0
        {
            playerDeck.Add(playerDeck[i]);
        }

        Weapon Bazooka = new Weapon(100, 1, 10, weaponAttackProjectileSpriteArray[0], weaponImageArray[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameHasStarted)
        {
            fillDeck();
            gameHasStarted = false;
        }

        if (activeWeapon.durability == 0)
        {
            discardPile.Add(activeWeapon);
            activeWeapon = null;
            switch (Input.inputString)
            {
                case "1":
                    activeWeapon = activePlayerCardHand[1];
                    DrawCard();
                    break;
                case "2":
                    activeWeapon = activePlayerCardHand[2];
                    DrawCard();
                    break;
                case "3":
                    activeWeapon = activePlayerCardHand[3];
                    DrawCard();
                    break;
                case "4":
                    activeWeapon = activePlayerCardHand[4];
                    DrawCard();
                    break;
                case "5":
                    activeWeapon = activePlayerCardHand[5];
                    DrawCard();
                    break;
                case "6":
                    activeWeapon = activePlayerCardHand[6];
                    DrawCard();
                    break;
            }

        }

        if (Input.GetKey(KeyCode.Mouse0)) {
            PlayerWeaponScript.attackScript = activeWeapon.weaponAttackProjectileSprite
            instantiate();
        }
    }

    public void fillDeck()
    {
        DrawCard();
        DrawCard();
        DrawCard();
        DrawCard();
        DrawCard();
        DrawCard();
    }

    public void DrawCard()
    {
        if (playerDeck.Count >= 1)
        {
            Weapon randomWeapon = playerDeck[Random.Range(0, playerDeck.Count)];
            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randomWeapon.gameObject.SetActive(true);
                    randomWeapon.transform.position = cardSlots[i].position;
                    activePlayerCardHand[i] = randomWeapon;
                    availableCardSlots[i] = false;
                    playerDeck.Remove(randomWeapon);
                    return;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (activeWeapon.) //for killing shit
    }
}

public class Weapon : MonoBehaviour
{
    //weapon card base stuff
    public Image weaponCardImage;
    public Sprite weaponAttackProjectileSprite;
    public bool hasBeenPlayed;

    //weapon stats
    public bool isWeapon;
    public bool penetratesEnemies;
    public bool isBroken;
    public int damage;
    public int attackSpeed;
    public int durability;
    public string weaponAnimationSetName;

    public Weapon(int damage, int attackSpeed, int durability, Sprite weaponAttackProjectileSprite, Image weaponCardImage)     
    {
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.durability = durability;
        this.weaponAttackProjectileSprite = weaponAttackProjectileSprite;
        this.weaponCardImage = weaponCardImage;
    }

}

//https://gamedev.stackexchange.com/questions/106352/how-to-instantiate-a-sprite-from-a-sprite-sheet-using-script-in-unity steal this dumbass
//maybe add a static class here for all of the weapons AND animations? At the same time you could just kill yourself

/*
TODO:
Multiple weapons (just clone this shit, you'll need the others for this one)
Auto fill deck at start X
Weapon durability usage X
Fill on start X
Draw card on keyboard press X
Card movement animation (This should be debated over, do it with the boys)
Card merging
*/
