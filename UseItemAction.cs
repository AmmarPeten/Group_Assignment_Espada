using System;

public class UseItemAction : Action
{
    private Character user;
    private Item item;

    public UseItemAction(Character user, Item item) : base("Use Item")
    {
        this.user = user;
        this.item = item;
    }

    public override void Execute(Character target)
    {
        Console.WriteLine(user.Name + " uses " + item.Name + " on " + target.Name + "!");
        
        item.Apply(target);
    }

    public override string GetDescription()
    {
        return actionName + ": " + item.Name;
    }
}
