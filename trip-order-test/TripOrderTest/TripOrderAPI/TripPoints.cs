using System;
using System.Collections.Generic;
using System.Text;

namespace org.nikshalman.example.TripOrderAPI
{

    public enum BaggageAction
    {
        DROP, //drop baggage
        TRANSFER, //baggage will be transferred, no user action needed
        PICKUP //user should pick up baggage at this point
    }


    public enum BaggageType
    {
        HANDLED, //baggage handled by travel company (usually by aircraft company)
        NOT_HANDLED //not handled - usually by bus and mostly by train company
    }


    public class BaggageMdl
    {
        public BaggageType pType { get; set; }

        public BaggageAction pAction { get; set; }

        public String pTickerCounter { get; set; }


        public BaggageCtr pController { get; set; }
    }


    public class BaggageCtr
    {
        protected BaggageMdl _mdl;

        public BaggageMdl create(BaggageType aType, BaggageAction aAction, String aTickerCounter)
        {
            _mdl = new BaggageMdl();
            _mdl.pController = this;

            _mdl.pType = aType;
            _mdl.pAction = aAction;
            _mdl.pTickerCounter = aTickerCounter;

            return _mdl;
        }
    }


    public enum TripPointType
    {
        DEPARTURE,
        ARRIVAL
    }



    public class TripPointMdl
    {
        public Guid pGUID { get; set; }

        public TripPointType pType { get; set; }

        public String pName { get; set; } //name of the point: airport of Gerona, bus station of Barcelona etc.

        public BaggageMdl pBaggage { get; set; }


        public TripPointCtr pController { get; set; }

    }


    public class TripPointCtr
    {
        protected TripPointMdl _mdl;

        public TripPointMdl create(String aName, TripPointType aType, BaggageMdl aBaggage)
        {
            _mdl = new TripPointMdl();
            _mdl.pController = this;

            _mdl.pGUID = Guid.NewGuid();
            _mdl.pName = aName;
            _mdl.pType = aType;

            _mdl.pBaggage = aBaggage;

            return _mdl;
        }
    }



    public class TripPointsMdl
    {

        public Dictionary<Guid, TripPointMdl> pTripPoints { get; set; }

        public TripPointsCtr pController { get; set; }
    }



    public class TripPointsCtr
    {

        protected TripPointsMdl _mdl;
        

        public TripPointsMdl create()
        {
            _mdl = new TripPointsMdl();
            _mdl.pController = this;

            _mdl.pTripPoints = new Dictionary<Guid, TripPointMdl>();

            return _mdl;
        }


        public void addPoint(TripPointMdl aPoint)
        {
            _mdl.pTripPoints.Add(aPoint.pGUID, aPoint);
        }


        public TripPointMdl findPointByName(String aPointName)
        {
            foreach(TripPointMdl point in _mdl.pTripPoints.Values)
            {
                if (point.pName == aPointName)
                    return point;
            }

            return null;
        }


        public TripPointMdl findPointByGuid(Guid aGuid)
        {

            return _mdl.pTripPoints[aGuid];
        }
    }

}
