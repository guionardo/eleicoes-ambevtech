namespace Provider.Fixtures
{
    /// <summary>
    /// Classe destinada a gerar dados para alimentar os arquivos CSV
    /// </summary>
    internal static class DataCreator
    {
        public static void CreateCsvIdNameFile(string fileName, int count)
        {
            using var file = new FileStream(fileName, FileMode.Create);
            using var writer = new StreamWriter(file);
            writer.NewLine= "\n";
            writer.WriteLine("Id,Name");            
            for (var i = 0; i < count; i++)
            {
                writer.WriteLine($"{i + 1},{Faker.Name.FullName()}");
            }
        }

        
    }
}
