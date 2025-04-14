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
            Title = "Horoskoop";

            zodiacSigns = new List<ZodiacSign>
            {
                new ZodiacSign("Jäär", "21. märts — 19. aprill", "♈", "Tuli", "Marss", "Julgus, energia, ambitsioonikus"),
                new ZodiacSign("Sõnn", "20. aprill — 20. mai", "♉", "Maa", "Veenus", "Usaldusväärsus, kannatlikkus, praktilisus"),
                new ZodiacSign("Kaksikud", "21. mai — 20. juuni", "♊", "Õhk", "Merkuur", "Intelligentsus, paindlikkus, seltskondlikkus"),
                new ZodiacSign("Vähk", "21. juuni — 22. juuli", "♋", "Vesi", "Kuu", "Tundlikkus, hoolivus, intuitsioon"),
                new ZodiacSign("Lõvi", "23. juuli — 22. august", "♌", "Tuli", "Päike", "Enesekindlus, heldus, juhtimine"),
                new ZodiacSign("Neitsi", "23. august — 22. september", "♍", "Maa", "Merkuur", "Analüüsivõime, täpsus, realism"),
                new ZodiacSign("Kaalud", "23. september — 22. oktoober", "♎", "Õhk", "Veenus", "Diplomaatia, tasakaal, stiil"),
                new ZodiacSign("Skorpion", "23. oktoober — 21. november", "♏", "Vesi", "Pluuto", "Kirglikkus, sügavus, otsusekindlus"),
                new ZodiacSign("Ambur", "22. november — 21. detsember", "♐", "Tuli", "Jupiter", "Optimism, vabadus, filosoofia"),
                new ZodiacSign("Kaljukits", "22. detsember — 19. jaanuar", "♑", "Maa", "Saturn", "Distsipliin, ambitsioonikus, stabiilsus"),
                new ZodiacSign("Veevalaja", "20. jaanuar — 18. veebruar", "♒", "Õhk", "Uraan", "Isikupära, leidlikkus"),
                new ZodiacSign("Kalad", "19. veebruar — 20. märts", "♓", "Vesi", "Neptuun", "Kaastunne, unistavus, loovus")
            };

            var dailyQuotes = new[]
            {
                "„Tähed mõjutavad, kuid ei sunni.” – Publilius Syrus",
                "„Niikaua kui sa usud imesse – see on võimalik.”",
                "„Tunne iseennast – ja sa tunnetad Universumit.”",
                "„Saatus on kirjutatud tähtedesse, aga valik on sinu.”",
                "„Iga hommik toob uue kaardi taevas.”"
            };

            var dailyFacts = new[]
            {
                "Kaalude märk on ainus elutu sümbol sodiaagis.",
                "Kaksikuid juhib Merkuur – suhtlemise planeet.",
                "Kalad on viimane märk, mis ühendab kõikide teiste omadusi.",
                "Igal tähemärgil on oma element: Tuli, Maa, Õhk, Vesi.",
                "Kaljukits on astroloogias kannatlikkuse ja distsipliini sümbol."
            };

            var rnd = new Random();
            string randomQuote = dailyQuotes[rnd.Next(dailyQuotes.Length)];
            string randomFact = dailyFacts[rnd.Next(dailyFacts.Length)];

            var searchBar = new SearchBar
            {
                Placeholder = "Otsi tähemärki...",
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
                Text = "🎲 Juhuslik märk",
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
                Title = "Menüü",
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

            Flyout = menuPage;
            Detail = new NavigationPage(new ContentPage
            {
                Title = "Avaleht",
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
                                Text = "🌌 Tere tulemast astroloogiamaailma!",
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
                                Text = $"✨ Päeva tsitaat:\n{randomQuote}",
                                FontSize = 16,
                                TextColor = Colors.Indigo
                            },
                            new Label
                            {
                                Text = $"🪐 Astroloogiline fakt:\n{randomFact}",
                                FontSize = 14,
                                TextColor = Colors.DarkSlateGray
                            },
                            new Button
                            {
                                Text = "🔮 Näita juhuslikku horoskoopi",
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
