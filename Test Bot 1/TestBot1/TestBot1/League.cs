using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TestBot1
{
    public class League
    {
        private ArrayList teams;
 

        public League()
        {
            teams = new ArrayList();
            //matches = new ArrayList();
        }

        public void addTeam(String nameParam)
        {
            
           teams.Add(new Team(nameParam));
           
        }

        public void addTeam(String nameParam, double elo, int wins, int losses)
        {

            teams.Add(new Team(nameParam, elo, wins, losses));

        }

        public Match newMatch(String t1Param, String t2Param)
        {
            String t1 = t1Param;
            String t2 = t2Param;
            Team team1 = null;
            Team team2 = null;
            bool t1Exist = false;
            bool t2Exist = false;
            foreach(Team t in teams)
            {
                if(t1.Equals(t.getName()))
                {
                    team1 = t;
                    t1Exist = true;
                }
                if (t2.Equals(t.getName()))
                {
                    team2 = t;
                    t2Exist = true;
                }
            }
            if (t1Exist && t2Exist)
            {
                Match m = new Match(team1, team2);
                Program.addMatch(m);
                return m;
            }
            return null;
            
        }

        public String getStats()
        {
            sortLeaderboard();
            String stats = "";
            foreach(Team t in teams)
            {
                stats+=(t.getName() + " " + t.getElo()+" - "+t.getWins()+" Wins "+t.getLosses()+" losses\n");
            }
            return stats;
        }

        private void sortLeaderboard()
        {
            bool finished = false;
            while(true)
            {
                finished = true;
                for(int i =0; i<teams.Count-1; i++)
                {                    
                    Team currentTeam = (Team)teams[i];
                    Team nextTeam = (Team)teams[i + 1];
                    double tElo = currentTeam.getElo();
                    double nextElo = nextTeam.getElo();
                    if(nextElo>tElo)
                    {
                        teams.RemoveAt(i + 1);
                        teams.Insert(i, nextTeam);
                        finished = false;
                   
                    }                
                    
                }
                if (finished == true) break;
      
            }
          
        }
        public Team getTeam(String s)
        {
            foreach(Team t in teams)
            {
                if(s.Equals(t.getName()))
                {
                    return t;
                }
            }
            return null;
        }
        

    }
}
