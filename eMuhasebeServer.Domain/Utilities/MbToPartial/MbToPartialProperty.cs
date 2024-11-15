using eMuhasebeServer.Domain.Entities.VmModel.MbToPartial;

namespace eMuhasebeServer.Domain.Utilities.MbToPartial
{
    public static class MbToPartialProperty
    {
        #region İstenilen Datalar
        //public class MbToPartialViewDto
        //{
        //    public string? FirstPartition { get; set; }
        //    public string? SecondPartition { get; set; }
        //    public string? ThirdPartition { get; set; }
        //    public List<MbToPartialReturnListDto> Data { get; set; }
        //}

        //public class MbToPartialReturnListDto
        //{
        //    public int Id { get; set; }
        //    public int? FirstPartition { get; set; }
        //    public int? SecondPartition { get; set; }
        //    public int? ThirdPartition { get; set; }
        //    public string Dosyaboyutu { get; set; } = string.Empty;
        //}

        //        @model IEnumerable<Proin_Business.Core.Models.TestModel2>
        //<h1>
        //    Toplam: @ViewBag.Toplam1
        //</h1>
        //<h1>
        //    Toplam 2: @ViewBag.Toplam2
        //</h1>
        //<h1>
        //    Toplam :3  @ViewBag.Toplam3
        //</h1>
        //<table class="table">
        //    <thead>
        //        <tr>
        //            <th scope = "col" > Id </ th >
        //            < th scope="col">Boyut</th>
        //            <th scope = "col" > First </ th >
        //            < th scope="col">Second</th>
        //            <th scope = "col" > Third </ th >
        //        </ tr >
        //    </ thead >
        //    < tbody >
        //        @foreach(var item in Model.OrderBy(x => x.Id))
        //        {
        //            <tr>
        //                <td>@item.Id</td>
        //                <td>@item.Dosyaboyutu</td>
        //                <td>@item.FirstPartition</td>
        //                <td>@item.SecondPartition</td>
        //                <td>@item.ThirdPartition</td>
        //            </tr>
        //        }
        //    </tbody>
        //    <tfoot>
        //        <tr>
        //            <td></td>
        //            <td></td>
        //            <td>
        //                @Model.Sum(x => x.FirstPartition)?.ToString("N")
        //            </td>
        //            <td>
        //                @Model.Sum(x => x.SecondPartition)?.ToString("N")
        //            </td>
        //            <td>
        //                @Model.Sum(x => x.ThirdPartition)?.ToString("N")
        //            </td>
        //        </tr>
        //    </tfoot>
        //</table>
        #endregion
        public static MbToPartialViewDto Get()
        {
            var data = Data();
            return MbToPartial.ToPartial(data);
        }

