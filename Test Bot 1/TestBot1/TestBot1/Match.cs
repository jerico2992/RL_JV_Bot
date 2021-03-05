using System;
using System.Collections.Generic;
using System.Text;

namespace TestBot1
{
    public class Match
    {
        private Team t1;
        private Team t2;
        private int matchNum;
        private double t1Prob;
        private double t2Prob;

        public Match(Team t1Param, Team t2Param)
        {
            t1 = t1Param;
            t2 = t2Param;
            matchNum = Program.getMatchNumber();
            Program.incMatchNumber();
            t1Prob = 0;
            t2Prob = 0;
            
        }
        /*
        public void setT1(Team t1Param)
        {
            t1 = t1Param;
        }

        public void setT2(Team t2Param)
        {
            t2 = t2Param;
        }
        */

        public void settleMatch(String winner)
        {
            setProbs();
            if(t1.getName().Equals(winner))
            {
                t1.setElo(Math.Round((t1.getElo() + 30 * (1 - t1Prob)), 2));
                t2.setElo(Math.Round((t2.getElo() + 30 * (0 - t2Prob)), 2));
                t1.win();
                t2.lose();
            }
            else
            {
                t1.setElo(Math.Round((t1.getElo() + 30 * (0 - t1Prob)), 2));
                t2.setElo(Math.Round((t2.getElo() + 30 * (1 - t2Prob)), 2));
                t2.win();
                t1.lose();
            }
        }

        private void setProbs()
        { 
            t1Prob = 1 / (1 + Math.Pow(10, ((t2.getElo() - t1.getElo())/400)));
            t2Prob = 1 / (1 + Math.Pow(10, ((t1.getElo() - t2.getElo())/400)));
        }

        public String getTeam1()
        {
            return t1.getName();
        }

        public String getTeam2()
        {
            return t2.getName();
        }

        public int matchNumber()
        {
            return matchNum;
        }
                
    }
}
