using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEngine;

public class MobData : MonoBehaviour
{
    public class Mob{
        string name;
        int health;
        int damage;
        double gusIndex;

        public Mob (string name){
            this.name = name;
            health = 100;
            damage = 5;
            gusIndex = 0.15;
        }
        public Mob (string name, int health, int damage){
            this.name =name;
            this.health = health;
            this.damage = damage;
            gusIndex = 0.45;
        }
        public Mob (string name, int health, int damage, double gusIndex){
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.gusIndex = gusIndex;
        }

        public string getName(){
            return name;
        }
        public int getHealth(){
            return health;
        }
        public int getDamage(){
            return damage;
        }
        public double getGusIndex(){
            return gusIndex;
        }
    }

    public static Mob insect = new Mob("Bug", 3, 2, 0.95);
    public static Mob boar = new Mob("Boar", 150, 10, 0.85);
    public static Mob rat = new Mob("Rat", 50, 3, 0.10);
    public static Mob wolf = new Mob("Wolf", 100, 5, 0.50);
    
}
