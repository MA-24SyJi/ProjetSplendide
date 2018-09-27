using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //Create and insert cards
            //TO DO
            CreateInsertCards();
            //Create and insert ressources
            //TO DO
            CreateInsertRessources();
        }


        /// <summary>
        /// get the list of cards according to the level
        /// </summary>
        /// <returns>cards stack</returns>
        public Stack<Card> GetListCardAccordingToLevel(int level)
        {
            //Get all the data from card table selecting them according to the data
            //TO DO
            //Create an object "Stack of Card"
            Stack<Card> listCard = new Stack<Card>();
            //do while to go to every record of the card table
            //while (....)
            //{
            //Get the ressourceid and the number of prestige points
            //Create a card object

            //select the cost of the card : look at the cost table (and other)

            //do while to go to every record of the card table
            //while (....)
            //{
            //get the nbRessource of the cost
            //}
            //push card into the stack

            //}
            return listCard;
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
            sql = "insert into Ressource (idRessource) values (1)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into Ressource (idRessource) values (2)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into Ressource (idRessource) values (3)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into Ressource (idRessource) values (4)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into Ressource (idRessource) values (5)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }


        private void InsertInto(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        ///  create tables "cards", "cost" and insert data
        /// </summary>
        private void CreateInsertCards()
        {
            //Insert cards without the necessary resources to buy them
            InsertInto("CREATE TABLE cards (idcard INT PRIMARY KEY, fkRessource Int, level Int, nbPtPrestige Int, fkPlayer Int)");

            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (2, 0,4,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (3, 0,4,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (4, 0,4,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (5, 0,4,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (6, 0,4,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (7, 0,4,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (8, 0,4,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (9, 0,4,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (10, 0,4,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (11, 0,4,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (12, 0,3,5)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (13, 0,3,5)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (14, 0,3,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (15, 0,3,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (16, 0,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (17, 0,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (18, 0,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (19, 0,3,5)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (20, 0,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (21, 0,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (22, 0,3,5)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (23, 0,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (24, 0,3,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (25, 0,3,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (26, 0,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (27, 0,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (28, 0,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (29, 0,3,5)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (30, 0,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (31, 0,3,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (32, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (33, 0,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (34, 0,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (35, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (36, 0,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (37, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (38, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (39, 0,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (40, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (41, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (42, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (43, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (44, 0,2,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (45, 0,2,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (46, 0,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (47, 0,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (48, 0,2,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (49, 0,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (50, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (51, 0,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (52, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (53, 0,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (54, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (55, 0,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (56, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (57, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (58, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (59, 0,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (60, 0,2,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (61, 0,2,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (62, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (63, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (64, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (65, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (66, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (67, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (68, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (69, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (70, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (71, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (72, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (73, 0,1,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (74, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (75, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (76, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (77, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (78, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (79, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (80, 0,1,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (81, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (82, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (83, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (84, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (85, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (86, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (87, 0,1,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (88, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (89, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (90, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (91, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (92, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (93, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (94, 0,1,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (95, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (96, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (97, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (98, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (99, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (100, 0,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (101, 0,1,1)");
        }

        private void CreateInsertCost()
        {
            //Insert cost of the cards
            InsertInto("Create table Cost (idCost int primary key, fkCard int, fkRessource int, nbRessource int)");

            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (3, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (6, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (7, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (9, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (11, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (13, 0, 0,7)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (14, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (15, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (16, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (23, 0, 0,7)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (25, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (27, 0, 0,6)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (29, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (30, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (31, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (32, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (33, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (34, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (35, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (36, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (38, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (39, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (42, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (48, 0, 0,6)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (51, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (53, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (57, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (59, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (62, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (63, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (64, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (66, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (67, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (70, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (72, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (76, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (81, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (84, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (85, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (86, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (88, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (91, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (93, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (94, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (96, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (97, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (98, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (100, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (2, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (3, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (8, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (9, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (11, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (15, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (16, 0, 0,6)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (17, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (20, 0, 0,7)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (22, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (24, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (25, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (27, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (29, 0, 0,7)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (31, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (34, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (35, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (37, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (39, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (41, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (42, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (47, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (49, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (51, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (55, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (57, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (58, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (60, 0, 0,6)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (62, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (66, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (70, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (71, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (72, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (73, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (74, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (77, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (78, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (79, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (82, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (83, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (84, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (85, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (86, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (88, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (91, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (92, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (93, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (95, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (4, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (5, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (6, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (7, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (11, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (13, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (14, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (15, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (18, 0, 0,7)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (19, 0, 0,7)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (21, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (24, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (25, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (27, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (30, 0, 0,6)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (33, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (34, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (35, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (38, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (40, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (43, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (46, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (47, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (49, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (53, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (54, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (59, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (61, 0, 0,6)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (62, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (64, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (65, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (66, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (67, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (68, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (70, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (71, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (72, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (74, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (78, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (79, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (88, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (89, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (90, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (92, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (96, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (97, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (100, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (2, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (4, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (8, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (9, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (10, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (12, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (14, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (15, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (16, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (17, 0, 0,6)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (21, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (22, 0, 0,7)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (24, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (26, 0, 0,7)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (31, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (33, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (36, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (37, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (39, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (40, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (45, 0, 0,6)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (46, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (49, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (52, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (55, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (56, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (57, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (58, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (65, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (68, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (69, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (70, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (71, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (72, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (74, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (77, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (79, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (81, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (85, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (86, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (87, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (93, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (95, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (96, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (97, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (98, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (99, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (100, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (101, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (4, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (5, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (7, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (8, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (10, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (12, 0, 0,7)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (14, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (17, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (19, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (21, 0, 0,6)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (24, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (25, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (28, 0, 0,7)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (30, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (31, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (36, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (38, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (40, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (43, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (44, 0, 0,6)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (46, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (47, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (50, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (51, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (53, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (55, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (56, 0, 0,5)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (58, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (64, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (65, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (66, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (74, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (75, 0, 0,3)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (76, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (78, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (79, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (80, 0, 0,4)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (81, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (82, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (85, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (86, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (88, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (89, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (91, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (95, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (97, 0, 0,1)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (99, 0, 0,2)");
            InsertInto("Insert into Cost(idCost, fkCard, fkRessource, nbRessource) values (100, 0, 0,1)");
        }
    }
}
