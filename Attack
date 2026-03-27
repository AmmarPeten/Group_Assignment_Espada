using System;

public class AttackAction : Action
{
    private Character attacker;
    private int baseDamage;

    public AttackAction(Character attacker) : base("Attack")
    {
        this.attacker = attacker;
        baseDamage = attacker.Attack;
    }

    public override void Execute(Character target)
    {
        int damage = CalculateDamage();

        Console.WriteLine(attacker.Name + " attacks " + target.Name + " for " + damage + " damage!");

        target.TakeDamage(damage);
    }

    private int CalculateDamage()
    {
        Random random = new Random();

        // simple variation: -2 to +2
        int damage = baseDamage + random.Next(-2, 3);

        return damage;
    }

    public override string GetDescription()
    {
        return actionName + " (" + baseDamage + " damage)";
    }
}
