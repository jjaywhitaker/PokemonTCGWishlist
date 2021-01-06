using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokemonTcgSdk;
using PokemonTcgSdk.Models;

namespace PokemonTCGWishlist.Controllers
{
    public class ExploreController : Controller
    {
        private readonly ILogger<ExploreController> _logger;
        // Get a list of cards via a query string
        Dictionary<string, string> query = new Dictionary<string, string>()
        {
            { "name", "Charizard" },
            { "set", "Base" }
        };

        public List<SetData> setList { get; set; }
        public SetData currSet { get; set; }
        public List<PokemonCard> currentSetCardList { get; set; }
        public PokemonCard currCard { get; set; }
        public SortOption currSort { get; set; }
        public bool showPokemon { get; set; }
        public bool showTrainer { get; set; }
        public bool showEnergy { get; set; }

        public ExploreController(ILogger<ExploreController> logger)
        {
            _logger = logger;
            //cardList = Card.Get(query).Cards;
            //var c = Card.All(new Dictionary<string, string>() { { "name", "Pikachu" } }); //returns 105
            //var b = Card.Get(new Dictionary<string, string>() { { "name", "Pikachu" } }); //returns 100 
            setList = Sets.All();
            currSort = SortOption.SetNumber;
            showPokemon = true;
            showTrainer = true;
            showEnergy = true;

        }

        /// <summary>
        /// Called when viewing the SetList results 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //add search bar 
            ViewData["SetList"] = setList;
            
            return View();
            
        }

        /// <summary>
        /// Called when viewing any sort of list of cards. ex all cards in a set, all pikachu cards, all fire type cards etc 
        /// </summary>
        /// <param name="setCode"> The setCode of the current set.</param>
        /// <param name="sortOrder"> What value cards should be sorted on </param>
        /// <param name="typeFilters"> String of types for which cards should be desplayed (by SuperType ex.) Pokemon, Trainer, Energy)</param>
        /// <returns></returns>
        public IActionResult ResultView(string setCode, string typeFilters, string sortOrder = "SetNumber", string searchText = null)
        {
            //TODO:: only make api calls when needed(check if anything changed)

            //get setData based on setCode passed in 
            var tempList = Sets.Find(new Dictionary<string, string>() { { "ptcgoCode", setCode } });
             currSet = tempList.Count != 0 ? tempList.First() : null;
            ViewData["CurrSet"] = currSet;

            //get list of cards for the current set
            var query = new Dictionary<string, string>() { { "setCode", currSet.Code } };
            if(!string.IsNullOrWhiteSpace(searchText))
            {
                query.Add("name", searchText);
            }
            currentSetCardList = Card.All(query);

            if (string.IsNullOrWhiteSpace(typeFilters))
            {
                typeFilters = GetTypeFilters();
            }
            else
            {
                //filter cards then sort
                showPokemon = typeFilters.Contains("pokemon");
                showTrainer = typeFilters.Contains("trainer");
                showEnergy = typeFilters.Contains("energy");
            }

            ViewData["ShowPokemon"] = showPokemon;
            ViewData["ShowTrainer"] = showTrainer;
            ViewData["ShowEnergy"] = showEnergy;
            if (!showPokemon)
                currentSetCardList = currentSetCardList.FindAll(x => x.SuperType != "Pokémon");
            if (!showTrainer)
                currentSetCardList = currentSetCardList.FindAll(x => x.SuperType != "Trainer");
            if (!showEnergy)
                currentSetCardList = currentSetCardList.FindAll(x => x.SuperType != "Energy");

            //sort card list 
            currSort = GetSortOrder(sortOrder);
            ViewData["CurrSort"] = currSort.ToString();
            List<PokemonCard> sortedCardList =
                currSort == SortOption.Name ? currentSetCardList.OrderBy(card => card.Name).ToList() :
                currSort == SortOption.PokedexNumber ? currentSetCardList.OrderBy(card => card.NationalPokedexNumber).ToList() :
                //default to setOrder 
                currentSetCardList.OrderBy(card => Int32.Parse(card.Number)).ToList();

            


            ViewData["CardList"] = sortedCardList;

            return View();
        }

        public IActionResult CardView(string id)
        {
            var result = Card.Get(new Dictionary<string, string>() { { "id", id } });
            currCard = result.Cards.Count >= 1 ? result.Cards.First() : null;
            ViewData["CurrCard"] = currCard;
            return View();
        }



        public void ViewSet()
        {
            return;
        }

        private SortOption GetSortOrder(string sortOrder)
        {
            switch(sortOrder)
            {
                case "Name":
                    return SortOption.Name;
                case "PokedexNumber":
                    return SortOption.PokedexNumber;
                case "Type":
                    return SortOption.Type;
                case "Rarity":
                    return SortOption.Rarity;
                default:
                    return SortOption.SetNumber;
                    
            }
        }

        private string GetTypeFilters()
        {
            return (showPokemon ? "pokemon" : string.Empty)
                + (showTrainer ? "trainer" : string.Empty)
                + (showEnergy ? "energy" : string.Empty);
        }
    }
}


public enum SortOption
{
    SetNumber,
    Name,
    PokedexNumber,
    Type, //not implemented 
    Rarity //not implemented
}


