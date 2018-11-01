using System;
using System.Collections.Generic;
using Dominoes.Players;
using Newtonsoft.Json;

namespace Dominoes
{
    public class GameConfiguration
    {
        [JsonProperty("players")]
        public GameConfigurationPlayer[] Players { get; set; }

        [JsonProperty("initial_domino")]
        public String InitialDomino { get; set; }

        [JsonProperty("pick_list")]
        public String[] PickList { get; set; }
    }   

    public class GameConfigurationPlayer
    {
        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("dominos")]
        public String[] Dominos { get; set; }

        [JsonProperty("type")]
        public String Type { get; set; }

        [JsonProperty("notes")]
        public String Notes { get; set; }
    }
}
