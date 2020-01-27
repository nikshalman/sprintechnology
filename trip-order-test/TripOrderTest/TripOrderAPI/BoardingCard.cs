using System;
using System.Collections.Generic;
using System.Text;

namespace org.nikshalman.example.TripOrderAPI
{

    public enum VehicleType
    {
        TRAIN,
        AIRPORT_BUS,
        AIRPLAIN
    }


    public class BoardingCardMdl
    {
        public VehicleType pVehicleType { get; set; }

        public String pVehicleNumber { get; set; } //vehicle number

        public String pSeatNumber { get; set; } //sit number
        
        public TripPointMdl pStartPoint { get; set; } //starting trip point

        public TripPointMdl pEndPoint { get; set; } //destination trip point

        public String pDescription { get; set; } //boarding card human unformatted text


        public BoardingCardCtr pController { get; set; }
    }


    public class BoardingCardCtr
    {
        protected BoardingCardMdl _mdl;

        public BoardingCardMdl create(VehicleType aVehicleType, String aVehicleNumber, String aSitNumber, 
            TripPointMdl aStartPoint, TripPointMdl aEndPoint, String aDesc)
        {
            _mdl = new BoardingCardMdl();
            _mdl.pController = this;

            _mdl.pVehicleType = aVehicleType;
            _mdl.pVehicleNumber = aVehicleNumber;
            _mdl.pSeatNumber = aSitNumber;
            _mdl.pStartPoint = aStartPoint;
            _mdl.pEndPoint = aEndPoint;

            _mdl.pDescription = aDesc;

            return _mdl;
        }

    }
}
