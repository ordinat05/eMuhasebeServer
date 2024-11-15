using eMuhasebeServer.Domain.Entities.VmModel.SearchVariant;
using eMuhasebeServer.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMuhasebeServer.Domain.Utilities.DymSearch
{
    public static class SearchFilterProperty
    {


        //İstenilen Datalar
        //{
        //    // Adı 
        //    var Adı = aranacak Kelime
        //    // içeride kullanılacak isim de "Adı" olarak değiştirilecek
        //    Soyadı :
        //    Doğum Tarihi : 
        //}

        //    Sunulacak Datalar
        //{
        //    Eşleşme Adeti :
        //    Satır No : 
        //    List Id :
        //    Array Id : 
        //    Bulunan Kelime : 
        //    ArrayMax: 

        //  }



        /*
         *  data listesi içerisinde Id ve AranacakKelimeler listesi gönderilecek(içerisinde aranacak)
         * data = {int Id ; string = AranacakKelimeler }
         *
         */
        //Bu Metodun çalışması için zorunlu istenilenler :
        // 1- Search Cümle yada Kelime (Array olarak gönderilmesine gerek yok.)
        // 2- Arama Kaynağı (List olarak gönderilmeli.)
        // 3- Kaç Eşleşme olanların getirilmesi isteniyor.
        // Zorunlu olmayanlar ayar değerleri - Kaç Harfli Kelimede , Kaç harf farklı ise sonuç döndürsün.
        //Bu Metodun bize geri döndürdükleri : 
        public static SearchFilterVmModel Search(List<SearchVariantDataDto> data, EntityTableEnum? tableEnum, string Search, int? resultMatchNumber)
        {
            var response = SearchVariantV2.Search(data, Search, resultMatchNumber);
            return response;
        }




    }
}
