using HarmonyLib;
using PhantomBrigade.Mods;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using YamlDotNet.Serialization;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Core;

namespace PB_EquipmentTypeRarity
{
    public class ModEntry : ModLink
    {

        public static ModSettings ModSettings { get; set; } = new ModSettings();

        public override void OnLoad(Harmony harmonyInstance)
        {
            harmonyInstance.PatchAll();

            try
            {
                ModSettings settings = LoadModSettings(this.metadata.path + "/metadata.yaml");

                if(settings != null)
                {
                    ModEntry.ModSettings = settings;
                }

            }
            catch (Exception ex)
            {
                FileLog.Log(ex.ToString());
                throw;
            }
        }

        public ModSettings LoadModSettings(string path)
        {

            IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(new CamelCaseNamingConvention())
                .IgnoreUnmatchedProperties()
                .Build();

            ModSettingsDto modSettings = deserializer.Deserialize<ModSettingsDto>(File.ReadAllText(path));
            return modSettings?.ModSettings;
        }
    }
}

