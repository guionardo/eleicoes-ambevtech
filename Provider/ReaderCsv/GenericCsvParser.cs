using CsvHelper;
using CsvHelper.Configuration;
using System.Collections;
using System.Globalization;

namespace Provider.ReaderCsv
{
    public abstract class GenericCsvParser<T, TModel> : IEnumerable<TModel> where T : ClassMap<TModel>
    {
        public static CsvConfiguration CsvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false,
            Delimiter = ",",
            MissingFieldFound = null,
        };


        private Stream _stream;

        protected GenericCsvParser(Stream csvStream)
        {
            _stream = csvStream;
        }

        public IEnumerator<TModel> GetEnumerator()
        {
            _stream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(_stream);
            using var csvReader = new CsvReader(reader, CsvConfig);
            csvReader.Context.RegisterClassMap<T>();
            var isFirstRow = true;
            while (csvReader.Read())
            {
                if (isFirstRow)
                {
                    isFirstRow = false;
                    continue;
                }
                var entidade = csvReader.GetRecord<TModel>();
                yield return entidade;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
