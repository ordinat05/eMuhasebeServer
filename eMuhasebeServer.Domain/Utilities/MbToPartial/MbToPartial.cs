using eMuhasebeServer.Domain.Entities.VmModel.MbToPartial;

namespace eMuhasebeServer.Domain.Utilities.MbToPartial
{
    public static class MbToPartial
    {

        public static MbToPartialViewDto ToPartial(List<MbToPartialDataDto> model)
        {
            //var model = Data();
            var firstPartition = new List<MbToPartialDataDto>();
            var secondPartition = new List<MbToPartialDataDto>();
            var thirdPartition = new List<MbToPartialDataDto>();

            var data = new List<MbToPartialReturnListDto>();
            int totalMb = model.Sum(m => m.Mb);
            int partTotal = totalMb / 3;
            int currentTotal = 0;
            foreach (var item in model.OrderByDescending(m => m.Mb))
            {
                if (currentTotal + item.Mb <= totalMb / 3)
                {
                    firstPartition.Add(item);
                    currentTotal += item.Mb;
                    data.Add(new MbToPartialReturnListDto
                    {
                        Id = item.Id,
                        Dosyaboyutu = item.Dosyaboyutu,
                        FirstPartition = item.Mb,
                    });
                }
                else if (currentTotal + item.Mb <= 2 * (totalMb / 3))
                {
                    secondPartition.Add(item);
                    currentTotal += item.Mb;
                    data.Add(new MbToPartialReturnListDto
                    {
                        Id = item.Id,
                        Dosyaboyutu = item.Dosyaboyutu,
                        SecondPartition = item.Mb,
                    });
                }
                else
                {
                    thirdPartition.Add(item);
                    currentTotal += item.Mb;
                    data.Add(new MbToPartialReturnListDto
                    {
                        Id = item.Id,
                        Dosyaboyutu = item.Dosyaboyutu,
                        ThirdPartition = item.Mb,
                    });
                }
            }

            var modelData = new MbToPartialViewDto();
            modelData.Data = data;

            modelData.FirstPartition = firstPartition.Sum(m => m.Mb).ToString("N");
            modelData.SecondPartition = secondPartition.Sum(m => m.Mb).ToString("N");
            modelData.ThirdPartition = thirdPartition.Sum(m => m.Mb).ToString("N");
            return modelData;
        }

    }
}