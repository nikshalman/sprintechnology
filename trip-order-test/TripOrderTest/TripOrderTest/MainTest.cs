using org.nikshalman.example.TripOrderAPI;
using System;


namespace TripOrderTest
{
    class MainTest
    {
        static void Main(string[] args)
        {
            Console.WriteLine(
                "THIS IS A TEST for TripOrderAPI\n"
                + "It will insert random boarding cards and TripOrderAPI will try to order them in one trip."
                );


            TripOrderMdl tripOrder = new TripOrderCtr().create();
            BoardingCardShuffleMdl shuffle = new BoardingCardShuffleCtr().create(tripOrder);

            //------------------------
            shuffle.pController.addBoardingCardNoBaggage("Madrid", "Barselona", VehicleType.TRAIN, "78A", "45B",
                "Take train 78A from Madrid to Barcelona. Sit in seat 45B.");

            //------------------------
            shuffle.pController.addBoardingCardNoBaggage("Barselona", "Gerona", VehicleType.AIRPORT_BUS, null, null,
                "Take the airport bus from Barcelona to Gerona Airport. No seat assignment.");

            //------------------------
            BaggageMdl startBag = new BaggageCtr().create(BaggageType.HANDLED, BaggageAction.DROP, "344");
            BaggageMdl endBag = new BaggageCtr().create(BaggageType.HANDLED, BaggageAction.TRANSFER, null);

            shuffle.pController.addBoardingCardWithBaggage(
                "Gerona", startBag, "Stockholm", endBag, VehicleType.AIRPLAIN, "SK455", "3A",
                "From Gerona Airport, take flight SK455 to Stockholm. Gate 45B, seat 3A. Baggage drop at ticket counter 344.");

            //------------------------
            startBag = new BaggageCtr().create(BaggageType.HANDLED, BaggageAction.TRANSFER, null);
            endBag = new BaggageCtr().create(BaggageType.HANDLED, BaggageAction.PICKUP, null);

            shuffle.pController.addBoardingCardWithBaggage(
                "Stockholm", startBag, "New York JFK", endBag, VehicleType.AIRPLAIN, "SK22", "7B",
                "From Stockholm, take flight SK22 to New York JFK. Gate 22, seat 7B. Baggage will we automatically transferred from your last leg.");


            //------------------------
            Console.WriteLine("Shuffle these boarding cards");
            shuffle.pController.shuffle();
            shuffle.pController.logShuffledCars();

            //------------------------
            Console.WriteLine("Try to ORDER these boarding cards");

            shuffle.pController.order();


            //------------------------
            shuffle.pController.logOrderedCars();
        }
    }
}
