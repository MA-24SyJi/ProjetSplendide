using System.Collections.Generic;
using System.Data.SQLite;

namespace Splendor
{
    /// <summary>
    /// contains methods and attributes to connect and deal with the database
    /// TO DO : le modèle de données n'est pas super, à revoir!!!!
    /// </summary>
    class ConnectionDB
    {
        //connection to the database
        private SQLiteConnection m_dbConnection;

        /// <summary>
        /// constructor : creates the connection to the database SQLite
        /// </summary>
        public ConnectionDB()
        {

            SQLiteConnection.CreateFile("Splendor.sqlite");

            m_dbConnection = new SQLiteConnection("Data Source=Splendor.sqlite;Version=3;");
            m_dbConnection.Open();

            //create and insert players
            CreateInsertPlayer();
            //Create and insert Cards
            //TO DO
            CreateInsertCards();
            //Create and insert ressources
            //TO DO
            CreateInsertRessources();

            CreateInsertCost();

            //Create and insert NbCoin
            CreateInsertNbCoin();
        }


        /// <summary>
        /// get the list of Cards according to the level
        /// </summary>
        /// <returns>Cards stack</returns>
        public List<Card> GetListCardAccordingToLevel(int level)
        {
            //Get all the data from Card table selecting them according to the data
            //TO DO
            //Create an object "Stack of Card"
            List<Card> listCard = new List<Card>();

            //Create a Card object
            Card Card = new Card();

            //do while to go to every record of the Card table
            string sql = "select fkRessource, nbPtPrestige from Card where level = " + level;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            int ressource = 0;
            int nbPtPrestige = 0;

            while (reader.Read())
            {
                //Get the ressourceid and the number of prestige points
                ressource = (int)reader["fkRessource"];
                nbPtPrestige = (int)reader["nbPtPrestige"];
                Card.Ress = (Ressources)ressource;
                Card.PrestigePt = nbPtPrestige;
                Card.Level = level;
                listCard.Add(Card);
            }

            //select the cost of the Card : look at the cost table (and other)
            //do while to go to every record of the Card table
            string sql2 = "select fkCard, fkRessource from Cost";
            SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
            SQLiteDataReader reader2 = command2.ExecuteReader();
            int[] ressource2;

            while (reader.Read())
            {
                //get the nbRessource of the cost
                ressource2 = (int[])reader["fkRessource"];
                Card.Cout = ressource2;
                //Add Card into the stack
                listCard.Add(Card);
            }
            return listCard;
        }

