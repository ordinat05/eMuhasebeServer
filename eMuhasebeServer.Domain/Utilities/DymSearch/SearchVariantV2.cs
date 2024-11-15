using eMuhasebeServer.Domain.Entities.VmModel.SearchVariant;
using System.Text.RegularExpressions;

namespace eMuhasebeServer.Domain.Utilities.DymSearch
{
    public static class SearchVariantV2
    {
        #region İstenilen Datalar
        //List
        //public class SearchVariantDataDto
        //{
        //    public int Id { get; set; }
        //    public string AranacakKelimeler { get; set; }
        //    public string Eslesme { get; set; } = null;
        //    public int? MatchNumber { get; set; }
        //    public List<int>? aranacakKelimeArrayId { get; set; } = new List<int>();
        //}

        //string searchString
        //int? resultMatchNumber

        //Return 
        //public class SearchFilterVmModel
        //{
        //    public List<SearchVariantDataDto>? SearchVariantDataDto { get; set; }
        //    public List<FiltrelenenSonuclarDto>? FiltrelenenSonuclarDto { get; set; }
        //}


        //public class SearchVariantDataDto
        //{
        //    public int Id { get; set; }
        //    public string AranacakKelimeler { get; set; }
        //    public string Eslesme { get; set; } = null;
        //    public int? MatchNumber { get; set; }
        //    public List<int>? aranacakKelimeArrayId { get; set; } = new List<int>();
        //}


        //public class FiltrelenenSonuclarDto
        //{
        //    public int Id { get; set; }
        //    public string AranacakKelime { get; set; } = string.Empty;
        //    public int? AranacakKelimeArrayId { get; set; }
        //    public int? FiltrelenenArrayId { get; set; } = null;
        //}

        #region View
        //        @using System.Text.RegularExpressions
        //@model Proin_Business.Core.VmModels.Search.SearchFilterVmModel

        //<style>
        //</style>
        //<br />
        //<div class="container-fluid">

        //    <form asp-action="Index" asp-controller="Test" method="get">
        //        <div class="form-row">
        //            <div class="col-6">
        //                <input step = "1" min="1" max="10" type="range" class="form-control">
        //            </div>
        //        </div>
        //        <div class="form-row">
        //            <div class="col-8">
        //                <input type = "text" value="@ViewBag.search" onkeydown="textchange()" name="search" Id="search" class="form-control" placeholder="Filtrele">
        //            </div>
        //            <div class="col-2">
        //                <input type = "number" value="@ViewBag.match" min="0" max="@ViewBag.splitCount" name="match" id="match" class="form-control" placeholder="Eşleşenler">
        //            </div>
        //            <div class="col-2">
        //                <button type = "submit" class="btn btn-sm btn-outline-primary">
        //                    Gönder
        //                </button>
        //            </div>
        //        </div>
        //    </form>
        //    @{
        //        var maxMatch = Model.SearchVariantDataDto.Max(x => x.MatchNumber);
        //    }

        //    <div class="col-12" style="width:100%">
        //        <table class="table table-bordered long-text-table word-wrap-table">
        //            @for(int i = (int)maxMatch; i >= 0; i--)
        //            {
        //                <thead>
        //                    <tr>
        //                        <th colspan = "4" class="bg-success">
        //                            @i Eşleşenler
        //                        </th>
        //                    </tr>
        //                    <tr>
        //                        <th class="col-1" scope="col">Sıra</th>
        //                        <th class="col-1" scope="col">Eslesme</th>
        //                        <th class="col-1" scope="col">Yanyana</th>
        //                        <th class="col-auto" scope="col">Text</th>

