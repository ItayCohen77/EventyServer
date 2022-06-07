using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventyServerBL.Models
{
    public partial class PlaceObj
    {
        public Place placeObj { get; set; }
        public Apartment apartmentObj { get; set; }
        public PrivateHouse privateHouseObj { get; set; }
        public Hall hallObj { get; set; }
        public HouseBackyard houseBackyardObj { get; set; }
    }   
}
