using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FoodShare.Models
{
    public class Cities
    {
        public ObservableCollection<City> MainCities { get; set; }

        public Cities()
        {
            MainCities = new ObservableCollection<City>();

            MainCities.Add(new City { description = "AMBALANGODA" });
            MainCities.Add(new City { description = "AMPARA" });
            MainCities.Add(new City { description = "ANURADHAPURA" });
            MainCities.Add(new City { description = "BADULLA" });
            MainCities.Add(new City { description = "BALANGODA" });
            MainCities.Add(new City { description = "BANDARAWELA" });
            MainCities.Add(new City { description = "BATTICALOA" });
            MainCities.Add(new City { description = "BERUWALA" });
            MainCities.Add(new City { description = "CHAVAKACHERI" });
            MainCities.Add(new City { description = "CHILAW" });
            MainCities.Add(new City { description = "COLOMBO" });
            MainCities.Add(new City { description = "DAMBULLA" });
            MainCities.Add(new City { description = "DEHIWALA" });
            MainCities.Add(new City { description = "MOUNT LAVINIA" });
            MainCities.Add(new City { description = "ERAVUR" });
            MainCities.Add(new City { description = "GALLE" });
            MainCities.Add(new City { description = "GAMPAHA" });
            MainCities.Add(new City { description = "GAMPOLA" });
            MainCities.Add(new City { description = "HAMBANTOTA" });
            MainCities.Add(new City { description = "HAPUTALE" });
            MainCities.Add(new City { description = "HARISPATTUWA" });
            MainCities.Add(new City { description = "HATTON" });
            MainCities.Add(new City { description = "HORANA" });
            MainCities.Add(new City { description = "JA-ELA" });
            MainCities.Add(new City { description = "JAFFNA" });
            MainCities.Add(new City { description = "KADUGANNAWA" });
            MainCities.Add(new City { description = "KALMUNAI" });
            MainCities.Add(new City { description = "KALUTARA" });
            MainCities.Add(new City { description = "KANDY" });
            MainCities.Add(new City { description = "KATTANKUDY" });
            MainCities.Add(new City { description = "KATUNAYAKE" });
            MainCities.Add(new City { description = "KEGALLE" });
            MainCities.Add(new City { description = "KELANIYA" });
            MainCities.Add(new City { description = "KOLONNAWA" });
            MainCities.Add(new City { description = "KULIYAPITIYA" });
            MainCities.Add(new City { description = "KURUNEGALA" });
            MainCities.Add(new City { description = "MANNAR" });
            MainCities.Add(new City { description = "MATALE" });
            MainCities.Add(new City { description = "MATARA" });
            MainCities.Add(new City { description = "MINUWANGODA" });
            MainCities.Add(new City { description = "MONERAGALA" });
            MainCities.Add(new City { description = "MORATUWA" });
            MainCities.Add(new City { description = "NAWALAPITIYA" });
            MainCities.Add(new City { description = "NEGOMBO" });
            MainCities.Add(new City { description = "NUWARA ELIYA" });
            MainCities.Add(new City { description = "PANADURA" });
            MainCities.Add(new City { description = "PELIYAGODA" });
            MainCities.Add(new City { description = "POINT PEDRO" });
            MainCities.Add(new City { description = "PUTTALAM" });
            MainCities.Add(new City { description = "AVISSAWELLA" });
            MainCities.Add(new City { description = "SIGIRIYA" });
            MainCities.Add(new City { description = "SRI JAYAWARDENAPURA KOTTE" });
            MainCities.Add(new City { description = "TALAWAKELE" });
            MainCities.Add(new City { description = "TANGALLE" });
            MainCities.Add(new City { description = "TRINCOMALEE" });
            MainCities.Add(new City { description = "VALVETTITHURAI" });
            MainCities.Add(new City { description = "VAVUNIYA" });
            MainCities.Add(new City { description = "WATTALA" });
            MainCities.Add(new City { description = "WATTEGAMA" });
            MainCities.Add(new City { description = "WELIGAMA" });
        }
        public class City
        {
            public string description { get; set; }
        }
    }
}
