using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    public SoundEvent soundEvent;
    public AudioSource sfxSource;
    protected HealthComponent healthComponent;
    protected AttackComp attackComp;
    protected Animator animator;
    protected EnemyCombatAI enemyCombatAI;
    protected NavMeshAgent agent;

    protected List<GameObject> SpawnedDrops = new List<GameObject>();
    public UnityEvent OnEnemyDeath;
    protected SpriteRenderer sprite;
    public float fleeTime = 2f;
    [HideInInspector] public bool timerDone = false;

    bool dead = false;
    private void Awake()
    {
        attackComp = GetComponent<AttackComp>();
        healthComponent = GetComponent<HealthComponent>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        enemyCombatAI = GetComponent<EnemyCombatAI>();
        agent = GetComponent<NavMeshAgent>();
        attackComp.damage = enemyData.Damage;
        attackComp.attackDelay = enemyData.AttackDelay;
        healthComponent.InitData(enemyData.MaxHp);
    }
    protected void Start()
    {
        healthComponent.OnZeroHealth += OnDeath;
        healthComponent.OnDamage += OnDamageTemp;
        InitializeLoot();
    }
    protected virtual void OnDamageTemp(float dmg)
    {
        //if (enemyCombatAI.stateLocked)
        //    return;

        StartCoroutine(TempDamageAnim());
    }
    IEnumerator TempDamageAnim()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;

    }
    Coroutine fleeTimerInstance;
    public void StartFleeTimer()
    {
        if (fleeTimerInstance != null)
        {
            StopCoroutine(fleeTimerInstance);
        }
        fleeTimerInstance = StartCoroutine(fleeTimer());
    }
    protected IEnumerator fleeTimer()
    {
        yield return new WaitForSeconds(fleeTime);
        timerDone = true;
    }

    protected void InitializeLoot()
    {
        int dropCount = Random.Range(enemyData.MinDropCount, enemyData.MaxDropCount);
        for (int i = 0; i < dropCount; i++)
        {
            GameObject drop = GameObject.Instantiate(enemyData.Drops[0]);
            drop.transform.SetParent(this.transform);
            drop.transform.localPosition = Vector3.zero;
            drop.transform.rotation = this.transform.rotation;
            Pickup dropData = drop.GetComponent<Pickup>();
            int angle = Random.Range(0, 360);
            dropData.Init(50, angle, true);

            SpawnedDrops.Add(drop);
            drop.SetActive(false);
        }
    }
    protected void DropLoot()
    {
        foreach (GameObject drop in SpawnedDrops)
        {
            drop.transform.SetParent(null);
            drop.SetActive(true);
            drop.GetComponent<Pickup>().Launch();
        }
    }

    protected void OnDeath()
    {
        if (dead) //enemyCombatAI.stateLocked)
            return;

        dead = true;
        enemyCombatAI.stateLocked = true;
        agent.ResetPath();
        agent.isStopped = true;
        OnEnemyDeath?.Invoke();

        animator.SetBool("die", true);
        sfxSource.PlayOneShot(soundEvent.clips[0]);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        StartCoroutine(HandleDestroy());
    }

    protected IEnumerator HandleDestroy()
    {
        sprite.enabled = false;
        yield return new WaitForSeconds(0.5f);
        DropLoot();
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
