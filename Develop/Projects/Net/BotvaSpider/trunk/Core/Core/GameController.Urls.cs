namespace BotvaSpider.Core
{
    partial class GameController
    {
        //private static DictionaryEx<String, String> images;
        public string DomainUrl { get; private set; }
        public string UrlImages { get; private set; }
        public string UrlButtons { get; private set; }
        public string UrlRuImages { get; private set; }


        public string UrlAccount { get; private set; }
        public string UrlTrade { get; private set; }
        public string UrlTradePlace { get; private set; }
        public string UrlTradeNow { get; private set; }
        public string UrlLogin { get; private set; }
        public string UrlDozor { get; private set; }
        public string UrlFarm { get; private set; }
        public string UrlShop { get; private set; }
        public string UrlPost { get; private set; }
        public string UrlTraining { get; private set; }
        public string UrlKormushka { get; private set; }
        public string UrlEmbassy { get; private set; }
        public string UrlCurrentWars { get; private set; }

        public string UrlPlayer { get; private set; }
        public string UrlClan { get; private set; }
        public string UrlClanTreasury { get; private set; }
        public string UrlClanTreasuryPage { get; private set; }
        public string UrlClanMembers { get; private set; }

        public string UrlCastle { get; private set; }
        public string UrlCastleTreasury { get; private set; }

        public string UrlShtab { get; private set; }
        public string UrlShtabNotes { get; private set; }
        public string UrlSearch { get; private set; }
        public string UrlLogout { get; private set; }

        public string UrlSmith { get; private set; }
        public string UrlSmithMaster { get; private set; }

        public string UrlTop { get; private set; }
        public string UrlTopUsers { get; private set; }
        public string UrlTopClans { get; private set; }

        public string UrlMine { get; private set; }
        public string UrlMineWork { get; private set; }
        public string UrlMineWorking { get; private set; }
        public string UrlMineShop { get; private set; }

        public string UrlMessages { get; private set; }
        public string UrlFightMessages { get; private set; }

        public string UrlFightLogPage { get; private set; }
        public string UrlFightLog { get; private set; }


        #region Images

        public string ImageTitleBackground { get; private set; }


        public string UrlItems { get; private set; }
        public string UrlGuilds { get; private set; }

        public string UrlImageDeleloperLogo { get; private set; }

        public string UrlImageGuildsSSTemplate { get; private set; }

        public string UrlMoneyImage { get; private set; }
        public string UrlPotionBlueStart { get; private set; }
        public string UrlImageBan { get; private set; }

        public string UrlItemImageSpiritPattern { get; private set; }

        public string UrlImagePick { get; private set; }
        //CristalMap
        public string UrlImageCristalNotFounded { get; private set; }
        public string UrlImageCristalFound { get; private set; }
        public string UrlImageEmptySpaceNotVisited { get; private set; }
        public string UrlImageEmptySpaceVisited { get; private set; }
        public string UrlImageSmallTicketFinded { get; private set; }
        public string UrlImageSmallTicketNotFinded { get; private set; }
        public string UrlImageBluePosionFounded { get; private set; }
        public string UrlImageBluePosionNotFounded { get; private set; }

        public string UrlImagePowerIcon { get; private set; }
        public string UrlImageProtectionIcon { get; private set; }
        public string UrlImageDexterityIcon { get; private set; }
        public string UrlImageWeightIcon { get; private set; }
        public string UrlImageMasteryIcon { get; private set; }

        public string UrlImageEmptySlot { get; private set; }
        public string UrlImageBlock { get; private set; }
        public string UrlImageStake { get; private set; }
        public string UrlImageTicketPrefix { get; private set; }

        public string UrlImageGold { get; private set; }
        public string UrlImageCristal { get; private set; }

        public string UrlImageSafePattern { get; private set; }

        //Buttons


        public string UrlImageButtonGetCrystal { get; private set; }
        public string UrlImageButtonSearchAgain { get; private set; }
        public string UrlButtonAttack { get; private set; }
        public string UrlImageButtonTradeFilterPattern { get; private set; }
        public string UrlImageButtonSmallGlade { get; private set; }
        public string UrlImageButtonBigGlade { get; private set; }
        public string UrlImageButtonSearch { get; private set; }
        public string UrlImageButtonGoAway { get; private set; }
        public string UrlImageButtonPutOff { get; private set; }
        public string UrlButtonSearchRival { get; private set; }
        public string UrlButtonTrade { get; private set; }
        public string UrlButtonBuy { get; private set; }

        #endregion

        private void InitUrls(string domainUrl)
        {
            DomainUrl = domainUrl;
            UrlImages = DomainUrl + "images/";
            UrlButtons = UrlImages + "buttons/";

            UrlAccount = DomainUrl + "index.php";
            UrlTrade = DomainUrl + "trade.php";
            UrlTradePlace = UrlTrade + "?m=add";
            UrlTradeNow = UrlTrade + "?m=now";
            UrlLogin = DomainUrl + "main.php";
            UrlDozor = DomainUrl + "dozor.php";
            UrlFarm = DomainUrl + "farm.php";
            UrlShop = DomainUrl + "shop.php";
            UrlPost = DomainUrl + "post.php";
            UrlTraining = DomainUrl + "training.php";
            UrlKormushka = DomainUrl + "kormushka.php";
            UrlEmbassy = DomainUrl + "clan_embassy.php";
            UrlCurrentWars = UrlEmbassy + "?m=wars";

            UrlPlayer = DomainUrl + "player.php";
            UrlClan = DomainUrl + "clan.php";
            UrlClanTreasury = DomainUrl + "clan_mod.php?m=treasury";
            UrlClanTreasuryPage = UrlClanTreasury + "&order=date&dir=desc";
            UrlClanMembers = DomainUrl + "clan_members.php";

            UrlCastle = DomainUrl + "castle.php";
            UrlCastleTreasury = UrlCastle + "?a=treasury";

            UrlShtab = DomainUrl + "shtab.php";
            UrlShtabNotes = UrlShtab + "?m=notes";
            UrlSearch = DomainUrl + "search.php";
            UrlLogout = DomainUrl + @"logout.php";

            UrlSmith = DomainUrl + @"smith.php";
            UrlSmithMaster = UrlSmith + "?a=master";

            UrlTop = DomainUrl + @"top.php";
            UrlTopUsers = UrlTop + @"?t=1";
            UrlTopClans = UrlTop + @"?t=2";

            UrlMine = DomainUrl + @"mine.php";
            UrlMineWork = UrlMine + @"?a=open";
            UrlMineWorking = UrlMine + @"?a=working";
            UrlMineShop = UrlMine + @"?a=shop";

            UrlMessages = DomainUrl + @"messages.php";
            UrlFightMessages = UrlMessages + "?folder=3";

            UrlFightLogPage = DomainUrl + @"fight_log.php";
            UrlFightLog = UrlFightLogPage + @"?log_id=";


            ImageTitleBackground = "images/menu_char_bg.png";


            UrlItems = UrlImages + "items/";
            UrlGuilds = UrlImages + "guilds/";

            UrlImageDeleloperLogo = UrlImages + "logo_destiny.png";

            UrlImageGuildsSSTemplate = UrlGuilds + "Guild_{0}ss.png";

            UrlMoneyImage = UrlImages + "ico_gold1.png";
            UrlPotionBlueStart = UrlImages + "Potion_2s_";
            UrlImageBan = UrlImages + "ban_01.png";

            UrlItemImageSpiritPattern = UrlItems + "Magic_{0}s.png";

            UrlImagePick = UrlItems + "Mine_1s.jpg";
            //CristalMap
            UrlImageCristalNotFounded = UrlImages + "mine_planeB.gif";
            UrlImageCristalFound = UrlImages + "mine_plane1.gif";
            UrlImageEmptySpaceNotVisited = UrlImages + "mine_planeA.gif";
            UrlImageEmptySpaceVisited = UrlImages + "mine_plane0.gif";
            UrlImageSmallTicketFinded = UrlImages + "mine_plane5.gif";
            UrlImageSmallTicketNotFinded = UrlImages + "mine_planeF.gif";
            UrlImageBluePosionFounded = UrlImages + "mine_plane2.gif";
            UrlImageBluePosionNotFounded = UrlImages + "mine_planeC.gif";

            UrlImagePowerIcon = UrlImages + "ico_12.png";
            UrlImageProtectionIcon = UrlImages + "ico_13.png";
            UrlImageDexterityIcon = UrlImages + "ico_14.png";
            UrlImageWeightIcon = UrlImages + "ico_15.png";
            UrlImageMasteryIcon = UrlImages + "ico_15.png";

            UrlImageEmptySlot = UrlImages + "ico_p0.jpg";
            UrlImageBlock = UrlImages + "ico_block.png";
            UrlImageStake = UrlImages + "ico_pc.jpg";
            UrlImageTicketPrefix = UrlImages + "Ticket_";

            UrlImageGold = UrlImages + "ico_gold1.png";
            UrlImageCristal = UrlImages + "ico_krist1.png";

            UrlImageSafePattern = UrlImages + "ico_m1_{0}.jpg";

            //Buttons


            UrlImageButtonGetCrystal = UrlRuImages + "b_krist_p.png";
            UrlImageButtonSearchAgain = UrlRuImages + "b_more2_p.png";
            UrlButtonAttack = UrlRuImages + "b_nap_p.png";
            UrlImageButtonTradeFilterPattern = UrlRuImages + "filter{0}_p.png";
            UrlImageButtonSmallGlade = UrlRuImages + "b_small_p.png";
            UrlImageButtonBigGlade = UrlRuImages + "b_big_p.png";
            UrlImageButtonSearch = UrlRuImages + "b_search_p.png";
            UrlImageButtonGoAway = UrlRuImages + "b_away_p.png";
            UrlImageButtonPutOff = UrlRuImages + "b_off.png";
            UrlButtonSearchRival = UrlRuImages + "b_find_p.png";
            UrlButtonTrade = UrlRuImages + "b_trade_confirm_p.png";
            UrlButtonBuy = UrlRuImages + "b_buy_p.png";

            var images = AppCore.AppSettings.Images;

            ImageTitleBackground = images.Get("ImageTitleBackground", ImageTitleBackground);
            UrlImageStake = images.Get("UrlImageStake", UrlImageStake);

            UrlImageButtonGetCrystal = DomainUrl + images.Get("ImageButtonGetCrystal", UrlImageButtonGetCrystal);
            UrlImageButtonSearchAgain = DomainUrl + images.Get("ImageButtonSearchAgain", UrlImageButtonSearchAgain);
            UrlButtonAttack = DomainUrl + images.Get("ButtonAttack", UrlButtonAttack);
            UrlImageButtonTradeFilterPattern = DomainUrl + images.Get("UrlImageButtonTradeFilterPattern", UrlImageButtonTradeFilterPattern);
            UrlImageButtonSmallGlade = DomainUrl + images.Get("UrlImageButtonSmallGlade", UrlImageButtonSmallGlade);
            UrlImageButtonBigGlade = DomainUrl + images.Get("UrlImageButtonBigGlade", UrlImageButtonBigGlade);
            UrlImageButtonSearch = DomainUrl + images.Get("UrlImageButtonSearch", UrlImageButtonSearch);

            UrlImageButtonGoAway = DomainUrl + images.Get("UrlImageButtonGoAway", UrlImageButtonGoAway);
            UrlImageButtonPutOff = DomainUrl + images.Get("UrlImageButtonPutOff", UrlImageButtonPutOff);
            UrlButtonBuy = DomainUrl + images.Get("UrlButtonBuy", UrlButtonBuy);
            UrlButtonTrade = DomainUrl + images.Get("UrlButtonTrade", UrlButtonTrade);
        }
    }
}
