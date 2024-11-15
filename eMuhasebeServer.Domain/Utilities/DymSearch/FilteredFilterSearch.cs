using System.Text.RegularExpressions;
using eMuhasebeServer.Domain.Entities.VmModel.SearchVariant;

namespace eMuhasebeServer.Domain.Utilities.DymSearch;


public static class FilteredFilterSearch
{
    public static SearchFilterVmModel Search(SearchFilterVmModel vmModel, string searchSplitString, int? searchSplitRow)
    {
        List<SearchVariantDataDto> newData = new List<SearchVariantDataDto>();
        List<FiltrelenenSonuclarDto> newFilterData = new List<FiltrelenenSonuclarDto>();

        var DataList = vmModel.SearchVariantDataDto.ToList();

        foreach (var data in DataList)
        {
            string[] dataSplit = Regex.Split(data.AranacakKelimeler?.ToLower() ?? "", @"\W+");
            int arrayRow = 0;
            foreach (var splitSingleString in dataSplit)
            {
                var _cleanSplitSingleString = CleanText(splitSingleString);
                var _cleanSearchSplitString = CleanText(searchSplitString);

                int instance = ComputeLevenshteinDistance(_cleanSplitSingleString, _cleanSearchSplitString);

                int splitStringLength = _cleanSplitSingleString.Length;

                if (splitStringLength >= 0 && splitStringLength <= 3)
                {
                    //eslesme = "0,1 Fark Eşleşme";

                    if (instance == 0)
                    {
                        data.Eslesme = "Tam Eşleşme -3,0" + " ArrayId:" + searchSplitRow + " ArananKelimeId: " + arrayRow;


                        //data.aranacakKelimeArrayId = searchSplitRow;
                        data.aranacakKelimeArrayId?.Add(arrayRow);
                        AddNewData(data);

                        //// Filtreye eklenecek sonuçları oluşturup listeye ekleme
                        AddNewFilteredRow(data.Id, searchSplitRow, searchSplitString, arrayRow);

                    }
                    else if (instance == 1)
                    {
                        data.Eslesme = "1 harf farklı 3,1" + " ArrayId:" + searchSplitRow + " ArananKelimeId: " + arrayRow;
                        //data.aranacakKelimeArrayId = searchSplitRow;
                        data.aranacakKelimeArrayId?.Add(arrayRow);

                        AddNewData(data);

                        AddNewFilteredRow(data.Id, searchSplitRow, searchSplitString, arrayRow);

                    }

                }
                else if (splitStringLength >= 4 && splitStringLength <= 6)
                {
                    //eslesme = "0,1,2 Fark Eşleşme";

                    if (instance == 0)
                    {
                        data.Eslesme = "Tam Eşleşme 4-6,0" + " ArrayId:" + searchSplitRow + " ArananKelimeId: " + arrayRow;
                        //data.aranacakKelimeArrayId = searchSplitRow;
                        data.aranacakKelimeArrayId?.Add(arrayRow);



                        AddNewData(data);

                        AddNewFilteredRow(data.Id, searchSplitRow, searchSplitString, arrayRow);

                    }
                    else if (instance == 1)
                    {
                        data.Eslesme = "1 harf farklı 4-6,1" + " ArrayId:" + searchSplitRow + " ArananKelimeId: " + arrayRow;
                        //data.aranacakKelimeArrayId = searchSplitRow;
                        data.aranacakKelimeArrayId?.Add(arrayRow);

                        AddNewData(data);

                        AddNewFilteredRow(data.Id, searchSplitRow, searchSplitString, arrayRow);

                    }

                    else if (instance == 2)
                    {
                        data.Eslesme = "2 harf farklı 4-6,2" + " ArrayId:" + searchSplitRow + " ArananKelimeId: " + arrayRow;
                        //data.aranacakKelimeArrayId = searchSplitRow;
                        data.aranacakKelimeArrayId?.Add(arrayRow);

                        AddNewData(data);

                        AddNewFilteredRow(data.Id, searchSplitRow, searchSplitString, arrayRow);

                    }
                }
                else
                {
                    //eslesme = "0,1,2,3 Fark Eşleşme";

                    if (instance == 0)
                    {
                        data.Eslesme = "Tam Eşleşme 7,0" + " ArrayId:" + searchSplitRow + " ArananKelimeId: " + arrayRow;
                        //data.aranacakKelimeArrayId = searchSplitRow;
                        data.aranacakKelimeArrayId?.Add(arrayRow);

                        AddNewData(data);

                        AddNewFilteredRow(data.Id, searchSplitRow, searchSplitString, arrayRow);

                    }
                    else if (instance == 1)
                    {
                        data.Eslesme = "1 harf farklı 7,1" + " ArrayId:" + searchSplitRow + " ArananKelimeId: " + arrayRow;
                        //data.aranacakKelimeArrayId = searchSplitRow;
                        data.aranacakKelimeArrayId?.Add(arrayRow);

                        AddNewData(data);

                        AddNewFilteredRow(data.Id, searchSplitRow, searchSplitString, arrayRow);

                    }
                    else if (instance == 2)
                    {
                        data.Eslesme = "2 harf farklı 7,2" + " ArrayId:" + searchSplitRow + " ArananKelimeId: " + arrayRow;
                        //data.aranacakKelimeArrayId = searchSplitRow;
                        data.aranacakKelimeArrayId?.Add(arrayRow);
                        AddNewData(data);

                        AddNewFilteredRow(data.Id, searchSplitRow, searchSplitString, arrayRow);
                    }
                    else if (instance == 3)
                    {
                        data.Eslesme = "3 harf farklı 7,3" + " ArrayId:" + searchSplitRow + " ArananKelimeId: " + arrayRow;
                        //data.aranacakKelimeArrayId = searchSplitRow;
                        data.aranacakKelimeArrayId?.Add(arrayRow);
                        AddNewData(data);

                        AddNewFilteredRow(data.Id, searchSplitRow, searchSplitString, arrayRow);
                    }
                }
                arrayRow++;


            }
        }

        vmModel.FiltrelenenSonuclarDto = newFilterData;
        vmModel.SearchVariantDataDto = newData;


        void AddNewData(SearchVariantDataDto item)
        {
            var isDuplicate = newData.FirstOrDefault(x => x.Id == item.Id);
            if (isDuplicate == null)
                newData.Add(item);
        }

        void AddNewFilteredRow(int Id, int? AranacakKelimeArrayId, string? AranacakKelime, int? FiltrelenenArrayId)
        {
            var AddFilteredRow = new FiltrelenenSonuclarDto
            {
                Id = Id,
                AranacakKelimeArrayId = AranacakKelimeArrayId,
                AranacakKelime = AranacakKelime,
                FiltrelenenArrayId = FiltrelenenArrayId,
            };
            newFilterData.Add(AddFilteredRow);
        }

        return vmModel;
    }


