namespace CleanApi.Infrastructure.Repositories
{
    public static class DatabaseInMemory
    {
        public class ClimateDto
        {
            public string Location { get; set; }
            public int LowTemperature { get; set; }
            public int HighTemperature { get; set;  }
        }

        public static ClimateDto[] LocationClimates = new[]
        {
            new ClimateDto{ Location = "poland/cracow", LowTemperature=-15, HighTemperature=38 },
            new ClimateDto{ Location = "india/chennai", LowTemperature=-1, HighTemperature=55 },
            new ClimateDto{ Location = "usa/richfield", LowTemperature=-10, HighTemperature=42 },
            new ClimateDto{ Location = "usa/cleveland", LowTemperature=-10, HighTemperature=42 },
            new ClimateDto{ Location = "usa/newyork", LowTemperature=-10, HighTemperature=42 },
            new ClimateDto{ Location = "usa/sanfranciso", LowTemperature=-1, HighTemperature=49 },
            new ClimateDto{ Location = "usa/redmond", LowTemperature=-12, HighTemperature=38 }
        };
    }
}