using UnityEngine;

// 攻擊報告 預計在碰狀箱接觸, 或其他方法觸發
public class OnAttackEvent
{
    public GameObject attacker; // 使用者, 用以讀各種數值
    public GameObject target;  // 在之後才計算抗性, 目前只傳值
    public Skill skill;  // 技能, 計算傷害倍率
}


public class OnSkillAnimationPlay { }


// 技能
public class OnSkillTriggerEvent
{
    public GameObject user; // 使用者, 用以讀各種數值
    public Skill usedSkill;       // 技能,
    public int damageDealt;  // 攻擊方傷害結算, 接下來傳給被攻擊方
    public string damageType; // 在之後才做使用, 目前只傳值
}


// 受傷「之前」觸發，訂閱者可以修改傷害值或取消傷害
public class BeforeTakeDamageEvent
{
    public GameObject target;   // 受傷的對象
    public int modifiableDamage;          // 可以被修改的傷害值
    public bool cancelled;      // 設為 true 可取消這次傷害
    public string damageSource; // 傷害來源描述（如 "fire", "sword"）
}


// 用於event trigger 傷害結算
public class TakingDamageEvent
{
    public GameObject target;   // 受傷的對象
    public int finaleDamage;          // 可以被修改的傷害值
}

// 受傷「之後」觸發，此時傷害已經結算，僅供通知用
public class AfterTakeDamageEvent
{
    public GameObject target;
    public int damageDealt;     // 實際造成的傷害, 用於UI 跳傷害數字等
    public int remainingHealth;  // 用於通知血條UI更新
    public string damageSource;  // 傷害屬性, 更改傷害顏色
}

// 治療「之前」觸發，訂閱者可以修改治療量或取消治療
public class BeforeHealEvent
{
    public GameObject target;
    public int healAmount;      // 可以被修改的治療量
    public bool cancelled;
}

// 治療「之後」觸發
public class AfterHealEvent
{
    public GameObject target;
    public int healedAmount;    // 實際治療的量
    public int remainingHealth;
}

// 死亡「之前」觸發，訂閱者可以取消死亡（例如「不死之身」效果）
public class BeforeDeathEvent
{
    public GameObject target;
    public bool cancelled;      // 設為 true 可阻止死亡（保留 1 HP）
}

// 死亡「之後」觸發
public class AfterDeathEvent
{
    public GameObject target;
}
