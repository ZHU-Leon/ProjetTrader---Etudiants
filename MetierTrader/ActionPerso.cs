using System;
using System.Collections.Generic;
using System.Text;

namespace MetierTrader
{
    public class ActionPerso
    {
        private int numAction;
        private int numTrader;
        private double prixAchat;
        private int quantite;

        public ActionPerso(int unNumAction, int unNumTrader, double unPrix, int uneQuantite)
        {
            NumAction = unNumAction;
            NumTrader = unNumTrader;
            PrixAchat = unPrix;
            Quantite = uneQuantite;
        }

        public int NumAction { get => numAction; set => numAction = value; }
        public int NumTrader { get => numTrader; set => numTrader = value; }
        public double PrixAchat { get => prixAchat; set => prixAchat = value; }
        public int Quantite { get => quantite; set => quantite = value; }
    }
}
