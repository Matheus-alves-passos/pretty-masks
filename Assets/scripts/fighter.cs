using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fighter : MonoBehaviour
{
    public string idName;
    public statusPanel statusPanel;

    public combateManager combatManager;

    protected Stats stats;

    protected virtual void Start()
    {
        this.statusPanel.SetStats(this.idName, this.stats);
    }

    public void ModifyHealth(float amount)
    {
        this.stats.health = Mathf.Clamp(this.stats.health + amount, 0f, this.stats.maxHealth);
        this.statusPanel.SetHealth(this.stats.health, this.stats.maxHealth);
    }
    public abstract void InitTurn();
}
