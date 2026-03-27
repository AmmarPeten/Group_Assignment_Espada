using System;

public class DefendAction : Action
{
    private Character defender;
    private int defenseBonus;

    public DefendAction(Character defender, int defenseBonus = 5) : base("Defend")
    {
        this.defender = defender;
        this.defenseBonus = defenseBonus;
    }

    public override void Execute(Character target)
    {
        Console.WriteLine(defender.Name + " is defending and will take less damage!");
        
        // simple example: increase defense stat
        defender.Defense += defenseBonus;
    }

    public override string GetDescription()
    {
        return actionName + " (-" + defenseBonus + "% damage taken)";
    }
}
