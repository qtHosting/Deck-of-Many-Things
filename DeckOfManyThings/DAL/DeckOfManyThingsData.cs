using System;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;

namespace DeckOfManyThings.DAL
{
    class DeckOfManyThingsData
    {
        #region Private Variables

        private string strLocationData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private string strLocationName = Properties.Settings.Default.LocationName.ToString();
        private string strArtLocation = Properties.Settings.Default.Pictures.ToString();
        private string strDeckToUse = Properties.Settings.Default.DeckToUse.ToString();
        private string strDatabaseName = Properties.Settings.Default.DatabaseName.ToString();

        private SQLiteConnection sqlCon;

        #endregion

        /// <summary>
        /// Instantiate the Data Collection from Deck of Many Things database.
        /// </summary>
        public DeckOfManyThingsData()
        {
            if (!CheckIfDeckOfManyThingsDatabaseExists())
            {
                CreateNewDatabase();
                FillNewDatabase();
            }

            ConnectToDatabase();
        }

        /// <summary>
        /// Gets the total number of cards stored in the Deck of Many Things
        /// </summary>
        /// <returns>This returns an integer that details how many cards are in the deck.</returns>
        public int GetTheNumberOfCardsFromTheDeck()
        {
            sqlCon.Open();
            int cards = 0;
            string sql = "select count(*) from DeckOfManyThings";
            using (SQLiteCommand com = new SQLiteCommand(sql, sqlCon))
            {
                cards = Convert.ToInt32(com.ExecuteScalar());
            }
            
           

            sqlCon.Close();
            return cards;
        }
        /// <summary>
        /// Gets the details of the cards.
        /// </summary>
        /// <param name="intCardID">Card ID</param>
        /// <returns>An object that has the details of the cards.</returns>
        public Card GetCardDetails(int intCardID)
        {
            sqlCon.Open();
            Card card = new Card();
            string sql = "Select * from DeckOfManyThings where cardID =" + intCardID.ToString();
            SQLiteCommand com = new SQLiteCommand(sql, sqlCon);
            SQLiteDataReader reader = com.ExecuteReader();
            while(reader.Read())
            {
                card.IntCardID = (int)reader["cardID"];
                card.strCardDescription = reader["description"].ToString();
                card.strCardName = reader["name"].ToString();
                card.strCardSummary = reader["summary"].ToString();
                card.StrCardArtLocation = strLocationData + strLocationName + strArtLocation + strDeckToUse + Regex.Replace(card.strCardName, "\\*","") + ".jpg";
            }

            return card;
        }

