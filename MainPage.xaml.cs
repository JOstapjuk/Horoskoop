using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Horoskoop
{
    public class MainPage : FlyoutPage
    {
        List<ZodiacSign> zodiacSigns;
        ListView listView;

        public MainPage()
        {
            Title = "Гороскоп";

            zodiacSigns = new List<ZodiacSign>
            {
                new ZodiacSign("Овен", "21 марта — 19 апреля", "♈", "Огонь", "Марс", "Смелость, энергия, амбиции"),
                new ZodiacSign("Телец", "20 апреля — 20 мая", "♉", "Земля", "Венера", "Надёжность, терпение, практичность"),
                new ZodiacSign("Близнецы", "21 мая — 20 июня", "♊", "Воздух", "Меркурий", "Интеллект, гибкость, общительность"),
                new ZodiacSign("Рак", "21 июня — 22 июля", "♋", "Вода", "Луна", "Чувствительность, забота, интуиция"),
                new ZodiacSign("Лев", "23 июля — 22 августа", "♌", "Огонь", "Солнце", "Уверенность, щедрость, лидерство"),
                new ZodiacSign("Дева", "23 августа — 22 сентября", "♍", "Земля", "Меркурий", "Аналитичность, аккуратность, реализм"),
                new ZodiacSign("Весы", "23 сентября — 22 октября", "♎", "Воздух", "Венера", "Дипломатия, баланс, стиль"),
                new ZodiacSign("Скорпион", "23 октября — 21 ноября", "♏", "Вода", "Плутон", "Страсть, глубина, решительность"),
                new ZodiacSign("Стрелец", "22 ноября — 21 декабря", "♐", "Огонь", "Юпитер", "Оптимизм, свобода, философия"),
                new ZodiacSign("Козерог", "22 декабря — 19 января", "♑", "Земля", "Сатурн", "Дисциплина, амбиции, стабильность"),
                new ZodiacSign("Водолей", "20 января — 18 февраля", "♒", "Воздух", "Уран", "Индивидуальность, изобретательность"),
                new ZodiacSign("Рыбы", "19 февраля — 20 марта", "♓", "Вода", "Нептун", "Эмпатия, мечтательность, креативность")
            };

            // Random data for quote and fact
            var dailyQuotes = new[]
            {
                "«Звёзды склоняют, но не обязывают.» – Публий Сир",
                "«Пока ты веришь в чудо — оно возможно.»",
                "«Познавай себя — и ты познаешь Вселенную.»",
                "«Судьба написана в звёздах, но выбор — за тобой.»",
                "«Каждое утро — новая карта на небесах.»"
            };

            var dailyFacts = new[]
            {
                "Знак Весов — единственный неодушевлённый символ в зодиаке.",
                "Близнецы управляются Меркурием — планетой общения.",
                "Рыбы — последний знак, объединяющий черты всех остальных.",
                "У каждого знака есть своя стихия: Огонь, Земля, Воздух, Вода.",
                "Козерог — символ терпения и дисциплины в астрологии."
            };

            var rnd = new Random();
            string randomQuote = dailyQuotes[rnd.Next(dailyQuotes.Length)];
            string randomFact = dailyFacts[rnd.Next(dailyFacts.Length)];

            // SEARCH
            var searchBar = new SearchBar
            {
                Placeholder = "Поиск знака...",
                Margin = new Thickness(10)
            };

            listView = new ListView { ItemsSource = zodiacSigns, Margin = 10 };
            listView.ItemTemplate = new DataTemplate(() =>
            {
                var cell = new TextCell();
                cell.SetBinding(TextCell.TextProperty, "Name");
                return cell;
            });

            listView.ItemSelected += async (s, e) =>
            {
                if (e.SelectedItem is ZodiacSign sign)
                {
                    await listView.FadeTo(0.5, 100);
                    Detail = new NavigationPage(new ZodiacPage(sign));
                    IsPresented = false;
                    await listView.FadeTo(1, 100);
                }
            };

            searchBar.TextChanged += (s, e) =>
            {
                listView.ItemsSource = zodiacSigns
                    .Where(z => z.Name.ToLower().Contains(e.NewTextValue.ToLower()));
            };

            var randomButton = new Button
            {
                Text = "🎲 Случайный знак",
                BackgroundColor = Colors.MediumPurple,
                TextColor = Colors.White,
                Margin = new Thickness(10)
            };

            randomButton.Clicked += (s, e) =>
            {
                var sign = zodiacSigns[rnd.Next(zodiacSigns.Count)];
                Detail = new NavigationPage(new ZodiacPage(sign));
                IsPresented = false;
            };

            var menuPage = new ContentPage
            {
                Title = "Содержание",
                Content = new StackLayout
                {
                    Children =
                    {
                        new Image { Source = "zodiac.jpg", HeightRequest = 200 },
                        searchBar,
                        randomButton,
                        listView
                    }
                }
            };

            // MAIN DETAIL PAGE
            Flyout = menuPage;
            Detail = new NavigationPage(new ContentPage
            {
                Title = "Главная",
                Content = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Padding = 20,
                        Spacing = 15,
                        Children =
                        {
                            new Label
                            {
                                Text = "🌌 Добро пожаловать в астромир!",
                                FontSize = 24,
                                FontAttributes = FontAttributes.Bold,
                                HorizontalOptions = LayoutOptions.Center
                            },
                            new Image
                            {
                                Source = "stars_bg.jpg",
                                HeightRequest = 200,
                                Aspect = Aspect.AspectFill
                            },
                            new Label
                            {
                                Text = $"✨ Цитата дня:\n{randomQuote}",
                                FontSize = 16,
                                TextColor = Colors.Indigo
                            },
                            new Label
                            {
                                Text = $"🪐 Астрологический факт:\n{randomFact}",
                                FontSize = 14,
                                TextColor = Colors.DarkSlateGray
                            },
                            new Button
                            {
                                Text = "🔮 Показать случайный гороскоп",
                                BackgroundColor = Colors.MediumPurple,
                                TextColor = Colors.White,
                                CornerRadius = 20,
                                HeightRequest = 50,
                                Command = new Command(() =>
                                {
                                    var randomSign = zodiacSigns[rnd.Next(zodiacSigns.Count)];
                                    Detail = new NavigationPage(new ZodiacPage(randomSign));
                                    IsPresented = false;
                                })
                            }
                        }
                    }
                }
            });
        }
    }
}