        #region Data
        public static List<MbToPartialDataDto> Data()
        {
            var model = new List<MbToPartialDataDto>();
            model.Add(new MbToPartialDataDto { Id = 1, Dosyaboyutu = "ejurczbwcg", Mb = 244213 });
            model.Add(new MbToPartialDataDto { Id = 2, Dosyaboyutu = "lohswglmvm", Mb = 156373 });
            model.Add(new MbToPartialDataDto { Id = 3, Dosyaboyutu = "wfcvnubram", Mb = 726350 });
            model.Add(new MbToPartialDataDto { Id = 4, Dosyaboyutu = "szwbwrhrav", Mb = 944506 });
            model.Add(new MbToPartialDataDto { Id = 5, Dosyaboyutu = "bjuhbrocuu", Mb = 859463 });
            model.Add(new MbToPartialDataDto { Id = 6, Dosyaboyutu = "wyyxhbfgmv", Mb = 865084 });
            model.Add(new MbToPartialDataDto { Id = 7, Dosyaboyutu = "osrofvjwug", Mb = 546938 });
            model.Add(new MbToPartialDataDto { Id = 8, Dosyaboyutu = "ldmqmxbbpl", Mb = 485325 });
            model.Add(new MbToPartialDataDto { Id = 9, Dosyaboyutu = "ngcyjvmtxh", Mb = 754451 });
            model.Add(new MbToPartialDataDto { Id = 10, Dosyaboyutu = "dxybnvehll", Mb = 220651 });
            model.Add(new MbToPartialDataDto { Id = 11, Dosyaboyutu = "oldusxqsum", Mb = 33020 });
            model.Add(new MbToPartialDataDto { Id = 12, Dosyaboyutu = "jcwxchwarm", Mb = 314711 });
            model.Add(new MbToPartialDataDto { Id = 13, Dosyaboyutu = "nbgivyaxmv", Mb = 520153 });
            model.Add(new MbToPartialDataDto { Id = 14, Dosyaboyutu = "bnibhldjkq", Mb = 172391 });
            model.Add(new MbToPartialDataDto { Id = 15, Dosyaboyutu = "vnmxufzyjz", Mb = 810437 });
            model.Add(new MbToPartialDataDto { Id = 16, Dosyaboyutu = "fknsqwlnxj", Mb = 838398 });
            model.Add(new MbToPartialDataDto { Id = 17, Dosyaboyutu = "jbxlsevzzw", Mb = 19941 });
            model.Add(new MbToPartialDataDto { Id = 18, Dosyaboyutu = "ebbigvcdrl", Mb = 127343 });
            model.Add(new MbToPartialDataDto { Id = 19, Dosyaboyutu = "uwmvuhkquk", Mb = 31706 });
            model.Add(new MbToPartialDataDto { Id = 20, Dosyaboyutu = "cmfcqjnzqs", Mb = 185693 });
            model.Add(new MbToPartialDataDto { Id = 21, Dosyaboyutu = "ixzpiiaktw", Mb = 59196 });
            model.Add(new MbToPartialDataDto { Id = 22, Dosyaboyutu = "ehhlxontoq", Mb = 978472 });
            model.Add(new MbToPartialDataDto { Id = 23, Dosyaboyutu = "crlgkctoup", Mb = 499056 });
            model.Add(new MbToPartialDataDto { Id = 24, Dosyaboyutu = "vjchfcoshi", Mb = 563268 });
            model.Add(new MbToPartialDataDto { Id = 25, Dosyaboyutu = "pmkshcjwtj", Mb = 556809 });
            model.Add(new MbToPartialDataDto { Id = 26, Dosyaboyutu = "physghftvc", Mb = 167725 });
            model.Add(new MbToPartialDataDto { Id = 27, Dosyaboyutu = "zqwgjejkzg", Mb = 618687 });
            model.Add(new MbToPartialDataDto { Id = 28, Dosyaboyutu = "xvvlogxpmm", Mb = 239821 });
            model.Add(new MbToPartialDataDto { Id = 29, Dosyaboyutu = "bpxypdgkia", Mb = 193936 });
            model.Add(new MbToPartialDataDto { Id = 30, Dosyaboyutu = "mpyunxrlkj", Mb = 19801 });
            model.Add(new MbToPartialDataDto { Id = 31, Dosyaboyutu = "zijxfztceu", Mb = 349230 });
            model.Add(new MbToPartialDataDto { Id = 32, Dosyaboyutu = "onnbcrsrhl", Mb = 870311 });
            model.Add(new MbToPartialDataDto { Id = 33, Dosyaboyutu = "uktrdujphz", Mb = 924457 });
            model.Add(new MbToPartialDataDto { Id = 34, Dosyaboyutu = "zrczojyksx", Mb = 699085 });
            model.Add(new MbToPartialDataDto { Id = 35, Dosyaboyutu = "xydwvnbuls", Mb = 61713 });
            model.Add(new MbToPartialDataDto { Id = 36, Dosyaboyutu = "lmovmjtszv", Mb = 531327 });
            model.Add(new MbToPartialDataDto { Id = 37, Dosyaboyutu = "guadrhpahb", Mb = 752495 });
            model.Add(new MbToPartialDataDto { Id = 38, Dosyaboyutu = "gokvxlmdpx", Mb = 404861 });
            model.Add(new MbToPartialDataDto { Id = 39, Dosyaboyutu = "mxginabbak", Mb = 603537 });
            model.Add(new MbToPartialDataDto { Id = 40, Dosyaboyutu = "krdxapusvl", Mb = 699547 });
            model.Add(new MbToPartialDataDto { Id = 41, Dosyaboyutu = "ybshodjpxe", Mb = 168107 });
            model.Add(new MbToPartialDataDto { Id = 42, Dosyaboyutu = "xirokmnwfw", Mb = 533266 });
            model.Add(new MbToPartialDataDto { Id = 43, Dosyaboyutu = "fldvrbryct", Mb = 650541 });
            model.Add(new MbToPartialDataDto { Id = 44, Dosyaboyutu = "xowtbcjqwo", Mb = 574964 });
            model.Add(new MbToPartialDataDto { Id = 45, Dosyaboyutu = "vnzoqblwas", Mb = 932400 });
            model.Add(new MbToPartialDataDto { Id = 46, Dosyaboyutu = "scbnmezqgh", Mb = 154597 });
            model.Add(new MbToPartialDataDto { Id = 47, Dosyaboyutu = "degraxapqp", Mb = 556893 });
            model.Add(new MbToPartialDataDto { Id = 48, Dosyaboyutu = "qvitwdozpb", Mb = 655063 });
            model.Add(new MbToPartialDataDto { Id = 49, Dosyaboyutu = "zguibqenrq", Mb = 299395 });
            model.Add(new MbToPartialDataDto { Id = 50, Dosyaboyutu = "patlyuisud", Mb = 2489 });
            model.Add(new MbToPartialDataDto { Id = 51, Dosyaboyutu = "ybjteoawmp", Mb = 519751 });
            model.Add(new MbToPartialDataDto { Id = 52, Dosyaboyutu = "jtduglqiru", Mb = 326332 });
            model.Add(new MbToPartialDataDto { Id = 53, Dosyaboyutu = "qazvtuqcok", Mb = 509286 });
            model.Add(new MbToPartialDataDto { Id = 54, Dosyaboyutu = "udlkbdeuwg", Mb = 294536 });
            model.Add(new MbToPartialDataDto { Id = 55, Dosyaboyutu = "zpjwyvukgn", Mb = 102355 });
            model.Add(new MbToPartialDataDto { Id = 56, Dosyaboyutu = "rnwgrdgvwh", Mb = 745208 });
            model.Add(new MbToPartialDataDto { Id = 57, Dosyaboyutu = "aaxkmxpwrz", Mb = 20599 });
            model.Add(new MbToPartialDataDto { Id = 58, Dosyaboyutu = "zigmmcmtup", Mb = 761292 });
            model.Add(new MbToPartialDataDto { Id = 59, Dosyaboyutu = "qcpxrrvheo", Mb = 608355 });
            model.Add(new MbToPartialDataDto { Id = 60, Dosyaboyutu = "lzvkjpeivm", Mb = 234256 });
            model.Add(new MbToPartialDataDto { Id = 61, Dosyaboyutu = "xbkjvzetai", Mb = 112471 });
            model.Add(new MbToPartialDataDto { Id = 62, Dosyaboyutu = "povzalxiva", Mb = 463637 });
            model.Add(new MbToPartialDataDto { Id = 63, Dosyaboyutu = "pjwirhgeyy", Mb = 782998 });
            model.Add(new MbToPartialDataDto { Id = 64, Dosyaboyutu = "giiqgjavyz", Mb = 11987 });
            model.Add(new MbToPartialDataDto { Id = 65, Dosyaboyutu = "zzhdifefiz", Mb = 896167 });
            model.Add(new MbToPartialDataDto { Id = 66, Dosyaboyutu = "zgczthpqqp", Mb = 22430 });
            model.Add(new MbToPartialDataDto { Id = 67, Dosyaboyutu = "vmahlcikus", Mb = 200753 });
            model.Add(new MbToPartialDataDto { Id = 68, Dosyaboyutu = "qferzogdez", Mb = 158409 });
            model.Add(new MbToPartialDataDto { Id = 69, Dosyaboyutu = "jlcppyuudm", Mb = 763678 });
            model.Add(new MbToPartialDataDto { Id = 70, Dosyaboyutu = "lsxxxorfmk", Mb = 21806 });
            model.Add(new MbToPartialDataDto { Id = 71, Dosyaboyutu = "zxcltsnazp", Mb = 111508 });
            model.Add(new MbToPartialDataDto { Id = 72, Dosyaboyutu = "bpyssempga", Mb = 382991 });
            model.Add(new MbToPartialDataDto { Id = 73, Dosyaboyutu = "ndmzxudquy", Mb = 114264 });
            model.Add(new MbToPartialDataDto { Id = 74, Dosyaboyutu = "vzxvwjdlvk", Mb = 925682 });
            model.Add(new MbToPartialDataDto { Id = 75, Dosyaboyutu = "zntgwnspns", Mb = 48384 });
            model.Add(new MbToPartialDataDto { Id = 76, Dosyaboyutu = "knbhlnfuuu", Mb = 201047 });
            model.Add(new MbToPartialDataDto { Id = 77, Dosyaboyutu = "ygbltmkpws", Mb = 341620 });
            model.Add(new MbToPartialDataDto { Id = 78, Dosyaboyutu = "ywzsuqqjmi", Mb = 827397 });
            model.Add(new MbToPartialDataDto { Id = 79, Dosyaboyutu = "dnktddnnew", Mb = 230552 });
            model.Add(new MbToPartialDataDto { Id = 80, Dosyaboyutu = "rzzwjpzoda", Mb = 539469 });
            model.Add(new MbToPartialDataDto { Id = 81, Dosyaboyutu = "rcybuanroi", Mb = 248739 });
            model.Add(new MbToPartialDataDto { Id = 82, Dosyaboyutu = "rofgtgztuz", Mb = 595326 });
            model.Add(new MbToPartialDataDto { Id = 83, Dosyaboyutu = "tsevglpmih", Mb = 42059 });
            model.Add(new MbToPartialDataDto { Id = 84, Dosyaboyutu = "kqluygbpyg", Mb = 323166 });
            model.Add(new MbToPartialDataDto { Id = 85, Dosyaboyutu = "awrqlubldy", Mb = 813432 });
            model.Add(new MbToPartialDataDto { Id = 86, Dosyaboyutu = "mwvbufqgbg", Mb = 471531 });
            model.Add(new MbToPartialDataDto { Id = 87, Dosyaboyutu = "cspmpirrhu", Mb = 670107 });
            model.Add(new MbToPartialDataDto { Id = 88, Dosyaboyutu = "ckwhprimbh", Mb = 764861 });
            model.Add(new MbToPartialDataDto { Id = 89, Dosyaboyutu = "xapacahhjz", Mb = 217661 });
            model.Add(new MbToPartialDataDto { Id = 90, Dosyaboyutu = "pkuxebxwzc", Mb = 540989 });
            model.Add(new MbToPartialDataDto { Id = 91, Dosyaboyutu = "mmugswvfhv", Mb = 591818 });
            model.Add(new MbToPartialDataDto { Id = 92, Dosyaboyutu = "sddzgvpnzf", Mb = 154079 });
            model.Add(new MbToPartialDataDto { Id = 93, Dosyaboyutu = "nzvrxxybku", Mb = 889135 });
            model.Add(new MbToPartialDataDto { Id = 94, Dosyaboyutu = "fqczoarmbv", Mb = 526032 });
            model.Add(new MbToPartialDataDto { Id = 95, Dosyaboyutu = "wnakmshyti", Mb = 731800 });
            model.Add(new MbToPartialDataDto { Id = 96, Dosyaboyutu = "zuqncxqxin", Mb = 386739 });
            model.Add(new MbToPartialDataDto { Id = 97, Dosyaboyutu = "yxlsiibuzp", Mb = 776632 });
            model.Add(new MbToPartialDataDto { Id = 98, Dosyaboyutu = "pkmtgssbus", Mb = 345672 });
            model.Add(new MbToPartialDataDto { Id = 99, Dosyaboyutu = "ebnqsrsxtw", Mb = 228245 });
            model.Add(new MbToPartialDataDto { Id = 100, Dosyaboyutu = "sqdlxbhhij", Mb = 875782 });
            model.Add(new MbToPartialDataDto { Id = 101, Dosyaboyutu = "zzvwqkcgpn", Mb = 544350 });
            model.Add(new MbToPartialDataDto { Id = 102, Dosyaboyutu = "mugyyqwwah", Mb = 545224 });
            model.Add(new MbToPartialDataDto { Id = 103, Dosyaboyutu = "diwyzflhsf", Mb = 777233 });
            model.Add(new MbToPartialDataDto { Id = 104, Dosyaboyutu = "glylwphpra", Mb = 916365 });
            model.Add(new MbToPartialDataDto { Id = 105, Dosyaboyutu = "ljqmkungjy", Mb = 539539 });
            model.Add(new MbToPartialDataDto { Id = 106, Dosyaboyutu = "octxjvhmui", Mb = 396864 });
            model.Add(new MbToPartialDataDto { Id = 107, Dosyaboyutu = "jkrpxjrjpe", Mb = 623545 });
            model.Add(new MbToPartialDataDto { Id = 108, Dosyaboyutu = "hhhkxcqxqc", Mb = 2932 });
            model.Add(new MbToPartialDataDto { Id = 109, Dosyaboyutu = "atwbxhcpnn", Mb = 124460 });
            model.Add(new MbToPartialDataDto { Id = 110, Dosyaboyutu = "mvoqznnhgq", Mb = 742964 });
            model.Add(new MbToPartialDataDto { Id = 111, Dosyaboyutu = "ncqxawrhje", Mb = 424262 });
            model.Add(new MbToPartialDataDto { Id = 112, Dosyaboyutu = "xbhvlwiver", Mb = 205121 });
            model.Add(new MbToPartialDataDto { Id = 113, Dosyaboyutu = "gkhqmcmfsn", Mb = 810159 });
            model.Add(new MbToPartialDataDto { Id = 114, Dosyaboyutu = "hnypmglifm", Mb = 792315 });
            model.Add(new MbToPartialDataDto { Id = 115, Dosyaboyutu = "kuvzljzpjm", Mb = 39715 });
            model.Add(new MbToPartialDataDto { Id = 116, Dosyaboyutu = "mlupwklita", Mb = 861742 });
            model.Add(new MbToPartialDataDto { Id = 117, Dosyaboyutu = "uwuydulafs", Mb = 188390 });
            model.Add(new MbToPartialDataDto { Id = 118, Dosyaboyutu = "dlzrwwwfoh", Mb = 361303 });
            model.Add(new MbToPartialDataDto { Id = 119, Dosyaboyutu = "qoreetiorr", Mb = 626612 });
            model.Add(new MbToPartialDataDto { Id = 120, Dosyaboyutu = "brhghtzjzp", Mb = 901321 });
            model.Add(new MbToPartialDataDto { Id = 121, Dosyaboyutu = "trhgulqlhj", Mb = 401466 });
            model.Add(new MbToPartialDataDto { Id = 122, Dosyaboyutu = "kcrwzehmjo", Mb = 786083 });
            model.Add(new MbToPartialDataDto { Id = 123, Dosyaboyutu = "mmkrkugdvs", Mb = 124550 });
            model.Add(new MbToPartialDataDto { Id = 124, Dosyaboyutu = "crszulsvfp", Mb = 376156 });
            model.Add(new MbToPartialDataDto { Id = 125, Dosyaboyutu = "xwubnsidrr", Mb = 345015 });
            model.Add(new MbToPartialDataDto { Id = 126, Dosyaboyutu = "vuariuarni", Mb = 686794 });
            model.Add(new MbToPartialDataDto { Id = 127, Dosyaboyutu = "buqskuypev", Mb = 941471 });
            model.Add(new MbToPartialDataDto { Id = 128, Dosyaboyutu = "rsvisndkoj", Mb = 267228 });
            model.Add(new MbToPartialDataDto { Id = 129, Dosyaboyutu = "zggwstfqsk", Mb = 40227 });
            model.Add(new MbToPartialDataDto { Id = 130, Dosyaboyutu = "rjwuveoblf", Mb = 998790 });
            model.Add(new MbToPartialDataDto { Id = 131, Dosyaboyutu = "jtjcpagfzw", Mb = 484253 });
            model.Add(new MbToPartialDataDto { Id = 132, Dosyaboyutu = "ijchybqtik", Mb = 148508 });
            model.Add(new MbToPartialDataDto { Id = 133, Dosyaboyutu = "nlgrjlvkgn", Mb = 656201 });
            model.Add(new MbToPartialDataDto { Id = 134, Dosyaboyutu = "kvqbtybqbj", Mb = 994273 });
            model.Add(new MbToPartialDataDto { Id = 135, Dosyaboyutu = "kezlxjxtbx", Mb = 271453 });
            model.Add(new MbToPartialDataDto { Id = 136, Dosyaboyutu = "kqfzqhyeei", Mb = 658772 });
            model.Add(new MbToPartialDataDto { Id = 137, Dosyaboyutu = "hfymxhjhxj", Mb = 224019 });
            model.Add(new MbToPartialDataDto { Id = 138, Dosyaboyutu = "jcbmahshhn", Mb = 355536 });
            model.Add(new MbToPartialDataDto { Id = 139, Dosyaboyutu = "slkskrdahw", Mb = 929666 });
            model.Add(new MbToPartialDataDto { Id = 140, Dosyaboyutu = "boaqipzmhu", Mb = 886970 });
            model.Add(new MbToPartialDataDto { Id = 141, Dosyaboyutu = "kxhvorguii", Mb = 293499 });
            model.Add(new MbToPartialDataDto { Id = 142, Dosyaboyutu = "mfpowtuvgj", Mb = 314990 });
            model.Add(new MbToPartialDataDto { Id = 143, Dosyaboyutu = "wxnwumuwum", Mb = 312626 });
            model.Add(new MbToPartialDataDto { Id = 144, Dosyaboyutu = "redlrobecf", Mb = 86058 });
            model.Add(new MbToPartialDataDto { Id = 145, Dosyaboyutu = "klqxrhkhyk", Mb = 820595 });
            model.Add(new MbToPartialDataDto { Id = 146, Dosyaboyutu = "tmcnjfqfpu", Mb = 503099 });
            model.Add(new MbToPartialDataDto { Id = 147, Dosyaboyutu = "wjgkylippg", Mb = 269163 });
            model.Add(new MbToPartialDataDto { Id = 148, Dosyaboyutu = "yqntqxsjnf", Mb = 334562 });
            model.Add(new MbToPartialDataDto { Id = 149, Dosyaboyutu = "niaivyyjji", Mb = 382234 });
            model.Add(new MbToPartialDataDto { Id = 150, Dosyaboyutu = "dymcmmpbph", Mb = 698401 });
            model.Add(new MbToPartialDataDto { Id = 151, Dosyaboyutu = "nnkgrzhutb", Mb = 673663 });
            model.Add(new MbToPartialDataDto { Id = 152, Dosyaboyutu = "aenzoqwomz", Mb = 72884 });
            model.Add(new MbToPartialDataDto { Id = 153, Dosyaboyutu = "rmuqznoeuq", Mb = 903006 });
            model.Add(new MbToPartialDataDto { Id = 154, Dosyaboyutu = "uxvcvquuyt", Mb = 361467 });
            model.Add(new MbToPartialDataDto { Id = 155, Dosyaboyutu = "iwtccqubxk", Mb = 116751 });
            model.Add(new MbToPartialDataDto { Id = 156, Dosyaboyutu = "fzfqnwqeuh", Mb = 878250 });
            model.Add(new MbToPartialDataDto { Id = 157, Dosyaboyutu = "dcljuhmwry", Mb = 696179 });
            model.Add(new MbToPartialDataDto { Id = 158, Dosyaboyutu = "qdhrtdymcl", Mb = 589034 });
            model.Add(new MbToPartialDataDto { Id = 159, Dosyaboyutu = "jzqhqrsvvv", Mb = 47570 });
            model.Add(new MbToPartialDataDto { Id = 160, Dosyaboyutu = "bbxjezinmg", Mb = 718996 });
            model.Add(new MbToPartialDataDto { Id = 161, Dosyaboyutu = "ztoqvqgaba", Mb = 700394 });
            model.Add(new MbToPartialDataDto { Id = 162, Dosyaboyutu = "matbpgyeoi", Mb = 609460 });
            model.Add(new MbToPartialDataDto { Id = 163, Dosyaboyutu = "uvbcoejwlo", Mb = 903041 });
            model.Add(new MbToPartialDataDto { Id = 164, Dosyaboyutu = "gbycvohcey", Mb = 195502 });
            model.Add(new MbToPartialDataDto { Id = 165, Dosyaboyutu = "tsjufjqxth", Mb = 571112 });
            model.Add(new MbToPartialDataDto { Id = 166, Dosyaboyutu = "rkjwytnnoo", Mb = 856708 });
            model.Add(new MbToPartialDataDto { Id = 167, Dosyaboyutu = "fjeqgxurhe", Mb = 480557 });
            model.Add(new MbToPartialDataDto { Id = 168, Dosyaboyutu = "ncsoyvamrs", Mb = 905384 });
            model.Add(new MbToPartialDataDto { Id = 169, Dosyaboyutu = "rdzycctetz", Mb = 40907 });
            model.Add(new MbToPartialDataDto { Id = 170, Dosyaboyutu = "fjiahrizmp", Mb = 531332 });
            model.Add(new MbToPartialDataDto { Id = 171, Dosyaboyutu = "hqbylccqkj", Mb = 506230 });
            model.Add(new MbToPartialDataDto { Id = 172, Dosyaboyutu = "uwtokajmzs", Mb = 382473 });
            model.Add(new MbToPartialDataDto { Id = 173, Dosyaboyutu = "dwwwkvkxzx", Mb = 336817 });
            model.Add(new MbToPartialDataDto { Id = 174, Dosyaboyutu = "kklemqlqgq", Mb = 763114 });
            model.Add(new MbToPartialDataDto { Id = 175, Dosyaboyutu = "xrbiapyfps", Mb = 174139 });
            model.Add(new MbToPartialDataDto { Id = 176, Dosyaboyutu = "wswpfpydhs", Mb = 178547 });
            model.Add(new MbToPartialDataDto { Id = 177, Dosyaboyutu = "cfmognjwwx", Mb = 232923 });
            model.Add(new MbToPartialDataDto { Id = 178, Dosyaboyutu = "sbhfvjhoib", Mb = 402154 });
            model.Add(new MbToPartialDataDto { Id = 179, Dosyaboyutu = "jeargrqisc", Mb = 42695 });
            model.Add(new MbToPartialDataDto { Id = 180, Dosyaboyutu = "xdwpxmpdeg", Mb = 235769 });
            model.Add(new MbToPartialDataDto { Id = 181, Dosyaboyutu = "eocxxibodj", Mb = 338876 });
            model.Add(new MbToPartialDataDto { Id = 182, Dosyaboyutu = "xnuvgnywng", Mb = 313354 });
            model.Add(new MbToPartialDataDto { Id = 183, Dosyaboyutu = "vqpsqflrji", Mb = 538079 });
            model.Add(new MbToPartialDataDto { Id = 184, Dosyaboyutu = "jpjgsjcshz", Mb = 918282 });
            model.Add(new MbToPartialDataDto { Id = 185, Dosyaboyutu = "gdiarjwfad", Mb = 192914 });
            model.Add(new MbToPartialDataDto { Id = 186, Dosyaboyutu = "bpfsrbtejv", Mb = 788365 });
            model.Add(new MbToPartialDataDto { Id = 187, Dosyaboyutu = "lnptpzlaut", Mb = 105016 });
            model.Add(new MbToPartialDataDto { Id = 188, Dosyaboyutu = "gktjhjclvm", Mb = 975455 });
            model.Add(new MbToPartialDataDto { Id = 189, Dosyaboyutu = "sypwsobhgi", Mb = 464705 });
            model.Add(new MbToPartialDataDto { Id = 190, Dosyaboyutu = "jovupyxkmk", Mb = 809772 });
            model.Add(new MbToPartialDataDto { Id = 191, Dosyaboyutu = "kuargiqlfe", Mb = 841884 });
            model.Add(new MbToPartialDataDto { Id = 192, Dosyaboyutu = "cubaejwbfy", Mb = 478153 });
            model.Add(new MbToPartialDataDto { Id = 193, Dosyaboyutu = "jnfvdjsxmj", Mb = 147113 });
            model.Add(new MbToPartialDataDto { Id = 194, Dosyaboyutu = "cdjxgmwyof", Mb = 159029 });
            model.Add(new MbToPartialDataDto { Id = 195, Dosyaboyutu = "zxglepjjxw", Mb = 318976 });
            model.Add(new MbToPartialDataDto { Id = 196, Dosyaboyutu = "pofdjrhdoe", Mb = 446923 });
            model.Add(new MbToPartialDataDto { Id = 197, Dosyaboyutu = "rjowsrvpbh", Mb = 276939 });
            model.Add(new MbToPartialDataDto { Id = 198, Dosyaboyutu = "bmllqnizdk", Mb = 818639 });
            model.Add(new MbToPartialDataDto { Id = 199, Dosyaboyutu = "mnodxzfxjp", Mb = 944312 });
            model.Add(new MbToPartialDataDto { Id = 200, Dosyaboyutu = "suvwfqmami", Mb = 287286 });

            return model;
        }
        #endregion
    }
}