        //                    </tr>
        //                </thead>
        //                <tbody>
        //                    @foreach(var item in Model.SearchVariantDataDto.Where(x => x.MatchNumber == i))
        //                    {
        //                        <tr>
        //                            <td>@item.Id</td>
        //                            <td>@item.Eslesme</td>
        //                            <td>@item.MatchNumber</td>
        //                            <td class="border-4" style="word-wrap: break-word; white-space: normal; min-width: 160px; max-width: 160px;">
        //                                @{
        //                                    var splitText = Regex.Split(item.AranacakKelimeler, @"\W+");
        //    var filterData = Model.FiltrelenenSonuclarDto.Where(x => x.Id == item.Id);
        //    int splitTextId = 0;
        //                                    foreach (var text in splitText)
        //                                    {
        //                                        var isCheck = filterData.FirstOrDefault(x => x.FiltrelenenArrayId == splitTextId);
        //                                        if (isCheck == null)
        //                                        {
        //                                            <span>@text</span>
        //}
        //                                        else
        //{
        //                                            < span class= "" style = "background-color:yellow" > @text </ span >
        //                                        }
        //                                        splitTextId++;
        //                                    }
        //                                }
        //                            </ td >
        //                        </ tr >
        //                    }
        //                </ tbody >
        //            }
        //        </ table >
        //    </ div >
        //</ div >


        //< table >
        //    < tr >
        //        < th >
        //            Id
        //        </ th >
        //        < th >
        //            ARanacak Kelime
        //        </ th >
        //        < th >
        //            Aranacak Kelime Array
        //        </th>
        //        <th>
        //            filtrelenen array ıd
        //        </th>
        //    </tr>

        //    @foreach (var asd in Model.FiltrelenenSonuclarDto)
        //{
        //        < tr >
        //            < td >
        //                @asd.Id
        //            </ td >
        //            < td >
        //                @asd.AranacakKelime
        //            </ td >
        //            < td >
        //                @asd.AranacakKelimeArrayId
        //            </ td >
        //            < td >
        //                @asd.FiltrelenenArrayId
        //            </ td >
        //        </ tr >
        //    }

        //</ table >

        //< script type = "text/javascript" >

        //    function textchange() {
        //    var inputValue = $('#search').val();
        //    //var inputValue = $(this).val();
        //    var cleanedText = cleanText(inputValue);
        //    var splitArray = splitSearchString(cleanedText);
        //    numberBoxSettings(splitArray.length);
        //}

        //function numberBoxSettings(length)
        //{
        //        $('#match').val(length);
        //        $('#match').attr('max', length);
        //}

        //function cleanText(text)
        //{
        //    var clearText = clearMultipleSpacesRegex(text);
        //    var turkishCharacter = clearTurkishCharacters(clearText);

        //    return turkishCharacter;
        //}

        //function clearMultipleSpacesRegex(val)
        //{
        //    // if (!val || /^\s*$/.test(val))
        //    //     return "";
        //    if (typeof val !== 'string' || !val.trim())
        //    {
        //        return "";
        //    }
        //    var regex = / []{ 2,}/ g;
        //    val = val.replace(regex, ' ');
        //    return val.trim();
        //}

        //function clearTurkishCharacters(val)
        //{
        //    // if (!val || /^\s*$/.test(val))
        //    //     return "";
        //    if (typeof val !== 'string' || !val.trim())
        //    {
        //        return "";
        //    }
        //    val = val.toLowerCase();
        //    val = val.replace(/ Ğ / g, 'g').replace(/ Ü / g, 'u').replace(/ Ş / g, 's').replace(/ İ / g, 'i')
        //        .replace(/ Ö / g, 'o').replace(/ Ç / g, 'c').replace(/ ğ / g, 'g').replace(/ ü / g, 'u')
        //        .replace(/ ş / g, 's').replace(/ ı / g, 'i').replace(/ ö / g, 'o').replace(/ ç / g, 'c')
        //        .replace(/ !/ g, '').replace(/\+/ g, ' ').replace(/\//g, ' ').replace(/{/g, ' ')
        //            .replace(/}/ g, ' ').replace(/\(/ g, ' ').replace(/\) / g, ' ').replace(/\[/ g, ' ')
        //            .replace(/]/ g, ' ').replace(/=/ g, ' ').replace(/\\/ g, ' ').replace(/ -/ g, ' ')
        //            .replace(/ _ / g, ' ').replace(/@@/ g, ' ').replace(/,/ g, ' ').replace(/\./ g, ' ')
        //            .replace(/:/ g, ' ').replace(/</ g, ' ').replace(/>/ g, ' ').replace(/ '/g, '');
        //        return val;
        //    }

