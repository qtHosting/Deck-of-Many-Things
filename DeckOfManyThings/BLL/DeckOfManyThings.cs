using System;
using DeckOfManyThings.DAL;

namespace DeckOfManyThings.BLL
{
    class DeckOfManyThings
    {
        DeckOfManyThingsData data;

        /// <summary>
        /// Instantiates the Deck of Many Things
        /// </summary>
        public DeckOfManyThings()
        {
            data = new DeckOfManyThingsData();
        }

        /// <summary>
        /// Draws a card from the deck
        /// </summary>
        /// <returns>Returns a card and its details from the deck of many things.</returns>
        public Card DrawACard()
        {
            Card card = new Card();

            int cardsInDeck = data.GetTheNumberOfCardsFromTheDeck();

            int pickedCard = PickACard(cardsInDeck);

            card = data.GetCardDetails(pickedCard);

            return card;
        }

        /// <summary>
        /// Picks a random card from the deck
        /// </summary>
        /// <param name="intTotalCards">The total number of cards in the deck</param>
        /// <returns>Returns an integer representing a card in the deck.</returns>
        private int PickACard(int intTotalCards)
        {
            Random rand = new Random();

            int num = rand.Next(0, intTotalCards);

            return num;
        }
    }
}
