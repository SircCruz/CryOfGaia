using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyAttackObject : MonoBehaviour
{
    public bool isMelee;

    [HideInInspector] public float duration;
    [HideInInspector] public bool destroyOnImpact;

    [HideInInspector] public float travelspeed;

    [HideInInspector] public BoxCollider2D meleeCollider;

    Transform character;
    Vector3 getCharaPos;
    private void Start()
    {
        if (!isMelee)
        {
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            getCharaPos = new Vector2(character.transform.position.x, character.transform.position.y);
        }
    }
    void FixedUpdate()
    {
        if (isMelee)
        {
            meleeCollider.enabled = true;
            meleeCollider.isTrigger = true;

            duration -= Time.fixedDeltaTime;
            if (duration <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if(transform.position.x < character.transform.position.x)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            }
            transform.position = Vector2.MoveTowards(transform.position, getCharaPos, travelspeed * Time.fixedDeltaTime);

            if(transform.position == getCharaPos)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyOnImpact)
        {
            Destroy(gameObject);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyAttackObject))]
class EnemyAttackObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var meleeFlag = target as EnemyAttackObject;
        meleeFlag.isMelee = GUILayout.Toggle(meleeFlag.isMelee, "Melee");

        if (meleeFlag.isMelee)
        {
            meleeFlag.meleeCollider = EditorGUILayout.ObjectField("Melee Hitbox:", meleeFlag.meleeCollider, typeof(BoxCollider2D), true) as BoxCollider2D;
            meleeFlag.duration = EditorGUILayout.FloatField("Active Duration:", meleeFlag.duration);
            meleeFlag.destroyOnImpact = GUILayout.Toggle(meleeFlag.destroyOnImpact, "Destroy On Impact");
        }
        else
        {
            meleeFlag.travelspeed = EditorGUILayout.FloatField("Travel Speed:", meleeFlag.travelspeed);
        }
        
    }
}
#endif