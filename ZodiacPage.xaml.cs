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
            Text = $"Даты: {sign.Dates}\nСтихия: {sign.Element}\nПланета: {sign.Planet}\nЧерты: {sign.Traits}",
            Margin = new Thickness(10)
        };

        todayHoroscopeLabel = new Label
        {
            Text = GenerateDailyHoroscope("Общий"),
            Margin = new Thickness(10),
            FontSize = 16
        };

        var picker = new Picker
        {
            Title = "Тип гороскопа",
            ItemsSource = new List<string> { "Общий", "Любовь", "Финансы" },
            SelectedIndex = 0
        };

        picker.SelectedIndexChanged += (s, e) =>
        {
            string selectedType = (string)picker.SelectedItem;
            todayHoroscopeLabel.Text = GenerateDailyHoroscope(selectedType);
        };

        var likeButton = new Button
        {
            Text = "❤️ Мне нравится",
            BackgroundColor = Colors.LightPink
        };

        int likes = 0;
        var likeLabel = new Label { Text = "Нравится: 0", HorizontalOptions = LayoutOptions.Center };

        likeButton.Clicked += async (s, e) =>
        {
            likes++;
            likeLabel.Text = $"Нравится: {likes}";
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
        "радостным и полным энергии",
        "спокойным и уравновешенным",
        "немного напряжённым, но сосредоточенным",
        "вдохновлённым и креативным",
        "мечтательным и отстранённым",
        "взволнованным новыми возможностями",
        "целеустремлённым и уверенным в себе",
        "немного рассеянным, но открытым"
    };

        var love = new[]
        {
        "Романтика витает в воздухе — прислушайтесь к сердцу.",
        "Проявите терпение и заботу — это укрепит отношения.",
        "Хорошее время для тёплых признаний.",
        "Новые чувства могут прийти неожиданно.",
        "Старые отношения могут заиграть новыми красками.",
        "Будьте открыты для флирта — это может стать началом чего-то большего.",
        "Важно доверие — без него не построить крепкий союз.",
        "Побалуйте любимого человека — даже мелочи имеют значение."
    };

        var money = new[]
        {
        "Финансовая удача на вашей стороне — но не рискуйте зря.",
        "Избегайте импульсивных покупок сегодня.",
        "Хороший день для планирования бюджета или инвестиций.",
        "Может поступить неожиданный доход — держите глаза открытыми.",
        "Остерегайтесь мошенников и проверьте свои траты.",
        "Маленькая экономия сегодня — большая стабильность завтра.",
        "Возможность карьерного роста рядом — не упустите шанс.",
        "Ваше упорство принесёт плоды, даже если не сразу."
    };

        var advice = new[]
        {
        "Доверяйте себе — вы на правильном пути.",
        "Будьте открыты к новому, даже если это пугает.",
        "Не бойтесь перемен — они несут рост.",
        "Позаботьтесь о себе: ментально и физически.",
        "Интуиция сегодня особенно сильна — слушайте её.",
        "День подходит для самоанализа и перезагрузки.",
        "Сосредоточьтесь на главном и не распыляйтесь.",
        "Проведите время с близкими — это даст силы."
    };

        // Создание уникального ключа на каждый день и тип
        string key = currentSign.Name + DateTime.Today.ToString("yyyyMMdd") + type;
        int hash = key.GetHashCode();
        var rnd = new Random(hash);

        string mood = moods[rnd.Next(moods.Length)];
        string baseText = $"Сегодня вы чувствуете себя {mood}. ";

        string addition = type switch
        {
            "Любовь" => love[rnd.Next(love.Length)],
            "Финансы" => money[rnd.Next(money.Length)],
            _ => advice[rnd.Next(advice.Length)]
        };

        return baseText + addition;
    }

}
