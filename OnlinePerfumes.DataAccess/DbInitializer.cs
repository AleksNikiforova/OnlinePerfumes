using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlinePerfumes.DataAccess;
using OnlinePerfumes.Models;
using static System.Net.WebRequestMethods;

namespace OnlinePerfumes
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if(!context.Categories.Any())
            {
                var categories = new Category[]
                {
                    new Category
                    {
                        Name="Унисекс парфюми"
                    },
                    new Category
                    {
                        Name="Нишови парфюми"
                    },
                    new Category
                    {
                        Name="Арабски парфюми"
                    },
                    new Category
                    {
                        Name="Женски парфюми"
                    },
                    new Category
                    {
                        Name="Мъжки парфюми"
                    },
                    new Category
                    {
                        Name="Луксозни парфюми"
                    }
                };
                foreach(var category in categories)
                {
                    await context.Categories.AddAsync(category);
                }
                await context.SaveChangesAsync();
            }
            if(!context.Products.Any())
            {
                var products = new Product[]
                {
                    new Product
                    {
                        Name="Christian Dior\r\nSauvage Elixir",
                        Aroma="Дървесни",
                        Description="Мъжкото ухание Sauvage Elixir е лансирано от популярния бранд Christian Dior през 2021 година. " +
                        "Притежава богатство и забележителна дълготрайност. " +
                        "Мъжественият и силен аромат се характеризира с добра проекция и плътност. " +
                        "Christian Dior Sauvage Elixir е артистично и висококачествено ухание, с което ще получите много комплименти за отличния си вкус. " +
                        "Ароматът е предимно вечерен и е най-подходящ за хладните и студени месеци на годината. ",
                        Price=389.99m,
                        StockQuantity=300,
                        CategoryId = context.Categories.First(c => c.Name == "Мъжки парфюми").Id,
                        ImagePath="https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/c/h/0703f900d8bac7d3cdfd9ce6a07912fb/christian-dior-sauvage-parfyumen-ekstrakt--60-ml-30.jpg"

                    },
                    new Product
                    {
                        Name="\r\n\r\nGucci\r\nGuilty Elixir Pour Femme ",
                        Aroma="Цитрусови",
                        Description="Gucci Guilty Elixir de Parfum pour Femme е луксозен ориенталско-цветен аромат за жени, лансиран през 2023 година. " +
                        "Създаден от парфюмериста Натали Грация-Сето, този парфюм представлява смела декларация за любов и самоприемане, " +
                        "насърчавайки жените да изразяват своята автентичност. " +
                        "Ароматът съчетава свежи цитрусови нотки с богати флорални акорди и топли базови нюанси, създавайки чувствено и провокативно ухание.",
                        Price=194.99m,
                        StockQuantity=200,
                        CategoryId=context.Categories.First(c=>c.Name=="Женски парфюми").Id,
                        ImagePath="https://www.heinemann-shop.com/medias/527Wx527H-null?context=bWFzdGVyfGltYWdlc3w0Njk0OXxpbWFnZS9qcGVnfGFHRTBMMmczTnk4eE1USTROVEE0TnpJeU16Z3pPQzgxTWpkWGVEVXlOMGhmYm5Wc2JBfDhkZTNjN2I3NGJjMTUxMzJlMzFlMzc0NTcxZDNiMzcxNDUyMDVjMzE4NDA5YTU4NjQ0ZGMxNGVhODI2MDdiMTE&v=1729305233675"

                    },
                    new Product
                    {
                        Name="Lattafa\r\nKhamrah Qahwa",
                        Aroma="Пикантни Ориенталски , Ориенталски Амброви ",
                        Description="Lattafa Khamrah Qahwa е изключително уникален унисекс парфюм, съчетаващ изтънчена елегантност и топлина." +
                        " Създаден за тези, които обичат уникални аромати, които събуждат сетивата и носят усещане за уют и комфорт." +
                        " Като част от колекцията на марката Lattafa, известна със своите луксозни и висококачествени парфюми," +
                        " Khamrah Qahwa предизвиква силно впечатление с интересната си ароматна композиция, в която преобладават кафе и сладки нотки.",
                        Price=74.99m,
                        StockQuantity=50,
                        CategoryId=context.Categories.First(c=>c.Name=="Унисекс парфюми").Id,
                        ImagePath="https://parfium.bg/27249-medium_default/lattafa-khamrah-qahwa-uniseks-parfyumna-voda-edp.jpg"
                    },
                    new Product
                    {
                        Name="Afnan\r\n9Pm",
                        Aroma="Амброви ,Дървесни",
                        Description="Afnan 9PM е съвременен унисекс парфюм, който олицетворява елегантността, " +
                        "страстта и динамиката на съвременния живот. " +
                        "Създаден от известната парфюмна марка Afnan, този аромат комбинира перфектно свежи, пикантни и топли нотки, " +
                        "които го правят идеален избор за вечерни събития, романтични излизания или специални поводи.",
                        Price=74.99m,
                        StockQuantity=130,
                        CategoryId=context.Categories.First(c=>c.Name=="Арабски парфюми").Id,
                        ImagePath="https://parfium.bg/12202-medium_default/afnan-9pm-uniseks-parfyumna-voda-edp.jpg"
                    },
                    new Product
                    {
                        Name="Xerjoff\r\nErba Pura",
                        Aroma="Цветни ,Цитрусови,Дървесни",
                        Description="Erba Pura Eau De Parfum е лансиран от нишовия бранд Xerjoff през 2019 година." +
                        " Впечатляващият унисекс аромат бързо печели сърцата на мъже и жени по целия свят със своите богати и дълготрайни плодови и кехлибарени нотки." +
                        " Днес Xerjoff Erba Pura е един от най-разпознаваемите парфюми. Усетете радостта и жизнената лъчезарност на живота по Средиземноморието, като се отдадете на магията на това забележително ухание! " +
                        "Xerjoff Erba Pura е един от парфюмите, който сякаш е създаден специално, за да напомня във всеки миг за най-приятните изживявани моменти. ",
                        Price=339.99m,
                        StockQuantity=70,
                        CategoryId=context.Categories.First(c=>c.Name=="Нишови парфюми").Id,
                        ImagePath="https://parfium.bg/25160-medium_default/xerjoff-erba-pura-uniseks-parfyum-edp.jpg"
                    },
                    new Product
                    {
                        Name="Jean Paul Gaultier\r\nLe Male Elixir",
                        Aroma="Дървесни ,Ароматни",
                        Description="Le Male Elixir е един от новите мъжки парфюми в колекцията на Jean Paul Gaultier. " +
                        "Уханието излиза на пазара през 2023 година и бързо намира своите почитатели сред ценителите на топли и многопластови аромати със завладяваща чувственост и сексапил." +
                        " Този парфюм за мъже е най-подходящ за есенно-зимния период, предимно вечерен заради наситения си, " +
                        "интензивен и съблазнителен характер. Силната проекция превръща Jean Paul Gaultier Le Male Elixir в идеален избор за мъже, които държат да правят впечатление. ",
                        Price=214.99m,
                        StockQuantity=90,
                        CategoryId=context.Categories.First(c=>c.Name=="Луксозни парфюми").Id,
                        ImagePath="https://parfium.bg/25649-medium_default/jean-paul-gaultier-le-male-elixir-parfyum-za-maje-edp.jpg"
                    }
                    
                };
                foreach (var product in products)
                {
                    await context.Products.AddAsync(product);
                }
                await context.SaveChangesAsync();
            }
        }
    }

}
