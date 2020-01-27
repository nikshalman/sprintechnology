using org.nikshalman.example.TripOrderAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TripOrderTest
{
    public class BoardingCardShuffleMdl
    {
        public List<BoardingCardMdl> pBoardingCards { get; set; } //inserted boarding cards
        public List<BoardingCardMdl> pShuffledCards { get; set; } //cards after shuffle

               

        public BoardingCardShuffleCtr pController { get; set; }
    }


    public class BoardingCardShuffleCtr
    {

        protected BoardingCardShuffleMdl _mdl;
        private TripOrderMdl _tripOrder;


        public BoardingCardShuffleMdl create(TripOrderMdl aTripOrder)
        {
            _mdl = new BoardingCardShuffleMdl();
            _mdl.pController = this;

            _mdl.pBoardingCards = new List<BoardingCardMdl>();
            _mdl.pShuffledCards = new List<BoardingCardMdl>();

            _tripOrder = aTripOrder;

            return _mdl;
        }


        //public void addBoardingCard()
        //{
        //    //1. define baggage
        //    BaggageMdl baggage = new BaggageCtr().create(BaggageType.NOT_HANDLED, BaggageAction.TRANSFER, null);

        //    //2. define start point
        //    TripPointMdl startPoint = new TripPointCtr().create("Madrid", TripPointType.DEPARTURE, baggage);

        //    //3. define end point
        //    TripPointMdl endPoint = new TripPointCtr().create("Barcelona", TripPointType.ARRIVAL, baggage);

        //    //4. define boarding card
        //    BoardingCardMdl card = new BoardingCardCtr().create(VehicleType.TRAIN, "78A", "45B", startPoint, endPoint);
        //}


        public void addBoardingCardNoBaggage(String aStartPointName, String aEndPointName, VehicleType aVehicleType, 
            String aVehicleNumber, String aSitNumber, String aDesc)
        {
            //1. define baggage
            BaggageMdl baggage = new BaggageCtr().create(BaggageType.NOT_HANDLED, BaggageAction.TRANSFER, null);

            //2. define start point
            TripPointMdl startPoint = new TripPointCtr().create(aStartPointName, TripPointType.DEPARTURE, baggage);

            //3. define end point
            TripPointMdl endPoint = new TripPointCtr().create(aEndPointName, TripPointType.ARRIVAL, baggage);

            //4. define boarding card
            BoardingCardMdl card = new BoardingCardCtr().create(aVehicleType, aVehicleNumber, aSitNumber, startPoint, endPoint, aDesc);

            _mdl.pBoardingCards.Add(card);
        }


        public void addBoardingCardWithBaggage(String aStartPointName, BaggageMdl aStartBaggage, String aEndPointName, BaggageMdl aEndBaggage, 
            VehicleType aVehicleType, String aVehicleNumber, String aSitNumber, String aDesc)
        {
            //2. define start point
            TripPointMdl startPoint = new TripPointCtr().create(aStartPointName, TripPointType.DEPARTURE, aStartBaggage);

            //3. define end point
            TripPointMdl endPoint = new TripPointCtr().create(aEndPointName, TripPointType.ARRIVAL, aEndBaggage);

            //4. define boarding card
            BoardingCardMdl card = new BoardingCardCtr().create(aVehicleType, aVehicleNumber, aSitNumber, startPoint, endPoint, aDesc);

            _mdl.pBoardingCards.Add(card);
        }


        public void shuffle()
        {
            int len = _mdl.pBoardingCards.Count;

            var random = new Random();
            var intArray = Enumerable.Range(0, len).OrderBy(t => random.Next()).ToArray();

            int lastRnd = -1;
            for (int x = 0; x < len; x++)
            {
                int ind = intArray[x];

                _mdl.pShuffledCards.Add(_mdl.pBoardingCards[ind]);
                lastRnd = ind;
            } 
        }


        public void order()
        {

            foreach (BoardingCardMdl card in _mdl.pShuffledCards)
            {
                _tripOrder.pController.addBoardingCard(card);
            }

            if(!_tripOrder.pController.tryOrder())
            {
                Console.WriteLine("ERROR: NOT POSSIBLE TO ORDER THESE CARDS");
            }
            else
            {
                Console.WriteLine("CARDS WAS SUCCESSFULLY ORDERED!");
            }
        }


        public void logShuffledCars()
        {
            Console.WriteLine("\n\nDISPLAY SHUFFLED CARDS:\n");

            foreach (BoardingCardMdl card in _mdl.pShuffledCards)
            {
                //Console.WriteLine("CARD: start: " + card.pStartPoint.pName + " end: " + card.pEndPoint.pName);
                Console.WriteLine(card.pDescription);
            }

            Console.WriteLine("\n");
        }


        public void logOrderedCars()
        {
            Console.WriteLine("\n\nDISPLAY ORDERED CARDS:\n");

            foreach (BoardingCardMdl card in _tripOrder.pBoardingCards)
            {
                Console.WriteLine(card.pDescription);
            }

            Console.WriteLine("\n");
        }

    }



}
