using Ensek.ViewModels;

namespace Ensek.Library
{
    public static class FileUtil
    {
        public static IEnumerable<MeterReadingViewModel> GetMeterReadings(FileViewModel file)
        {
            var meterReadings = new List<MeterReadingViewModel>();
            var header = true;
            
            using (var stream = new MemoryStream())
            {
                file.FormFile?.CopyTo(stream);

                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line?.Split(',');

                        if (header)
                        {
                            header = false;
                            
                            continue;
                        }

                        if (values?.Length > 3)
                        {
                            _ = int.TryParse(values[0], out int accountId);
                            _ = DateTime.TryParse(values[1], out DateTime meterReadingDateTime);

                            meterReadings.Add(new MeterReadingViewModel
                            {
                                AccountId = accountId,
                                MeterReadingDateTime = meterReadingDateTime,
                                MeterReadingValue = values[2],
                            });
                        }
                    }
                }
            }

            return meterReadings;
        }
    }
}
