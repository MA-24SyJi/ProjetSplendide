/**
 * \file      frmAddVideoGames.cs
 * \author    F. Andolfatto
 * \version   1.0
 * \date      August 22. 2018
 * \brief     Form to play.
 *
 * \details   This form enables to choose coins or cards to get ressources (precious stones) and prestige points 
 * to add and to play with other players
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Splendor
{
    /// <summary>
    /// manages the form that enables to play with the Splendor
    /// </summary>
    public partial class frmSplendor : Form
    {

        private List<List<TextBox>> gridCard = new List<List<TextBox>>();

        private List<List<Card>> GridStockCard = new List<List<Card>>();

        private Random rand = new Random();

        //Creat object 
        coin Gold = new coin();
        coin Diamand = new coin();
        coin Emeraude = new coin();
        coin Onyx = new coin();
        coin Rubis = new coin();
        coin Saphir = new coin();
        //used to store the number of coins selected for the current round of game
        private int nbRubis;
        private int nbOnyx;
        private int nbEmeraude;
        private int nbDiamand;
        private int nbSaphir;

        private int nbTotal;
        // Set coin label with default value



        //id of the player that is playing
        private int currentPlayerId;
        //boolean to enable us to know if the user can click on a coin or a card
        private bool enableClicLabel;
        //connection to the database
        private ConnectionDB conn;

        /// <summary>
        /// constructor
        /// </summary>
        public frmSplendor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// loads the form and initialize data in it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSplendor_Load(object sender, EventArgs e)
        {
            //set object            
            Gold.Number = 5;
            Diamand.Number = 7;
            Emeraude.Number = 7;
            Onyx.Number = 7;
            Rubis.Number = 7;
            Saphir.Number = 7;
            // Diplay on l           
            lblGoldCoin.Text = Gold.Number.ToString();
            lblDiamandCoin.Text = Diamand.Number.ToString();
            lblEmeraudeCoin.Text = Emeraude.Number.ToString();
            lblOnyxCoin.Text = Onyx.Number.ToString();
            lblRubisCoin.Text = Rubis.Number.ToString();
            lblSaphirCoin.Text = Saphir.Number.ToString();
            //Initialize a connection with sqlite database
            conn = new ConnectionDB();

            //load cards from the database
            //they are not hard coded any more
            //TO DO

            gridCard.Add(new List<TextBox>() { txtLevel11, txtLevel12, txtLevel13, txtLevel14 });
            gridCard.Add(new List<TextBox>() { txtLevel21, txtLevel22, txtLevel23, txtLevel24 });
            gridCard.Add(new List<TextBox>() { txtLevel31, txtLevel32, txtLevel33, txtLevel34 });
            gridCard.Add(new List<TextBox>() { txtNoble1, txtNoble2, txtNoble3, txtNoble4 });

            foreach (List<TextBox> txtBoxes in gridCard)
            {
                foreach (TextBox txtBox in txtBoxes)
                {
                    txtBox.ReadOnly = true;
                }
            }


            //load cards from the database
            List<Card> listCardOne = conn.GetListCardAccordingToLevel(1);
            List<Card> listCardTwo = conn.GetListCardAccordingToLevel(2);
            List<Card> listCardThree = conn.GetListCardAccordingToLevel(3);
            List<Card> listCardNoble = conn.GetListCardAccordingToLevel(4);

            GridStockCard = new List<List<Card>>() { listCardOne, listCardTwo, listCardThree, listCardNoble };

            for (int a = 0; a < gridCard.Count; a++)
            {
                for (int b = 0; b < gridCard[a].Count; b++)
                {
                    var RandomValue = rand.Next((GridStockCard[a].Count()) - 1);
                    gridCard[a][b].Text = GridStockCard[a].ElementAt(RandomValue).ToString();
                    GridStockCard[a].RemoveAt(RandomValue);
                }
            }
            //Go through the results
            //Don't forget to check when you are at the end of the stack

            //fin TO DO
            this.Width = 680;
            this.Height = 540;

            enableClicLabel = false;

            lblChoiceDiamand.Visible = false;
            lblChoiceOnyx.Visible = false;
            lblChoiceRubis.Visible = false;
            lblChoiceSaphir.Visible = false;
            lblChoiceEmeraude.Visible = false;
            cmdValidateChoice.Visible = false;
            cmdNextPlayer.Visible = false;

            //we wire the click on all cards to the same event
            //TO DO for all cards
            txtLevel11.Click += ClickOnCard;
            txtLevel12.Click += ClickOnCard;

        }

        private void ClickOnCard(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {




                TextBox txtBox = sender as TextBox;
                String[] TableauDeLignes = txtBox.Lines;
                


                //get the text displayed in the textbox that has been clicked
                MessageBox.Show(TableauDeLignes[2]);

            }
            //We get the value on the card and we split it to get all the values we need (number of prestige points and ressource)
            //Enable the button "Validate"
            //TO DO
        }

        /// <summary>
        /// click on the play button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPlay_Click(object sender, EventArgs e)
        {
            this.Width = 680;
            this.Height = 780;

            int id = 0;

            LoadPlayer(id);

        }


        /// <summary>
        /// load data about the current player
        /// </summary>
        /// <param name="id">identifier of the player</param>
        private void LoadPlayer(int id)
        {
            currentPlayerId = id;
            enableClicLabel = true;

            string name = conn.GetPlayerName(currentPlayerId);

            //no coins or card selected yet, labels are empty
            lblChoiceDiamand.Text = "";
            lblChoiceOnyx.Text = "";
            lblChoiceRubis.Text = "";
            lblChoiceSaphir.Text = "";
            lblChoiceEmeraude.Text = "";
            lblChoiceCard.Text = "";

            //no coins selected
            nbDiamand = 0;
            nbOnyx = 0;
            nbRubis = 0;
            nbSaphir = 0;
            nbEmeraude = 0;

            Player player = new Player();
            player.Name = name;
            player.Id = id;
            player.Ressources = new int[] { 2, 0, 1, 1, 1 };
            player.Coins = new int[] { 0, 1, 0, 1, 1 };

            lblPlayerDiamandCoin.Text = player.Coins[0].ToString();
            lblPlayerOnyxCoin.Text = player.Coins[0].ToString();
            lblPlayerRubisCoin.Text = player.Coins[0].ToString();
            lblPlayerSaphirCoin.Text = player.Coins[0].ToString();
            lblPlayerEmeraudeCoin.Text = player.Coins[0].ToString();
            currentPlayerId = id;

            lblPlayer.Text = "Jeu de " + name;

            cmdPlay.Enabled = false;
        }

        /// <summary>
        /// Coin selection control
        /// </summary>
        /// <param name="nbCoin"></param>
        /// <param name="lblCoin"></param>
        /// <param name="lblChoiceCoin"></param>
        private void CoinChecker(ref int nbCoin, ref Label lblCoin, ref Label lblChoiceCoin)
        {
            lblChoiceCoin.Visible = true;
            int var = Convert.ToInt32(lblCoin.Text);
            if (var != 0)
            {
                if (var == 2 && nbCoin == 1)
                {
                    MessageBox.Show("Vous pouvez prendre 2 jetons de la même couleur uniquement à condition qu'il en reste au moin 4 dans la pile");
                }

                if (nbRubis == 2 || nbSaphir == 2 || nbOnyx == 2 || nbEmeraude == 2 || nbDiamand == 2)
                {
                    MessageBox.Show("Vous ne pouvez pas prendre ce jeton car vous avez déja pris 2 mêmes pierres précieuse");
                }
                else
                {
                    if ((nbCoin == 1 && nbRubis == 1) || (nbCoin == 1 && nbSaphir == 1 ) || (nbCoin == 1 && nbOnyx == 1) || (nbCoin == 1 && nbEmeraude == 1) || (nbCoin == 1 && nbDiamand == 1))
                    {
                        MessageBox.Show("Vous ne pouvez pas prendre 2 même jetons ainsi qu'un jeton différent");
                    }
                    else
                    {
                        nbTotal = nbRubis + nbSaphir + nbOnyx + nbEmeraude + nbDiamand;
                        if (nbTotal >= 3)
                        {
                            MessageBox.Show("Vous avez pris le nombre de jetons maximum");
                        }
                        else
                        {
                            nbCoin++;
                            var--;
                            lblCoin.Text = var.ToString();
                            lblChoiceCoin.Text = nbCoin + "\r\n";
                        }
                    }
                }
            }

        }


        /// <summary>
        /// click on the red coin (rubis) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblRubisCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                CoinChecker(ref nbRubis, ref lblRubisCoin, ref lblChoiceRubis);
                cmdValidateChoice.Visible = true;               
            }
        }

        /// <summary>
        /// click on the blue coin (saphir) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSaphirCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                CoinChecker(ref nbSaphir, ref lblSaphirCoin, ref lblChoiceSaphir);
                cmdValidateChoice.Visible = true;             
            }
        }

        /// <summary>
        /// click on the black coin (onyx) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblOnyxCoin_Click(object sender, EventArgs e)
        {

            if (enableClicLabel)
            {
                CoinChecker(ref nbOnyx, ref lblOnyxCoin, ref lblChoiceOnyx);
                cmdValidateChoice.Visible = true;
            }
        }

        /// <summary>
        /// click on the green coin (emeraude) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblEmeraudeCoin_Click(object sender, EventArgs e)
        {

            if (enableClicLabel)
            {
                CoinChecker(ref nbEmeraude, ref lblEmeraudeCoin, ref lblChoiceEmeraude);
                cmdValidateChoice.Visible = true;
            }
        }

        /// <summary>
        /// click on the white coin (diamand) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDiamandCoin_Click(object sender, EventArgs e)
        {

            if (enableClicLabel)
            {
                CoinChecker(ref nbDiamand, ref lblDiamandCoin, ref lblChoiceDiamand);
                cmdValidateChoice.Visible = true;
            }
        }

        /// <summary>
        /// click on the validate button to approve the selection of coins or card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdValidateChoice_Click(object sender, EventArgs e)
        {
            cmdNextPlayer.Visible = true;
            //TO DO Check if card or coins are selected, impossible to do both at the same time

            cmdNextPlayer.Enabled = true;
        }

        /// <summary>
        /// click on the insert button to insert player in the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInsertPlayer_Click(object sender, EventArgs e)
        {
            AddNewUser f2 = new AddNewUser();
            f2.ShowDialog();

        }

        /// <summary>
        /// click on the next player to tell him it is his turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNextPlayer_Click(object sender, EventArgs e)
        {
            //TO DO in release 1.0 : 3 is hard coded (number of players for the game), it shouldn't. 
            //TO DO Get the id of the player : in release 0.1 there are only 3 players
            //Reload the data of the player
            //We are not allowed to click on the next button

        }

        private void txtLevel12_TextChanged(object sender, EventArgs e)
        {

        }


        /*
         * Selection card : 
         */

        private void txtLevel12_DoubleClick(object sender, EventArgs e)
        {
            txtPlayerBookedCard.Text = txtLevel12.Text;
        }

        private void txtLevel11_DoubleClick(object sender, EventArgs e)
        {
            txtPlayerBookedCard.Text = txtLevel11.Text;
        }

        private void txtLevel14_TextChanged(object sender, EventArgs e)
        {

        }

        /*
         * fin Selection card : 
         */
    }
}