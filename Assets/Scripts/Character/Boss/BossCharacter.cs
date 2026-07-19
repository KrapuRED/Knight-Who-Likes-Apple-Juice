using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public enum BossAttackType
{
    StaffAttack,
    ClawAttack,
    ShadowDodge
}

[System.Serializable]
public class BossAttackData
{
    public BossAttackType attackType;
    public float weight = 1f;
    public float cooldown = 3f;
    public int repeatAttack = 1;       // how many times this attack fires in a row
    public float delayBetweenRepeats = 0.5f; // pause between each repeat
    public float postAttackDelay = 1f; // pause after finishing, before boss is "free" again

    [HideInInspector] public float lastUsedTime = -999f;
}

public class BossCharacter : EnemyCharacter
{
    [Header("Boss Attacks")] 
    [SerializeField] private List<BossAttackData> availableAttacks = new();
    [SerializeField] private StatusBarUI attackBarUI;
    
    private bool _isAttacking;
    private Coroutine _attackRoutine;

    private void Update()
    {
        if (_isAttacking)
            return; // don't fill the bar or choose a new attack while one is in progress

        if (currentAttackBar >= maxAttackBar)
        {
            ChooseAttack();
            currentAttackBar = 0f;
            return;
        }
        
        currentAttackBar += Time.deltaTime * rateAttackBar;
        currentAttackBar = Mathf.Clamp(currentAttackBar, 0f, maxAttackBar);

        isAttackBarFull = (currentAttackBar >= maxAttackBar);
        
        attackBarUI.UpdateStatusBar(currentAttackBar, maxAttackBar);
    }

    private void ChooseAttack()
    {
        List<BossAttackData> eligible = availableAttacks
            .Where(a => Time.time - a.lastUsedTime >= a.cooldown)
            .ToList();

        if (eligible.Count == 0)
        {
            Debug.Log($"{gameObject.name}: no eligible boss attacks (all on cooldown)");
            return;
        }

        BossAttackData chosen = WeightedRandom(eligible);
        chosen.lastUsedTime = Time.time;

        Debug.Log($"{gameObject.name} chose attack: {chosen.attackType} x{chosen.repeatAttack}");

        _attackRoutine = StartCoroutine(RunAttackSequence(chosen));
    }

    private BossAttackData WeightedRandom(List<BossAttackData> pool)
    {
        float totalWeight = pool.Sum(a => a.weight);
        float roll = Random.Range(0f, totalWeight);

        float cumulative = 0f;
        foreach (var attack in pool)
        {
            cumulative += attack.weight;
            if (roll <= cumulative)
                return attack;
        }

        return pool[^1];
    }

    private IEnumerator RunAttackSequence(BossAttackData data)
    {
        _isAttacking = true;
        int repeatCount = Mathf.Max(1, data.repeatAttack);

        for (int i = 0; i < repeatCount; i++)
        {
            ExecuteAttack(data.attackType);

            if (i < repeatCount - 1)
            {
                yield return new WaitForSeconds(data.delayBetweenRepeats);
            }
        }

        if (data.postAttackDelay > 0f)
            yield return new WaitForSeconds(data.postAttackDelay);
        
        _isAttacking = false;
        _attackRoutine = null;
    }

    private void ExecuteAttack(BossAttackType type)
    {
        int attackId = (int)type;

        switch (type)
        {
            case BossAttackType.StaffAttack:
                AttackSingleLane(attackId, "staff");
                break;

            case BossAttackType.ClawAttack:
                AttackAllLanes(attackId, "claw");
                break;
        }
    }

    private void AttackSingleLane(int index, string soundEffect)
    {
        Debug.LogWarning("Attacking single lane");
        PointMovement target = ManagerPosition.Instance.CurrentPlayer;
        CharacterAttack.OnAttackByState(target, soundEffect);
    }

    private void AttackAllLanes(int attackIndex, string soundEffect)
    {
        CharacterAttack.OnAttackMultipleByState(attackIndex, soundEffect);
    }

    private void AttackRandomBarrage(int attackIndex)
    {
        PointMovement target = ManagerPosition.Instance.GetRandomPoint();
        ManagerPosition.Instance.DropHitBoxByPoint(target, this, DamageValue, target.PointDirection, attackIndex);
    }

    public void OnDodgeAttack()
    {
        // boss-specific dodge reaction
    }
}