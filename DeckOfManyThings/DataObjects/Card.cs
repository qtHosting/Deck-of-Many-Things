using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckOfManyThings
{
    class Card
    {
        private int intCardID;
        /// <summary>
        /// The Card's ID Number
        /// </summary>
        public int IntCardID
        {
            get
            {
                return intCardID;
            }
            set
            {
                if(value >= 0)
                {
                    this.intCardID = value;
                }
                else
                {
                    this.intCardID = 0;
                }
            }
        }
        /// <summary>
        /// The Card's Name
        /// </summary>
        public string strCardName { get; set; }
        /// <summary>
        /// The Card's full description
        /// </summary>
        public string strCardDescription { get; set; }
        /// <summary>
        /// The Card's Summary
        /// </summary>
        public string strCardSummary { get; set; }
        /// <summary>
        /// The Card's Art Location
        /// </summary>
        public string StrCardArtLocation { get; set; }

        /// <summary>
        /// Creates an object that will allow the card to be used.
        /// </summary>
        public Card()
        {

        }
    }
}
