using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCharacterPos : MonoBehaviour
{
    public bool isStep;

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        isStep = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isStep = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Delay());
        }
    }
}
