﻿using System.Linq;
using AngleSharp.Dom;
using BuildingBlocks.Parsers;

namespace NbaOracle.Providers.BasketballReference.Teams.Parsers.TeamRooster
{
    public class TeamRoosterParser : IDocumentParser<TeamRoosterData>
    {
        public TeamRoosterData Parse(IDocument document)
        {
            //todo do with selector thingy
            var element = document.All.FirstOrDefault(x => x.LocalName == "div" && x.HasAttribute("id") && x.GetAttribute("id") == "all_roster");
            if (element == null)
                return null;

            var output = new TeamRoosterData();

            foreach (var player in element.QuerySelectorAll("tbody tr"))
            {
                var playerNameElement = player.QuerySelector("td[data-stat='player']");
                var playerNumberElement = player.QuerySelector("th[data-stat='number']");
                var positionElement = player.QuerySelector("td[data-stat='pos']");
                var birthDateElement = player.QuerySelector("td[data-stat='birth_date']");
                var birthCountryElement = player.QuerySelector("td[data-stat='birth_country']");
                var heightElement = player.QuerySelector("td[data-stat='height']");
                var weightElement = player.QuerySelector("td[data-stat='weight']");
                var yearsExperienceElement = player.QuerySelector("td[data-stat='years_experience']");
                var college = player.QuerySelector("td[data-stat='college']");

                output.AddPlayer(new PlayerRoosterData(playerNameElement.TextContent, playerNumberElement.TextContent, positionElement.TextContent, birthDateElement.GetAttribute("csk"), birthCountryElement.TextContent, heightElement.TextContent, weightElement.TextContent, yearsExperienceElement.TextContent, college.TextContent));
            }

            return output;
        }
    }
}