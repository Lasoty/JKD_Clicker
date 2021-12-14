using CommandLine;

namespace JKD_Clicker
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Informuje o szczegółach każdego kroku.")]
        public bool Verbose { get; set; }

        [Option('l', "link", Required = false, HelpText = "Link do ankiety", Default = "https://sport.interia.pl/raporty/raport-as-sportu-2021/plebiscyt/news-as-sportu-2021-wybierz-z-interia-najlepszego-sportowca-roku-,nId,5651058")]
        public string Link { get; set; }

        [Option('r', "repeats", Required = false, HelpText = "Określa ile ma nastąpić prób zagłosowania (domyślnie 1000)", Default = 1000)]
        public int Repeats { get; set; }

        [Option('w', "load", Required = false, HelpText = "Określa czas oczekiwania na załadowanie się strony w pamięci (domyślnie 2000 ms)", Default = 2000)]
        public int LoadWhait { get; set; }

        [Option('a', "after", Required = false, HelpText = "Określa czas oczekiwania na wykonanie kolejnej próby (domyślnie 1000 ms)", Default = 1000)]
        public int AfterWhait { get; set; }
    }
}
