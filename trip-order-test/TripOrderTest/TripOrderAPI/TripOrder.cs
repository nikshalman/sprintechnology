using System;
using System.Collections.Generic;
using System.Text;

namespace org.nikshalman.example.TripOrderAPI
{
    public class TripOrderMdl
    {

        public List<BoardingCardMdl> pBoardingCards { get; set; } //ordered boarding cards

        public TripOrderCtr pController { get; set; }
    }


    public class TripOrderCtr
    {
        protected TripOrderMdl _mdl;

        private List<BoardingCardMdl> _unorderedCards;


        public TripOrderMdl create()
        {
            _mdl = new TripOrderMdl();
            _mdl.pController = this;
            _mdl.pBoardingCards = new List<BoardingCardMdl>();

            _unorderedCards = new List<BoardingCardMdl>();

            return _mdl;
        }


        public void addBoardingCard(BoardingCardMdl aCard)
        {
            _unorderedCards.Add(aCard);
        }


        public bool tryOrder()
        {
            //1. at this step move first card to ordered list
            moveFirst();

            //2. now try to move all others
            moveOthers();

            //3. check if all cards was moved to ordered list
            if (_unorderedCards.Count == 0)
                return true;

            return false;
        }


        private void moveFirst()
        {
            BoardingCardMdl card = _unorderedCards[0];
            _unorderedCards.RemoveAt(0);

            _mdl.pBoardingCards.Add(card);
        }


        private void moveOthers()
        {
            //when isNotFinished will be false, after all cards passed and no one moved to ordered map
            //then finish this loop
            bool isFinished = false;

            while (!isFinished)
            {
                int lenStart = _unorderedCards.Count;

                for (int x = 0; x < lenStart; x++)
                {
                    BoardingCardMdl card = _unorderedCards[0];
                    _unorderedCards.RemoveAt(0);

                    bool placed = tryPlaceInOrderedList(card);
                    if (!placed)
                        _unorderedCards.Add(card);
                }

                int lenEnd = _unorderedCards.Count;

                if (lenStart == lenEnd)
                    isFinished = true;
            }
        }


        /*
         * boarding cards should be ordered like that:
         * CARD0: start: Barselona, end: Gerona
         * CARD1: start: Gerona, end: Paris
         * CARD2: start: Paris, end London
         * 
         * Then: 
         * a. if start of current card match end of any placed one: place current card AFTER that placed one
         * b. if end of current card match start of placed one: place current card BEFORE that placed one
         * c. if not match, then leave in unordered list
         */
        private bool tryPlaceInOrderedList(BoardingCardMdl aCard)
        {
            //search for matched start or end
            foreach(BoardingCardMdl placedCard in _mdl.pBoardingCards)
            {
                int ind = _mdl.pBoardingCards.IndexOf(placedCard);

                if (placedCard.pStartPoint.pName == aCard.pEndPoint.pName)
                {
                    //place before
                    _mdl.pBoardingCards.Insert(ind, aCard);
                    return true;
                }
                else if(placedCard.pEndPoint.pName == aCard.pStartPoint.pName)
                {
                    //place after
                    ind++;

                    if (ind < _mdl.pBoardingCards.Count)
                        _mdl.pBoardingCards.Insert(ind, aCard);
                    else
                        _mdl.pBoardingCards.Add(aCard);

                    return true;
                }
            }
            

            return false;
        }
    }
}
