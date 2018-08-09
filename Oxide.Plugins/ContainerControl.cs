using Newtonsoft.Json;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("ContainerControl", "Hamster", "1.0.0")]
    [Description("Allows you to restrict types of items which can be put in a certain kind of containers")]
    class ContainerControl : RustPlugin
    {

        #region Config
        private PluginConfig _config;
        private class PluginConfig
        {
            [JsonProperty("Settings: Container - items")]
            public Dictionary<string, string[]> ListItem { get; set; }
        }
        protected override void LoadDefaultConfig()
        {
            PrintWarning("Сreate a new configuration file");
            _config = new PluginConfig()
            {
                ListItem = new Dictionary<string, string[]>()
                {
                    ["cupboard.tool.deployed"] = new[] 
                    {
                        "cloth",
                        "scrap",
                        "sulfur",
                        "sulfur.ore",
                        "charcoal",
                        "hq.metal.ore",
                        "fat.animal",
                        "leather",
                        "crude.oil",
                        "gunpowder",
                        "metal.ore",
                        "lowgradefuel"
                    },
                    ["campfire"] = new[]
                    {
                        "metal.ore",
                        "fat.animal",
                        "stones",
                        "cctv.camera",
                        "leather",
                        "targeting.computer",
                        "metal.refined",
                        "scrap",
                        "bone.fragments",
                        "gunpowder",
                        "sulfur",
                        "sulfur.ore"
                    },
                    ["bbq.deployed"] = new[]
                    {
                        "metal.ore",
                        "fat.animal",
                        "stones",
                        "cctv.camera",
                        "leather",
                        "targeting.computer",
                        "metal.refined",
                        "scrap",
                        "bone.fragments",
                        "gunpowder",
                        "sulfur",
                        "sulfur.ore"
                    },
                    ["dropbox.deployed"] = new[]
                    {
                        "metal.ore",
                        "fat.animal",
                        "stones",
                        "leather",
                        "metal.refined",
                        "scrap",
                        "bone.fragments",
                        "gunpowder",
                        "sulfur",
                        "sulfur.ore",
                        "crude.oil",
                        "lowgradefuel",
                        "cloth",
                        "explosive.timed",
                        "explosive.satchel",
                        "grenade.beancan",
                        "grenade.f1",
                        "explosives"
                    },
                    ["furnace"] = new[]
                    {
                        "fat.animal",
                        "stones",
                        "cctv.camera",
                        "leather",
                        "targeting.computer",
                        "bone.fragments",
                        "crude.oil",
                        "gunpowder",
                        "lowgradefuel",
                        "jackhammer",
                        "cloth",
                        "chainsaw",
                        "explosive.timed"
                    },
                    ["furnace.large"] = new[]
                    {
                        "fat.animal",
                        "stones",
                        "cctv.camera",
                        "leather",
                        "targeting.computer",
                        "bone.fragments",
                        "crude.oil",
                        "gunpowder",
                        "lowgradefuel",
                        "jackhammer",
                        "cloth",
                        "chainsaw",
                        "explosive.timed"
                    }
                }
            };
        }
        protected override void LoadConfig()
        {
            base.LoadConfig();
            _config = Config.ReadObject<PluginConfig>();
        }
        protected override void SaveConfig()
        {
            Config.WriteObject(_config);
        }

        #endregion

        #region Oxide hook

        private void Loaded()
        {
            LoadConfig();
        }

        private ItemContainer.CanAcceptResult? CanAcceptItem(ItemContainer container, Item item, int targetPos)
        {
            if (container == null || item == null) return null;
            BaseEntity baseEntity = container.entityOwner;
            if (baseEntity == null) return null;
            if (baseEntity.OwnerID <= 76560000000000000L) return null;
            string[] values;
            var configHasItem = _config.ListItem.TryGetValue(baseEntity.ShortPrefabName, out values);
            if (configHasItem)
            {
                foreach (var name in values)
                {
                    if (item.info.shortname == name)
                    {
                        return ItemContainer.CanAcceptResult.CannotAccept;
                    }
                }
            }
            return null;
        }


        #endregion

    }
}
