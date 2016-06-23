using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum estate
{
    jump =1,
    stand = 2,
    fight = 3,
    walk = 4,
    resist = 5
}

public class IAStats
{
    public static IAStats FIRST_IA = new IAStats(estate.walk);
    public static IAStats SECONDE_IA = new IAStats(estate.stand);
    
    Dictionary<estate, int> counter;
    estate actual, second;

    private IAStats() { }
    private IAStats(estate baseState) {
        counter = new Dictionary<estate, int>();
        counter.Add(estate.fight, 0);
        counter.Add(estate.jump, 0);
        counter.Add(estate.resist, 0);
        counter.Add(estate.stand, 0);
        counter.Add(estate.walk, 0);
        actual = baseState;
        second = baseState;
    }

    private void AddOneIncToEstate(estate whereToAdd)
    {
        counter[estate.fight]--;
        counter[estate.jump]--;
        counter[estate.resist]--;
        counter[estate.stand]--;
        counter[estate.walk]--;
        if (actual == whereToAdd)
        {
            if (counter[actual] < counter[second] + 5)
                counter[actual]+=2;
        }
        else {
            counter[whereToAdd]+=2;
            if (counter[whereToAdd] > counter[second])
                second = whereToAdd;
            if (counter[whereToAdd] > counter[actual]+5)
            {
                second = actual;
                actual = whereToAdd;
            }
        }

    }


     public estate? CheckActionToDo(Rigidbody2D Player, IAInterpretator PNJ, Animator playerAnimator) {
        estate? toChange = null;
        if (Mathf.Abs(Player.velocity.x) > 3.5)
            toChange = estate.fight;
        if(Mathf.Abs(Player.velocity.y) > 0)
            toChange = estate.jump;
        if (playerAnimator.GetBool("isPunchLeft") || playerAnimator.GetBool("isPunchRight") || playerAnimator.GetBool("isKickLeft") || playerAnimator.GetBool("isKickRight"))
            toChange = estate.resist;
        Debug.Log(toChange);
        if (toChange.HasValue)
            AddOneIncToEstate(toChange.Value);
        return actual;
    }

}