        //    function splitSearchString(searchString)
        //{
        //    var splitArray = searchString.toLowerCase().split(/\W +/);
        //    return splitArray;
        //}
        //</ script >
        #endregion

        #endregion
        public static SearchFilterVmModel Search(List<SearchVariantDataDto>? data, string searchString, int? resultMatchNumber)
        {
            SearchFilterVmModel vmModelOld = new SearchFilterVmModel();

            //if (data == null)


            vmModelOld.SearchVariantDataDto = data ?? new List<SearchVariantDataDto>();

            SearchFilterVmModel vmNewModel = new SearchFilterVmModel();

            vmNewModel.SearchVariantDataDto = new List<SearchVariantDataDto>();
            vmNewModel.FiltrelenenSonuclarDto = new List<FiltrelenenSonuclarDto>();

            var splitSearchString = Regex.Split(searchString.ToLower(), @"\W+");

            vmNewModel.ArananCumleKelimeSayisiSplit = splitSearchString.Count();

            int splitIndis = 0;
            foreach (var _splitSingleString in splitSearchString)
            {
                AddData(FilteredFilterSearch.Search(vmModelOld, _splitSingleString, splitIndis));
                splitIndis++;
            }


            void AddData(SearchFilterVmModel vmData)
            {
                foreach (var filtersingle in vmData.FiltrelenenSonuclarDto)
                {
                    vmNewModel.FiltrelenenSonuclarDto.Add(filtersingle);
                }

                foreach (var searchSingle in vmData.SearchVariantDataDto)
                {
                    var duplicate = vmNewModel.SearchVariantDataDto.FirstOrDefault(x => x.Id == searchSingle.Id);
                    //Arananan cümle varsa count güncelle yoksa, ekle
                    //ListId ArrayId geliyor.
                    if (duplicate != null)
                    {

                        //duplicate.MatchNumber = vmNewModel.FiltrelenenSonuclarDto.Where(x => x.Id == duplicate.Id).Count();
                        duplicate.MatchNumber = ArrayArdisikUzunlugu(duplicate.aranacakKelimeArrayId);
                    }
                    else
                    {
                        //searchSingle.MatchNumber = vmNewModel.FiltrelenenSonuclarDto.Where(x => x.Id == searchSingle.Id).Count();
                        searchSingle.MatchNumber = ArrayArdisikUzunlugu(searchSingle.aranacakKelimeArrayId);
                        vmNewModel.SearchVariantDataDto.Add(searchSingle);
                    }
                }

            }
            if (resultMatchNumber != null)
            {
                vmNewModel.SearchVariantDataDto = vmNewModel.SearchVariantDataDto.Where(x => x.MatchNumber <= resultMatchNumber).ToList();
                return vmNewModel;
            }




            return vmNewModel;
        }


        public static int ArrayArdisikUzunlugu(List<int>? aranacakKelimeArrayId)
        {

            if (aranacakKelimeArrayId == null || aranacakKelimeArrayId.Count() == 0)
                return 0;
            //List<int>? aranacakKelimeArrayId = new List<int> { 0, 1, 2, 3, 15, 55, 69 };


            int maxLength = 1;
            int currentLength = 1;

            if (aranacakKelimeArrayId != null)
            {
                aranacakKelimeArrayId.Sort();

                for (int i = 1; i < aranacakKelimeArrayId.Count; i++)
                {
                    if (aranacakKelimeArrayId[i] == aranacakKelimeArrayId[i - 1] + 1)
                    {
                        currentLength++;
                        maxLength = Math.Max(maxLength, currentLength);
                    }
                    else if (aranacakKelimeArrayId[i] != aranacakKelimeArrayId[i - 1])
                    {
                        currentLength = 1;
                    }
                }
            }



            return maxLength;
        }
    }
}