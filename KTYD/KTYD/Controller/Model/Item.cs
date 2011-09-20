using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTYD;

namespace KTYD.Model
{
    public enum ItemType
    {
        HEALTH,
        SHOTGUN_WEAPON,
        FLAME_WEAPON,
        M16_WEAPON
    }

    /// <summary>
    /// Item 
    /// 
    /// Base: Entity
    /// </summary>
    public class Item:Entity
    {
        private ItemType itemType;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">X Location</param>
        /// <param name="y">Y Location</param>
        /// <param name="type">Item type</param>
        public Item(float x, float y, ItemType type):base
            (x,y,0, EntityType.ITEM)
        {
            itemType = type;
            //baseStateEntity = GameConfig.IMG_ITEM_ENTITY;
            stateEntityOffset = GameConfig.IMG_ITEM_ENTITY;
            switch(type)
            {
                case Model.ItemType.HEALTH:
                    state = GameConfig.IMG_ITEM_HEALTH;
                    break;

                case Model.ItemType.FLAME_WEAPON:
                    state = GameConfig.IMG_ITEM_FLAMETHROWER;
                    break;

                case Model.ItemType.SHOTGUN_WEAPON:
                    state = GameConfig.IMG_ITEM_SHOTGUN;
                    break;
                case Model.ItemType.M16_WEAPON:
                    state  = GameConfig.IMG_ITEM_M16;
                    break;

            }
            this.rangeRadius = GameConfig.DEFAULT_RANGE_RADIUS_ITEM;
            //state = state + stateEntityOffset;
        }


        /// <summary>
        /// Get item type
        /// </summary>
        public ItemType ItemType
        {
            get
            {
                return itemType;
            }
        }


        /// <summary>
        /// Update
        /// </summary>
        public override void update()
        {
            
        }
    }
}
