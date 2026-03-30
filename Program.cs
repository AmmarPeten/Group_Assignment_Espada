using System;
using System.Collections.Generic;

namespace TextRPG
{
    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsDefending { get; set; }

        public Character(string name, int health, int attack, int defense)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Defense = defense;
            IsDefending = false;
        }

        public virtual void TakeDamage(int damage)
        {
            int damageTaken = damage;
            
            if (IsDefending)
            {
                damageTaken -= Defense;
                if (damageTaken < 0) damageTaken = 0;
            Console.WriteLine(Name + " is defending! Damage reduced by " + Defense + ".");
            }

            Health -= damageTaken;
            if (Health < 0) Health = 0;

            Console.WriteLine(Name + " took " + damageTaken + " damage! Current Health: " + Health);
        }
    }

    public class Player : Character
    {
        public List<Item> Inventory { get; set; }

        public Player(string name) : base(name, 100, 20, 5)
        {
            Inventory = new List<Item>();
            
            Inventory.Add(new HealthPotion());
            Inventory.Add(new HealthPotion());
            Inventory.Add(new HealthPotion());
        }
    }

    public class Goblin : Character
    {
        public Goblin(int id) : base("Goblin " + id, 30, 10, 2)
        {
        }
    }

    public class Dragon : Character
    {
        public Dragon() : base("Dragon", 150, 25, 10)
        {
        }
    }

    public class Action
    {
        protected string actionName;

        public Action()
        {
            actionName = "Unknown Action";
        }

        public virtual void Execute(Character target)
        {
           
        }

        public virtual string GetDescription()
        {
            return actionName;
        }
    }

    public class AttackAction : Action
    {
        private Character attacker;

        public AttackAction(Character attacker)
        {
            actionName = "Attack";
            this.attacker = attacker;
        }

        public override void Execute(Character target)
        {
            Console.WriteLine(attacker.Name + " attacks " + target.Name + " with " + attacker.Attack + " attack power!");
            target.TakeDamage(attacker.Attack);
        }
    }

    public class DefendAction : Action
    {
        public DefendAction()
        {
            actionName = "Defend";
        }

        public override void Execute(Character target) 
        {
            Console.WriteLine(target.Name + " takes a defensive stance.");
            target.IsDefending = true;
        }
    }

    public class UseItemAction : Action
    {
        private Item itemToUse;

        public UseItemAction(Item item)
        {
            actionName = "Use Item";
            itemToUse = item;
        }

        public override void Execute(Character target)
        {
            Console.WriteLine(target.Name + " uses " + itemToUse.Name + "!");
            itemToUse.Apply(target);
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public Item(string name)
        {
            Name = name;
        }

        public virtual void Apply(Character target)
        {
        }
    }

    public class HealthPotion : Item
    {
        public HealthPotion() : base("Health Potion")
        {
        }

        public override void Apply(Character target)
        {
            int heal = 40;
            int maxHealth = 100;
            target.Health += heal;
            if (target.Health > maxHealth) target.Health = maxHealth;
            Console.WriteLine(target.Name + " restores " + heal + " HP! Current Health is now " + target.Health + ".");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to the Text RPG ===");
            Console.Write("Enter your character's name: ");
            string playerName = Console.ReadLine() ?? "Hero";
            if (string.IsNullOrWhiteSpace(playerName)) playerName = "Hero";

            Player player = new Player(playerName);
            
            Console.WriteLine("════════════════════════════════════════");
            Console.WriteLine("  A princess has been kidnapped by a    ");
            Console.WriteLine("  dragon, enter the dragon's lair and   ");
            Console.WriteLine("    save the princess dear hero          ");
            Console.WriteLine("         " + playerName + "!                          ");
            Console.WriteLine("════════════════════════════════════════");
            
            int currentLevel = 1;
            int enemiesDefeatedInLevel = 0;
            int totalEnemiesDefeated = 0;
            bool gameRunning = true;
            bool quitGame = false;

            while (gameRunning && player.Health > 0)
            {
                Character currentEnemy;

                if (currentLevel == 3)
                {
                    currentEnemy = new Dragon();
                    Console.WriteLine("*** A MIGHTY DRAGON APPEARS! THE FINAL BATTLE BEGINS! ***");
                }
                else
                {
                    currentEnemy = new Goblin(totalEnemiesDefeated + 1);
                    Console.WriteLine("--- A wild " + currentEnemy.Name + " appears! ---");
                }

                // Combat Loop
                while (currentEnemy.Health > 0 && player.Health > 0)
                {
                    
                    player.IsDefending = false;
                    currentEnemy.IsDefending = false;

                    Console.WriteLine("========================================");
                    Console.WriteLine("[Level " + currentLevel + " | Enemies Defeated: " + totalEnemiesDefeated + "]");
                    Console.WriteLine(player.Name + " HP: " + player.Health);
                    Console.WriteLine(currentEnemy.Name + " HP: " + currentEnemy.Health);
                    Console.WriteLine("========================================");
                    
                    Console.WriteLine("Choose an action:");
                    Console.WriteLine("1. Attack");
                    Console.WriteLine("2. Defend");
                    
                    int potionsCount = 0;
                    foreach (var item in player.Inventory)
                    {
                        if (item is HealthPotion) potionsCount++;
                    }
                    Console.WriteLine("3. Use Potion (" + potionsCount + " left)");
                    Console.WriteLine("4. View Stats");
                    Console.WriteLine("5. Quit");

                    Console.Write("Your choice: ");
                    string choice = Console.ReadLine() ?? "";
                    Console.WriteLine();
                    
                    if (choice == "4")
                    {
                        Console.WriteLine("═══════════════════════════════════════");
                        Console.WriteLine("  CURRENT PLAYER STATS          ");
                        Console.WriteLine("════════════════════════════════════════");
                        Console.WriteLine("Character Name: " + player.Name);
                        Console.WriteLine("Health: " + player.Health);
                        Console.WriteLine("Attack: " + player.Attack);
                        Console.WriteLine("Defense: " + player.Defense);
                        Console.WriteLine("Total Enemies Defeated: " + totalEnemiesDefeated);
                        Console.WriteLine("Current Level: " + currentLevel);
                        Console.WriteLine("Potions Remaining: " + potionsCount);
                        Console.WriteLine("════════════════════════════════════════");
                        continue;
                    }
                    
                    if (choice == "5")
                    {
                        quitGame = true;
                        gameRunning = false;
                        Console.WriteLine("════════════════════════════════════════");
                        Console.WriteLine("  FINAL PLAYER STATS            ");
                        Console.WriteLine("════════════════════════════════════════");
                        Console.WriteLine("Character Name: " + player.Name);
                        Console.WriteLine("Health: " + player.Health);
                        Console.WriteLine("Attack: " + player.Attack);
                        Console.WriteLine("Defense: " + player.Defense);
                        Console.WriteLine("Total Enemies Defeated: " + totalEnemiesDefeated);
                        Console.WriteLine("Current Level: " + currentLevel);
                        Console.WriteLine("Potions Remaining: " + potionsCount);
                        Console.WriteLine("════════════════════════════════════════");
                        break;
                    }

                    Action playerAction = new Action();
                    Character actionTarget = currentEnemy; 

                    if (choice == "1")
                    {
                        playerAction = new AttackAction(player);
                        actionTarget = currentEnemy;
                    }
                    else if (choice == "2")
                    {
                        playerAction = new DefendAction();
                        actionTarget = player; 
                    }
                    else if (choice == "3")
                    {
                        Item potionToUse = new Item("empty");
                        foreach (var item in player.Inventory)
                        {
                            if (item is HealthPotion)
                            {
                                potionToUse = item;
                                break;
                            }
                        }

                        if (potionToUse.Name != "empty")
                        {
                            playerAction = new UseItemAction(potionToUse);
                            actionTarget = player; // Player uses Item on themselves
                            player.Inventory.Remove(potionToUse); // consume the item
                        }
                        else
                        {
                            Console.WriteLine("You reach into your bag but find no potions!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice! You missed your turn.");
                    }

                    // Execute player's action if valid
                    if (playerAction.GetDescription() != "Unknown Action")
                    {
                        playerAction.Execute(actionTarget);
                    }

                    Console.WriteLine("─────────────────────────────────────────");


                    if (currentEnemy.Health > 0)
                    {
                        Console.WriteLine("--- " + currentEnemy.Name + "'s Turn ---");

                        Action enemyAction = new AttackAction(currentEnemy);
                        enemyAction.Execute(player);
                    }
                    
                    Console.WriteLine("─────────────────────────────────────────");
                }

                if (quitGame)
                {
                    break;
                }

                if (player.Health <= 0)
                {
                    Console.WriteLine("════════════════════════════════════════");
                    Console.WriteLine("     YOU HAVE BEEN DEFEATED!            ");
                    Console.WriteLine("           GAME OVER                    ");
                    Console.WriteLine("════════════════════════════════════════");
                    gameRunning = false;
                }
                else
                {
                    Console.WriteLine("════════════════════════════════════════");
                    Console.WriteLine("      You defeated " + currentEnemy.Name + "!               ");
                    Console.WriteLine("════════════════════════════════════════");

                    if (currentLevel == 3)
                    {
                        Console.WriteLine("════════════════════════════════════════");
                        Console.WriteLine("       CONGRATULATIONS!                 ");
                        Console.WriteLine("  You defeated the Dragon and won!      ");
                        Console.WriteLine("════════════════════════════════════════");
                        gameRunning = false;
                    }
                    else
                    {
                        totalEnemiesDefeated++;
                        enemiesDefeatedInLevel++;

                        // Give player a potion as loot
                        player.Inventory.Add(new HealthPotion());
                        Console.WriteLine("You looted a Health Potion from the enemy!");

                        if (enemiesDefeatedInLevel >= 2)
                        {
                            currentLevel++;
                            enemiesDefeatedInLevel = 0;
                            Console.WriteLine("════════════════════════════════════════");
                            Console.WriteLine("  LEVEL UP! You are now Level " + currentLevel + "!      ");
                            Console.WriteLine(" Your stats have been increased!        ");
                            Console.WriteLine("════════════════════════════════════════");
                            player.Health += 30; // Health boost on level up
                            if (player.Health > 100) player.Health = 100;
                            player.Attack += 5; // Stat boost
                            player.Defense += 2; // Stat boost
                        }
                    }
                }
            }
        }
    }
}
