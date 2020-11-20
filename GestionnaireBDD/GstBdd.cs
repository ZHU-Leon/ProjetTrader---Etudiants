using MySql.Data.MySqlClient;
using System;
using MetierTrader;
using System.Collections.Generic;

namespace GestionnaireBDD
{
    public class GstBdd
    {
        private MySqlConnection cnx;
        private MySqlCommand cmd;
        private MySqlDataReader dr;

        // Constructeur
        public GstBdd()
        {
            string chaine = "Server=localhost;Database=bourse;Uid=root;Pwd=";
            cnx = new MySqlConnection(chaine);
            cnx.Open();
        }

        public List<Trader> getAllTraders()
        {
            List<Trader> mesTraders = new List<Trader>();
            cmd = new MySqlCommand("Select idTrader, nomTrader from trader", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Trader unTrader = new Trader(Convert.ToInt16(dr[0].ToString()), dr[1].ToString());
                mesTraders.Add(unTrader);
            }
            dr.Close();
            return mesTraders;
        }
        public List<ActionPerso> getAllActionsByTrader(int numTrader)
        {
            List<ActionPerso> mesActionsPersos = new List<ActionPerso>();
            cmd = new MySqlCommand("select numAction, numTrader, prixAchat, quantite from acheter where numTrader=" + numTrader, cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ActionPerso uneActionPerso = new ActionPerso(Convert.ToInt32(dr[0].ToString()), Convert.ToInt32(dr[1].ToString()), Convert.ToInt32(dr[2].ToString()), Convert.ToInt32(dr[3].ToString()));
                mesActionsPersos.Add(uneActionPerso);
            }
            dr.Close();
            return mesActionsPersos;
        }

        public List<MetierTrader.Action> getAllActionsNonPossedees(int numTrader)
        {
            List<MetierTrader.Action> mesActionsNonPossedes = new List<MetierTrader.Action>();
            cmd = new MySqlCommand("select idAction, nomAction from action where idAction not in (select numAction from action act inner join acheter ach on act.idAction = ach.numAction inner join trader t on ach.numTrader = t.idTrader where idTrader=" + numTrader + ")", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MetierTrader.Action uneActionNonPossede = new MetierTrader.Action(Convert.ToInt32(dr[0].ToString()), dr[1].ToString());
                mesActionsNonPossedes.Add(uneActionNonPossede);
            }
            dr.Close();
            return mesActionsNonPossedes;
        }

        public void SupprimerActionAcheter(int numAction, int numTrader)
        {
            cmd = new MySqlCommand("delete from acheter where numaction=" + numAction + " and numTrader=" + numTrader, cnx);
            cmd.ExecuteNonQuery();
        }

        public void UpdateQuantite(int numAction, int numTrader, int quantite)
        {
            cmd = new MySqlCommand("UPDATE acheter SET quantite =" + quantite + " where `numAction`=" + numAction + " and `numTrader`=" + numTrader, cnx);
            cmd.ExecuteNonQuery();
        }

        public double getCoursReel(int numAction)
        {
            double cours;
            cmd = new MySqlCommand("SELECT coursReelfrom action where `idAction`=" + numAction, cnx);
            dr = cmd.ExecuteReader();
            dr.Read();
            cours = Convert.ToInt32(dr[0].ToString());
            dr.Close();
            return cours;
        }
        public void AcheterAction(int numAction, int numTrader, double prix, int quantite)
        {
            cmd = new MySqlCommand("insert into acheter values(" + numAction + "," + numTrader + "," + prix + "," + quantite + ")", cnx);
            cmd.ExecuteNonQuery();
        }
        public double getTotalPortefeuille(int numTrader)
        {
            double total;
            cmd = new MySqlCommand("select sum(`prixAchat`*`quantite`) as Total from acheter where numTrader =" + numTrader, cnx);
            dr = cmd.ExecuteReader();
            dr.Read();
            total = Convert.ToInt32(dr[0].ToString());
            dr.Close();
            return total;
        }
    }
}
