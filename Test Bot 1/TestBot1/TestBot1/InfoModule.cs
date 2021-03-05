using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestBot1;

namespace TestBot1
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        

        [Command("ping", RunMode = RunMode.Async)]
        [Summary("Test command")]
        [RequireContext(ContextType.Guild)]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync("pong!");
        }



        [Command("add", RunMode = RunMode.Async)]
        [Summary("Adds a team")]
        [RequireContext(ContextType.Guild)]
        public async Task Add(String nameParam)
        {
            League l = Program.getLeague();
            l.addTeam(nameParam);
            Program.setLeague(l);
            await Context.Channel.SendMessageAsync(nameParam+" added!");
        }


        [Command("add", RunMode = RunMode.Async)]
        [Summary("Adds a team")]
        [RequireContext(ContextType.Guild)]
        public async Task Add(String nameParam, String eloParam, String winParam, String lossParam)
        {
            String name = nameParam;
            double elo = Convert.ToDouble(eloParam);
            int wins = Convert.ToInt32(winParam);
            int losses = Convert.ToInt32(lossParam);
            League l = Program.getLeague();
            l.addTeam(name, elo, wins, losses);
            Program.setLeague(l);
            await Context.Channel.SendMessageAsync(nameParam + " added!");
        }



        [Command("match", RunMode = RunMode.Async)]
        [Summary("Adds a match")]
        [RequireContext(ContextType.Guild)]
        public async Task Match(String t1, String t2)
        {
            League l = Program.getLeague();
            Match m=l.newMatch(t1, t2);
            Program.setLeague(l);
            if(m==null)
            {
                await Context.Channel.SendMessageAsync("At least one of those teams does not exist.");
            }
            else
            {
                await Context.Channel.SendMessageAsync("Match Number: " + m.matchNumber() + " started.\nPlease use \"!report match# winner\" to report your match when finished.");
            }
            
        }

        [Command("report", RunMode = RunMode.Async)]
        [Summary("Reports a match score")]
        [RequireContext(ContextType.Guild)]
        public async Task Report(String matchNumParam, String winnerParam)
        {
            String winner = winnerParam;
            int matchNum = Convert.ToInt32(matchNumParam);
            Match mat = null;
            bool numExist = false;
            bool nameExist = false;

            foreach(Match m in Program.getMatches())
            {
                if(matchNum==m.matchNumber())
                {
                    mat = m;
                    numExist = true;
                    if (mat.getTeam1().Equals(winner) || mat.getTeam2().Equals(winner))
                    {
                        nameExist = true;
                    }
                }
                if(!(numExist && nameExist))
                {
                    mat = null;
                }
            }
            if(mat==null)
            {
                await Context.Channel.SendMessageAsync("Invalid team name or match number.");
            }
            else
            {
                mat.settleMatch(winner);
                await Context.Channel.SendMessageAsync("Match number: " + mat.matchNumber() + " concluded. Thank You!");
                Program.getMatches().Remove(mat);
            }
            

        }

        [Command("leaderboard", RunMode = RunMode.Async)]
        [Summary("Displays leaderboard")]
        [RequireContext(ContextType.Guild)]
        public async Task Leaderboard()
        {
           await Context.Channel.SendMessageAsync(Program.getLeague().getStats());
        }


        [Command("correct", RunMode = RunMode.Async)]
        [Summary("Alter's a team's information")]
        [RequireContext(ContextType.Guild)]
        public async Task Correct(String t, String e, String w, String l)
        {
            League lg = Program.getLeague();
            Team team = lg.getTeam(t);
            if(team==null)
            {
                await Context.Channel.SendMessageAsync("Team does not exist");
            }
            else
            {
                double elo = Convert.ToDouble(e);
                int wins = Convert.ToInt32(w);
                int losses = Convert.ToInt32(l);
                team.correct(elo, wins, losses);
                await Context.Channel.SendMessageAsync("Team info corrected.");
            }

            
        }
    }
}