    #region Regex
    static string CleanText(string text)
    {
        //return new string(text.Where(char.IsLetter).ToArray());

        //TODO
        //İşlem basamağında boşlukları trimle ve noktayı bpşluğa çevirme.
        //Yer değiştirilmesi gerekebilir.

        //SearchVarianV2 => Search => 
        //var splitSearchString = Regex.Split(searchString.ToLower(), @"\W+");
        //metounda boşluk olan diziler kaldırılabilir.


        //YÖNTEM 1 Önce çoklu boşluk karakterleri temizle, sonra bazı karakterleri boşluğa çevir.
        //var clearText = text.ClearMultipleSpacesRegex();
        //var turkishCharacter = clearText.ClearTurkishCharacters();

        //return turkishCharacter;

        //YÖNTEM 2 Önce harfleri temizle ve boşluğa çevir. YENİ
        var turkishCharacter = text.ClearTurkishCharacters();
        var clearText = turkishCharacter.ClearMultipleSpacesRegex();

        return clearText;

    }

    public static string ClearMultipleSpacesRegex(this string val)
    {
        if (string.IsNullOrWhiteSpace(val))
            return "";
        const RegexOptions options = RegexOptions.None;
        var regex = new Regex(@"[ ]{2,}", options);
        val = regex.Replace(val, @" ");
        return val.Trim();
    }
    public static string ClearTurkishCharacters(this string val)
    {
        if (string.IsNullOrWhiteSpace(val))
            return "";
        string val2 = val.ToLower();
        var clean = val2.Replace("Ğ", "g").Replace("Ü", "u").Replace("Ş", "s").Replace("İ", "i")
                   .Replace("Ö", "o").Replace("Ç", "c").Replace("ğ", "g").Replace("ü", "u")
                   .Replace("ş", "s").Replace("ı", "i").Replace("ö", "o").Replace("ç", "c")
                   .Replace("!", "").Replace("+", " ").Replace("/", " ").Replace("{", " ")
                   .Replace("}", " ").Replace("(", " ").Replace(")", " ").Replace("[", " ")
                   .Replace("]", " ").Replace("=", " ").Replace("\\", " ").Replace("-", " ")
                   .Replace("_", " ").Replace("@", " ").Replace(",", " ").Replace(".", " ")
                   .Replace(":", " ").Replace("<", " ").Replace(">", " ").Replace("'", "");
        return clean;
    }
    #endregion
    #region ComputeLevenshteinDistance
    static int ComputeLevenshteinDistance(string word1, string word2)
    {
        int[,] distance = new int[word1.Length + 1, word2.Length + 1];

        for (int i = 0; i <= word1.Length; i++)
        {
            for (int j = 0; j <= word2.Length; j++)
            {
                if (i == 0)
                    distance[i, j] = j;
                else if (j == 0)
                    distance[i, j] = i;
                else if (word1[i - 1] == word2[j - 1])
                    distance[i, j] = distance[i - 1, j - 1];
                else
                    distance[i, j] = 1 + Math.Min(Math.Min(distance[i, j - 1], distance[i - 1, j]), distance[i - 1, j - 1]);
            }
        }
        return distance[word1.Length, word2.Length];
    }
    #endregion

}
