using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS_Client
{
    public class Player
    {
        public string PlayerName { get; set; }
        public Skill Firemaking { get; set; }
        public Skill Farming { get; set; }
        public Skill Attack { get; set; }
        public Skill Strength { get; set; }
        public Skill Defence { get; set; }
        public Skill Dungeoneering { get; set; }
        public Skill Ranged { get; set; }
        public Skill Prayer { get; set; }
        public Skill Magic { get; set; }
        public Skill Runecrafting { get; set; }
        public Skill Constitution { get; set; }
        public Skill Crafting { get; set; }
        public Skill Mining { get; set; }
        public Skill Smithing { get; set; }
        public Skill Fishing { get; set; }
        public Skill Cooking { get; set; }
        public Skill Woodcutting { get; set; }
        public Skill Agility { get; set; }
        public Skill Herblore { get; set; }
        public Skill Thieving { get; set; }
        public Skill Fletching { get; set; }
        public Skill Slayer { get; set; }
        public Skill Construction { get; set; }
        public Skill Hunter { get; set; }
        public Skill Summoning { get; set; }
        public Skill Overall { get; set; }

        public Player(string aPlayerName)
        {
            this.PlayerName = aPlayerName;
            Firemaking = new Skill("Firemaking");
            Farming = new Skill("Farming");
            Summoning = new Skill("Summoning");
            Hunter = new Skill("Hunter");
            Construction = new Skill("Construction");
            Slayer = new Skill("Slayer");
            Fletching = new Skill("Fletching");
            Thieving = new Skill("Thieving");
            Herblore = new Skill("Herblore");
            Agility = new Skill("Agility");
            Woodcutting = new Skill("Woodcutting");
            Cooking = new Skill("Cooking");
            Fishing = new Skill("Fishing");
            Smithing = new Skill("Smithing");
            Mining = new Skill("Mining");
            Crafting = new Skill("Crafting");
            Constitution = new Skill("Constitution");
            Runecrafting = new Skill("Runecrafting");
            Magic = new Skill("Magic");
            Prayer = new Skill("Prayer");
            Ranged = new Skill("Ranged");
            Dungeoneering = new Skill("Dungeoneering");
            Defence = new Skill("Defence");
            Strength = new Skill("Strength");
            Attack = new Skill("Attack");
            Overall = new Skill("Overall");
        }
    }
}
