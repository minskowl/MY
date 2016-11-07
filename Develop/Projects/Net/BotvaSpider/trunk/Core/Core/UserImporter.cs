using BotvaSpider.Data;
using BotvaSpider.Gears;

namespace BotvaSpider.Core
{
    /// <summary>
    /// UserImporter
    /// </summary>
    public class UserImporter
    {
        private User _user;

        /// <summary>
        /// Imports the specified result.
        /// </summary>
        /// <param name="result">The result.</param>
        public void Import(FightResult result)
        {
            Import(result.Rival);
            if (ObjectProvider.Instance.GetFight(result.Rival.UserID, result.Date) == null)
                ObjectProvider.Instance.AddFight(result);
        }

        /// <summary>
        /// Imports the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Import(User user)
        {
            _user = user;
            ImportClan();
            var existsUser = ObjectProvider.Instance.GetUserByName(user.Name);
            if (existsUser != null)
            {
                _user.UserID = existsUser.UserID;
                _user.Female = existsUser.Female;
                _user.UserType = existsUser.UserType;

                ObjectProvider.Instance.UpdateUser(_user);
            }
            else
            {
                if (_user.Safe.HasValue)
                {
                    var safe = _user.Safe.Value;
                    _user.MilkingCoulomb = safe > 0 ? Coulomb.Drill : Coulomb.CrystalThief;
                }


                ObjectProvider.Instance.AddUser(_user);
            }

            ObjectProvider.Instance.UserSaveSkills(_user);

        }



        private void ImportClan()
        {
            if (_user.Clan == null) return;

            if (string.IsNullOrEmpty(_user.Clan.Tag))
            {
                var readed = ObjectProvider.Instance.GetClanByName(_user.Clan.Name);
                if (readed.Count > 0)
                {
                    _user.ClanID = readed[0].ClanID;
                    return;
                }
            }
            else
            {
                var readed = ObjectProvider.Instance.GetClanByTag(_user.Clan.Tag);
                if (readed != null)
                {
                    _user.ClanID = readed.ClanID;
                    return;
                }
            }
            //if (readedClan != null) return;//TODO: Possible Update exists Clan

            ObjectProvider.Instance.AddClan(_user.Clan);
            _user.ClanID = _user.Clan.ClanID;

        }
    }
}