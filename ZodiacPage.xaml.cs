namespace Horoskoop;

public partial class ZodiacPage : ContentPage
{
    Label todayHoroscopeLabel;
    ZodiacSign currentSign;

    public ZodiacPage(ZodiacSign sign)
    {
        currentSign = sign;
        Title = sign.Name;

        var symbolLabel = new Label
        {
            Text = sign.Symbol,
            FontSize = 80,
            HorizontalOptions = LayoutOptions.Center
        };

        var nameLabel = new Label
        {
            Text = sign.Name,
            FontSize = 30,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center
        };

        var infoLabel = new Label
        {
            Text = $"Kuupäevad: {sign.Dates}\nElement: {sign.Element}\nPlaneet: {sign.Planet}\nOmadused: {sign.Traits}",
            Margin = new Thickness(10)
        };

        todayHoroscopeLabel = new Label
        {
            Text = GenerateDailyHoroscope("Üldine"),
            Margin = new Thickness(10),
            FontSize = 16
        };

        var picker = new Picker
        {
            Title = "Horoskoobi tüüp",
            ItemsSource = new List<string> { "Üldine", "Armastus", "Raha" },
            SelectedIndex = 0
        };

        picker.SelectedIndexChanged += (s, e) =>
        {
            string selectedType = (string)picker.SelectedItem;
            todayHoroscopeLabel.Text = GenerateDailyHoroscope(selectedType);
        };

        var likeButton = new Button
        {
            Text = "❤️ Mulle meeldib",
            BackgroundColor = Colors.LightPink
        };

        int likes = 0;
        var likeLabel = new Label { Text = "Meeldimisi: 0", HorizontalOptions = LayoutOptions.Center };

        likeButton.Clicked += async (s, e) =>
        {
            likes++;
            likeLabel.Text = $"Meeldimisi: {likes}";
            await likeButton.ScaleTo(1.2, 100);
            await likeButton.ScaleTo(1.0, 100);
        };

        Content = new ScrollView
        {
            Content = new StackLayout
            {
                Padding = 20,
                Children =
                {
                    symbolLabel,
                    nameLabel,
                    infoLabel,
                    picker,
                    todayHoroscopeLabel,
                    likeButton,
                    likeLabel
                }
            }
        };
    }

    private string GenerateDailyHoroscope(string type)
    {
        var moods = new[]
        {
            "rõõmus ja energiline",
            "rahulik ja tasakaalukas",
            "veidi pinges, aga keskendunud",
            "inspireeritud ja loominguline",
            "unistav ja eemalolev",
            "erutatud uutest võimalustest",
            "eesmärgile pühendunud ja enesekindel",
            "veidi hajameelne, aga avatud"
        };

        var love = new[]
        {
            "Romantika on õhus — kuula oma südant.",
            "Ole kannatlik ja hooliv — see tugevdab suhet.",
            "Hea aeg soojadeks ülestunnistusteks.",
            "Uued tunded võivad tulla ootamatult.",
            "Vanad suhted võivad saada uue hingamise.",
            "Ole avatud flirtimiseks — see võib olla millegi suure algus.",
            "Usaldus on oluline — ilma selleta pole tugevat liitu.",
            "Hellita oma kallimat — isegi pisiasjad loevad."
        };

        var money = new[]
        {
            "Rahaline õnn on sinu poolel — aga väldi tarbetut riski.",
            "Väldi täna impulsiivseid oste.",
            "Hea päev eelarve planeerimiseks või investeerimiseks.",
            "Võib tulla ootamatu sissetulek — hoia silmad lahti.",
            "Ole ettevaatlik petturite suhtes ja kontrolli oma kulutusi.",
            "Väike sääst täna — suurem stabiilsus homme.",
            "Karjäärivõimalus on lähedal — ära maga seda maha.",
            "Sinu järjekindlus toob tulemusi, isegi kui mitte kohe."
        };

        var advice = new[]
        {
            "Usalda ennast — oled õigel teel.",
            "Ole avatud uuele, isegi kui see hirmutab.",
            "Ära karda muutusi — need toovad kasvu.",
            "Hoolitse enda eest: vaimselt ja füüsiliselt.",
            "Intuitsioon on täna eriti tugev — kuula seda.",
            "Sobiv päev eneseanalüüsiks ja taaskäivituseks.",
            "Keskendu olulisele ja ära hajuta oma tähelepanu.",
            "Veeda aega lähedastega — see annab jõudu."
        };

        string key = currentSign.Name + DateTime.Today.ToString("yyyyMMdd") + type;
        int hash = key.GetHashCode();
        var rnd = new Random(hash);

        string mood = moods[rnd.Next(moods.Length)];
        string baseText = $"Täna tunned end {mood}. ";

        string addition = type switch
        {
            "Armastus" => love[rnd.Next(love.Length)],
            "Raha" => money[rnd.Next(money.Length)],
            _ => advice[rnd.Next(advice.Length)]
        };

        return baseText + addition;
    }
}
