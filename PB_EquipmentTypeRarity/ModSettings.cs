using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_EquipmentTypeRarity
{
    public class ModSettings
    {
        /// <summary>
        /// If true, will show item "type / rarity".  Otherwise only the type is shown.
        /// </summary>
        public bool ShowRarity { get; set; } = false;


        /// <summary>
        /// If true, will add the "Group By Alpha" alphabetical sort in the inventory.
        /// </summary>
        public bool EnableGroupByAlpha { get; set; } = true;
    }
}
