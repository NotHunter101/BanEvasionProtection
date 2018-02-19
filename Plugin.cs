using System;
using System.Collections.Generic;
using System.Linq;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using System.Net.Http;

namespace BanEvasionProtection
{
    [ApiVersion(2, 1)]
    public class BanEvasionProtection : TerrariaPlugin
    {
        public override string Author => "Hunter101";
        public override string Description => "A plugin that adds ban evasion protection.";
        public override string Name => "tehrealpermaban";
        public override Version Version
        {
            get { return new Version(1, 0, 0, 0); }
        }
        public BanEvasionProtection(Main game) : base(game)
        {

        }

        private static readonly HttpClient client = new HttpClient();

        public override void Initialize()
        {
            ServerApi.Hooks.ServerJoin.Register(this, OnJoinAsync);
        }

        async void OnJoinAsync(JoinEventArgs args)
        {
            var response = await client.PostAsync("http://check.getipintel.net/check.php?ip=" + TShock.Players[args.Who].IP + "&contact=teamtitanium6@gmail.com", null);

            var responseString = await response.Content.ReadAsStringAsync();

            int responseInt;

            int.TryParse(responseString, out responseInt);

            if (responseDouble == 1)
            {
                TShock.Players[args.Who].Disconnect("AntiProxy: Proxy connections are not permitted.");
            }

            if (TShock.Players[args.Who].Name.Length > 20)
            {
                TShock.Players[args.Who].Disconnect("Invalid Name! Must be less than 20 characters.");
                return;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.ServerJoin.Deregister(this, OnJoinAsync);
            }
            base.Dispose(disposing);
        }
    }
}
