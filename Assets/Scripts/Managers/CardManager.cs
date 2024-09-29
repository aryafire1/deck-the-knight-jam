using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;

public class CardManager : MonoBehaviour
{
    // Allows CardManager to be accessed by name from other scripts
    #region Singleton

    private static CardManager _singleton;
    public static CardManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
            {
                _singleton = value;
                DontDestroyOnLoad(value);
            }
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(CardManager)} instance already exists, destoying duplicate");
                Destroy(value);
            }
        }
    }

    private void Awake()
    {
        Singleton = this;
    }

    #endregion

    public delegate void OnCardChanged(); // When called, calls every method subscribed. Used to update inventory UI
    public OnCardChanged onCardChangedCallback;
    public Wizard wizard;

    public int space = 0;

    public List<Card> cardList = new List<Card>();

    bool timer = true; // Makes sure we can't spam card activations

    Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public bool Add(Card newCard) // Adds new card that player collects
    {
        if (cardList.Count >= space) // If the player has too many cards, don't add new card
        {
            Debug.Log("Not enough room for this card");
            return false;
        }
        else
        {
            cardList.Add(newCard);
        }

        CallbackMethod(); // Update UI
        return true;
    }

    public void Remove(Card newCard) // Removes card upon usage
    {
        if (cardList.Contains(newCard)) // Clears card from inventory if all cards used up
        {
            cardList.Remove(newCard);
        }

        CallbackMethod(); // Update UI
    }

    void CallbackMethod()
    {
        if (onCardChangedCallback != null)
        {
            onCardChangedCallback.Invoke();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            string result = "List contents: ";
            foreach (Card card in cardList)
            {
                result += card.cardName + ", ";
            }
            Debug.Log(result);
        }

        if (timer && Input.GetKeyDown(KeyCode.T))
        {
            timer = false;
            foreach (Card card in cardList)
            {
                UnityAction<Card> cardAction = (UnityAction<Card>)Delegate.CreateDelegate(typeof(UnityAction<Card>), this, card.posType.ToString()); // Gets card effect name, matches it with card effect method
                card.OnPositive += delegate { cardAction(card); }; // Actually makes the code run for positive effect
                UnityAction<Card> negCardAction = (UnityAction<Card>)Delegate.CreateDelegate(typeof(UnityAction<Card>), this, card.negType.ToString()); // Gets card effect name, matches it with card effect method
                card.OnNegative += delegate { negCardAction(card); }; // Actually makes the code run for negative effect
                StartCoroutine(WaitForEffectToEnd(card, card.Use())); // Waits for effect to end
            }
        }
    }

    public void UseCard(Card card)
    {
        UnityAction<Card> cardAction = (UnityAction<Card>)Delegate.CreateDelegate(typeof(UnityAction<Card>), this, card.posType.ToString()); // Gets card effect name, matches it with card effect method
        card.OnPositive += delegate { cardAction(card); }; // Actually makes the code run for positive effect
        UnityAction<Card> negCardAction = (UnityAction<Card>)Delegate.CreateDelegate(typeof(UnityAction<Card>), this, card.negType.ToString()); // Gets card effect name, matches it with card effect method
        card.OnNegative += delegate { negCardAction(card); }; // Actually makes the code run for negative effect
        StartCoroutine(WaitForEffectToEnd(card, card.Use())); // Waits for effect to end
    }

    private IEnumerator WaitForEffectToEnd(Card playedCard, float duration) // Do the negative effect after the positive effect
    {
        yield return new WaitForSeconds(duration);
        playedCard.NegativeEffect(); // Does negative effect for the card
        timer = true;
        Remove(playedCard); // Removes card from inventory
    }

    #region Card Effects
    // Make sure the method has the same name as the cardType name in the Card scriptable object


    public void Attack(Card card)
    {
        GameManager.ChangeSpellSlot(-(int)card.effectAmount);

        Debug.Log("attack");
    }

    public void Shield(Card card)
    {
        player.becomeInvul(card.duration * 5);

        Debug.Log("shield");
    }
    public void Health(Card card)
    {
        player.takeDamage(-(int)card.effectAmount);

        Debug.Log("health");
    }
    public void Stun(Card card)
    {
        wizard.canAttack = false;
        StartCoroutine(StunEnd(card.duration));

        Debug.Log("stun");
    }
    IEnumerator StunEnd(float duration)
    {
        yield return new WaitForSeconds(duration);
        wizard.canAttack = true;
    }

    public void Gamble(Card card)
    {
        GameManager.manager.score += 20;

        Debug.Log("gamble");
    }
    public void Anger(Card card)
    {
        //Player.speed = Player.speed * 2;

        Debug.Log("anger");
    }

    public void Slow(Card card)
    {
        player.changeSpeed(0.5f, card.duration);
        Debug.Log("slow");
    }

    public void WizHeal(Card card)
    {
        GameManager.ChangeSpellSlot((int)card.effectAmount);

        Debug.Log("wizHeal");
    }
    public void WizStun(Card card)
    {
        player.dashCooldown = card.duration;

        Debug.Log("wizStun");
    }

    public void WizGamble(Card card)
    {
        int originalDmg = wizard.damage;
        wizard.damage *= 2;
        StartCoroutine(WizGambleEnd(card.duration, originalDmg));

        Debug.Log("wizGamble");
    }

    IEnumerator WizGambleEnd(float duration, int originalDmg)
    {
        yield return new WaitForSeconds(duration);
        wizard.damage = originalDmg;
    }

    #endregion
}