        #region Private Methods
        /// <summary>
        /// Checks if the deck of many things Exists.
        /// </summary>
        /// <returns>True if the deck exist</returns>
        private bool CheckIfDeckOfManyThingsDatabaseExists()
        {
            if(File.Exists(strLocationData + strLocationName + strDatabaseName))
            {
                return true;
            }

            CheckIfDirectoryExists();
            return false;
        }
        /// <summary>
        /// Checks if the Directory for the Database exists. If not, it creates it.
        /// </summary>
        /// <returns>True if the directory exists</returns>
        private bool CheckIfDirectoryExists()
        {
            if(!Directory.Exists(strLocationData + strLocationName))
            {
                CreateDirectory();
            }

            return true;
        }
        /// <summary>
        /// Creates a directory if necessary.
        /// </summary>
        private void CreateDirectory()
        {
            Directory.CreateDirectory(strLocationData + strLocationName);
        }
        /// <summary>
        /// Creates new Database file if it doesn't exist.
        /// </summary>
        private void CreateNewDatabase()
        {
            SQLiteConnection.CreateFile(strLocationData + strLocationName + strDatabaseName);

        }
        /// <summary>
        /// Fills a new Database with new Deck data.
        /// </summary>
        private void FillNewDatabase()
        {
            SQLiteConnection newDB = new SQLiteConnection("DataSource ="+ strLocationData + strLocationName + strDatabaseName);
            newDB.Open();
            string sql = "CREATE TABLE DeckOfManyThings (cardID int UNIQUE, name varchar(10), summary varchar(100), description varchar(2000))";
            SQLiteCommand com = new SQLiteCommand(sql, newDB);

            com.ExecuteNonQuery();

            string insert = "insert into DeckOfManyThings (cardID, Name, summary, description) VALUES" +
                "(0, '*Balance', '*Change alignment instantly.', '*The character must change to a radically different alignment. If the character fails to act according to the new alignment, she gains a negative level.')," +
                "(1, '*Comet', '*Defeat the next monster you meet to gain one level.', '*The character must single-handedly defeat the next hostile monster or monsters encountered, or the benefit is lost. If successful, the character gains enough XP to attain the next experience level.')," +
                "(2, '*Donjon', '*You are imprisoned.', '*This card signifies imprisonment— either by the imprisonment spell or by some powerful being. All gear and spells are stripped from the victim in any case. Draw no more cards.')," +
                "(3, '*Euryale', '*–1 penalty on all saving throws henceforth.', '*The medusalike visage of this card brings a curse that only the fates card or a deity can remove. The –1 penalty on all saving throws is otherwise permanent.')," +
                "(4, '*The Fates', '*Avoid any situation you choose . . . once.', '*This card enables the character to avoid even an instantaneous occurrence if so desired, for the fabric of reality is unraveled and respun. Note that it does not enable something to happen. It can only stop something from happening or reverse a past occurrence. The reversal is only for the character who drew the card; other party members may have to endure the situation.')," +
                "(5, '*Flames', '*Enmity between you and an outsider.', '*Hot anger, jealousy, and envy are but a few of the possible motivational forces for the enmity. The enmity of the outsider can’t be ended until one of the parties has been slain. Determine the outsider randomly, and assume that it attacks the character (or plagues her life in some way) within 1d20 days.')," +
                "(6, '*Fool', '*Lose 10,000 experience points and you must draw again.', '*The payment of XP and the redraw are mandatory. This card is always discarded when drawn, unlike all others except the jester.')," +
                "(7, '*Gem', '*Gain your choice of twenty-five pieces of jewelry or fifty gems.', '*This card indicates wealth. The jewelry is all gold set with gems, each piece worth 2,000 gp, the gems 1,000 gp value each.')," +
                "(8, '*Idiot', '*Lose Intelligence (permanent drain). You may draw again.', '* This card causes the drain of 1d4+1 points of Intelligence immediately. The additional draw is optional.')," +
                "(9, '*Jester', '*Gain 10,000 XP or two more draws from the deck.', '*This card is always discarded when drawn, unlike all others except the fool. The redraws are optional.')," +
                "(10, '*Key', '*Gain a major magic weapon.', '*The magic weapon granted must be one usable by the character. It suddenly appears out of nowhere in the character’s hand.')," +
                "(11, '*Knight', '*Gain the service of a 4th-level fighter.', '*The fighter appears out of nowhere and serves loyally until death. He or she is of the same race (or kind) and gender as the character.')," +
                "(12, '*Moon', '*You are granted 1d4 wishes.', '*This card sometimes bears the image of a moonstone gem with the appropriate number of wishes shown as gleams therein; sometimes it depicts a moon with its phase indicating the number of wishes (full=four; gibbous=three; half=two; quarter=one). These wishes are the same as those granted by the 9th-level wizard spell and must be used within a number of minutes equal to the number received.')," +
                "(13, '*Rogue', '*One of your friends turns against you.', '*When this card is drawn, one of the character’s NPC friends (preferably a cohort) is totally alienated and forever after hostile. If the character has no cohorts, the enmity of some powerful personage (or community, or religious order) can be substituted. The hatred is secret until the time is ripe for it to be revealed with devastating effect.')," +
                "(14, '*Ruin', '*Immediately lose all wealth and real property.', '*As implied by its name, when this card is drawn, all nonmagical possessions of the drawer are lost.')," +
                "(15, '*Skull', '*Defeat dread wraith or be forever destroyed.', '*A dread wraith appears. Treat this creature as an unturnable undead. The character must fight it alone—if others help, they get dread wraiths to fight as well. If the character is slain, she is slain forever and cannot be revived, even with a wish or a miracle.')," +
                "(16, '*Star', '*Immediately gain a +2 inherent bonus to one ability score.', '*The 2 points are added to any ability the character chooses. They cannot be divided among two abilities.')," +
                "(17, '*Sun', '*Gain beneficial medium wondrous item and 50,000 XP.', '*Roll for a medium wondrous item until a useful item is indicated.')," +
                "(18, '*Talons', '*All magic items you possess disappear permanently.', '*When this card is drawn, every magic item owned or possessed by the character is instantly and irrevocably gone.')," +
                "(19, '*Throne', '*Gain a +6 bonus on Diplomacy checks plus a small keep.', '*he character becomes a true leader in people’s eyes. The castle gained appears in any open area she wishes (but the decision where to place it must be made within 1 hour).')," +
                "(20, '*Vizier', '*Know the answer to your next dilemma.', '*This card empowers the character drawing it with the one-time ability to call upon a source of wisdom to solve any single problem or answer fully any question upon her request. The query or request must be made within one year. Whether the information gained can be successfully acted upon is another question entirely.')," +
                "(21, '*The Void', '*Body functions, but soul is trapped elsewhere.', '*This black card spells instant disaster. The character’s body continues to function, as though comatose, but her psyche is trapped in a prison somewhere—in an object on a far plane or planet, possibly in the possession of an outsider. A wish or a miracle does not bring the character back, instead merely revealing the plane of entrapment. Draw no more cards.')";

            com.CommandText = insert;

            com.ExecuteNonQuery();

            newDB.Close();
        }
        /// <summary>
        /// Connects to the Deck of Many Things
        /// </summary>
        private void ConnectToDatabase()
        {
            sqlCon = new SQLiteConnection("DataSource =" + strLocationData + strLocationName + strDatabaseName + ";");
        }
        #endregion

    }
}