        private void CreateInsertNbCoin()
        {
            string sql = "Create table NbCoin (IdNbCoin int primary key, fkPlayer int, fkRessource int, nbCoin int, Foreign key(fkPlayer) references player(id), foreign key(fkRessource) references Ressource(idRessource))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }


        /// <summary>
        /// create the "player" table and insert data
        /// </summary>
        private void CreateInsertPlayer()
        {
            string sql = "CREATE TABLE player (id INT PRIMARY KEY, pseudo VARCHAR(20))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into player (id, pseudo) values (0, 'Fred')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into player (id, pseudo) values (1, 'Harry')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into player (id, pseudo) values (2, 'Sam')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }


        /// <summary>
        /// get the name of the player according to his id
        /// </summary>
        /// <param name="id">id of the player</param>
        /// <returns></returns>
        public string GetPlayerName(int id)
        {
            string sql = "select pseudo from player where id = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string name = "";
            while (reader.Read())
            {
                name = reader["pseudo"].ToString();
            }
            return name;
        }

        /// <summary>
        /// create the table "ressources" and insert data
        /// </summary>
        private void CreateInsertRessources()
        {
            //Create Ressource table
            string sql = "Create table Ressource (idRessource int primary key)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            //Insert Ressources of the game
            sql = "insert into Ressource (idRessource) values (1)"; //Rubis
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into Ressource (idRessource) values (2)"; //Emeraude
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into Ressource (idRessource) values (3)"; //Onyx
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into Ressource (idRessource) values (4)"; //Saphir
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into Ressource (idRessource) values (5)"; //Diamant
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }


        private void InsertInto(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        ///  create tables "Cards", "cost" and insert data
        /// </summary>
        private void CreateInsertCards()
        {
            //Insert Cards without the necessary resources to buy them
            InsertInto("CREATE TABLE Card (idCard INT PRIMARY KEY, fkRessource Int, level Int, nbPtPrestige Int, fkPlayer Int, foreign key(fkRessource) references Ressource(idRessource), foreign key(fkPlayer) references player(id))");

            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (2, 0,4,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (3, 0,4,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (4, 0,4,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (5, 0,4,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (6, 0,4,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (7, 0,4,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (8, 0,4,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (9, 0,4,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (10, 0,4,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (11, 0,4,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (12, 0,3,5)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (13, 0,3,5)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (14, 0,3,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (15, 0,3,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (16, 0,3,4)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (17, 0,3,4)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (18, 0,3,4)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (19, 0,3,5)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (20, 0,3,4)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (21, 0,3,4)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (22, 0,3,5)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (23, 0,3,4)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (24, 0,3,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (25, 0,3,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (26, 0,3,4)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (27, 0,3,4)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (28, 0,3,4)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (29, 0,3,5)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (30, 0,3,4)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (31, 0,3,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (32, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (33, 0,2,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (34, 0,2,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (35, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (36, 0,2,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (37, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (38, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (39, 0,2,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (40, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (41, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (42, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (43, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (44, 0,2,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (45, 0,2,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (46, 0,2,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (47, 0,2,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (48, 0,2,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (49, 0,2,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (50, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (51, 0,2,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (52, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (53, 0,2,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (54, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (55, 0,2,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (56, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (57, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (58, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (59, 0,2,2)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (60, 0,2,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (61, 0,2,3)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (62, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (63, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (64, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (65, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (66, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (67, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (68, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (69, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (70, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (71, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (72, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (73, 0,1,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (74, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (75, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (76, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (77, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (78, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (79, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (80, 0,1,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (81, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (82, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (83, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (84, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (85, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (86, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (87, 0,1,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (88, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (89, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (90, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (91, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (92, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (93, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (94, 0,1,1)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (95, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (96, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (97, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (98, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (99, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (100, 0,1,0)");
            InsertInto("insert into Card(idCard, fkRessource, level, nbPtPrestige) values (101, 0,1,1)");
        }

        private void CreateInsertCost()
        {
            //Insert cost of the Cards
            InsertInto("Create table Cost (idCost integer primary key autoincrement, fkCard int, fkRessource int, nbRessource int, foreign key(fkCard) references Card(idCard), foreign key(fkRessource) references Ressource(idRessource))");


            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (3, 1,4)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (6, 1,4)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (7, 1,3)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (9, 1,3)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (11, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (13, 1,7)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (14, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (15, 1,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (16, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (23, 1,7)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (25, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (27, 1,6)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (29, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (30, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (31, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (32, 1,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (33, 1,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (34, 1,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (35, 1,4)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (36, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (38, 1,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (39, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (42, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (48, 1,6)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (51, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (53, 1,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (57, 1,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (59, 1,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (62, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (63, 1,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (64, 1,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (66, 1,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (67, 1,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (70, 1,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (72, 1,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (76, 1,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (81, 1,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (84, 1,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (85, 1,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (86, 1,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (88, 1,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (91, 1,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (93, 1,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (94, 1,4)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (96, 1,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (97, 1,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (98, 1,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (100, 1,1)");

            // Emeraude


            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (2, 2,4)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (3, 2,4)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (8, 2,3)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (9, 2,3)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (11, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (15, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (16, 2,6)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (17, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (20, 2,7)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (22, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (24, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (25, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (27, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (29, 2,7)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (31, 2,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (34, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (35, 2,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (37, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (39, 2,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (41, 2,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (42, 2,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (47, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (49, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (51, 2,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (55, 2,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (57, 2,4)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (58, 2,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (60, 2,6)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (62, 2,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (66, 2,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (70, 2,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (71, 2,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (72, 2,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (73, 2,4)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (74, 2,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (77, 2,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (78, 2,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (79, 2,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (82, 2,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (83, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (84, 2,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (85, 2,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (86, 2,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (88, 2,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (91, 2,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (92, 2,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (93, 2,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (95, 2,1)");

            // Onyx

            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (4, 3,3)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (5, 3,4)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (6, 3,4)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (7, 3,3)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (11, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (13, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (14, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (15, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (18, 3,7)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (19, 3,7)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (21, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (24, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (25, 3,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (27, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (30, 3,6)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (33, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (34, 3,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (35, 3,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (38, 3,4)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (40, 3,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (43, 3,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (46, 3,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (47, 3,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (49, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (53, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (54, 3,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (59, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (61, 3,6)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (62, 3,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (64, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (65, 3,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (66, 3,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (67, 3,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (68, 3,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (70, 3,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (71, 3,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (72, 3,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (74, 3,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (78, 3,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (79, 3,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (88, 3,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (89, 3,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (90, 3,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (92, 3,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (96, 3,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (97, 3,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (100, 3,2)");

            // Saphire

            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (2, 4,4)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (4, 4,3)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (8, 4,3)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (9, 4,3)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (10, 4,4)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (12, 4,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (14, 4,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (15, 4,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (16, 4,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (17, 4,6)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (21, 4,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (22, 4,7)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (24, 4,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (26, 4,7)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (31, 4,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (33, 4,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (36, 4,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (37, 4,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (39, 4,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (40, 4,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (45, 4,6)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (46, 4,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (49, 4,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (52, 4,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (55, 4,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (56, 4,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (57, 4,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (58, 4,4)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (65, 4,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (68, 4,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (69, 4,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (70, 4,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (71, 4,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (72, 4,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (74, 4,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (77, 4,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (79, 4,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (81, 4,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (85, 4,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (86, 4,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (87, 4,4)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (93, 4,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (95, 4,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (96, 4,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (97, 4,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (98, 4,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (99, 4,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (100, 4,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (101, 4,4)");

            // Diamant

            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (4, 5,3)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (5, 5,4)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (7, 5,3)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (8, 5,3)"); 
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (10, 5,4)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (12, 5,7)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (14, 5,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (17, 5,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (19, 5,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (21, 5,6)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (24, 5,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (25, 5,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (28, 5,7)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (30, 5,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (31, 5,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (36, 5,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (38, 5,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (40, 5,4)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (43, 5,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (44, 5,6)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (46, 5,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (47, 5,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (50, 5,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (51, 5,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (53, 5,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (55, 5,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (56, 5,5)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (58, 5,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (64, 5,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (65, 5,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (66, 5,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (74, 5,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (75, 5,3)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (76, 5,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (78, 5,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (79, 5,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (80, 5,4)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (81, 5,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (82, 5,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (85, 5,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (86, 5,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (88, 5,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (89, 5,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (91, 5,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (95, 5,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (97, 5,1)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (99, 5,2)");
            InsertInto( "insert into cost(fkCard, fkRessource, nbRessource) values (100, 5,1)"); 

        }
    }
}
