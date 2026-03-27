public class Action
{
    protected string actionName;

    public Action(string name)
    {
        actionName = name;
    }

    public virtual void Execute(Character target)
    {
        // default action (can be empty)
    }

    public virtual string GetDescription()
    {
        return actionName;
    }
}
