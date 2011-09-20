using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTYD.Controller
{
    /// <summary>
    /// Player object
    /// 
    /// This class will focus only on entities that belong to players.
    /// 
    /// interface: Map Observer
    /// </summary>
    public class Players:KTYD.ObserverModel.MapObserver
    {

        List<Entity> player_list;                   // Player List
        List<Weapon[]> weapons_list;   // Player's weapon 
        private List<int> currentWeapon;            // Current weapon for player
        private List<int> currentLifeHas;           // Current life the player has

        private bool[] aliveList;                    // Keep track of who died

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// 
        Dictionary<int,Microsoft.Xna.Framework.Input.Keys > shootMapping;
        Dictionary<int, Microsoft.Xna.Framework.Input.Keys> forwardMapping;
        Dictionary<int, Microsoft.Xna.Framework.Input.Keys> backwardMapping;
        Dictionary<int, Microsoft.Xna.Framework.Input.Keys> rightMapping;
        Dictionary<int, Microsoft.Xna.Framework.Input.Keys> leftMapping;
        public Players()
        {
            player_list = new List<Entity>();
            weapons_list = new List<Weapon[]>();
            currentWeapon = new List<int>();
            currentLifeHas = new List<int>();
            aliveList = new bool[player_list.Count];

            for (int i = 0; i < aliveList.Length; ++i)
            {
                aliveList[i] = false;
            }
            shootMapping = new Dictionary<int,Microsoft.Xna.Framework.Input.Keys>();
            shootMapping.Add(0,ControlConfig.P1_SHOOT);
            shootMapping.Add(1,ControlConfig.P2_SHOOT);

            leftMapping = new Dictionary<int, Microsoft.Xna.Framework.Input.Keys>();
            leftMapping.Add(0, ControlConfig.P1_LEFT);
            leftMapping.Add(1, ControlConfig.P2_LEFT);


            rightMapping = new Dictionary<int, Microsoft.Xna.Framework.Input.Keys>();
            rightMapping.Add(0, ControlConfig.P1_RIGHT);
            rightMapping.Add(1, ControlConfig.P2_RIGHT);


            forwardMapping = new Dictionary<int, Microsoft.Xna.Framework.Input.Keys>();
            forwardMapping.Add(0, ControlConfig.P1_FORWARD);
            forwardMapping.Add(1, ControlConfig.P2_FORWARD);


            backwardMapping = new Dictionary<int, Microsoft.Xna.Framework.Input.Keys>();
            backwardMapping.Add(0, ControlConfig.P1_BACKWARD);
            backwardMapping.Add(1, ControlConfig.P2_BACKWARDS);
        }

        public Dictionary<int,Microsoft.Xna.Framework.Input.Keys> getShootMapping()
        {
            return shootMapping;
        }
        public Dictionary<int, Microsoft.Xna.Framework.Input.Keys> getLeftMapping()
        {
            return leftMapping;
        }
        public Dictionary<int, Microsoft.Xna.Framework.Input.Keys> getRightMapping()
        {
            return rightMapping;
        }
        public Dictionary<int, Microsoft.Xna.Framework.Input.Keys> getForwardMapping()
        {
            return forwardMapping;
        }
        public Dictionary<int, Microsoft.Xna.Framework.Input.Keys> getBackwardMapping()
        {
            return backwardMapping;
        }
        public List<Entity> getPlayers()
        {
            return player_list;
        }
        public void Add(Entity e)
        {
            loadPlayerIn(e);

        }
        /// <summary>
        /// Load a new player in
        /// </summary>
        /// <param name="e"></param>
        public void loadPlayerIn(Entity e)
        {
            player_list.Add(e);
            weapons_list.Add(new Weapon[GameConfig.MAX_WEAPON_SLOT]);
            weapons_list[weapons_list.Count - 1][GameConfig.WEAPON_SLOT_ONE] = Weapon.createPistol();
            weapons_list[weapons_list.Count - 1][GameConfig.WEAPON_SLOT_TWO] = Weapon.createPistol();
            currentWeapon.Add(GameConfig.WEAPON_SLOT_ONE);
            currentLifeHas.Add(GameConfig.DEFAULT_LIFE_GIVEN);
        }
        public void updateCooldowns(bool[] pressShoot, float[] time)
        {
             // Reset spawn/cool down time for spawning bullets for all players
                    for (int i = 0; i < GameConfig.MAX_PLAYERS; ++i)
                    {
                        if (this.isBulletReady(i, time[i]))
                        {
                            //gamePlayers.getPlayerOne().setState(EntityState.RUN);
                            pressShoot[i] = false;
                            time[i] = 0;
                        }
                    }

        }

        /// <summary>
        /// Checking whether if it is time for allowing to spawn the next bullet
        /// </summary>
        /// <param name="player_index">Player index</param>
        /// <param name="time">current elapsed time</param>
        /// <returns>True if ready, false is not</returns>
        public bool isBulletReady(int player_index, float elapsed_time)
        {


            if (elapsed_time > this.getPlayerWeaponTypeAt(player_index).getCooldown())
            {
                return true;
            }


            return false;
        } 
        /// <summary>
        /// Get #1 player
        /// </summary>
        /// <returns></returns>
        public Entity getPlayerOne()
        {
            return getPlayerAt(0);
        }

        /// <summary>
        /// Get #2 Player
        /// </summary>
        /// <returns></returns>
        public Entity getPlayerTwo()
        {
            return getPlayerAt(1);
        }

        /// <summary>
        /// Get generic # player
        /// </summary>
        /// <param name="player_index">Index of player</param>
        /// <returns>Entity object that represents that specific player, Null if nothing</returns>
        public Entity getPlayerAt(int player_index)
        {
            if (player_index < player_list.Count)
            {
                return player_list[player_index];
            } else  return null;
        }

        /// <summary>
        /// Get a current weapon using by the specific player
        /// </summary>
        /// <param name="player_index">Player index</param>
        /// <returns>Type of weapon</returns>
        public Weapon getPlayerWeaponTypeAt(int player_index)
        {
            return weapons_list[player_index][currentWeapon[player_index]];
        }

        /// <summary>
        /// Get a current weapon using by the first player
        /// </summary>
        /// <returns>Type of weapon</returns>
        public Weapon getPlayerOneWeaponType()
        {
            return getPlayerWeaponTypeAt(0);
        }

        /// <summary>
        /// Change current weapon of the specific player
        /// </summary>
        /// <param name="player_index">Player index</param>
        /// <param name="new_weapon">new weapon</param>
        public void changeWeaponAtPlayer(int player_index, Weapon new_weapon)
        {
            weapons_list[player_index][currentWeapon[player_index]] = new_weapon;
        }

        /// <summary>
        /// Switch between weapon for specific player
        /// </summary>
        /// <param name="player_index">Player index</param>
        public void switchWeaponAtPlayer(int player_index)
        {
            if (currentWeapon[player_index] < 1)
            {
                ++currentWeapon[player_index];
            }
            else
            {
                currentWeapon[player_index] = 0;
            }
            switchWeaponGraphic(player_index);
            
        }

        /// <summary>
        /// Reset player at the particular index to initial values
        /// </summary>
        /// <param name="player_index">Player index</param>
        public void resetPlayerAt(int player_index)
        {
            --currentLifeHas[player_index];
          
            player_list[player_index].resurrect();
        }

        /// <summary>
        /// Get total lives the specific player has
        /// </summary>
        /// <param name="player_index">Player index</param>
        /// <returns>the number of lives</returns>
        public int getPlayerLivesAt(int player_index)
        {
            return currentLifeHas[player_index];
        }

        /// <summary>
        /// Check if the player at specific index is out of life
        /// </summary>
        /// <param name="player_index">Player index</param>
        /// <returns>True if player runs out of lives</returns>
        public bool isPlayerOutOfLifeAt(int player_index)
        {
            if (currentLifeHas[player_index] < 1)
            {
                return true;
            }
            else return false;
        }


        /// <summary>
        /// Switch an image to reflect changes of switching weapon
        /// </summary>
        /// <param name="player_index">Player index</param>
        private void switchWeaponGraphic(int player_index)
        {

            getPlayerAt(player_index).setStateEntity(weapons_list[player_index][currentWeapon[player_index]].getEntityOffset());

         
           
        }

        /// <summary>
        /// Check if all players alives
        /// </summary>
        /// <returns></returns>
        public bool areSomePlayersAlive()
        {
            for (int i = 0; i < this.player_list.Count; ++i)
            {
                if (!player_list[i].isDead())
                {
                    return true;
                }
            }
            return false;
            
        }

        /// <summary>
        /// Update
        /// </summary>
        public void update()
        {
           
            // Update current weapon
            for (int player_index = 0; player_index < player_list.Count; ++player_index)
            {
                    switch (player_list[player_index].HoldItem)
                    {
                        case KTYD.Model.ItemType.M16_WEAPON:
                            // if never have this weapon
                            if (weapons_list[player_index][GameConfig.WEAPON_SLOT_ONE].getType() != WeaponType.M16 && weapons_list[player_index][GameConfig.WEAPON_SLOT_TWO].getType() != WeaponType.M16)
                            {
                                changeWeaponAtPlayer(player_index, Weapon.createM16());
                            }
                            break;
                        case KTYD.Model.ItemType.FLAME_WEAPON:
                            if (weapons_list[player_index][GameConfig.WEAPON_SLOT_ONE].getType() != WeaponType.FLAMETHROWER&& weapons_list[player_index][GameConfig.WEAPON_SLOT_TWO].getType() !=WeaponType.M16)
                            {
                                changeWeaponAtPlayer(player_index, Weapon.createFlamethrower());
                            }
                            break;

                        case KTYD.Model.ItemType.SHOTGUN_WEAPON:
                            if(weapons_list[player_index][GameConfig.WEAPON_SLOT_ONE].getType() != WeaponType.SHOTGUN&& weapons_list[player_index][GameConfig.WEAPON_SLOT_TWO].getType() !=WeaponType.SHOTGUN)
                            {
                                changeWeaponAtPlayer(player_index,Weapon.createShotgun());
                            }
                            break;
                    }

                    
                    Weapon a=weapons_list[player_index][GameConfig.WEAPON_SLOT_ONE];
                Weapon b=weapons_list[player_index][GameConfig.WEAPON_SLOT_ONE];
                    // Check if both weapon slot of each player is having the same weapon
                    if ( a !=b)
                    {
                        switchWeaponGraphic(player_index);
                    }
                    else
                    {
                        // if it is, make one a pistol
                        weapons_list[player_index][GameConfig.WEAPON_SLOT_TWO] = Weapon.createPistol();
                    }


            }//for
        }
    }
}
