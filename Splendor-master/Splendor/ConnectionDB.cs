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

        /// <summary>
        /// Create the "NbCoin" table and insert data
        /// </summary>
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

        /// <summary>
        /// Insert data in the cost table
        /// </summary>
        private void CreateInsertCost()
        {
            //Insert cost of the Cards
            InsertInto("Create table Cost (idCost integer primary key autoincrement, fkCard int, fkRessource int, nbRessource int, foreign key(fkCard) references Card(idCard), foreign key(fkRessource) references Ressource(idRessource))");

            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,7)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,7)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,6)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,6)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,6)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,7)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,7)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,6)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,7)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,7)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,6)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,6)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,6)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,7)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,7)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,6)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,7)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,6)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,7)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,6)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,5)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,3)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,4)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,2)");
            InsertInto("Insert into Cost(fkCard, fkRessource, nbRessource) values (0, 0,1)");
        }
    }
}
